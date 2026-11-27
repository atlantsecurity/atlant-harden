using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace AtlantHarden.Services
{
    /// <summary>Checks that a file carries a valid Authenticode signature from Atlant Security.</summary>
    public static class AuthenticodeVerifier
    {
        /// <summary>Substring expected in the signer certificate's subject (O= / CN=).</summary>
        public const string ExpectedPublisher = "Atlant Security";

        public static bool IsSignedByAtlant(string path, out string detail)
        {
            if (!VerifyTrust(path, out var code))
            {
                detail = $"no valid Authenticode signature (WinVerifyTrust 0x{code:X8})";
                return false;
            }
            try
            {
#pragma warning disable SYSLIB0057
                using var cert = new X509Certificate2(X509Certificate.CreateFromSignedFile(path));
#pragma warning restore SYSLIB0057
                if (cert.Subject.Contains(ExpectedPublisher, StringComparison.OrdinalIgnoreCase))
                {
                    detail = cert.Subject;
                    return true;
                }
                detail = $"signed by someone else: {cert.Subject}";
                return false;
            }
            catch (Exception ex)
            {
                detail = ex.Message;
                return false;
            }
        }

        // ---- WinVerifyTrust ----------------------------------------------------------------
        private static readonly Guid GenericVerifyV2 = new("00AAC56B-CD44-11d0-8CC2-00C04FC295EE");

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private sealed class WinTrustFileInfo
        {
            public uint cbStruct = (uint)Marshal.SizeOf<WinTrustFileInfo>();
            public string pcwszFilePath = "";
            public IntPtr hFile = IntPtr.Zero;
            public IntPtr pgKnownSubject = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private sealed class WinTrustData
        {
            public uint cbStruct = (uint)Marshal.SizeOf<WinTrustData>();
            public IntPtr pPolicyCallbackData = IntPtr.Zero;
            public IntPtr pSIPClientData = IntPtr.Zero;
            public uint dwUIChoice = 2;            // WTD_UI_NONE
            public uint fdwRevocationChecks = 0;   // WTD_REVOKE_NONE (offline-safe; timestamped)
            public uint dwUnionChoice = 1;         // WTD_CHOICE_FILE
            public IntPtr pFile;
            public uint dwStateAction = 0;
            public IntPtr hWVTStateData = IntPtr.Zero;
            public IntPtr pwszURLReference = IntPtr.Zero;
            public uint dwProvFlags = 0x00000080;  // WTD_CACHE_ONLY_URL_RETRIEVAL
            public uint dwUIContext = 0;
            public IntPtr pSignatureSettings = IntPtr.Zero;
        }

        [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = false, CharSet = CharSet.Unicode)]
        private static extern int WinVerifyTrust(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionID, WinTrustData pWVTData);

        private static bool VerifyTrust(string path, out int code)
        {
            var file = new WinTrustFileInfo { pcwszFilePath = path };
            var filePtr = Marshal.AllocHGlobal(Marshal.SizeOf<WinTrustFileInfo>());
            try
            {
                Marshal.StructureToPtr(file, filePtr, false);
                var data = new WinTrustData { pFile = filePtr };
                code = WinVerifyTrust(IntPtr.Zero, GenericVerifyV2, data);
                return code == 0;
            }
            finally
            {
                Marshal.DestroyStructure<WinTrustFileInfo>(filePtr);
                Marshal.FreeHGlobal(filePtr);
            }
        }
    }
}
