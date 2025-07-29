namespace CourseMaster.Forms
{
    partial class AdminPanelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanelForm));
            panelMenu = new Panel();
            buttonDeleteCourse = new Button();
            buttonEditCourse = new Button();
            buttonAddCourse = new Button();
            panelMain = new Panel();
            labelMain = new Label();
            labelName = new Label();
            mainPanel = new Panel();
            labelWelcome = new Label();
            panelMenu.SuspendLayout();
            panelMain.SuspendLayout();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(178, 255, 102);
            panelMenu.Controls.Add(buttonDeleteCourse);
            panelMenu.Controls.Add(buttonEditCourse);
            panelMenu.Controls.Add(buttonAddCourse);
            panelMenu.Controls.Add(panelMain);
            panelMenu.Dock = DockStyle.Top;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(1170, 48);
            panelMenu.TabIndex = 0;
            // 
            // buttonDeleteCourse
            // 
            buttonDeleteCourse.Cursor = Cursors.Hand;
            buttonDeleteCourse.Dock = DockStyle.Left;
            buttonDeleteCourse.FlatAppearance.BorderSize = 0;
            buttonDeleteCourse.FlatStyle = FlatStyle.Flat;
            buttonDeleteCourse.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonDeleteCourse.ForeColor = Color.FromArgb(0, 82, 165);
            buttonDeleteCourse.Location = new Point(450, 0);
            buttonDeleteCourse.Name = "buttonDeleteCourse";
            buttonDeleteCourse.Size = new Size(145, 48);
            buttonDeleteCourse.TabIndex = 3;
            buttonDeleteCourse.Text = "Odstrániť kurz";
            buttonDeleteCourse.UseVisualStyleBackColor = true;
            buttonDeleteCourse.Click += buttonDeleteCourse_Click;
            // 
            // buttonEditCourse
            // 
            buttonEditCourse.Cursor = Cursors.Hand;
            buttonEditCourse.Dock = DockStyle.Left;
            buttonEditCourse.FlatAppearance.BorderSize = 0;
            buttonEditCourse.FlatStyle = FlatStyle.Flat;
            buttonEditCourse.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonEditCourse.ForeColor = Color.FromArgb(0, 82, 165);
            buttonEditCourse.Location = new Point(321, 0);
            buttonEditCourse.Name = "buttonEditCourse";
            buttonEditCourse.Size = new Size(129, 48);
            buttonEditCourse.TabIndex = 2;
            buttonEditCourse.Text = "Upraviť kurz";
            buttonEditCourse.UseVisualStyleBackColor = true;
            buttonEditCourse.Click += buttonEditCourse_Click;
            // 
            // buttonAddCourse
            // 
            buttonAddCourse.Cursor = Cursors.Hand;
            buttonAddCourse.Dock = DockStyle.Left;
            buttonAddCourse.FlatAppearance.BorderSize = 0;
            buttonAddCourse.FlatStyle = FlatStyle.Flat;
            buttonAddCourse.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonAddCourse.ForeColor = Color.FromArgb(0, 82, 165);
            buttonAddCourse.Location = new Point(192, 0);
            buttonAddCourse.Name = "buttonAddCourse";
            buttonAddCourse.Size = new Size(129, 48);
            buttonAddCourse.TabIndex = 1;
            buttonAddCourse.Text = "Pridať kurz";
            buttonAddCourse.UseVisualStyleBackColor = true;
            buttonAddCourse.Click += buttonAddCourse_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(39, 39, 58);
            panelMain.Controls.Add(labelMain);
            panelMain.Dock = DockStyle.Left;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(192, 48);
            panelMain.TabIndex = 0;
            // 
            // labelMain
            // 
            labelMain.BackColor = Color.FromArgb(144, 208, 80);
            labelMain.Dock = DockStyle.Fill;
            labelMain.Font = new Font("Segoe UI Black", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelMain.ForeColor = Color.FromArgb(0, 82, 165);
            labelMain.Location = new Point(0, 0);
            labelMain.Name = "labelMain";
            labelMain.Size = new Size(192, 48);
            labelMain.TabIndex = 0;
            labelMain.Text = "ADMIN PANEL";
            labelMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            labelName.Dock = DockStyle.Top;
            labelName.Font = new Font("Segoe UI Black", 25F, FontStyle.Regular, GraphicsUnit.Point);
            labelName.ForeColor = Color.FromArgb(0, 82, 165);
            labelName.Location = new Point(0, 48);
            labelName.Name = "labelName";
            labelName.Size = new Size(1170, 58);
            labelName.TabIndex = 1;
            labelName.TextAlign = ContentAlignment.BottomCenter;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(labelWelcome);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 106);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1170, 554);
            mainPanel.TabIndex = 2;
            // 
            // labelWelcome
            // 
            labelWelcome.Anchor = AnchorStyles.None;
            labelWelcome.AutoSize = true;
            labelWelcome.Font = new Font("Segoe UI Black", 45F, FontStyle.Regular, GraphicsUnit.Point);
            labelWelcome.Location = new Point(219, 202);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(706, 81);
            labelWelcome.TabIndex = 0;
            labelWelcome.Text = "Vitajte v admin paneli!";
            // 
            // AdminPanelForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1170, 660);
            Controls.Add(mainPanel);
            Controls.Add(labelName);
            Controls.Add(panelMenu);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1186, 699);
            MinimumSize = new Size(1186, 699);
            Name = "AdminPanelForm";
            Text = "AdminPanelForm";
            panelMenu.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Panel panelMain;
        private Button buttonAddCourse;
        private Label labelMain;
        private Button buttonDeleteCourse;
        private Button buttonEditCourse;
        private Label labelName;
        private Panel mainPanel;
        private Label labelWelcome;
    }
}