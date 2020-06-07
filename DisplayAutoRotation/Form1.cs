using DisplayAutoRotation.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DisplayAutoRotation
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Перечислитель для ориентации дисплея
        /// </summary>
        internal enum Orientation : uint
        {
            LANDSCAPE = 0,
            L_PORTRAIT = 1,
            UNLANDSCAPE = 2,
            R_PORTRAIT = 3
        }
        /// <summary>
        /// Подключили класс управления сенсором
        /// </summary>
        G_Sensor gs = new G_Sensor();
        /// <summary>
        /// Массив данных датчика положения дисплея
        /// </summary>
        List<Sensor_Orientation> sens_orients = new List<Sensor_Orientation> { new Sensor_Orientation() { X = 0, Y = 0, Z = 0 } };
        /// <summary>
        /// Переменная учёта корректировки координат для нулевого положения дисплея
        /// </summary>
        Average_Coordinates corr_orient = new Average_Coordinates() { X = 0.00, Y = 0.00, Z = 0.00 };
        /// <summary>
        /// Объект блокировки потока выполнения по таймеру
        /// </summary>
        static object locker = new object();
        /// <summary>
        /// Соответствие номера порта его полному имени
        /// </summary>
        Dictionary<string, string> fullportname = new Dictionary<string, string>();
        /// <summary>
        /// Массив доступных в системе портов
        /// </summary>
        string[] ports = SerialPort.GetPortNames();
        /// <summary>
        /// Статус порта GPS для чтения
        /// </summary>
        bool gpsportopen = false;
        /// <summary>
        /// Динамичное изменение или запись в реестр положения экрана (для сохранения после перезагрузки)
        /// </summary>
        uint reestrwrite = (uint)Disp_Settings.CDS_.DYNAMIC;
        /// <summary>
        /// Текущая ориентация экрана
        /// </summary>
        uint Current_orientation = (uint)Orientation.LANDSCAPE;
        #region Функции контролов формы
        /// <summary>
        /// Основная форма загрузки и исполнения программы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Функции, выполняемые перед загрузкой формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide(); // Запускаем в свёрнутом в трей режиме
            timer1.Enabled = true;
            timer1.Interval = Sens_trackBar.Value;
            Sens_count_label.Text = ((float)Sens_trackBar.Value / 1000).ToString() + " \u2033";
            Vert_vector_label.Text = Vert_trackBar.Value.ToString() + " \u00B0";
            Hor_vector_label.Text = Hor_trackBar.Value.ToString() + " \u00B0";
            Min_Hor_label.Text = Hor_trackBar.Minimum.ToString() + " \u00B0";
            Max_Hor_label.Text = Hor_trackBar.Maximum.ToString() + " \u00B0";
            Min_Vert_label.Text = Vert_trackBar.Minimum.ToString() + " \u00B0";
            Max_Vert_label.Text = Vert_trackBar.Maximum.ToString() + " \u00B0";
            Min_Sens_label.Text = ((float)Sens_trackBar.Minimum / 1000).ToString() + " \u2033";
            Max_Sens_label.Text = ((float)Sens_trackBar.Maximum / 1000).ToString() + " \u2033";
            corr_orient.X = int.Parse(X_Corr_button.Text, NumberStyles.Integer);
            corr_orient.Y = int.Parse(Y_Corr_button.Text, NumberStyles.Integer);
            corr_orient.Z = int.Parse(Z_Corr_button.Text, NumberStyles.Integer);
            // Прописываем доступные порты в комбобоксы для GPS
            ListingUSBDic();    //Список всех когда-либо подключённых устройств из реестра
            ControlInterface(Resources.Cont_Interface_Modem);  //Выбираем управляющий порт по уникальному имени
            GPSInterface(Resources.GPS_Interface_Modem);    //Выбираем GPS по уникальному имени
            if (gs.Handle_Driver("\\\\.\\BMA150")) timer1.Start(); // При успешной инициализации драйвера запускаем таймер
        }
        /// <summary>
        /// При минимизировании сворачиваем в трей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                Size_toolStripMenuItem.Text = Resources.Size_Normal;
            }
            else
            {
                Size_toolStripMenuItem.Text = Resources.Size_Min;
            }
        }
        /// <summary>
        /// При закрытии приложения сохраняем настройки пользователя и закрываем порт GPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gpsportopen = false;
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// Определяем сохранять ли в реестре запись о положении экрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reestr_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Reestr_checkBox.Checked) reestrwrite = (uint)Disp_Settings.CDS_.UPDATEREGISTRY;
            else reestrwrite = (uint)Disp_Settings.CDS_.DYNAMIC;
        }
        /// <summary>
        /// Очищаем контролы при скидывании флага анимации 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Curr_Anim_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!Curr_Anim_checkBox.Checked)
            {
                X_progressBar.Value = X_progressBar.Minimum;
                Y_progressBar.Value = Y_progressBar.Minimum;
                Z_progressBar.Value = Z_progressBar.Minimum;
                Cont_Hor_pictureBox.BackgroundImage = null;
                Cont_Vert_pictureBox.BackgroundImage = null;
                Control_Vert_label.Text = string.Empty;
                Control_Hor_label.Text = string.Empty;
                label_X.Text = "X";
                label_Y.Text = "Y";
                label_Z.Text = "Z";
            }
        }
        /// <summary>
        /// Двойной клик по уведомлению (разворачиваем, сворачиваем)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }
        /// <summary>
        /// Закрываем приложение из меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Меню включения/отключения датчика GPS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPS_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GPSOnRadioButton.Checked)
            {
                GPSOnRadioButton.Checked = false;
                GPSOffRadioButton.Checked = true;
                GPS_toolStripMenuItem.Text = Resources.GPS_Start_String;
            }
            else
            {
                GPSOnRadioButton.Checked = true;
                GPSOffRadioButton.Checked = false;
                GPS_toolStripMenuItem.Text = Resources.GPS_Stop_String;
            }
        }
        /// <summary>
        /// Меню "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form newform = new AboutBox1();
            newform.Show();
        }
        /// <summary>
        /// Изменение управляющего порта модема 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlPortСomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> cpcb = (KeyValuePair<string, string>)ControlPortСomboBox.SelectedItem;
            if (cpcb.Value != ControlInterSerialPort.PortName)
            {
                if (ControlInterSerialPort.IsOpen) ControlInterSerialPort.Close();
                ControlInterSerialPort.PortName = cpcb.Value;
                GPSOffRadioButton.Checked = true;
            }
        }
        /// <summary>
        /// Изменение порта датчика GPS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<string, string> gpcb = (KeyValuePair<string, string>)GPSPortComboBox.SelectedItem;
            if (gpcb.Value != GPSInterSerialPort.PortName)
            {
                if (GPSInterSerialPort.IsOpen)
                {
                    GPSInterSerialPort.Close();
                    gpsportopen = false;
                }
                GPSTextBox.Text = string.Empty;
                GPSInterSerialPort.PortName = gpcb.Value;
                GPSPortButton.Text = Resources.GPS_Read_Stirng;
            }
        }
        /// <summary>
        /// Включаем анализ данных датчика GPS с записью в текстовое поле 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSPortButton_Click(object sender, EventArgs e)
        {
            if (GPSInterSerialPort.IsOpen)
            {
                GPSInterSerialPort.Close();
                gpsportopen = false;
                GPSPortButton.Text = Resources.GPS_Read_Stirng;
                GPSTextBox.Text = string.Empty;
            }
            else
            {
                GPSInterSerialPort.Open();
                GPSPortButton.Text = Resources.GPS_Clear_String;
                gpsportopen = true;
            }
        }
        /// <summary>
        /// Установка скорректированных значений ориентации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Corr_button_Click(object sender, EventArgs e)
        {
            switch (Current_orientation)
            {
                case (uint)Orientation.LANDSCAPE: //0
                    corr_orient.X = -1 * sens_orients[sens_orients.Count - 1].X;    //0
                    corr_orient.Y = 256 - sens_orients[sens_orients.Count - 1].Y;   //256
                    corr_orient.Z = -1 * sens_orients[sens_orients.Count - 1].Z;    //0
                    break;
                case (uint)Orientation.L_PORTRAIT: //90
                    corr_orient.X = -256 - sens_orients[sens_orients.Count - 1].X;    //-256
                    corr_orient.Y = -1 * sens_orients[sens_orients.Count - 1].Y;   //0
                    corr_orient.Z = -1 * sens_orients[sens_orients.Count - 1].Z;    //0

                    break;
                case (uint)Orientation.UNLANDSCAPE: //180
                    corr_orient.X = -1 * sens_orients[sens_orients.Count - 1].X;    //0
                    corr_orient.Y = -256 - sens_orients[sens_orients.Count - 1].Y;   //-256
                    corr_orient.Z = -1 * sens_orients[sens_orients.Count - 1].Z;    //0
                    break;
                case (uint)Orientation.R_PORTRAIT: //270
                    corr_orient.X = 256 - sens_orients[sens_orients.Count - 1].X;    //256
                    corr_orient.Y = -1 * sens_orients[sens_orients.Count - 1].Y;   //0
                    corr_orient.Z = -1 * sens_orients[sens_orients.Count - 1].Z;    //0 
                    break;
                default:
                    break;
            }
            X_Corr_button.Text = corr_orient.X.ToString();
            Y_Corr_button.Text = corr_orient.Y.ToString();
            Z_Corr_button.Text = corr_orient.Z.ToString();
        }
        /// <summary>
        /// Возврат к настройкам по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Settings_button_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }
        /// <summary>
        /// Слушаем GPS порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSInterSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (gpsportopen)
            {
                Thread.Sleep(10);// Небольшая задержка перед чтением
                try
                {
                    if (GPSInterSerialPort.BytesToRead > 0)
                    {
                        string incomingString = GPSInterSerialPort.ReadLine();
                        this.BeginInvoke(new SetDelegateText(gpsData), new object[] { incomingString });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения GPS-порта." + Environment.NewLine + ex.Message);
                }
            }
        }
        /// <summary>
        /// Включили получение данных датчика GPS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSOnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GPSOnRadioButton.Checked)
            {
                try
                {
                    if (!ControlInterSerialPort.IsOpen)
                    {
                        ControlInterSerialPort.PortName = ControlPortСomboBox.ValueMember;
                        ControlInterSerialPort.Open();
                    }
                    ControlInterSerialPort.Write("at^wpdgp" + Environment.NewLine);
                    ControlInterSerialPort.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Выключили получение данных датчика GPS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSOffRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GPSOffRadioButton.Checked)
            {
                try
                {
                    if (!ControlInterSerialPort.IsOpen)
                    {
                        ControlInterSerialPort.Open();
                    }
                    ControlInterSerialPort.Write("at^wpend" + Environment.NewLine);
                    ControlInterSerialPort.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// Сброс на 0 корректировки по Х
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void X_Corr_button_Click(object sender, EventArgs e)
        {
            X_Corr_button.Text = "0";
        }
        /// <summary>
        /// Сброс на 0 корректировки по Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_Corr_button_Click(object sender, EventArgs e)
        {
            Y_Corr_button.Text = "0";
        }
        /// <summary>
        /// Сброс на 0 корректировки по Z
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_Corr_button_Click(object sender, EventArgs e)
        {
            Z_Corr_button.Text = "0";
        }
        /// <summary>
        /// Синхронизация переменной Х корректировки и её отображения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void X_Corr_button_TextChanged(object sender, EventArgs e)
        {
            corr_orient.X = int.Parse(X_Corr_button.Text, NumberStyles.Integer);
        }
        /// <summary>
        /// Синхронизация переменной Y корректировки и её отображения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_Corr_button_TextChanged(object sender, EventArgs e)
        {
            corr_orient.Y = int.Parse(Y_Corr_button.Text, NumberStyles.Integer);
        }
        /// <summary>
        /// Синхронизация переменной Z корректировки и её отображения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_Corr_button_TextChanged(object sender, EventArgs e)
        {
            corr_orient.Z = int.Parse(Z_Corr_button.Text, NumberStyles.Integer);
        }
        /// <summary>
        /// Всплывающая подсказка для кнопки корректировки положения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Corr_button_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(Corr_button, Resources.Corr_String);
        }
        /// <summary>
        /// Всплывающая подсказка для кнопки Х сброса корректировки положения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void X_Corr_button_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(X_Corr_button, Resources.Zero_String);
        }
        /// <summary>
        /// Всплывающая подсказка для кнопки Y сброса корректировки положения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_Corr_button_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(Y_Corr_button, Resources.Zero_String);
        }
        /// <summary>
        /// Всплывающая подсказка для кнопки Z сброса корректировки положения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_Corr_button_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(Z_Corr_button, Resources.Zero_String);
        }
        /// <summary>
        /// Отрисовываем линии на вкладке "Контроль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControltabPage_Paint(object sender, PaintEventArgs e)
        {
            Graphics ZeroLine = e.Graphics;
            Pen Pen_Line = new Pen(Color.Black);
            Rectangle rect1 = new Rectangle(470, 270, 80, 80);
            Rectangle rect2 = new Rectangle(49, 235, 80, 80);
            Point p1 = new Point(X_progressBar.Location.X + X_progressBar.Width / 2, X_progressBar.Location.Y - 10);
            Point p2 = new Point(X_progressBar.Location.X + X_progressBar.Width / 2, Z_progressBar.Location.Y + Z_progressBar.Height + 10);
            Point[] points1 = // Верхний вертикальный
                {
                    new Point(547,315),
                    new Point(550,310),
                    new Point(552,315)
                };
            Point[] points2 = // Нижний вертикальный
                {
                    new Point(537,335),
                    new Point(535,341),
                    new Point(541,338)
                };
            Point[] points3 = // Левый горизонтальный
                {
                    new Point(73,242),
                    new Point(66,241),
                    new Point(70,236)
                };
            Point[] points4 = // Правый горизонтальный
                {
                    new Point(105,242),
                    new Point(111,241),
                    new Point(107,236)
                };
            ZeroLine.DrawLine(Pen_Line, p1, p2); // Нулевая линия
            if (Curr_Anim_checkBox.Checked)
            {
                ZeroLine.DrawArc(Pen_Line, rect1, 1f, 50f); // Левый сектор
                ZeroLine.DrawArc(Pen_Line, rect2, 240f, 60f); // Правый сектор
                // Стрелки
                ZeroLine.DrawLines(Pen_Line, points1);
                ZeroLine.DrawLines(Pen_Line, points2);
                ZeroLine.DrawLines(Pen_Line, points3);
                ZeroLine.DrawLines(Pen_Line, points4);
            }
        }
        /// <summary>
        /// Изменили критический угол наклона 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vert_trackBar_Scroll(object sender, EventArgs e)
        {
            Vert_vector_label.Text = Vert_trackBar.Value.ToString() + " \u00B0";
            if (Vert_trackBar.Value <= Hor_trackBar.Value)
            {
                Hor_trackBar.Value = Vert_trackBar.Value - 1;
            }

        }
        /// <summary>
        /// Изменили угол наклона для автоповорота 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hor_trackBar_Scroll(object sender, EventArgs e)
        {
            Hor_vector_label.Text = Hor_trackBar.Value.ToString() + " \u00B0";
            if (Hor_trackBar.Value >= Vert_trackBar.Value)
            {
                Vert_trackBar.Value = Hor_trackBar.Value + 1;
            }
        }
        /// <summary>
        /// Изменили частоту опроса датчика положения 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sens_trackBar_Scroll(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                timer1.Interval = Sens_trackBar.Value;
                timer1.Start();
            }
            Sens_count_label.Text = ((float)Sens_trackBar.Value / 1000).ToString() + " \u2033";
        }
        /// <summary>
        /// Выполняем функции по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private unsafe void timer1_Tick(object sender, EventArgs e)
        {
            lock (locker)
            {
                Monitor.Enter(locker);
                Sensor_Orientation curr_sens_orient = new Sensor_Orientation();
                Average_Coordinates ave_sens = new Average_Coordinates() { X = 0.00, Y = 0.00, Z = 0.00 };
                short[] OutBuffer = new short[3];
                uint bytesReturned = 0;
                if (UnsafeNativeMethods.ReadFile(
                    gs.hDriver,
                    OutBuffer,
                    6u,
                    &bytesReturned,
                    overlapped: null))
                {
                    curr_sens_orient.X = OutBuffer[0]; // X
                    curr_sens_orient.Y = OutBuffer[1]; // Y
                    curr_sens_orient.Z = OutBuffer[2]; // Z
                    sens_orients.Add(curr_sens_orient);
                    // Оставляем пару значений элементов массива для расчёта среднего и отслеживания выключателя
                    if (sens_orients.Count >= 3) sens_orients.RemoveRange(0, sens_orients.Count - 2);
                    // Теперь проверяем положение переключателя
                    if (sens_orients.Count > 1)
                    {
                        if (sens_orients[sens_orients.Count - 1].X.Equals(sens_orients[sens_orients.Count - 2].X) &&
                            sens_orients[sens_orients.Count - 1].Y.Equals(sens_orients[sens_orients.Count - 2].Y) &&
                            sens_orients[sens_orients.Count - 1].Z.Equals(sens_orients[sens_orients.Count - 2].Z))
                        {
                            notifyIcon1.Icon = Resources.Off;
                            Monitor.Exit(locker);
                            return;
                        }
                    }
                    // Тут продолжаем процедуру автоматического поворота экрана (4 положения)
                    // Выводим среднее для сглаживания
                    for (int i = 0; i < sens_orients.Count; i++)
                    {
                        ave_sens.X += sens_orients[i].X;
                        ave_sens.Y += sens_orients[i].Y;
                        ave_sens.Z += sens_orients[i].Z;
                    }
                    if (((ave_sens.X / sens_orients.Count) + corr_orient.X) == 0) ave_sens.X = 0.01;
                    else ave_sens.X = (ave_sens.X / sens_orients.Count) + corr_orient.X;
                    if (((ave_sens.Y / sens_orients.Count) + corr_orient.Y) == 0) ave_sens.Y = 0.01;
                    else ave_sens.Y = (ave_sens.Y / sens_orients.Count) + corr_orient.Y;
                    ave_sens.Z = (ave_sens.Z / sens_orients.Count) + corr_orient.Z;
                    // Переводим значения датчика в углы ориентации
                    double Vert_angel = Math.PI * Vert_trackBar.Value / 180;
                    double Hor_angel = Math.PI * Hor_trackBar.Value / 180;
                    double Vert_Y_gip = Math.Sqrt(Math.Pow(ave_sens.Y, 2) + Math.Pow(ave_sens.Z, 2));
                    double Hor_gip = Math.Sqrt(Math.Pow(ave_sens.X, 2) + Math.Pow(ave_sens.Y, 2));
                    double Vert_X_gip = Math.Sqrt(Math.Pow(ave_sens.X, 2) + Math.Pow(ave_sens.Z, 2));
                    // Отрисовываем показатели сенсора
                    if (Curr_Anim_checkBox.Checked)
                    {
                        Average_Coordinates temp = new Average_Coordinates() { X = (int)ave_sens.X + 256, Y = (int)ave_sens.Y + 256, Z = (int)ave_sens.Z + 256 };
                        if ((int)temp.X < 0) temp.X = X_progressBar.Minimum;
                        if ((int)temp.X > 512) temp.X = X_progressBar.Maximum;
                        X_progressBar.Value = (int)temp.X;
                        label_X.Text = "X: " + curr_sens_orient.X.ToString("0");
                        if ((int)temp.Y < 0) temp.Y = Y_progressBar.Minimum;
                        if ((int)temp.Y > 512) temp.Y = Y_progressBar.Maximum;
                        Y_progressBar.Value = (int)temp.Y;
                        label_Y.Text = "Y: " + curr_sens_orient.Y.ToString("0");
                        if ((int)temp.Z < 0) temp.Z = Z_progressBar.Minimum;
                        if ((int)temp.Z > 512) temp.Z = Z_progressBar.Maximum;
                        Z_progressBar.Value = (int)temp.Z;
                        label_Z.Text = "Z: " + curr_sens_orient.Z.ToString("0");
                        double chl = 0.00;
                        double cvl = 0.00;
                        switch (Current_orientation)
                        {
                            case (uint)Orientation.LANDSCAPE: // 0
                                Cont_Hor_pictureBox.BackgroundImage = Resources.Hor_0;
                                chl = Math.Acos(ave_sens.Y / Hor_gip) * 180 / Math.PI;
                                cvl = Math.Acos(ave_sens.Y / Vert_Y_gip) * 180 / Math.PI;
                                break;
                            case (uint)Orientation.L_PORTRAIT: // 90
                                Cont_Hor_pictureBox.BackgroundImage = Resources.Hor_1;
                                chl = 180 - (Math.Acos(ave_sens.X / Hor_gip) * 180 / Math.PI);
                                cvl = 180 - (Math.Acos(ave_sens.X / Vert_X_gip) * 180 / Math.PI);
                                break;
                            case (uint)Orientation.UNLANDSCAPE: // 180
                                Cont_Hor_pictureBox.BackgroundImage = Resources.Hor_2;
                                chl = 180 - (Math.Acos(ave_sens.Y / Hor_gip) * 180 / Math.PI);
                                cvl = 180 - (Math.Acos(ave_sens.Y / Vert_Y_gip) * 180 / Math.PI);
                                break;
                            case (uint)Orientation.R_PORTRAIT: // 270
                                Cont_Hor_pictureBox.BackgroundImage = Resources.Hor_3;
                                chl = Math.Acos(ave_sens.X / Hor_gip) * 180 / Math.PI;
                                cvl = Math.Acos(ave_sens.X / Vert_X_gip) * 180 / Math.PI;
                                break;
                            default:
                                break;
                        }
                        Cont_Vert_pictureBox.BackgroundImage = Resources.Vert_0;
                        Control_Hor_label.Text = chl.ToString("0.00") + " \u00B0";
                        Control_Vert_label.Text = cvl.ToString("0.00") + " \u00B0";
                    }
                    // При уходе за критические углы наклона просто выходим из цикла и меняем иконку
                    if (Math.Abs(ave_sens.Z / ave_sens.Y) > Math.Tan(Vert_angel) & Math.Abs(ave_sens.Z / ave_sens.X) > Math.Tan(Vert_angel))
                    {
                        notifyIcon1.Icon = Resources.Pause;
                        Monitor.Exit(locker);
                        return;
                    }
                    // Получаем текущую ориентацию экрана (портрет или ландшафт)
                    notifyIcon1.Icon = Resources.On;
                    SafeNativeMethods.DEVMODEW dm = new SafeNativeMethods.DEVMODEW();
                    dm.dmSize = (ushort)Marshal.SizeOf(dm);
                    dm.dmFields = (uint)Disp_Settings.DM_.DISPLAYORIENTATION | (uint)Disp_Settings.DM_.PELSHEIGHT | (uint)Disp_Settings.DM_.PELSWIDTH;
                    uint New_orientation = correct_orient(ave_sens.X, ave_sens.Y, Hor_angel);
                    if (SafeNativeMethods.EnumDisplaySettingsExW(null, Disp_Settings.ENUM_CURRENT_SETTINGS, ref dm, Disp_Settings.EDS_ROTATEDMODE))
                    {
                        Current_orientation = dm.dmDisplayOrientation;
                        if (Current_orientation != New_orientation) vectorOrientation(dm, New_orientation);
                    }
                }
                Monitor.Exit(locker);
            }
        }
        #endregion

        #region Функции, исполняемые самостоятельно
        /// <summary>
        /// Расчёт нового положения дисплея по данным датчика положения и относительно текущего положения
        /// </summary>
        /// <param name="X">Данные по оси Х (ave_sens.X)</param>
        /// <param name="Y">Данные по оси Y (ave_sens.Y)</param>
        /// <param name="ha">Угол ограничения поворота - горизонталь (Hor_angel)</param>
        /// <returns>Отдаёт число - новую ориентацию экрана, текущую - при ошибке</returns>
        private uint correct_orient(double X, double Y, double ha)
        {
            switch (Current_orientation)
            {
                case (uint)Orientation.LANDSCAPE:
                    // Если угол горизонтали больше настроек, то проверяем поворот
                    if (Math.Abs(X / Y) > Math.Tan(ha))
                    {
                        if (X > 0) return (uint)Orientation.R_PORTRAIT;
                        else return (uint)Orientation.L_PORTRAIT;
                    }
                    //Угол в пределах поворота. Смотрим портрет или унпортрет
                    else if (Y < 0) return (uint)Orientation.UNLANDSCAPE;
                    break;
                case (uint)Orientation.L_PORTRAIT:
                    if (Math.Abs(Y / X) > Math.Tan(ha))
                    {
                        if (Y > 0) return (uint)Orientation.LANDSCAPE;
                        else return (uint)Orientation.UNLANDSCAPE;
                    }
                    else if (X > 0) return (uint)Orientation.R_PORTRAIT;
                    break;
                case (uint)Orientation.UNLANDSCAPE:
                    if (Math.Abs(X / Y) > Math.Tan(ha))
                    {
                        if (X > 0) return (uint)Orientation.R_PORTRAIT;
                        else return (uint)Orientation.L_PORTRAIT;
                    }
                    else if (Y > 0) return (uint)Orientation.LANDSCAPE;
                    break;
                case (uint)Orientation.R_PORTRAIT:
                    if (Math.Abs(Y / X) > Math.Tan(ha))
                    {
                        if (Y > 0) return (uint)Orientation.LANDSCAPE;
                        else return (uint)Orientation.UNLANDSCAPE;
                    }
                    else if (X < 0) return (uint)Orientation.L_PORTRAIT;
                    break;
                default:
                    break;
            }
            return Current_orientation;
        }
        /// <summary>
        /// Функция поворота экрана
        /// </summary>
        /// <param name="newOrientation">0 - 0, 1 - 90, 2 - 180, 3 - 270</param>
        private void vectorOrientation(SafeNativeMethods.DEVMODEW dm, uint newOrientation)
        {
            sens_orients.Clear(); // Чистим массив данных для корректного следующего поворота
            if (
    ((newOrientation == (uint)Orientation.LANDSCAPE || newOrientation == (uint)Orientation.UNLANDSCAPE) && (dm.dmDisplayOrientation == (uint)Orientation.L_PORTRAIT || dm.dmDisplayOrientation == (uint)Orientation.R_PORTRAIT))
      ||
    ((newOrientation == (uint)Orientation.L_PORTRAIT || newOrientation == (uint)Orientation.R_PORTRAIT) && (dm.dmDisplayOrientation == (uint)Orientation.LANDSCAPE || dm.dmDisplayOrientation == (uint)Orientation.UNLANDSCAPE)))
            {
                // Меняем местами высоту и ширину экрана 
                uint temp2 = dm.dmPelsHeight;
                dm.dmPelsHeight = dm.dmPelsWidth;
                dm.dmPelsWidth = temp2;
            }
            dm.dmDisplayOrientation = newOrientation;
            switch (SafeNativeMethods.ChangeDisplaySettingsExW(null, ref dm, IntPtr.Zero, (uint)Disp_Settings.CDS_.TEST, IntPtr.Zero))// Тестим возможность изменения параметров дисплея
            {
                case Disp_Settings.DISP_CHANGE_SUCCESSFUL: // 0
                    SafeNativeMethods.ChangeDisplaySettingsExW(null, ref dm, IntPtr.Zero, reestrwrite, IntPtr.Zero); // Изменяем значения дисплея в динамическом режиме
                    break;
                case Disp_Settings.DISP_CHANGE_RESTART: // 1
                    timer1.Stop();
                    SafeNativeMethods.ChangeDisplaySettingsExW(null, ref dm, IntPtr.Zero, (uint)Disp_Settings.CDS_.UPDATEREGISTRY, IntPtr.Zero);
                    if (MessageBox.Show(Resources.DISP_CHANGE_RESTART_String + Environment.NewLine + Resources.Restart_Now_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes) Process.Start("shutdown", "-r -t 5 -f");
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_FAILED: // -1
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_FAILED_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_BADMODE: // -2
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_BADMODE_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                       MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_BADDUALVIEW: // -6
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_BADDUALVIEW_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_BADFLAGS: // -4
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_BADFLAGS_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_BADPARAM: // -5
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_BADPARAM_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                case Disp_Settings.DISP_CHANGE_NOTUPDATED: // -3
                    timer1.Stop();
                    if (MessageBox.Show(Resources.DISP_CHANGE_NOTUPDATED_String + Environment.NewLine + Resources.Close_App_String, Resources.Attention_String, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        gpsportopen = false;
                        Close();
                    }
                    else timer1.Start();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Делегат для записи текста из одного потока в другой
        /// </summary>
        /// <param name="text">Строка для записи</param>
        private delegate void SetDelegateText(string text);
        /// <summary>
        /// Записываем данные GPS в текстовое поле
        /// </summary>
        /// <param name="inputdata">Строка с данными спутника</param>
        private void gpsData(string inputdata)
        {
            GPSTextBox.Text += inputdata + Environment.NewLine;
        }
        /// <summary>
        /// Готовим листинг всех устройств, прописанных в реестре на USB порту и отмечаем доступные
        /// </summary>
        /// <returns>Возвращает массив устройств (название, порт), которые были подключены к системе.</returns>
        internal void ListingUSBDic()
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine; // Зашли в локал машин
                string currentFolder = "USB";
                RegistryKey openRK = rk.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\" + currentFolder); // Открыли на чтение папку USB устройств
                string[] USBDevices = openRK.GetSubKeyNames();  // Получили имена всех, когда-либо подключаемых устройств
                foreach (string stepOne in USBDevices)  // Для каждого производителя устройства проверяем подпапки, т.к. бывает несколько устройств на одном ВИД/ПИД
                {
                    RegistryKey stepOneReg = openRK.OpenSubKey(stepOne);    // Открываем каждого производителя на чтение
                    string[] stepTwo = stepOneReg.GetSubKeyNames(); // Получили список всех устройств для каждого производителя
                    foreach (string friendName in stepTwo)
                    {
                        RegistryKey friendRegName = stepOneReg.OpenSubKey(friendName);
                        string[] fn = friendRegName.GetValueNames();
                        foreach (string currentName in fn)
                        {
                            if (currentName == "FriendlyName")
                            {
                                object frn = friendRegName.GetValue("FriendlyName");
                                RegistryKey devPar = friendRegName.OpenSubKey("Device Parameters");
                                object dp = devPar.GetValue("PortName");
                                fullportname.Add((string)frn, (string)dp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                fullportname.Add("Внимание! Ошибка!", err);
            }
        }
        /// <summary>
        /// Заполняем GPS комбобокс доступными портами и устройствами и сразу выбираем нужный
        /// </summary>
        internal void GPSInterface(string autoConnectString)
        {
            bool deviceDetected = false;
            if (fullportname != null)
            {
                foreach (KeyValuePair<string, string> FrNmDvPr in fullportname)
                {
                    if (FrNmDvPr.Value != null && FrNmDvPr.Value.StartsWith("COM") && availablePort(FrNmDvPr.Value)) // Если не пустое значение, начинается с ком и присутствует в списке доступных портов - наш клиент!
                    {
                        GPSPortComboBox.Items.Add(FrNmDvPr);
                        GPSPortComboBox.DisplayMember = FrNmDvPr.Key;
                        GPSPortComboBox.ValueMember = FrNmDvPr.Value;
                        if (FrNmDvPr.Key.Contains(autoConnectString) && !deviceDetected)
                        {
                            deviceDetected = true;  // Только одно устройство отмечаем галкой
                            GPSPortComboBox.SelectedItem = FrNmDvPr;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Заполняем комбобокс порта управления и выбираем доступный по уникальному имени
        /// </summary>
        /// <param name="autoConnectString">Уникальное имя, по которому можно осуществить отбор и подключение порта</param>
        internal void ControlInterface(string autoConnectString)
        {
            bool deviceDetected = false;
            if (fullportname != null)
            {
                foreach (KeyValuePair<string, string> FrNmDvPr in fullportname)
                {
                    if (FrNmDvPr.Value != null && FrNmDvPr.Value.StartsWith("COM") && availablePort(FrNmDvPr.Value)) // Если не пустое значение, начинается с ком и присутствует в списке доступных портов - наш клиент!
                    {
                        ControlPortСomboBox.Items.Add(FrNmDvPr);
                        ControlPortСomboBox.DisplayMember = FrNmDvPr.Key;
                        ControlPortСomboBox.ValueMember = FrNmDvPr.Value;
                        if (FrNmDvPr.Key.Contains(autoConnectString) && !deviceDetected)
                        {
                            deviceDetected = true;  // Только одно устройство отмечаем галкой
                            ControlPortСomboBox.SelectedItem = FrNmDvPr;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Проверяем доступность портов
        /// </summary>
        /// <param name="portNumber">Имя проверяемого порта в строковом выражении</param>
        /// <returns>true - доступен, false - не доступен</returns>
        internal bool availablePort(string portNumber)
        {
            foreach (string port in ports)
            {
                if (port.Contains(portNumber))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
       }
    /// <summary>
    /// Данные ориентации датчика акселерометра
    /// </summary>
    public class Sensor_Orientation
    {
        public short X { get; internal set; }
        public short Y { get; internal set; }
        public short Z { get; internal set; }
    }
    /// <summary>
    /// Среднее значение для данных сенсора
    /// </summary>
    public class Average_Coordinates
    {
        public double X { get; internal set; }
        public double Y { get; internal set; }
        public double Z { get; internal set; }
    }
}
