using System;
using System.Runtime.InteropServices;

namespace DisplayAutoRotation
{
    class Disp_Settings
    {
        /// <summary>Константы</summary>
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int EDS_ROTATEDMODE = 0x00000004;
        public const int DISP_CHANGE_RESTART = 1;
        public const int DISP_CHANGE_SUCCESSFUL = 0;
        public const int DISP_CHANGE_FAILED = -1;
        public const int DISP_CHANGE_BADMODE = -2;
        public const int DISP_CHANGE_NOTUPDATED = -3;
        public const int DISP_CHANGE_BADFLAGS = -4;
        public const int DISP_CHANGE_BADPARAM = -5;
        public const int DISP_CHANGE_BADDUALVIEW = -6;

        public enum DM_ : uint
        {
            SPECVERSION = 0x0401,
            ORIENTATION = 0x00000001,
            PAPERSIZE = 0x00000002,
            PAPERLENGTH = 0x00000004,
            PAPERWIDTH = 0x00000008,
            SCALE = 0x00000010,
            POSITION = 0x00000020,
            NUP = 0x00000040,
            DISPLAYORIENTATION = 0x00000080,
            COPIES = 0x00000100,
            DEFAULTSOURCE = 0x00000200,
            PRINTQUALITY = 0x00000400,
            COLOR = 0x00000800,
            DUPLEX = 0x00001000,
            YRESOLUTION = 0x00002000,
            TTOPTION = 0x00004000,
            COLLATE = 0x00008000,
            FORMNAME = 0x00010000,
            LOGPIXELS = 0x00020000,
            BITSPERPEL = 0x00040000,
            PELSWIDTH = 0x00080000,
            PELSHEIGHT = 0x00100000,
            DISPLAYFLAGS = 0x00200000,
            DISPLAYFREQUENCY = 0x00400000,
            ICMMETHOD = 0x00800000,
            ICMINTENT = 0x01000000,
            MEDIATYPE = 0x02000000,
            DITHERTYPE = 0x04000000,
            PANNINGWIDTH = 0x08000000,
            PANNINGHEIGHT = 0x10000000,
            DISPLAYFIXEDOUTPUT = 0x20000000
        }
        public enum CDS_ : uint
        {
            DYNAMIC = 0,
            UPDATEREGISTRY = 1,
            TEST = 2,
            FULLSCREEN = 4,
            GLOBAL = 8,
            SET_PRIMARY = 10,
            NORESET = 0x10000000,
            RESET = 0x40000000
        }
    }

    class SafeNativeMethods
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DEVMODEW
        {
            // You can define the following constant
            // but OUTSIDE the structure because you know
            // that size and layout of the structure
            // is very important
            // CCHDEVICENAME = 32 = 0x50
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            // In addition you can define the last character array
            // as following:
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            //public Char[] dmDeviceName;

            // After the 32-bytes array
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmSpecVersion;

            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmDriverVersion;

            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmSize;

            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmDriverExtra;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmFields;

            public POINTL dmPosition;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayOrientation;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFixedOutput;

            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmColor;

            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmDuplex;

            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmYResolution;

            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmTTOption;

            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmCollate;

            // CCHDEVICENAME = 32 = 0x50
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            // Also can be defined as
            //[MarshalAs(UnmanagedType.ByValArray,
            //    SizeConst = 32, ArraySubType = UnmanagedType.U1)]
            //public Byte[] dmFormName;

            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmLogPixels;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmBitsPerPel;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPelsWidth;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPelsHeight;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFlags;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFrequency;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmICMMethod;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmICMIntent;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmMediaType;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDitherType;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmReserved1;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmReserved2;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPanningWidth;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPanningHeight;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINTL
        {
            [MarshalAs(UnmanagedType.I4)]
            public int x;
            [MarshalAs(UnmanagedType.I4)]
            public int y;
        }
        /// <summary>PInvoke declaration for EnumDisplaySettings Win32 API</summary>
        /// <param name="lpszDeviceName">null - для текущего потока</param>
        /// <param name="iModeNum">-1 - для параметров устройства, -2 - для данных из реестра</param>
        /// <param name="lpDevMode">текущий DEVMODE</param>
        /// <param name="dwFlags">EDS_RAWMODE - If set, the function will return all graphics modes reported 
        /// by the adapter driver, regardless of monitor capabilities. Otherwise, it will only return modes 
        /// that are compatible with current monitors.
        /// EDS_ROTATEDMODE - If set, the function will return graphics modes in all orientations. Otherwise, 
        /// it will only return modes that have the same orientation as the one currently set for the 
        /// requested display.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return
        /// value is zero.</returns>
        [DllImport("user32", CharSet = CharSet.Unicode)]
        public static extern bool EnumDisplaySettingsExW(
            [param: MarshalAs(UnmanagedType.LPWStr)]
            [In]string lpszDeviceName,
            [param: MarshalAs(UnmanagedType.U4)]
            [In]int iModeNum,
            [In, Out]ref DEVMODEW lpDevMode,
            [In] int dwFlags);

        /// <summary>PInvoke declaration for ChangeDisplaySettings Win32 API</summary>
        /// <param name="lpDevMode">Текущий DEVMODE</param>
        /// <param name="dwFlags">0 - для динамического изменения</param>
        /// <returns>Возвращает 0 - если удачно, или число при ошибке</returns>
        [DllImport("user32", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int ChangeDisplaySettingsExW(
            [param: MarshalAs(UnmanagedType.LPWStr)]
            [In]string lpszDeviceName,
            [In, Out]ref DEVMODEW lpDevMode,
            IntPtr hwnd,
            [param: MarshalAs(UnmanagedType.U4)]
            [In]uint dwFlags,
            [In]IntPtr lParam);
    }
}
