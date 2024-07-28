namespace NexusACDL
{
    partial class NexusACDL
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startButton = new Button();
            stopButton = new Button();
            cbTimer = new ComboBox();
            lbl_cbTimer = new Label();
            comboBoxScreens = new ComboBox();
            lblScreens = new Label();
            richTB = new RichTextBox();
            label1 = new Label();
            checkBoxAlwaysOnTop = new CheckBox();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(29, 89);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 1;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Location = new Point(110, 89);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // cbTimer
            // 
            cbTimer.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTimer.ForeColor = SystemColors.WindowText;
            cbTimer.FormattingEnabled = true;
            cbTimer.ImeMode = ImeMode.Disable;
            cbTimer.Items.AddRange(new object[] { "1000", "2000", "3000", "4000", "5000" });
            cbTimer.Location = new Point(12, 12);
            cbTimer.Name = "cbTimer";
            cbTimer.Size = new Size(68, 23);
            cbTimer.TabIndex = 4;
            // 
            // lbl_cbTimer
            // 
            lbl_cbTimer.AutoSize = true;
            lbl_cbTimer.Location = new Point(86, 15);
            lbl_cbTimer.Name = "lbl_cbTimer";
            lbl_cbTimer.Size = new Size(122, 15);
            lbl_cbTimer.TabIndex = 5;
            lbl_cbTimer.Text = "Milliseconds for timer";
            // 
            // comboBoxScreens
            // 
            comboBoxScreens.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxScreens.FormattingEnabled = true;
            comboBoxScreens.Location = new Point(12, 41);
            comboBoxScreens.Name = "comboBoxScreens";
            comboBoxScreens.Size = new Size(121, 23);
            comboBoxScreens.TabIndex = 6;
            // 
            // lblScreens
            // 
            lblScreens.AutoSize = true;
            lblScreens.Location = new Point(139, 44);
            lblScreens.Name = "lblScreens";
            lblScreens.Size = new Size(76, 15);
            lblScreens.TabIndex = 7;
            lblScreens.Text = "Select Screen";
            // 
            // richTB
            // 
            richTB.Location = new Point(234, 12);
            richTB.Name = "richTB";
            richTB.Size = new Size(423, 319);
            richTB.TabIndex = 8;
            richTB.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 115);
            label1.Name = "label1";
            label1.Size = new Size(168, 15);
            label1.TabIndex = 9;
            label1.Text = "To force STOP press the F8 key!";
            // 
            // checkBoxAlwaysOnTop
            // 
            checkBoxAlwaysOnTop.AutoSize = true;
            checkBoxAlwaysOnTop.Location = new Point(45, 191);
            checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            checkBoxAlwaysOnTop.Size = new Size(102, 19);
            checkBoxAlwaysOnTop.TabIndex = 10;
            checkBoxAlwaysOnTop.Text = "Always on Top";
            checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            checkBoxAlwaysOnTop.CheckedChanged += checkBoxAlwaysOnTop_CheckedChanged;
            // 
            // NexusACDL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 341);
            Controls.Add(checkBoxAlwaysOnTop);
            Controls.Add(label1);
            Controls.Add(richTB);
            Controls.Add(lblScreens);
            Controls.Add(comboBoxScreens);
            Controls.Add(lbl_cbTimer);
            Controls.Add(cbTimer);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            Name = "NexusACDL";
            Text = "NexusACDL";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button startButton;
        private Button stopButton;
        private ComboBox cbTimer;
        private Label lbl_cbTimer;
        private ComboBox comboBoxScreens;
        private Label lblScreens;
        private RichTextBox richTB;
        private Label label1;
        private CheckBox checkBoxAlwaysOnTop;
    }
}
