namespace MainForm
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.inputLabel = new System.Windows.Forms.LinkLabel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.accuracyRichTextBox = new System.Windows.Forms.RichTextBox();
            this.subintervalsCountRichTextBox = new System.Windows.Forms.RichTextBox();
            this.intervalMaxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.intervalMinRichTextBox = new System.Windows.Forms.RichTextBox();
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.solveButton = new System.Windows.Forms.Button();
            this.methodsComboBox = new System.Windows.Forms.ComboBox();
            this.mathodsLinkLabel = new System.Windows.Forms.LinkLabel();
            this.intervalLinkLabel = new System.Windows.Forms.LinkLabel();
            this.intervalFromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.accuracyLinkLabel = new System.Windows.Forms.LinkLabel();
            this.subIntervalsCountlinkLabel = new System.Windows.Forms.LinkLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.equationGraph = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pdfReader = new AxAcroPDFLib.AxAcroPDF();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.answerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.loadLabel = new System.Windows.Forms.Label();
            this.outputTabControl = new System.Windows.Forms.TabControl();
            this.startPictireBox = new System.Windows.Forms.PictureBox();
            this.loadPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.inputGroupBox.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfReader)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.outputTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startPictireBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "&Файл";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(169, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveToolStripMenuItem.Text = "&Сохранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.printToolStripMenuItem.Text = "&Печать";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exitToolStripMenuItem.Text = "Вы&ход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem1
            // 
            this.infoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.manualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            this.infoToolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.infoToolStripMenuItem1.Text = "Спра&вка";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.manualToolStripMenuItem.Text = "Справка";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.aboutToolStripMenuItem.Text = "&О программе...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.inputGroupBox.Controls.Add(this.inputLabel);
            this.inputGroupBox.Controls.Add(this.cancelButton);
            this.inputGroupBox.Controls.Add(this.accuracyRichTextBox);
            this.inputGroupBox.Controls.Add(this.subintervalsCountRichTextBox);
            this.inputGroupBox.Controls.Add(this.intervalMaxRichTextBox);
            this.inputGroupBox.Controls.Add(this.intervalMinRichTextBox);
            this.inputGroupBox.Controls.Add(this.inputTextBox);
            this.inputGroupBox.Controls.Add(this.solveButton);
            this.inputGroupBox.Controls.Add(this.methodsComboBox);
            this.inputGroupBox.Controls.Add(this.mathodsLinkLabel);
            this.inputGroupBox.Controls.Add(this.intervalLinkLabel);
            this.inputGroupBox.Controls.Add(this.intervalFromLabel);
            this.inputGroupBox.Controls.Add(this.toLabel);
            this.inputGroupBox.Controls.Add(this.accuracyLinkLabel);
            this.inputGroupBox.Controls.Add(this.subIntervalsCountlinkLabel);
            this.inputGroupBox.Location = new System.Drawing.Point(12, 27);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(911, 154);
            this.inputGroupBox.TabIndex = 2;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Ввод уравнения";
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.inputLabel.LinkColor = System.Drawing.Color.Navy;
            this.inputLabel.Location = new System.Drawing.Point(6, 25);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(36, 25);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.TabStop = true;
            this.inputLabel.Text = "f =";
            this.inputLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputLabel_LinkClicked);
            // 
            // cancelButton
            // 
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(300, 112);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(288, 36);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // accuracyRichTextBox
            // 
            this.accuracyRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.accuracyRichTextBox.Location = new System.Drawing.Point(839, 59);
            this.accuracyRichTextBox.Multiline = false;
            this.accuracyRichTextBox.Name = "accuracyRichTextBox";
            this.accuracyRichTextBox.Size = new System.Drawing.Size(66, 20);
            this.accuracyRichTextBox.TabIndex = 9;
            this.accuracyRichTextBox.Text = "3";
            this.accuracyRichTextBox.TextChanged += new System.EventHandler(this.accuracyRichTextBox_TextChanged);
            // 
            // subintervalsCountRichTextBox
            // 
            this.subintervalsCountRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.subintervalsCountRichTextBox.Location = new System.Drawing.Point(495, 59);
            this.subintervalsCountRichTextBox.Multiline = false;
            this.subintervalsCountRichTextBox.Name = "subintervalsCountRichTextBox";
            this.subintervalsCountRichTextBox.Size = new System.Drawing.Size(65, 20);
            this.subintervalsCountRichTextBox.TabIndex = 7;
            this.subintervalsCountRichTextBox.Text = "40";
            this.subintervalsCountRichTextBox.TextChanged += new System.EventHandler(this.subintervalsCountRichTextBox_TextChanged);
            // 
            // intervalMaxRichTextBox
            // 
            this.intervalMaxRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.intervalMaxRichTextBox.Location = new System.Drawing.Point(247, 59);
            this.intervalMaxRichTextBox.Multiline = false;
            this.intervalMaxRichTextBox.Name = "intervalMaxRichTextBox";
            this.intervalMaxRichTextBox.Size = new System.Drawing.Size(50, 20);
            this.intervalMaxRichTextBox.TabIndex = 5;
            this.intervalMaxRichTextBox.Text = "20";
            this.intervalMaxRichTextBox.TextChanged += new System.EventHandler(this.intervalMaxRichTextBox_TextChanged);
            // 
            // intervalMinRichTextBox
            // 
            this.intervalMinRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.intervalMinRichTextBox.Location = new System.Drawing.Point(179, 59);
            this.intervalMinRichTextBox.Multiline = false;
            this.intervalMinRichTextBox.Name = "intervalMinRichTextBox";
            this.intervalMinRichTextBox.Size = new System.Drawing.Size(50, 20);
            this.intervalMinRichTextBox.TabIndex = 4;
            this.intervalMinRichTextBox.Text = "-20";
            this.intervalMinRichTextBox.TextChanged += new System.EventHandler(this.intervalMinRichTextBox_TextChanged);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputTextBox.Location = new System.Drawing.Point(44, 24);
            this.inputTextBox.Multiline = false;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(861, 29);
            this.inputTextBox.TabIndex = 2;
            this.inputTextBox.Text = "";
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(6, 112);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(288, 36);
            this.solveButton.TabIndex = 12;
            this.solveButton.Text = "Решить";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // methodsComboBox
            // 
            this.methodsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodsComboBox.FormattingEnabled = true;
            this.methodsComboBox.ItemHeight = 13;
            this.methodsComboBox.Items.AddRange(new object[] {
            "Метод половинного деления",
            "Метод Золотого сечения",
            "Метод Ньютона (метод касательных)",
            "Модифицированный метод Ньютона",
            "Метод секущих",
            "Метод хорд",
            "Комбинированный метод",
            "Метод Риддера"});
            this.methodsComboBox.Location = new System.Drawing.Point(95, 85);
            this.methodsComboBox.Name = "methodsComboBox";
            this.methodsComboBox.Size = new System.Drawing.Size(285, 21);
            this.methodsComboBox.TabIndex = 11;
            // 
            // mathodsLinkLabel
            // 
            this.mathodsLinkLabel.AutoSize = true;
            this.mathodsLinkLabel.LinkColor = System.Drawing.Color.Navy;
            this.mathodsLinkLabel.Location = new System.Drawing.Point(3, 88);
            this.mathodsLinkLabel.Name = "mathodsLinkLabel";
            this.mathodsLinkLabel.Size = new System.Drawing.Size(89, 13);
            this.mathodsLinkLabel.TabIndex = 10;
            this.mathodsLinkLabel.TabStop = true;
            this.mathodsLinkLabel.Text = "Метод решения:";
            this.mathodsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mathodsLinkLabel_LinkClicked);
            // 
            // intervalLinkLabel
            // 
            this.intervalLinkLabel.AutoSize = true;
            this.intervalLinkLabel.LinkColor = System.Drawing.Color.Navy;
            this.intervalLinkLabel.Location = new System.Drawing.Point(3, 61);
            this.intervalLinkLabel.Name = "intervalLinkLabel";
            this.intervalLinkLabel.Size = new System.Drawing.Size(158, 13);
            this.intervalLinkLabel.TabIndex = 3;
            this.intervalLinkLabel.TabStop = true;
            this.intervalLinkLabel.Text = "Интервал для поиска корней:";
            this.intervalLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.intervalLinkLabel_LinkClicked);
            // 
            // intervalFromLabel
            // 
            this.intervalFromLabel.AutoSize = true;
            this.intervalFromLabel.Location = new System.Drawing.Point(161, 61);
            this.intervalFromLabel.Name = "intervalFromLabel";
            this.intervalFromLabel.Size = new System.Drawing.Size(18, 13);
            this.intervalFromLabel.TabIndex = 3;
            this.intervalFromLabel.Text = "от";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(229, 61);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(19, 13);
            this.toLabel.TabIndex = 5;
            this.toLabel.Text = "до";
            // 
            // accuracyLinkLabel
            // 
            this.accuracyLinkLabel.AutoSize = true;
            this.accuracyLinkLabel.LinkColor = System.Drawing.Color.Navy;
            this.accuracyLinkLabel.Location = new System.Drawing.Point(598, 61);
            this.accuracyLinkLabel.Name = "accuracyLinkLabel";
            this.accuracyLinkLabel.Size = new System.Drawing.Size(240, 13);
            this.accuracyLinkLabel.TabIndex = 8;
            this.accuracyLinkLabel.TabStop = true;
            this.accuracyLinkLabel.Text = "Точность (количество знаков после запятой):";
            this.accuracyLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.accuracyLinkLabel_LinkClicked);
            // 
            // subIntervalsCountlinkLabel
            // 
            this.subIntervalsCountlinkLabel.AutoSize = true;
            this.subIntervalsCountlinkLabel.LinkColor = System.Drawing.Color.Navy;
            this.subIntervalsCountlinkLabel.Location = new System.Drawing.Point(306, 61);
            this.subIntervalsCountlinkLabel.Name = "subIntervalsCountlinkLabel";
            this.subIntervalsCountlinkLabel.Size = new System.Drawing.Size(188, 13);
            this.subIntervalsCountlinkLabel.TabIndex = 6;
            this.subIntervalsCountlinkLabel.TabStop = true;
            this.subIntervalsCountlinkLabel.Text = "Количество интервалов разбиения:";
            this.subIntervalsCountlinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.subIntervalsCountlinkLabel_LinkClicked);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.equationGraph);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(903, 423);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "График";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // equationGraph
            // 
            this.equationGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equationGraph.Location = new System.Drawing.Point(0, 0);
            this.equationGraph.Name = "equationGraph";
            this.equationGraph.ScrollGrace = 0D;
            this.equationGraph.ScrollMaxX = 0D;
            this.equationGraph.ScrollMaxY = 0D;
            this.equationGraph.ScrollMaxY2 = 0D;
            this.equationGraph.ScrollMinX = 0D;
            this.equationGraph.ScrollMinY = 0D;
            this.equationGraph.ScrollMinY2 = 0D;
            this.equationGraph.Size = new System.Drawing.Size(903, 423);
            this.equationGraph.TabIndex = 0;
            this.equationGraph.UseExtendedPrintDialog = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pdfReader);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(903, 423);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Отчет";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pdfReader
            // 
            this.pdfReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfReader.Enabled = true;
            this.pdfReader.Location = new System.Drawing.Point(3, 3);
            this.pdfReader.Name = "pdfReader";
            this.pdfReader.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("pdfReader.OcxState")));
            this.pdfReader.Size = new System.Drawing.Size(897, 417);
            this.pdfReader.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.answerRichTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(903, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ответ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // answerRichTextBox
            // 
            this.answerRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.answerRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.answerRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.answerRichTextBox.Name = "answerRichTextBox";
            this.answerRichTextBox.Size = new System.Drawing.Size(897, 417);
            this.answerRichTextBox.TabIndex = 0;
            this.answerRichTextBox.Text = "";
            // 
            // loadLabel
            // 
            this.loadLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadLabel.AutoSize = true;
            this.loadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadLabel.Location = new System.Drawing.Point(380, 296);
            this.loadLabel.Name = "loadLabel";
            this.loadLabel.Size = new System.Drawing.Size(180, 20);
            this.loadLabel.TabIndex = 1;
            this.loadLabel.Text = "Состояние программы";
            this.loadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // outputTabControl
            // 
            this.outputTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTabControl.Controls.Add(this.tabPage1);
            this.outputTabControl.Controls.Add(this.tabPage2);
            this.outputTabControl.Controls.Add(this.tabPage3);
            this.outputTabControl.Location = new System.Drawing.Point(12, 187);
            this.outputTabControl.Name = "outputTabControl";
            this.outputTabControl.SelectedIndex = 0;
            this.outputTabControl.Size = new System.Drawing.Size(911, 449);
            this.outputTabControl.TabIndex = 3;
            // 
            // startPictireBox
            // 
            this.startPictireBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startPictireBox.Image = global::MainForm.Properties.Resources.startPicture;
            this.startPictireBox.Location = new System.Drawing.Point(12, 187);
            this.startPictireBox.Name = "startPictireBox";
            this.startPictireBox.Size = new System.Drawing.Size(923, 457);
            this.startPictireBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startPictireBox.TabIndex = 1;
            this.startPictireBox.TabStop = false;
            // 
            // loadPictureBox
            // 
            this.loadPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.loadPictureBox.Image = global::MainForm.Properties.Resources._712__7_;
            this.loadPictureBox.Location = new System.Drawing.Point(12, 210);
            this.loadPictureBox.Name = "loadPictureBox";
            this.loadPictureBox.Size = new System.Drawing.Size(911, 83);
            this.loadPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadPictureBox.TabIndex = 1;
            this.loadPictureBox.TabStop = false;
            this.loadPictureBox.SizeChanged += new System.EventHandler(this.loadPictureBox_SizeChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(935, 641);
            this.Controls.Add(this.outputTabControl);
            this.Controls.Add(this.inputGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.loadPictureBox);
            this.Controls.Add(this.loadLabel);
            this.Controls.Add(this.startPictireBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(951, 680);
            this.Name = "MainForm";
            this.Text = "Численное решение уравнений";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pdfReader)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.outputTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startPictireBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.LinkLabel intervalLinkLabel;
        private System.Windows.Forms.Label intervalFromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.LinkLabel accuracyLinkLabel;
        private System.Windows.Forms.LinkLabel subIntervalsCountlinkLabel;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.ComboBox methodsComboBox;
        private System.Windows.Forms.LinkLabel mathodsLinkLabel;
        private System.Windows.Forms.RichTextBox inputTextBox;
        private System.Windows.Forms.RichTextBox intervalMinRichTextBox;
        private System.Windows.Forms.RichTextBox intervalMaxRichTextBox;
        private System.Windows.Forms.RichTextBox subintervalsCountRichTextBox;
        private System.Windows.Forms.RichTextBox accuracyRichTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox answerRichTextBox;
        private System.Windows.Forms.TabControl outputTabControl;
        private System.Windows.Forms.PictureBox loadPictureBox;
        private System.Windows.Forms.Label loadLabel;
        private System.Windows.Forms.Button cancelButton;
        private AxAcroPDFLib.AxAcroPDF pdfReader;
        private ZedGraph.ZedGraphControl equationGraph;
        private System.Windows.Forms.PictureBox startPictireBox;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.LinkLabel inputLabel;
    }
}

