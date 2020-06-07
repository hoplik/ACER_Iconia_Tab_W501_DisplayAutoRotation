using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DisplayAutoRotation
{
    class G_Sensor : IDisposable
    {
        public IntPtr hDriver; // Хендл драйвера
        /// <summary>Инициализация драйвера G-Sensor</summary>
        /// <param name="FilePath">Путь к драйверу</param>
        /// <returns>true - при успешном открытии файла на чтение и/или чтение/запись</returns>
        public unsafe bool Handle_Driver(string FilePath)
        {
            hDriver = UnsafeNativeMethods.CreateFile(
                FilePath,
                0xC0000000u,   // GenericRead | GenericWrite,
                1u, // Read,
                IntPtr.Zero,
                3u, //OpenExisting,
                0x80u,  // Normal,
                IntPtr.Zero);
            int err = Marshal.GetLastWin32Error();
            if (hDriver == (IntPtr)(-1))
            {
                if (err != 0x20)
                {
                    MessageBox.Show("Ошибка доступа к драйверу - " + err.ToString());
                    UnsafeNativeMethods.CloseHandle(hDriver);
                    return false;
                }
                else
                {
                    // В случае ERROR_SHARING_VIOLATION предполагаем, что устройство открыл и 
                    // проинициализаровал Device Control
                    hDriver = UnsafeNativeMethods.CreateFile(
                        FilePath,
                        0x80000000u, // GenericRead
                        1u,
                        IntPtr.Zero,
                        3u,
                        0x80u,
                        IntPtr.Zero);
                    MessageBox.Show("BMA150 открыт в режиме чтения");
                }
                return true;
            }
            else
            {
                uint bytesReturned = 0;
                byte inBuffer = 0;
                if (!UnsafeNativeMethods.DeviceIoControl(
                   hDriver,
                   0x222004u,
                   &inBuffer,
                   1,
                   null,
                   0,
                   &bytesReturned,
                   IntPtr.Zero))
                {
                    MessageBox.Show("Ошибка 1-го IOCTRL - " + Marshal.GetLastWin32Error().ToString());
                    UnsafeNativeMethods.CloseHandle(hDriver);
                    return false;
                }
                //MessageBox.Show("Успешно открыли первый IOCTRL");

                inBuffer = 1;
                if (!UnsafeNativeMethods.DeviceIoControl(
                       hDriver,
                       0x22200Cu,
                       &inBuffer,
                       1,
                       null,
                       0,
                       &bytesReturned,
                       IntPtr.Zero))
                {
                    MessageBox.Show("Ошибка 2-го IOCTRL - " + Marshal.GetLastWin32Error().ToString());
                    UnsafeNativeMethods.CloseHandle(hDriver);
                    return false;
                }
                //MessageBox.Show("Успешно открыли второй IOCTRL");
                // Успешно открыли все IOCTRL, теперь читаем по таймеру то, что отдаёт драйвер
                return true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        internal virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты).
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже метод завершения.
                // TODO: задать большим полям значение NULL.
                hDriver = IntPtr.Zero;

                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        ~G_Sensor()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        void IDisposable.Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    #region Функции импорта для поворота экрана

    [ComVisible(true)]
    internal class UnsafeNativeMethods
    {
        private UnsafeNativeMethods() { }
        /// <summary>Открываем доступ к файлу драйвера
        /// </summary>
        /// <param name="FileName">file name</param>
        /// <param name="DesiredAccess">access mode</param>
        /// <param name="ShareMode">share mode</param>
        /// <param name="SecurityAttributes">Security Attributes</param>
        /// <param name="CreationDisposition">how to create</param>
        /// <param name="FlagsAndAttributes">file attributes</param>
        /// <param name="hTemplateFile">handle to template file</param>
        /// <returns></returns>
        /// <remarks>http://www.pinvoke.net/default.aspx/kernel32.CreateFile</remarks>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateFile(
            [param: MarshalAs(UnmanagedType.LPWStr)]
            string FileName,
            uint DesiredAccess,
            uint ShareMode,
            IntPtr SecurityAttributes,
            uint CreationDisposition,
            uint FlagsAndAttributes,
            IntPtr hTemplateFile);
        /// <summary>Контроль ввода/вывода
        /// </summary>
        /// <param name="hDevice">Хендл драйвера</param>
        /// <param name="IoControlCode">IOCTRL - код контроля ввода/вывода</param>
        /// <param name="InBuffer">Массив буфера ввода</param>
        /// <param name="nInBufferSize">Размер массива для ввода</param>
        /// <param name="OutBuffer">Массив буфера вывода</param>
        /// <param name="nOutBufferSize">Размер массива для вывода (.Length)</param>
        /// <param name="pBytesReturned">Количество байт для чтения</param>
        /// <param name="Overlapped"></param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool DeviceIoControl(
         [In]IntPtr hDevice,
         [In]uint IoControlCode,
         [In]byte* InBuffer,
         [In]uint nInBufferSize,
         [Out]short[] OutBuffer,
         [Out]uint nOutBufferSize,
         [Out]uint* pBytesReturned,
         [In]IntPtr Overlapped);
        /// <summary>Читаем файл драйвера
        /// </summary>
        /// <param name="hFile">handle to file</param>
        /// <param name="pBuffer">data buffer</param>
        /// <param name="NumberOfBytesToRead">number of bytes to read</param>
        /// <param name="pNumberOfBytesRead">number of bytes read</param>
        /// <param name="overlapped">overlapped buffer</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        public unsafe static extern bool ReadFile(
            [In]IntPtr hFile,
            [Out]short[] pBuffer,
            [In]uint NumberOfBytesToRead,
            [Out]uint* pNumberOfBytesRead,
            [In]NativeOverlapped* overlapped);

        /// <summary>Закрываем файл драйвера
        /// </summary>
        /// <param name="hObject">handle to object</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool CloseHandle(
            IntPtr hObject);
        #endregion
    }
}
