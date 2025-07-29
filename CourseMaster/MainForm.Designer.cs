namespace CourseMaster
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel1 = new Panel();
            userLabel = new Label();
            radioButtonEng = new RadioButton();
            radioButtonSvk = new RadioButton();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(144, 208, 80);
            panel1.ForeColor = SystemColors.HighlightText;
            panel1.Location = new Point(0, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(838, 1);
            panel1.TabIndex = 1;
            // 
            // userLabel
            // 
            userLabel.AutoSize = true;
            userLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            userLabel.ForeColor = Color.FromArgb(0, 82, 165);
            userLabel.Location = new Point(583, 9);
            userLabel.Name = "userLabel";
            userLabel.RightToLeft = RightToLeft.Yes;
            userLabel.Size = new Size(0, 25);
            userLabel.TabIndex = 2;
            userLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // radioButtonEng
            // 
            radioButtonEng.AutoSize = true;
            radioButtonEng.Checked = true;
            radioButtonEng.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonEng.ForeColor = Color.FromArgb(0, 82, 165);
            radioButtonEng.Location = new Point(758, 55);
            radioButtonEng.Name = "radioButtonEng";
            radioButtonEng.Size = new Size(62, 25);
            radioButtonEng.TabIndex = 3;
            radioButtonEng.TabStop = true;
            radioButtonEng.Text = "ENG";
            radioButtonEng.UseVisualStyleBackColor = true;
            radioButtonEng.CheckedChanged += radioButtonEng_CheckedChanged;
            // 
            // radioButtonSvk
            // 
            radioButtonSvk.AutoSize = true;
            radioButtonSvk.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonSvk.ForeColor = Color.FromArgb(0, 82, 165);
            radioButtonSvk.Location = new Point(758, 77);
            radioButtonSvk.Name = "radioButtonSvk";
            radioButtonSvk.Size = new Size(59, 25);
            radioButtonSvk.TabIndex = 4;
            radioButtonSvk.Text = "SVK";
            radioButtonSvk.UseVisualStyleBackColor = true;
            radioButtonSvk.CheckedChanged += radioButtonSvk_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(178, 255, 102);
            ClientSize = new Size(839, 487);
            Controls.Add(radioButtonSvk);
            Controls.Add(radioButtonEng);
            Controls.Add(userLabel);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CourseMaster";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel1;
        private Label userLabel;
        private RadioButton radioButtonEng;
        private RadioButton radioButtonSvk;
    }
}