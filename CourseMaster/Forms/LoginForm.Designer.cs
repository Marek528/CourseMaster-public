namespace CourseMaster
{
    partial class LoginForm
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
            labelLogin = new Label();
            emailTextbox = new TextBox();
            passwordTextbox = new TextBox();
            labelEmail = new Label();
            labelPassword = new Label();
            usernameLabel = new Label();
            usernameTextBox = new TextBox();
            repeatPassLabel = new Label();
            repeatPassTextBox = new TextBox();
            registerLinkLabel = new LinkLabel();
            bottomLabel = new Label();
            SuspendLayout();
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            labelLogin.ForeColor = Color.FromArgb(0, 82, 165);
            labelLogin.Location = new Point(52, 27);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(195, 46);
            labelLogin.TabIndex = 0;
            labelLogin.Text = "Prihlásenie";
            // 
            // emailTextbox
            // 
            emailTextbox.BackColor = Color.FromArgb(178, 255, 102);
            emailTextbox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            emailTextbox.ForeColor = Color.FromArgb(0, 82, 165);
            emailTextbox.Location = new Point(49, 166);
            emailTextbox.Name = "emailTextbox";
            emailTextbox.PlaceholderText = "name@smth.com";
            emailTextbox.Size = new Size(193, 27);
            emailTextbox.TabIndex = 2;
            // 
            // passwordTextbox
            // 
            passwordTextbox.BackColor = Color.FromArgb(178, 255, 102);
            passwordTextbox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            passwordTextbox.ForeColor = Color.FromArgb(0, 82, 165);
            passwordTextbox.Location = new Point(49, 229);
            passwordTextbox.Name = "passwordTextbox";
            passwordTextbox.PasswordChar = '*';
            passwordTextbox.PlaceholderText = "heslo123";
            passwordTextbox.Size = new Size(193, 27);
            passwordTextbox.TabIndex = 3;
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelEmail.ForeColor = Color.FromArgb(0, 82, 165);
            labelEmail.Location = new Point(47, 142);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(57, 21);
            labelEmail.TabIndex = 1;
            labelEmail.Text = "Email:";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelPassword.ForeColor = Color.FromArgb(0, 82, 165);
            labelPassword.Location = new Point(47, 205);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(57, 21);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "Heslo:";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            usernameLabel.ForeColor = Color.FromArgb(0, 82, 165);
            usernameLabel.Location = new Point(47, 80);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(83, 21);
            usernameLabel.TabIndex = 8;
            usernameLabel.Text = "Prezývka:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.BackColor = Color.FromArgb(178, 255, 102);
            usernameTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            usernameTextBox.ForeColor = Color.FromArgb(0, 82, 165);
            usernameTextBox.Location = new Point(49, 104);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.PlaceholderText = "Name456";
            usernameTextBox.Size = new Size(193, 27);
            usernameTextBox.TabIndex = 1;
            // 
            // repeatPassLabel
            // 
            repeatPassLabel.AutoSize = true;
            repeatPassLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            repeatPassLabel.ForeColor = Color.FromArgb(0, 82, 165);
            repeatPassLabel.Location = new Point(47, 268);
            repeatPassLabel.Name = "repeatPassLabel";
            repeatPassLabel.Size = new Size(108, 21);
            repeatPassLabel.TabIndex = 9;
            repeatPassLabel.Text = "Heslo znovu:";
            // 
            // repeatPassTextBox
            // 
            repeatPassTextBox.BackColor = Color.FromArgb(178, 255, 102);
            repeatPassTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            repeatPassTextBox.ForeColor = Color.FromArgb(0, 82, 165);
            repeatPassTextBox.Location = new Point(49, 292);
            repeatPassTextBox.Name = "repeatPassTextBox";
            repeatPassTextBox.PasswordChar = '*';
            repeatPassTextBox.PlaceholderText = "heslo123";
            repeatPassTextBox.Size = new Size(193, 27);
            repeatPassTextBox.TabIndex = 4;
            // 
            // registerLinkLabel
            // 
            registerLinkLabel.ActiveLinkColor = Color.Blue;
            registerLinkLabel.AutoSize = true;
            registerLinkLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            registerLinkLabel.ForeColor = Color.FromArgb(0, 82, 165);
            registerLinkLabel.LinkColor = Color.FromArgb(0, 82, 165);
            registerLinkLabel.Location = new Point(150, 411);
            registerLinkLabel.Name = "registerLinkLabel";
            registerLinkLabel.Size = new Size(74, 19);
            registerLinkLabel.TabIndex = 11;
            registerLinkLabel.TabStop = true;
            registerLinkLabel.Text = "Registrácia";
            registerLinkLabel.LinkClicked += registerLinkLabel_LinkClicked;
            // 
            // bottomLabel
            // 
            bottomLabel.AutoSize = true;
            bottomLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            bottomLabel.ForeColor = Color.FromArgb(0, 82, 165);
            bottomLabel.Location = new Point(59, 411);
            bottomLabel.Name = "bottomLabel";
            bottomLabel.Size = new Size(93, 19);
            bottomLabel.TabIndex = 12;
            bottomLabel.Text = "Nemáte účet?";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(178, 255, 102);
            ClientSize = new Size(295, 449);
            Controls.Add(bottomLabel);
            Controls.Add(registerLinkLabel);
            Controls.Add(repeatPassLabel);
            Controls.Add(repeatPassTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(usernameLabel);
            Controls.Add(labelPassword);
            Controls.Add(labelEmail);
            Controls.Add(passwordTextbox);
            Controls.Add(emailTextbox);
            Controls.Add(labelLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Log In";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLogin;
        private TextBox emailTextbox;
        private TextBox passwordTextbox;
        private Label labelEmail;
        private Label labelPassword;
        private Label usernameLabel;
        private TextBox usernameTextBox;
        private Label repeatPassLabel;
        private TextBox repeatPassTextBox;
        private LinkLabel registerLinkLabel;
        private Label bottomLabel;
    }
}