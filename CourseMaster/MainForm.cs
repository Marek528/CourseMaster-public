using CourseMaster.Forms;
using CourseMaster.CustomControls;
using MySqlConnector;

namespace CourseMaster
{
    public partial class MainForm : Form
    {
        public User? UserLogged { get; private set; }
        private bool adminUser = false;
        private ButtonAnimation loginButton, homeButton, myCoursesButton;
        private List<Course> courses = new List<Course>();
        private List<Control> displayedCourses = new List<Control>();
        private Label? noCourseLabel;
        private bool homeState = true;
        //private string[] avaiableCourses = new string[] { "python_eng.xml", "cpp_eng.xml", "cpp_eng.xml", "js_eng.xml", "rust_eng.xml", "python_svk.xml" };
        private string[] avaiableCourses;

        private int loginButonLocation = 720;

        public MainForm()
        {
            InitializeComponent();

            this.AutoScroll = false;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.HorizontalScroll.Maximum = 0;
            this.VerticalScroll.Enabled = false;
            this.VerticalScroll.Visible = false;
            this.VerticalScroll.Maximum = 0;
            this.AutoScroll = true;

            loginButton = new ButtonAnimation(14, true)
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(loginButonLocation, 5),
                Size = new Size(107, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Prihási sa"
            };
            loginButton.Click += (sender, e) => loginButton_Click();

            homeButton = new ButtonAnimation(14, true, false)
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(5, 7),
                Size = new Size(87, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Domov"
            };
            homeButton.Click += (sender, e) => homeButton_Click();

            myCoursesButton = new ButtonAnimation(14, true, false)
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(97, 7),
                Size = new Size(100, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Moje kurzy"
            };
            myCoursesButton.Click += (sender, e) => myCoursesButton_Click();

            Controls.Add(loginButton);
            Controls.Add(homeButton);
            Controls.Add(myCoursesButton);

            myCoursesButton.Hide();
            drawCourses();
        }

        public static string[] loadAvaibleCourses()
        {
            List<string> tempCourses = new List<string>();

            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM courses", Database.connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tempCourses.Add(reader["Course"].ToString());
                }
            }
            Database.DBClose();

            if (tempCourses.Any())
            {
                return tempCourses.ToArray();
            }
            else
            {
                return Array.Empty<string>();
            }
        }

        private void getCourses(string[] arr)
        {
            string[] coursesFileNames = Array.Empty<string>();
            avaiableCourses = loadAvaibleCourses();

            if (avaiableCourses.Length == 0)
            {
                return;
            }

            if (arr == null)
            {
                if (radioButtonSvk.Checked)
                {
                    coursesFileNames = avaiableCourses.Where(course => course.Contains("svk")).ToArray();
                }
                else if (radioButtonEng.Checked)
                {
                    coursesFileNames = avaiableCourses.Where(course => course.Contains("eng")).ToArray();
                }
            }
            else
            {
                if (radioButtonSvk.Checked)
                {
                    coursesFileNames = arr.Where(course => course.Contains("svk")).ToArray();
                }
                else if (radioButtonEng.Checked)
                {
                    coursesFileNames = arr.Where(course => course.Contains("eng")).ToArray();
                }
            }

            Course course = null;

            foreach (string item in coursesFileNames)
            {
                course = new Course(item);
                courses.Add(course);
                course = null;
            }
        }

        private void drawCourses(string[] array = null)
        {
            getCourses(array);
            int x = 0, y = 0, counter = 0;

            foreach (Course item in courses)
            {
                PictureBox pictureBox = new PictureBox
                {
                    ImageLocation = "https://adsro.sk/xml/" + item.Thumbnail,
                    Location = new Point(27 + x, 60 + y),
                    Size = new Size(320, 160),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BorderStyle = BorderStyle.None,
                    Cursor = Cursors.Hand
                };

                Label labelName = new Label
                {
                    BackColor = Color.FromArgb(144, 208, 80),
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                    ForeColor = Color.FromArgb(0, 82, 165),
                    Location = new Point(27 + x, 220 + y),
                    Size = new Size(320, 23),
                    Text = item.Name,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };

                pictureBox.Click += (sender, e) => pictureBoxClick(item, UserLogged);
                labelName.Click += (sender, e) => pictureBoxClick(item, UserLogged);

                x += pictureBox.Width + 50;
                counter++;

                if ((counter % 2) == 0)
                {
                    x = 0;
                    y += pictureBox.Height + labelName.Height + 25;
                    counter = 0;
                }

                this.Controls.Add(pictureBox);
                this.Controls.Add(labelName);
                displayedCourses.Add(pictureBox);
                displayedCourses.Add(labelName);
            }

            courses.Clear();
        }

        private void loginButton_Click()
        {
            if (UserLogged == null)
            {
                using (LoginForm loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        UserLogged = loginForm.User;
                        if (UserLogged != null)
                        {
                            UserLogged.getUsername();
                            //MessageBox.Show($"Login successful: {UserLogged.Username}");
                            if (UserLogged.Email == "admin@admin.com")
                            {
                                adminUser = true;
                            }

                            loginButton.Text = "Odhlási sa";

                            userLabel.Text = UserLogged.Username;
                            userLabel.Location = new Point(loginButonLocation - userLabel.Width - 5, 6);
                            if (!adminUser)
                            {
                                myCoursesButton.Show();
                            }
                            else
                            {
                                AdminPanelForm adminPanelForm = new AdminPanelForm(UserLogged, loadAvaibleCourses());
                                adminPanelForm.StartPosition = FormStartPosition.CenterScreen;
                                adminPanelForm.Text = "Admin Panel";

                                adminPanelForm.FormClosed += (sender, e) =>
                                {
                                    loginButton_Click();
                                    this.Show();
                                };

                                this.Hide();
                                adminPanelForm.Show();
                            }
                        }
                    }
                }
            }
            else
            {
                UserLogged = null;
                adminUser = false;
                loginButton.Text = "Prihlási sa";
                userLabel.Text = "";
                myCoursesButton.Hide();

                this.Controls.Remove(noCourseLabel);
                noCourseLabel = null;
                homeButton_Click();
                homeState = true;
                MessageBox.Show("Úspešné odhlásenie");
            }
        }

        private void homeButton_Click()
        {
            homeState = true;
            this.Controls.Remove(noCourseLabel);
            noCourseLabel = null;

            foreach (var control in displayedCourses)
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
            displayedCourses.Clear();
            drawCourses();
        }

        private void myCoursesButton_Click()
        {
            if (adminUser)
            {
                return;
            }

            this.Controls.Remove(noCourseLabel);
            noCourseLabel = null;

            foreach (var control in displayedCourses)
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
            displayedCourses.Clear();

            homeState = false;
            string coursesInProgress = "";

            if (UserLogged == null)
            {
                MessageBox.Show("Musíte by prihlásený!");
                return;
            }

            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE Email='{UserLogged.Email}'", Database.connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    coursesInProgress = reader["Courses"].ToString();
                }
            }
            Database.DBClose();

            //MessageBox.Show($"kurzy: '{coursesInProgress}'");
            coursesInProgress = coursesInProgress == "" ? null : coursesInProgress;
            string[] tempCourses = null;

            if (coursesInProgress != null)
            {
                tempCourses = coursesInProgress.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }

            if (radioButtonSvk.Checked && tempCourses != null)
            {
                tempCourses = tempCourses.Where(course => course.Contains("svk")).ToArray();
            }
            else if (radioButtonEng.Checked && tempCourses != null)
            {
                tempCourses = tempCourses.Where(course => course.Contains("eng")).ToArray();
            }

            if (tempCourses == null || tempCourses.Length == 0)
            {
                foreach (var control in displayedCourses)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                displayedCourses.Clear();

                noCourseLabel = new Label
                {
                    BackColor = Color.FromArgb(178, 255, 102),
                    Font = new Font("Segoe UI", 18, FontStyle.Regular),
                    ForeColor = Color.FromArgb(0, 82, 165),
                    Location = new Point(50, 50),
                    AutoSize = true,
                    Text = "Zatia¾ neabsolvujete žiadny kurz.\nPrejdite na domovskú stránku a vyberte si jeden hneï teraz!",
                    TextAlign = ContentAlignment.MiddleLeft
                };

                this.Controls.Add(noCourseLabel);
            }
            else
            {
                foreach (var control in displayedCourses)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                displayedCourses.Clear();

                drawCourses(tempCourses);
                int x = 0, y = 0, counter = 0;

                foreach (string c in tempCourses)
                {
                    CustomProgressBar progressBar = new CustomProgressBar
                    {
                        Location = new Point(27 + x, 243 + y),
                        Size = new Size(320, 13),
                        ChannelColor = Color.FromArgb(178, 255, 102),
                        SliderColor = Color.FromArgb(144, 208, 80),
                        ForeColor = Color.FromArgb(0, 82, 165),
                        ChannelHeight = 12,
                        SliderHeight = 12,
                        ShowValue = TextPosition.Center,
                        Maximum = 100,
                        Minimum = 0,
                        Value = (int)((double)Int32.Parse(UserLogged.getCourseInfo(c)[0]) / Int32.Parse(UserLogged.getCourseInfo(c)[1]) * 100),
                        SymbolAfter = "%"
                    };

                    x += progressBar.Width + 50;
                    counter++;

                    if ((counter % 2) == 0)
                    {
                        x = 0;
                        y += 208;
                    }

                    this.Controls.Add(progressBar);
                    displayedCourses.Add(progressBar);
                }
            }
        }

        private void pictureBoxClick(Course course, User user)
        {
            if (UserLogged == null)
            {
                MessageBox.Show("Musíte by prihlásený!");
                return;
            }

            CourseForm courseForm = new CourseForm(course, user);

            Screen currentScreen = Screen.FromControl(this);
            courseForm.StartPosition = FormStartPosition.Manual;
            courseForm.Location = new Point(currentScreen.WorkingArea.Left + (currentScreen.WorkingArea.Width - courseForm.Width) / 2, currentScreen.WorkingArea.Top + (currentScreen.WorkingArea.Height - courseForm.Height) / 2);

            courseForm.WindowState = FormWindowState.Maximized;
            courseForm.Text = course.Name;

            courseForm.FormClosed += (sender, e) =>
            {
                this.Show();
                if (homeState)
                {
                    homeButton_Click();
                }
                else
                {
                    myCoursesButton_Click();
                }
            };

            this.Hide();
            courseForm.Show();
        }

        private void radioButtonSvk_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSvk.Checked)
            {
                if (homeState)
                {
                    homeButton_Click();
                }
                else
                {
                    myCoursesButton_Click();
                }
            }
        }

        private void radioButtonEng_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEng.Checked)
            {
                if (homeState)
                {
                    homeButton_Click();
                }
                else
                {
                    myCoursesButton_Click();
                }
            }
        }
    }
}