using System.Text.RegularExpressions;
using CourseMaster.CustomControls;

namespace CourseMaster
{
    public partial class LoginForm : Form
    {
        public User? User { get; private set; }
        private bool loginMode = true;
        private ButtonAnimation loginButton;

        public LoginForm()
        {
            InitializeComponent();
            loginButton = new ButtonAnimation(20, true);
            loginButton.BackColor = Color.FromArgb(178, 255, 102);
            loginButton.Cursor = Cursors.Hand;
            loginButton.ForeColor = Color.FromArgb(0, 82, 165);
            loginButton.Location = new Point(72, 342);
            loginButton.Size = new Size(153, 58);
            loginButton.Text = "Prihlásiť";
            loginButton.TabIndex = 5;
            loginButton.Click += loginButton_Click;
            Controls.Add(loginButton);
        }

        private void loginButton_Click(object? sender, EventArgs e)
        {
            if (loginMode)
            {
                if (string.IsNullOrEmpty(emailTextbox.Text) || string.IsNullOrEmpty(passwordTextbox.Text))
                {
                    MessageBox.Show("Prosím, vyplňte všetky polia");
                    return;
                }
                else
                {
                    User = new User(usernameTextBox.Text, emailTextbox.Text, passwordTextbox.Text);

                    if (User.loginUser())
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Email alebo heslo je nesprávne!");
                        User = null;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(emailTextbox.Text) || string.IsNullOrEmpty(passwordTextbox.Text) || string.IsNullOrEmpty(repeatPassTextBox.Text))
                {
                    MessageBox.Show("Prosím, vyplňte všetky polia");
                    return;
                }
                else
                {
                    if (emailValidate())
                    {
                        User = new User(usernameTextBox.Text, emailTextbox.Text, passwordTextbox.Text);

                        if (User.checkUser())
                        {
                            MessageBox.Show("Používateľ už existuje");
                            User = null;

                            usernameTextBox.Text = "";
                            emailTextbox.Text = "";
                            passwordTextbox.Text = "";
                            repeatPassTextBox.Text = "";
                            return;
                        }

                        if (passwordTextbox.Text == repeatPassTextBox.Text)
                        {
                            User.addUser();
                            MessageBox.Show("Úspešne ste sa zaregistrovali");
                            User = null;

                            usernameTextBox.Text = "";
                            emailTextbox.Text = "";
                            passwordTextbox.Text = "";
                            repeatPassTextBox.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Heslá sa nezhodujú");
                            User = null;
                        }
                    }
                }
            }
        }

        private bool emailValidate()
        {
            string pattern = @"^[a-z0-9\.]+@[a-z]+\.[a-z]+$";

            if (Regex.IsMatch(emailTextbox.Text, pattern))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Neplatný emailový formát");
                return false;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(311, 420);

            usernameTextBox.Visible = false;
            usernameLabel.Visible = false;
            repeatPassLabel.Visible = false;
            repeatPassTextBox.Visible = false;

            labelEmail.Location = new Point(47, 102); // - 40
            emailTextbox.Location = new Point(49, 126);  // - 40
            labelPassword.Location = new Point(47, 165); // - 40
            passwordTextbox.Location = new Point(49, 189); // - 40

            loginButton.Location = new Point(68, 252); // - 90
            bottomLabel.Location = new Point(67, 331); // from button its (originally) +69
            registerLinkLabel.Location = new Point(158, 331);
        }

        private void registerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginMode = !loginMode;
            if (loginMode)
            {
                this.Size = new Size(311, 420);

                usernameTextBox.Visible = false;
                usernameLabel.Visible = false;
                repeatPassLabel.Visible = false;
                repeatPassTextBox.Visible = false;

                labelLogin.Text = "Prihlásenie";
                labelLogin.Location = new Point(52, 27);
                loginButton.Text = "Prihlásiť";
                bottomLabel.Text = "Nemáte účet?";
                registerLinkLabel.Text = "Registrácia";

                labelEmail.Location = new Point(47, 102); // - 40
                emailTextbox.Location = new Point(49, 126);  // - 40
                labelPassword.Location = new Point(47, 165); // - 40
                passwordTextbox.Location = new Point(49, 189); // - 40

                loginButton.Location = new Point(68, 252); // - 90
                bottomLabel.Location = new Point(67, 331); // from button its (originally) +69
                registerLinkLabel.Location = new Point(158, 331);

                usernameTextBox.Text = "";
                emailTextbox.Text = "";
                passwordTextbox.Text = "";
                repeatPassTextBox.Text = "";

                this.Text = "Prihlásenie";
            }
            else
            {
                this.Size = new Size(311, 488);

                usernameTextBox.Visible = true;
                usernameLabel.Visible = true;
                repeatPassLabel.Visible = true;
                repeatPassTextBox.Visible = true;

                labelLogin.Text = "Registrácia";
                labelLogin.Location = new Point(52, 17);
                loginButton.Text = "Registrovať";
                bottomLabel.Text = "Už máte účet?";
                registerLinkLabel.Text = "Prihlásiť";

                labelEmail.Location = new Point(47, 142);
                emailTextbox.Location = new Point(49, 166);
                labelPassword.Location = new Point(47, 205);
                passwordTextbox.Location = new Point(49, 229);

                loginButton.Location = new Point(68, 342);
                loginButton.Width += 10;
                bottomLabel.Location = new Point(75, 411);
                registerLinkLabel.Location = new Point(166, 411);

                usernameTextBox.Text = "";
                emailTextbox.Text = "";
                passwordTextbox.Text = "";
                repeatPassTextBox.Text = "";

                this.Text = "Registrácia";
            }
        }
    }
}
