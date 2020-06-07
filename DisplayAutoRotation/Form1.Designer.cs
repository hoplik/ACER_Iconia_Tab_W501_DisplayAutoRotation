namespace DisplayAutoRotation
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            gs.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Control_tabPage = new System.Windows.Forms.TabControl();
            this.ControltabPage = new System.Windows.Forms.TabPage();
            this.Z_Corr_button = new System.Windows.Forms.Button();
            this.Y_Corr_button = new System.Windows.Forms.Button();
            this.X_Corr_button = new System.Windows.Forms.Button();
            this.Corr_button = new System.Windows.Forms.Button();
            this.Control_Vert_label = new System.Windows.Forms.Label();
            this.Control_Hor_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Cont_Vert_pictureBox = new System.Windows.Forms.PictureBox();
            this.Cont_Hor_pictureBox = new System.Windows.Forms.PictureBox();
            this.Corr_label = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.label_Z = new System.Windows.Forms.Label();
            this.label_X = new System.Windows.Forms.Label();
            this.Z_progressBar = new System.Windows.Forms.ProgressBar();
            this.Y_progressBar = new System.Windows.Forms.ProgressBar();
            this.X_progressBar = new System.Windows.Forms.ProgressBar();
            this.Settings_tabPage = new System.Windows.Forms.TabPage();
            this.Curr_Anim_checkBox = new System.Windows.Forms.CheckBox();
            this.Freeze_label = new System.Windows.Forms.Label();
            this.Max_Freeze_label = new System.Windows.Forms.Label();
            this.Min_Freeze_label = new System.Windows.Forms.Label();
            this.Max_Sens_label = new System.Windows.Forms.Label();
            this.Min_Sens_label = new System.Windows.Forms.Label();
            this.Max_Vert_label = new System.Windows.Forms.Label();
            this.Min_Vert_label = new System.Windows.Forms.Label();
            this.Max_Hor_label = new System.Windows.Forms.Label();
            this.Min_Hor_label = new System.Windows.Forms.Label();
            this.Vert_vector_label = new System.Windows.Forms.Label();
            this.Smooth_count_label = new System.Windows.Forms.Label();
            this.Sens_count_label = new System.Windows.Forms.Label();
            this.Hor_vector_label = new System.Windows.Forms.Label();
            this.Sens_label = new System.Windows.Forms.Label();
            this.Vert_label = new System.Windows.Forms.Label();
            this.Hor_label = new System.Windows.Forms.Label();
            this.Reset_Settings_button = new System.Windows.Forms.Button();
            this.Sens_trackBar = new System.Windows.Forms.TrackBar();
            this.Vert_trackBar = new System.Windows.Forms.TrackBar();
            this.Hor_trackBar = new System.Windows.Forms.TrackBar();
            this.Reestr_checkBox = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GPSTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GPSPortComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GPSOffRadioButton = new System.Windows.Forms.RadioButton();
            this.ControlPortСomboBox = new System.Windows.Forms.ComboBox();
            this.GPSPortButton = new System.Windows.Forms.Button();
            this.GPSOnRadioButton = new System.Windows.Forms.RadioButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Size_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.GPS_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.About_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ControlInterSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.GPSInterSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Control_tabPage.SuspendLayout();
            this.ControltabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cont_Vert_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cont_Hor_pictureBox)).BeginInit();
            this.Settings_tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sens_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vert_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hor_trackBar)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_tabPage
            // 
            this.Control_tabPage.Controls.Add(this.ControltabPage);
            this.Control_tabPage.Controls.Add(this.Settings_tabPage);
            this.Control_tabPage.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.Control_tabPage, "Control_tabPage");
            this.Control_tabPage.Name = "Control_tabPage";
            this.Control_tabPage.SelectedIndex = 0;
            // 
            // ControltabPage
            // 
            this.ControltabPage.Controls.Add(this.Z_Corr_button);
            this.ControltabPage.Controls.Add(this.Y_Corr_button);
            this.ControltabPage.Controls.Add(this.X_Corr_button);
            this.ControltabPage.Controls.Add(this.Corr_button);
            this.ControltabPage.Controls.Add(this.Control_Vert_label);
            this.ControltabPage.Controls.Add(this.Control_Hor_label);
            this.ControltabPage.Controls.Add(this.label4);
            this.ControltabPage.Controls.Add(this.label3);
            this.ControltabPage.Controls.Add(this.Cont_Vert_pictureBox);
            this.ControltabPage.Controls.Add(this.Cont_Hor_pictureBox);
            this.ControltabPage.Controls.Add(this.Corr_label);
            this.ControltabPage.Controls.Add(this.label_Y);
            this.ControltabPage.Controls.Add(this.label_Z);
            this.ControltabPage.Controls.Add(this.label_X);
            this.ControltabPage.Controls.Add(this.Z_progressBar);
            this.ControltabPage.Controls.Add(this.Y_progressBar);
            this.ControltabPage.Controls.Add(this.X_progressBar);
            resources.ApplyResources(this.ControltabPage, "ControltabPage");
            this.ControltabPage.Name = "ControltabPage";
            this.ControltabPage.UseVisualStyleBackColor = true;
            this.ControltabPage.Paint += new System.Windows.Forms.PaintEventHandler(this.ControltabPage_Paint);
            // 
            // Z_Corr_button
            // 
            this.Z_Corr_button.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            resources.ApplyResources(this.Z_Corr_button, "Z_Corr_button");
            this.Z_Corr_button.Name = "Z_Corr_button";
            this.Z_Corr_button.UseVisualStyleBackColor = true;
            this.Z_Corr_button.TextChanged += new System.EventHandler(this.Z_Corr_button_TextChanged);
            this.Z_Corr_button.Click += new System.EventHandler(this.Z_Corr_button_Click);
            this.Z_Corr_button.MouseEnter += new System.EventHandler(this.Z_Corr_button_MouseEnter);
            // 
            // Y_Corr_button
            // 
            this.Y_Corr_button.Cursor = System.Windows.Forms.Cursors.NoMoveVert;
            resources.ApplyResources(this.Y_Corr_button, "Y_Corr_button");
            this.Y_Corr_button.Name = "Y_Corr_button";
            this.Y_Corr_button.UseVisualStyleBackColor = true;
            this.Y_Corr_button.TextChanged += new System.EventHandler(this.Y_Corr_button_TextChanged);
            this.Y_Corr_button.Click += new System.EventHandler(this.Y_Corr_button_Click);
            this.Y_Corr_button.MouseEnter += new System.EventHandler(this.Y_Corr_button_MouseEnter);
            // 
            // X_Corr_button
            // 
            this.X_Corr_button.Cursor = System.Windows.Forms.Cursors.NoMoveHoriz;
            resources.ApplyResources(this.X_Corr_button, "X_Corr_button");
            this.X_Corr_button.Name = "X_Corr_button";
            this.X_Corr_button.UseVisualStyleBackColor = true;
            this.X_Corr_button.TextChanged += new System.EventHandler(this.X_Corr_button_TextChanged);
            this.X_Corr_button.Click += new System.EventHandler(this.X_Corr_button_Click);
            this.X_Corr_button.MouseEnter += new System.EventHandler(this.X_Corr_button_MouseEnter);
            // 
            // Corr_button
            // 
            this.Corr_button.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            resources.ApplyResources(this.Corr_button, "Corr_button");
            this.Corr_button.Name = "Corr_button";
            this.Corr_button.UseVisualStyleBackColor = true;
            this.Corr_button.Click += new System.EventHandler(this.Corr_button_Click);
            this.Corr_button.MouseEnter += new System.EventHandler(this.Corr_button_MouseEnter);
            // 
            // Control_Vert_label
            // 
            resources.ApplyResources(this.Control_Vert_label, "Control_Vert_label");
            this.Control_Vert_label.Name = "Control_Vert_label";
            // 
            // Control_Hor_label
            // 
            resources.ApplyResources(this.Control_Hor_label, "Control_Hor_label");
            this.Control_Hor_label.Name = "Control_Hor_label";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // Cont_Vert_pictureBox
            // 
            resources.ApplyResources(this.Cont_Vert_pictureBox, "Cont_Vert_pictureBox");
            this.Cont_Vert_pictureBox.Name = "Cont_Vert_pictureBox";
            this.Cont_Vert_pictureBox.TabStop = false;
            // 
            // Cont_Hor_pictureBox
            // 
            resources.ApplyResources(this.Cont_Hor_pictureBox, "Cont_Hor_pictureBox");
            this.Cont_Hor_pictureBox.Name = "Cont_Hor_pictureBox";
            this.Cont_Hor_pictureBox.TabStop = false;
            // 
            // Corr_label
            // 
            resources.ApplyResources(this.Corr_label, "Corr_label");
            this.Corr_label.Name = "Corr_label";
            // 
            // label_Y
            // 
            resources.ApplyResources(this.label_Y, "label_Y");
            this.label_Y.Name = "label_Y";
            // 
            // label_Z
            // 
            resources.ApplyResources(this.label_Z, "label_Z");
            this.label_Z.Name = "label_Z";
            // 
            // label_X
            // 
            resources.ApplyResources(this.label_X, "label_X");
            this.label_X.Name = "label_X";
            // 
            // Z_progressBar
            // 
            resources.ApplyResources(this.Z_progressBar, "Z_progressBar");
            this.Z_progressBar.Maximum = 512;
            this.Z_progressBar.Name = "Z_progressBar";
            this.Z_progressBar.Step = 1;
            // 
            // Y_progressBar
            // 
            resources.ApplyResources(this.Y_progressBar, "Y_progressBar");
            this.Y_progressBar.Maximum = 512;
            this.Y_progressBar.Name = "Y_progressBar";
            this.Y_progressBar.Step = 1;
            // 
            // X_progressBar
            // 
            resources.ApplyResources(this.X_progressBar, "X_progressBar");
            this.X_progressBar.Maximum = 512;
            this.X_progressBar.Name = "X_progressBar";
            this.X_progressBar.Step = 1;
            // 
            // Settings_tabPage
            // 
            this.Settings_tabPage.Controls.Add(this.Curr_Anim_checkBox);
            this.Settings_tabPage.Controls.Add(this.Freeze_label);
            this.Settings_tabPage.Controls.Add(this.Max_Freeze_label);
            this.Settings_tabPage.Controls.Add(this.Min_Freeze_label);
            this.Settings_tabPage.Controls.Add(this.Max_Sens_label);
            this.Settings_tabPage.Controls.Add(this.Min_Sens_label);
            this.Settings_tabPage.Controls.Add(this.Max_Vert_label);
            this.Settings_tabPage.Controls.Add(this.Min_Vert_label);
            this.Settings_tabPage.Controls.Add(this.Max_Hor_label);
            this.Settings_tabPage.Controls.Add(this.Min_Hor_label);
            this.Settings_tabPage.Controls.Add(this.Vert_vector_label);
            this.Settings_tabPage.Controls.Add(this.Smooth_count_label);
            this.Settings_tabPage.Controls.Add(this.Sens_count_label);
            this.Settings_tabPage.Controls.Add(this.Hor_vector_label);
            this.Settings_tabPage.Controls.Add(this.Sens_label);
            this.Settings_tabPage.Controls.Add(this.Vert_label);
            this.Settings_tabPage.Controls.Add(this.Hor_label);
            this.Settings_tabPage.Controls.Add(this.Reset_Settings_button);
            this.Settings_tabPage.Controls.Add(this.Sens_trackBar);
            this.Settings_tabPage.Controls.Add(this.Vert_trackBar);
            this.Settings_tabPage.Controls.Add(this.Hor_trackBar);
            this.Settings_tabPage.Controls.Add(this.Reestr_checkBox);
            resources.ApplyResources(this.Settings_tabPage, "Settings_tabPage");
            this.Settings_tabPage.Name = "Settings_tabPage";
            this.Settings_tabPage.UseVisualStyleBackColor = true;
            // 
            // Curr_Anim_checkBox
            // 
            resources.ApplyResources(this.Curr_Anim_checkBox, "Curr_Anim_checkBox");
            this.Curr_Anim_checkBox.Checked = global::DisplayAutoRotation.Properties.Settings.Default.Curr_Anim;
            this.Curr_Anim_checkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DisplayAutoRotation.Properties.Settings.Default, "Curr_Anim", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Curr_Anim_checkBox.Name = "Curr_Anim_checkBox";
            this.Curr_Anim_checkBox.UseVisualStyleBackColor = true;
            this.Curr_Anim_checkBox.CheckedChanged += new System.EventHandler(this.Curr_Anim_checkBox_CheckedChanged);
            // 
            // Freeze_label
            // 
            resources.ApplyResources(this.Freeze_label, "Freeze_label");
            this.Freeze_label.Name = "Freeze_label";
            // 
            // Max_Freeze_label
            // 
            resources.ApplyResources(this.Max_Freeze_label, "Max_Freeze_label");
            this.Max_Freeze_label.Name = "Max_Freeze_label";
            // 
            // Min_Freeze_label
            // 
            resources.ApplyResources(this.Min_Freeze_label, "Min_Freeze_label");
            this.Min_Freeze_label.Name = "Min_Freeze_label";
            // 
            // Max_Sens_label
            // 
            resources.ApplyResources(this.Max_Sens_label, "Max_Sens_label");
            this.Max_Sens_label.Name = "Max_Sens_label";
            // 
            // Min_Sens_label
            // 
            resources.ApplyResources(this.Min_Sens_label, "Min_Sens_label");
            this.Min_Sens_label.Name = "Min_Sens_label";
            // 
            // Max_Vert_label
            // 
            resources.ApplyResources(this.Max_Vert_label, "Max_Vert_label");
            this.Max_Vert_label.Name = "Max_Vert_label";
            // 
            // Min_Vert_label
            // 
            resources.ApplyResources(this.Min_Vert_label, "Min_Vert_label");
            this.Min_Vert_label.Name = "Min_Vert_label";
            // 
            // Max_Hor_label
            // 
            resources.ApplyResources(this.Max_Hor_label, "Max_Hor_label");
            this.Max_Hor_label.Name = "Max_Hor_label";
            // 
            // Min_Hor_label
            // 
            resources.ApplyResources(this.Min_Hor_label, "Min_Hor_label");
            this.Min_Hor_label.Name = "Min_Hor_label";
            // 
            // Vert_vector_label
            // 
            resources.ApplyResources(this.Vert_vector_label, "Vert_vector_label");
            this.Vert_vector_label.Name = "Vert_vector_label";
            // 
            // Smooth_count_label
            // 
            resources.ApplyResources(this.Smooth_count_label, "Smooth_count_label");
            this.Smooth_count_label.Name = "Smooth_count_label";
            // 
            // Sens_count_label
            // 
            resources.ApplyResources(this.Sens_count_label, "Sens_count_label");
            this.Sens_count_label.Name = "Sens_count_label";
            // 
            // Hor_vector_label
            // 
            resources.ApplyResources(this.Hor_vector_label, "Hor_vector_label");
            this.Hor_vector_label.Name = "Hor_vector_label";
            // 
            // Sens_label
            // 
            resources.ApplyResources(this.Sens_label, "Sens_label");
            this.Sens_label.Name = "Sens_label";
            // 
            // Vert_label
            // 
            resources.ApplyResources(this.Vert_label, "Vert_label");
            this.Vert_label.Name = "Vert_label";
            // 
            // Hor_label
            // 
            resources.ApplyResources(this.Hor_label, "Hor_label");
            this.Hor_label.Name = "Hor_label";
            // 
            // Reset_Settings_button
            // 
            resources.ApplyResources(this.Reset_Settings_button, "Reset_Settings_button");
            this.Reset_Settings_button.Name = "Reset_Settings_button";
            this.Reset_Settings_button.UseVisualStyleBackColor = true;
            this.Reset_Settings_button.Click += new System.EventHandler(this.Reset_Settings_button_Click);
            // 
            // Sens_trackBar
            // 
            this.Sens_trackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::DisplayAutoRotation.Properties.Settings.Default, "Sens_Track", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.Sens_trackBar, "Sens_trackBar");
            this.Sens_trackBar.LargeChange = 200;
            this.Sens_trackBar.Maximum = 500;
            this.Sens_trackBar.Minimum = 100;
            this.Sens_trackBar.Name = "Sens_trackBar";
            this.Sens_trackBar.SmallChange = 100;
            this.Sens_trackBar.TickFrequency = 200;
            this.Sens_trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.Sens_trackBar.Value = global::DisplayAutoRotation.Properties.Settings.Default.Sens_Track;
            this.Sens_trackBar.Scroll += new System.EventHandler(this.Sens_trackBar_Scroll);
            this.Sens_trackBar.ValueChanged += new System.EventHandler(this.Sens_trackBar_Scroll);
            // 
            // Vert_trackBar
            // 
            this.Vert_trackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::DisplayAutoRotation.Properties.Settings.Default, "Vert_Track", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.Vert_trackBar, "Vert_trackBar");
            this.Vert_trackBar.LargeChange = 1;
            this.Vert_trackBar.Maximum = 81;
            this.Vert_trackBar.Minimum = 46;
            this.Vert_trackBar.Name = "Vert_trackBar";
            this.Vert_trackBar.Value = global::DisplayAutoRotation.Properties.Settings.Default.Vert_Track;
            this.Vert_trackBar.Scroll += new System.EventHandler(this.Vert_trackBar_Scroll);
            this.Vert_trackBar.ValueChanged += new System.EventHandler(this.Vert_trackBar_Scroll);
            // 
            // Hor_trackBar
            // 
            this.Hor_trackBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::DisplayAutoRotation.Properties.Settings.Default, "Hor_Track", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.Hor_trackBar, "Hor_trackBar");
            this.Hor_trackBar.LargeChange = 1;
            this.Hor_trackBar.Maximum = 80;
            this.Hor_trackBar.Minimum = 45;
            this.Hor_trackBar.Name = "Hor_trackBar";
            this.Hor_trackBar.Value = global::DisplayAutoRotation.Properties.Settings.Default.Hor_Track;
            this.Hor_trackBar.Scroll += new System.EventHandler(this.Hor_trackBar_Scroll);
            this.Hor_trackBar.ValueChanged += new System.EventHandler(this.Hor_trackBar_Scroll);
            // 
            // Reestr_checkBox
            // 
            resources.ApplyResources(this.Reestr_checkBox, "Reestr_checkBox");
            this.Reestr_checkBox.Checked = global::DisplayAutoRotation.Properties.Settings.Default.Reestr_Check;
            this.Reestr_checkBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DisplayAutoRotation.Properties.Settings.Default, "Reestr_Check", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Reestr_checkBox.Name = "Reestr_checkBox";
            this.Reestr_checkBox.UseVisualStyleBackColor = true;
            this.Reestr_checkBox.CheckedChanged += new System.EventHandler(this.Reestr_checkBox_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GPSTextBox);
            this.tabPage1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GPSTextBox
            // 
            resources.ApplyResources(this.GPSTextBox, "GPSTextBox");
            this.GPSTextBox.Name = "GPSTextBox";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightGray;
            this.groupBox1.Controls.Add(this.GPSPortComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.GPSOffRadioButton);
            this.groupBox1.Controls.Add(this.ControlPortСomboBox);
            this.groupBox1.Controls.Add(this.GPSPortButton);
            this.groupBox1.Controls.Add(this.GPSOnRadioButton);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // GPSPortComboBox
            // 
            this.GPSPortComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.GPSPortComboBox, "GPSPortComboBox");
            this.GPSPortComboBox.Name = "GPSPortComboBox";
            this.GPSPortComboBox.SelectedIndexChanged += new System.EventHandler(this.GPSPortComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // GPSOffRadioButton
            // 
            resources.ApplyResources(this.GPSOffRadioButton, "GPSOffRadioButton");
            this.GPSOffRadioButton.BackColor = System.Drawing.Color.LightCoral;
            this.GPSOffRadioButton.Checked = true;
            this.GPSOffRadioButton.Name = "GPSOffRadioButton";
            this.GPSOffRadioButton.TabStop = true;
            this.GPSOffRadioButton.UseVisualStyleBackColor = false;
            this.GPSOffRadioButton.CheckedChanged += new System.EventHandler(this.GPSOffRadioButton_CheckedChanged);
            // 
            // ControlPortСomboBox
            // 
            this.ControlPortСomboBox.FormattingEnabled = true;
            resources.ApplyResources(this.ControlPortСomboBox, "ControlPortСomboBox");
            this.ControlPortСomboBox.Name = "ControlPortСomboBox";
            this.ControlPortСomboBox.SelectedIndexChanged += new System.EventHandler(this.ControlPortСomboBox_SelectedIndexChanged);
            // 
            // GPSPortButton
            // 
            resources.ApplyResources(this.GPSPortButton, "GPSPortButton");
            this.GPSPortButton.Name = "GPSPortButton";
            this.GPSPortButton.UseVisualStyleBackColor = true;
            this.GPSPortButton.Click += new System.EventHandler(this.GPSPortButton_Click);
            // 
            // GPSOnRadioButton
            // 
            resources.ApplyResources(this.GPSOnRadioButton, "GPSOnRadioButton");
            this.GPSOnRadioButton.BackColor = System.Drawing.Color.LightGreen;
            this.GPSOnRadioButton.Name = "GPSOnRadioButton";
            this.GPSOnRadioButton.UseVisualStyleBackColor = false;
            this.GPSOnRadioButton.CheckedChanged += new System.EventHandler(this.GPSOnRadioButton_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Size_toolStripMenuItem,
            this.toolStripSeparator1,
            this.GPS_toolStripMenuItem,
            this.toolStripSeparator3,
            this.About_toolStripMenuItem,
            this.toolStripSeparator2,
            this.Exit_toolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // Size_toolStripMenuItem
            // 
            resources.ApplyResources(this.Size_toolStripMenuItem, "Size_toolStripMenuItem");
            this.Size_toolStripMenuItem.Name = "Size_toolStripMenuItem";
            this.Size_toolStripMenuItem.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // GPS_toolStripMenuItem
            // 
            this.GPS_toolStripMenuItem.Name = "GPS_toolStripMenuItem";
            resources.ApplyResources(this.GPS_toolStripMenuItem, "GPS_toolStripMenuItem");
            this.GPS_toolStripMenuItem.Click += new System.EventHandler(this.GPS_toolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // About_toolStripMenuItem
            // 
            this.About_toolStripMenuItem.Name = "About_toolStripMenuItem";
            resources.ApplyResources(this.About_toolStripMenuItem, "About_toolStripMenuItem");
            this.About_toolStripMenuItem.Click += new System.EventHandler(this.About_toolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // Exit_toolStripMenuItem
            // 
            this.Exit_toolStripMenuItem.Name = "Exit_toolStripMenuItem";
            resources.ApplyResources(this.Exit_toolStripMenuItem, "Exit_toolStripMenuItem");
            this.Exit_toolStripMenuItem.Click += new System.EventHandler(this.Exit_toolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GPSInterSerialPort
            // 
            this.GPSInterSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.GPSInterSerialPort_DataReceived);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.Control_tabPage);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Control_tabPage.ResumeLayout(false);
            this.ControltabPage.ResumeLayout(false);
            this.ControltabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cont_Vert_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cont_Hor_pictureBox)).EndInit();
            this.Settings_tabPage.ResumeLayout(false);
            this.Settings_tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sens_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vert_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hor_trackBar)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Control_tabPage;
        private System.Windows.Forms.TabPage ControltabPage;
        private System.Windows.Forms.TabPage Settings_tabPage;
        private System.Windows.Forms.Button Reset_Settings_button;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Size_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem About_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Exit_toolStripMenuItem;
        private System.Windows.Forms.ProgressBar X_progressBar;
        private System.Windows.Forms.ProgressBar Z_progressBar;
        private System.Windows.Forms.ProgressBar Y_progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.CheckBox Reestr_checkBox;
        private System.Windows.Forms.Label Vert_label;
        private System.Windows.Forms.Label Hor_label;
        private System.Windows.Forms.TrackBar Vert_trackBar;
        private System.Windows.Forms.TrackBar Hor_trackBar;
        private System.Windows.Forms.Label Sens_label;
        private System.Windows.Forms.TrackBar Sens_trackBar;
        private System.Windows.Forms.Label Vert_vector_label;
        private System.Windows.Forms.Label Smooth_count_label;
        private System.Windows.Forms.Label Sens_count_label;
        private System.Windows.Forms.Label Hor_vector_label;
        private System.Windows.Forms.Label Corr_label;
        private System.Windows.Forms.Label Max_Sens_label;
        private System.Windows.Forms.Label Min_Sens_label;
        private System.Windows.Forms.Label Max_Vert_label;
        private System.Windows.Forms.Label Min_Vert_label;
        private System.Windows.Forms.Label Max_Hor_label;
        private System.Windows.Forms.Label Min_Hor_label;
        private System.Windows.Forms.Label Max_Freeze_label;
        private System.Windows.Forms.Label Min_Freeze_label;
        private System.Windows.Forms.Label Control_Vert_label;
        private System.Windows.Forms.Label Control_Hor_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox Cont_Vert_pictureBox;
        private System.Windows.Forms.PictureBox Cont_Hor_pictureBox;
        private System.Windows.Forms.Label Freeze_label;
        private System.Windows.Forms.CheckBox Curr_Anim_checkBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton GPSOffRadioButton;
        private System.Windows.Forms.RadioButton GPSOnRadioButton;
        private System.Windows.Forms.Button GPSPortButton;
        private System.Windows.Forms.ComboBox GPSPortComboBox;
        private System.Windows.Forms.TextBox GPSTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ControlPortСomboBox;
        private System.IO.Ports.SerialPort ControlInterSerialPort;
        private System.IO.Ports.SerialPort GPSInterSerialPort;
        private System.Windows.Forms.ToolStripMenuItem GPS_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button Corr_button;
        private System.Windows.Forms.Button X_Corr_button;
        private System.Windows.Forms.Button Y_Corr_button;
        private System.Windows.Forms.Button Z_Corr_button;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

