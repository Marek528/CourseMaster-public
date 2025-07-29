using CourseMaster.CustomControls;
using MySqlConnector;

namespace CourseMaster.Forms
{
    public partial class CourseForm : Form
    {
        private User currentUser;
        private string langOfCourse = "";
        private int leftPadding = 290, thickness = 5, leftCornerHeight = 150;
        
        private Course currentCourse;
        private FlowLayoutPanel listPanel, mainPanel, horizontalPanel;
        private RichTextBox? topicContentTextBox;
        private Button? confirmButton, startCourse, exitCourse;
        private Label? topicName, courseNameLabel;
        private Panel? topicNameDivider, topicNamePanel;

        private Dictionary<(int section, int question), string> userAnswers = new Dictionary<(int section, int question), string>();

        public CourseForm(Course course, User user)
        {

            InitializeComponent();
            this.Load += CourseForm_Load;

            currentCourse = course;
            currentUser = user;

            if (currentCourse.Link.Contains("_eng"))
            {
                langOfCourse = "eng";
            }
            else if (currentCourse.Link.Contains("_svk"))
            {
                langOfCourse = "svk";
            }

            //
            // TWO MAIN DIVIDERS
            //
            Panel sideDivider = new Panel
            {
                Location = new Point(leftPadding, 0),
                Height = Screen.PrimaryScreen.WorkingArea.Height,
                Width = thickness,
                BackColor = Color.Black
            };

            Panel nameDivider = new Panel
            {
                Location = new Point(0, leftCornerHeight),
                Height = thickness,
                Width = leftPadding,
                BackColor = Color.Black
            };

            //
            // LEFT TOP CORNER
            //
            Panel cornerPanel = new Panel
            {
                Location = new Point(0, 0),
                Height = nameDivider.Location.Y,
                Width = sideDivider.Location.X,
                BackColor = Color.FromArgb(178, 255, 102),
            };

            Label nameLabel = new Label
            {
                Text = course.Name,
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(0, cornerPanel.Height / 4 - 40),
                AutoSize = false,
                Width = leftPadding,
                Height = leftCornerHeight,
                TextAlign = ContentAlignment.TopLeft,
                Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
            };
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;

            //
            // LEFT LIST OF SECTIONS/TOPICS
            //
            listPanel = new FlowLayoutPanel
            {
                Location = new Point(0, leftCornerHeight + thickness),
                Height = Screen.PrimaryScreen.WorkingArea.Height - (leftCornerHeight + thickness), // was changed (idk if its working for now)
                Width = leftPadding,
                BackColor = Color.White,
                AutoScroll = false,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,

            };

            //
            // MAIN PANEL
            //
            mainPanel = new FlowLayoutPanel
            {
                Location = new Point(leftPadding + thickness + 250, leftCornerHeight + 35),
                Height = Screen.PrimaryScreen.WorkingArea.Height - 35 - (leftCornerHeight + 35),
                Width = Screen.PrimaryScreen.WorkingArea.Width - (leftPadding + thickness + 250),
                BackColor = this.BackColor,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
            };

            Controls.Add(sideDivider);
            Controls.Add(nameDivider);
            Controls.Add(cornerPanel);
            cornerPanel.Controls.Add(nameLabel);

            Controls.Add(listPanel);
            Controls.Add(mainPanel);

            if (!currentUser.checkCourses(currentCourse))
            {
                //
                // CONFIRMATION OF STARTING COURSE
                //
                courseNameLabel = new Label
                {
                    Text = course.Name,
                    ForeColor = Color.FromArgb(0, 82, 165),
                    AutoSize = true,
                    Margin = new Padding(mainPanel.Width / 4, mainPanel.Height / 4, 0, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
                };

                horizontalPanel = new FlowLayoutPanel
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlowDirection = FlowDirection.LeftToRight,
                    Margin = new Padding(mainPanel.Width / 4, 25, 0, 0)
                };

                startCourse = new Button
                {
                    BackColor = Color.FromArgb(178, 255, 102),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.FromArgb(0, 82, 165),
                    Margin = new Padding(10, 5, 10, 5),
                    AutoSize = true,
                    Font = new Font("Segoe UI Black", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "Začať kurz",
                    FlatStyle = FlatStyle.Flat
                };
                startCourse.Click += (sender, e) => startCourse_Click();
                if (langOfCourse == "eng")
                {
                    startCourse.Text = "Start course";
                }

                exitCourse = new Button
                {
                    BackColor = Color.FromArgb(178, 255, 102),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.FromArgb(0, 82, 165),
                    Margin = new Padding(10, 5, 10, 5),
                    AutoSize = true,
                    Font = new Font("Segoe UI Black", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "Späť na kurzy",
                    FlatStyle = FlatStyle.Flat
                };
                exitCourse.Click += (sender, e) => exitCourse_Click();
                if (langOfCourse == "eng")
                {
                    exitCourse.Text = "Back to courses";
                }

                mainPanel.Controls.Add(courseNameLabel);
                horizontalPanel.Controls.Add(startCourse);
                horizontalPanel.Controls.Add(exitCourse);
                mainPanel.Controls.Add(horizontalPanel);

                int difference = (horizontalPanel.Width - courseNameLabel.Width) / 2;
                courseNameLabel.Margin = new Padding((mainPanel.Width / 4) + difference + 5, mainPanel.Height / 4, 0, 0);

                loadList(false);
            }
            else
            {
                loadList(true);

                string[] progress = currentUser.getCourseInfo(currentCourse.Link);
                if (progress[0] == progress[1])
                {
                    drawCompletedCourse();
                }
                else
                {
                    loadLastCheckedTopic(progress);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_MOVE)
            {
                return;
            }
            
            base.WndProc(ref m);
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            this.MinimumSize = new Size(workingArea.Width, workingArea.Height);
            this.MaximumSize = new Size(workingArea.Width, workingArea.Height);

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void loadLastCheckedTopic(string[] progress)
        {
            if (progress == null || progress.Length < 2)
            {
                return;
            }

            int currentProgress = int.Parse(progress[0]);

            if (currentProgress == 0)
            {
                return;
            }

            List<CheckBox> checkBoxes = listPanel.Controls.OfType<CheckBox>().ToList();

            CheckBox lastCheckedTopicBox = null;
            int section = 0;
            int topic = 0;
            int topicCount = 0;

            for (int i = 0; i < currentCourse.Sections; i++)
            {
                for (int j = 0; j <= currentCourse.TopicTitles[i].Count; j++)
                {
                    topicCount++;

                    if (topicCount == currentProgress)
                    {
                        section = i;
                        topic = j;

                        foreach (CheckBox cb in checkBoxes)
                        {
                            if (j < currentCourse.TopicTitles[i].Count && cb.Text == currentCourse.TopicTitles[i][j] && cb.Tag.ToString() == "NotQuiz")
                            {
                                lastCheckedTopicBox = cb;
                                break;
                            }
                            else if (j == currentCourse.TopicTitles[i].Count && cb.Tag is int tag && tag == i + 1)
                            {
                                lastCheckedTopicBox = cb;
                                break;
                            }
                        }

                        break;
                    }
                }

                if (lastCheckedTopicBox != null)
                {
                    break;
                }
            }

            // find final quiz checkbox
            if (lastCheckedTopicBox == null && currentProgress == int.Parse(progress[1]) - 1)
            {
                lastCheckedTopicBox = checkBoxes.LastOrDefault(cb => cb.Tag is int);
                if (lastCheckedTopicBox != null)
                {
                    section = currentCourse.Questions.Count - 1;
                    topic = -1;
                }
            }

            if (lastCheckedTopicBox != null)
            {
                topicCheckBoxClick(lastCheckedTopicBox, lastCheckedTopicBox.Tag, section, topic);
            }
        }

        private void startCourse_Click()
        {
            deleteControls();

            // opening the first topic
            if (listPanel.Controls[1] is CheckBox topicCheckBox)
            {
                var tag = topicCheckBox.Tag;

                int currentI = 0, currentJ = 0;
                topicCheckBoxClick(topicCheckBox, tag, currentI, currentJ);

                listPanel.SuspendLayout();
                foreach (Control c in listPanel.Controls)
                {
                    listPanel.Controls.Remove(c);
                    c.Dispose();
                }
                listPanel.Controls.Clear();

                loadList(true);
                listPanel.ResumeLayout(true);
            }

            // add the course to the db (course name and 0/topics + quizes)
            if(!currentUser.checkCourses(currentCourse))
            {
                Database.DBConnect();
                MySqlCommand cmd = new MySqlCommand($"UPDATE users SET Courses = CONCAT(IFNULL(Courses, ''), '{currentCourse.Link};'), Progress = CONCAT(IFNULL(Progress, ''), '0/{currentCourse.Topics + currentCourse.Quizes};') WHERE Email = '{currentUser.Email}'", Database.connection);
                cmd.ExecuteNonQuery();
                Database.DBClose();
            }
        }

        private void exitCourse_Click()
        {
            this.Close();
        }

        private void loadList(bool enabled)
        {
            int y = 0, sections = 1;

            if (listPanel == null)
            {
                return;
            }

            string[] progress = currentUser.getCourseInfo(currentCourse.Link);
            int currentProgress = progress != null && progress.Length > 0 ? int.Parse(progress[0]) : -1;

            for (int i = 0; i < currentCourse.Sections; i++)
            {
                Label sectionLabel = new Label
                {
                    Text = currentCourse.SectionTitles[i],
                    Margin = new Padding(10, 20, 0, 5),
                    ForeColor = Color.FromArgb(0, 82, 165),
                    AutoSize = true,
                    Font = new Font("Segoe UI Black", 12, FontStyle.Bold | FontStyle.Underline)
                };

                listPanel.Controls.Add(sectionLabel);
                y += 20;

                for (int j = 0; j <= currentCourse.TopicTitles[i].Count; j++)
                {
                    int currentI = i;
                    int currentJ = j;
                    
                    CheckBox topicCheckBox = new CheckBox
                    {
                        Margin = new Padding(20, 0, 20, 0),
                        ForeColor = Color.FromArgb(0, 82, 165),
                        AutoSize = true, // NOTE: try to check if the width is more than leftPadding -> than set height/width manually
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Enabled = enabled,
                        Cursor = Cursors.Hand
                    };

                    topicCheckBox.Checked = (currentProgress >= getTotalTopicsCount(i, j));

                    if (j == currentCourse.TopicTitles[i].Count)
                    {
                        topicCheckBox.Text = "Kvíz";
                        if (langOfCourse == "eng")
                        {
                            topicCheckBox.Text = "Quiz";
                        }
                        topicCheckBox.Tag = sections;
                        sections++;
                    }
                    else
                    {
                        topicCheckBox.Text = currentCourse.TopicTitles[i][j];
                        topicCheckBox.Tag = "NotQuiz";
                    }

                    topicCheckBox.MouseEnter += (sender, e) =>
                    {
                        topicCheckBox.Font = new Font(topicCheckBox.Font, FontStyle.Bold | FontStyle.Underline);
                    };

                    topicCheckBox.MouseLeave += (sender, e) =>
                    {
                        topicCheckBox.Font = new Font(topicCheckBox.Font, FontStyle.Bold);
                    };

                    topicCheckBox.Click += (sender, e) => topicCheckBoxClick(sender, topicCheckBox.Tag, currentI, currentJ);

                    listPanel.Controls.Add(topicCheckBox);
                }
            }

            Label finalQuizLabel = new Label
            {
                Text = "Záverečný test",
                Margin = new Padding(10, 20, 0, 5),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 12, FontStyle.Bold | FontStyle.Underline)
            };

            CheckBox finalQuizCheckBox = new CheckBox
            {
                Margin = new Padding(20, 0, 20, 0),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Enabled = enabled,
                Cursor = Cursors.Hand,
                Text = "Kvíz",
                Tag = sections
            };
            if (langOfCourse == "eng")
            {
                finalQuizLabel.Text = "Final Exam";
                finalQuizCheckBox.Text = "Quiz";
            }

            if (progress != null)
            {
                finalQuizCheckBox.Checked = (currentProgress == int.Parse(progress[1]));
            }

            finalQuizCheckBox.MouseEnter += (sender, e) =>
            {
                finalQuizCheckBox.Font = new Font(finalQuizCheckBox.Font, FontStyle.Bold | FontStyle.Underline);
            };

            finalQuizCheckBox.MouseLeave += (sender, e) =>
            {
                finalQuizCheckBox.Font = new Font(finalQuizCheckBox.Font, FontStyle.Bold);
            };

            finalQuizCheckBox.Click += (sender, e) => topicCheckBoxClick(sender, finalQuizCheckBox.Tag, currentCourse.Questions.Count - 1, -1);

            listPanel.Controls.Add(finalQuizLabel);
            listPanel.Controls.Add(finalQuizCheckBox);
        }

        private int getTotalTopicsCount(int currSection, int currTopic)
        {
            int totalTopics = 0;

            for (int i = 0; i < currSection; i++)
            {
                totalTopics += currentCourse.TopicTitles[i].Count + 1;
            }

            totalTopics += currTopic + 1;
            return totalTopics;
        }

        private void topicCheckBoxClick(object sender, object sectionQuizID, int section, int topic)
        {
            deleteControls();

            CheckBox? clickedCheckBox = sender as CheckBox;

            List<CheckBox> checkBoxes = listPanel.Controls.OfType<CheckBox>().ToList();
            int currIndex = checkBoxes.IndexOf(clickedCheckBox);

            if (clickedCheckBox.Tag != "NotQuiz")
            {
                if (currIndex > 0)
                {
                    CheckBox previousCheckBox = checkBoxes[currIndex - 1];

                    if (!previousCheckBox.Checked)
                    {
                        if (langOfCourse == "eng")
                        {
                            MessageBox.Show("You need to have checked previous topic(s)!");
                        }
                        else
                        {
                            MessageBox.Show("Musíte mať vypracované predchádzajúce témy!");
                        }

                        clickedCheckBox.Checked = false;
                        return;
                    }
                }
            }

            string[] progress = currentUser.getCourseInfo(currentCourse.Link);

            if (progress != null && progress.Length > 0)
            {
                int currentProgress = int.Parse(progress[0]);
                
                if (topic != -1)
                {
                    int totalTopicNumber = getTotalTopicsCount(section, topic);

                    if (!clickedCheckBox.Checked && currentProgress >= totalTopicNumber)
                    {
                        clickedCheckBox.Checked = true;
                    }
                    else if (clickedCheckBox.Checked && currentProgress < totalTopicNumber)
                    {
                        clickedCheckBox.Checked = false;
                    }
                }
                else
                {
                    if (clickedCheckBox.Checked && currentProgress != int.Parse(progress[1]))
                    {
                        clickedCheckBox.Checked = false;
                    }
                    else if (!clickedCheckBox.Checked && currentProgress == int.Parse(progress[1]))
                    {
                        clickedCheckBox.Checked = true;
                    }
                }
            }

            if (sectionQuizID is string)
            {
                topicNameDivider = new Panel
                {
                    Location = new Point(0, leftCornerHeight),
                    Height = thickness,
                    Width = Screen.PrimaryScreen.WorkingArea.Width,
                    BackColor = Color.Black
                };

                topicNamePanel = new Panel
                {
                    Location = new Point(leftPadding - 150, 0),
                    Height = topicNameDivider.Location.Y,
                    Width = topicNameDivider.Width,
                    BackColor = Color.FromArgb(178, 255, 102),
                };

                topicName = new Label
                {
                    Text = currentCourse.TopicTitles[section][topic],
                    ForeColor = Color.FromArgb(0, 82, 165),
                    AutoSize = false,
                    Width = topicNamePanel.Width,
                    Height = topicNamePanel.Height,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
                };

                topicContentTextBox = new RichTextBox
                {
                    Text = currentCourse.TopicContent[section][topic],
                    ForeColor = Color.Black,
                    BackColor = mainPanel.BackColor,
                    Font = new Font("Segoe UI", 18),
                    Location = new Point(0, leftCornerHeight + 50),
                    Width = mainPanel.Width - 250,
                    ReadOnly = true,
                    BorderStyle = BorderStyle.None,
                    ScrollBars = RichTextBoxScrollBars.None,
                };

                // gets height based on how much space (in axis Y) is taking the text
                Size preferredSize = topicContentTextBox.GetPreferredSize(new Size(topicContentTextBox.Width, 0));
                topicContentTextBox.Height = preferredSize.Height;

                // with cursor in topicContentTextBox, I can scroll the mainPanel
                topicContentTextBox.MouseWheel += (sender, e) =>
                {
                    int newScrollY = mainPanel.VerticalScroll.Value - e.Delta;
                    newScrollY = Math.Max(mainPanel.VerticalScroll.Minimum, Math.Min(newScrollY, mainPanel.VerticalScroll.Maximum));
                    mainPanel.AutoScrollPosition = new Point(mainPanel.AutoScrollPosition.X, newScrollY);
                };

                Controls.Add(topicNameDivider);
                Controls.Add(topicNamePanel);
                topicNamePanel.Controls.Add(topicName);
                mainPanel.Controls.Add(topicContentTextBox);

                if (!clickedCheckBox.Checked)
                {
                    confirmButton = new Button
                    {
                        BackColor = Color.FromArgb(178, 255, 102),
                        Cursor = Cursors.Hand,
                        ForeColor = Color.FromArgb(0, 51, 102),
                        Margin = new Padding(50, 70, 50, 0),
                        AutoSize = false,
                        Font = new Font("Segoe UI Black", 12),
                        Size = new Size(mainPanel.Width - 300, 50),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = "Naučil som sa to!",
                        FlatStyle = FlatStyle.Flat,
                    };
                    confirmButton.FlatAppearance.BorderSize = 0;
                    if (langOfCourse == "eng")
                    {
                        confirmButton.Text = "I've checked it out!";
                    }

                    confirmButton.Click += (sender, e) => confirmButton_Click(clickedCheckBox, currIndex, checkBoxes, confirmButton);

                    mainPanel.Controls.Add(confirmButton);
                }

            }
            else if (sectionQuizID is int quizID)
            {
                Label titleQuiz = new Label
                {
                    Text = $"KVÍZ #{quizID}",
                    ForeColor = Color.Black,
                    Margin = new Padding(0, 50, 0, 50),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
                };
                if (langOfCourse == "eng")
                {
                    titleQuiz.Text = $"QUIZ #{quizID}";
                }

                Label descQuiz = new Label
                {
                    Text = "Pripravte sa na testovanie svojich vedomostí!\nTento kvíz pozostáva z 5 otázok, z ktorých každá má 4 možnosti odpovede. Na každú otázku je správna len jedna odpoveď.\nVyberajte múdro a prajem vám veľa šťastia!",
                    ForeColor = Color.Black,
                    Margin = new Padding(0, 15, 0, 50),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 17),
                };
                if (langOfCourse == "eng")
                {
                    descQuiz.Text = "Get ready to test your knowledge!\nThis quiz consists of 5 questions, each with 4 answer choices. Only one answer is correct per question.\nChoose wisely and good luck!";
                }

                if (topic == -1)
                {
                    if (langOfCourse == "eng")
                    {
                        titleQuiz.Text = "FINAL QUIZ";
                        descQuiz.Text = "Get ready for the final assessment!\nThis quiz consists of 15 questions, each with 4 answer choices. Only one answer is correct per question.\nThink carefully, choose wisely, and best of luck!";
                    }
                    else
                    {
                        titleQuiz.Text = "ZÁVEREČNÝ KVÍZ";
                        descQuiz.Text = "Pripravte sa na záverečné hodnotenie!\nTento kvíz pozostáva z 15 otázok, z ktorých každá má 4 možnosti odpovede. Na každú otázku je správna len jedna odpoveď.\nDobre si to premyslite, vyberajte múdro a želáme vám veľa šťastia!";
                    }
                }

                Button startQuiz = new Button
                {
                    BackColor = Color.FromArgb(178, 255, 102),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.FromArgb(0, 51, 102),
                    Margin = new Padding(50, 15, 50, 0),
                    AutoSize = false,
                    Font = new Font("Segoe UI Black", 15),
                    Size = new Size(mainPanel.Width - 300, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "ZAČAŤ",
                    FlatStyle = FlatStyle.Flat,
                };
                if (langOfCourse == "eng")
                {
                    startQuiz.Text = "START";
                }

                startQuiz.Click += (sender, e) => startQuiz_Click(section);

                mainPanel.Controls.Add(titleQuiz);
                mainPanel.Controls.Add(descQuiz);
                mainPanel.Controls.Add(startQuiz);
            }
        }

        private void drawQuestion(int section, int question)
        {
            deleteControls();

            Label questionLabel = new Label
            {
                Text = $"{question+1}. {currentCourse.Questions[section][question]}",
                ForeColor = Color.Black,
                Margin = new Padding(0, 50, 0, 50),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
            };
            mainPanel.Controls.Add(questionLabel);

            string[] answersToQuiz = currentCourse.Answers[section][question].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] correctAnswersToQuiz = currentCourse.CorrectAnswers[section][question].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < answersToQuiz.Count(); i++)
            {
                RadioButton answer = new RadioButton
                {
                    Text = $"{answersToQuiz[i]}",
                    ForeColor = Color.Black,
                    Margin = new Padding(5, 10, 0, 10),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 19),
                };

                if (answersToQuiz[i] == correctAnswersToQuiz[0])
                {
                    answer.Tag = "true";
                }
                else
                {
                    answer.Tag = "false";
                }

                if (userAnswers.TryGetValue((section, question), out string storedAnswer))
                {
                    answer.Checked = answer.Text == storedAnswer;
                }


                mainPanel.Controls.Add(answer);
            }

            TableLayoutPanel buttonPanel = new TableLayoutPanel
            {
                Width = mainPanel.Width - 250,
                Height = 50,
                ColumnCount = 2,
                Margin = new Padding(0, 15, 0, 0)
            };

            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            Button nextButton = new Button
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 51, 102),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = ">",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Right
            };
            nextButton.Click += (sender, e) => nextButtonClick(section, question);

            Button previousButton = new Button
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 51, 102),
                Margin = new Padding(0),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "<",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Left
            };
            previousButton.Click += (sender, e) => previousButtonClick(section, question);

            if (question == 0)
            {
                previousButton.Dispose();
                previousButton = null;
                buttonPanel.Controls.Add(nextButton, 1, 0);
            }
            else if (question == currentCourse.Questions[section].Count - 1)
            {
                nextButton.Dispose();
                nextButton = null;
                buttonPanel.Controls.Add(previousButton, 0, 0);

                Button finishButton = new Button
                {
                    BackColor = Color.FromArgb(178, 255, 102),
                    Cursor = Cursors.Hand,
                    ForeColor = Color.FromArgb(0, 51, 102),
                    AutoSize = true,
                    Font = new Font("Segoe UI Black", 15),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "DOKONČIŤ",
                    FlatStyle = FlatStyle.Flat,
                    Anchor = AnchorStyles.Right
                };
                if (langOfCourse == "eng")
                {
                    finishButton.Text = "FINISH";
                }

                finishButton.Click += (sender, e) => finishButtonClick(section);

                buttonPanel.Controls.Add(finishButton);
            }
            else
            {
                buttonPanel.Controls.Add(previousButton, 0, 0);
                buttonPanel.Controls.Add(nextButton, 1, 0);
            }

            mainPanel.Controls.Add(buttonPanel);
        }

        private void startQuiz_Click(int section)
        {
            userAnswers.Clear();
            drawQuestion(section, 0);
        }

        private void nextButtonClick(int section, int question)
        {
            foreach (RadioButton rb in mainPanel.Controls.OfType<RadioButton>())
            {
                if (rb.Checked)
                {
                    userAnswers[(section, question)] = rb.Text;
                    break;
                }
            }

            drawQuestion(section, question + 1);
        }

        private void previousButtonClick(int section, int question)
        {
            foreach (RadioButton rb in mainPanel.Controls.OfType<RadioButton>())
            {
                if (rb.Checked)
                {
                    userAnswers[(section, question)] = rb.Text;
                    break;
                }
            }

            drawQuestion(section, question - 1);
        }

        private void finishButtonClick(int section)
        {
            // save the last question to dictionary
            foreach (RadioButton rb in mainPanel.Controls.OfType<RadioButton>())
            {
                if (rb.Checked)
                {
                    int lastQuestionIndex = currentCourse.Questions[section].Count - 1;
                    userAnswers[(section, lastQuestionIndex)] = rb.Text;
                    break;
                }
            }

            List<int> unansweredQuestions = new List<int>();

            for (int i = 0; i < currentCourse.Questions[section].Count; i++)
            {
                if (!userAnswers.ContainsKey((section, i)) || string.IsNullOrWhiteSpace(userAnswers[(section, i)]))
                {
                    unansweredQuestions.Add(i + 1);
                }
            }

            if (unansweredQuestions.Count > 0)
            {
                if (langOfCourse == "eng")
                {
                    MessageBox.Show("Please answer all of the questions!", "Unanswered Questions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Odpovedzte na všetky otázky!", "Nezodpovedané otázky", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            int totalQuestions = 0, correctAnswers = 0;

            for (int i = 0; i < currentCourse.Questions[section].Count; i++)
            {
                totalQuestions++;

                if (userAnswers.TryGetValue((section, i), out string userAnswer))
                {
                    string[] correctAnswersToQuiz = currentCourse.CorrectAnswers[section][i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (correctAnswersToQuiz.Contains(userAnswer))
                    {
                        correctAnswers++;
                    }
                }
            }

            double finalScore = (double)correctAnswers / totalQuestions * 100;
            showScore(finalScore, section);
        }

        private void showScore(double finalScore, int section)
        {
            deleteControls();

            Label yourScoreLabel = new Label
            {
                Text = "Vaše skóre je:",
                ForeColor = Color.Black,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI Black", 22, FontStyle.Bold),
            };

            CustomProgressBar progressBar = new CustomProgressBar
            {
                Size = new Size(320, 50),
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                ChannelColor = Color.FromArgb(144, 208, 80),
                SliderColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                ChannelHeight = 50,
                SliderHeight = 50,
                ShowValue = TextPosition.Center,
                Maximum = 100,
                Minimum = 0,
                Value = (int)finalScore,
                SymbolAfter = "%"
            };
            progressBar.Margin = new Padding(((mainPanel.Width - progressBar.Width) / 2) - 140, 50, 0, 50);
            yourScoreLabel.Margin = new Padding(((mainPanel.Width - yourScoreLabel.Width) / 2) - 200, 50, 0, 0);
            if (langOfCourse == "eng")
            {
                yourScoreLabel.Text = "Your score is:";
            }

            mainPanel.Controls.Add(yourScoreLabel);
            mainPanel.Controls.Add(progressBar);

            Label resultOfQuiz = new Label
            {
                Text = "Na úspešné absolvovanie potrebujete aspoň 80%!",
                ForeColor = Color.Black,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 17),
            };

            FlowLayoutPanel panelForButtons = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(yourScoreLabel.Margin.Left - 15, 25, 0, 0)
            };

            Button retakeCourse = new Button
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 51, 102),
                Margin = new Padding(10, 5, 10, 5),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Opakovanie kvízu",
                FlatStyle = FlatStyle.Flat
            };
            if (langOfCourse == "eng")
            {
                retakeCourse.Text = "Retake quiz";
            }
            retakeCourse.Click += (sender, e) => retakeCourse_Click(section);

            Button completeQuiz = new Button
            {
                BackColor = Color.FromArgb(178, 255, 102),
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(0, 51, 102),
                Margin = new Padding(10, 5, 10, 5),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Pokračovať",
                FlatStyle = FlatStyle.Flat
            };
            if (langOfCourse == "eng")
            {
                completeQuiz.Text = "Continue";
            }
            completeQuiz.Click += (sender, e) => completeQuiz_Click(section);

            if ((int)finalScore < 80)
            {
                if (langOfCourse == "eng")
                {
                    resultOfQuiz.Text = "You need at least 80% to pass!";
                    resultOfQuiz.Margin = new Padding(((mainPanel.Width - resultOfQuiz.Width) / 2) - 250, 0, 0, 0);
                    retakeCourse.Margin = new Padding(65, 5, 10, 5);
                }
                else
                {
                    resultOfQuiz.Text = "Na úspešné absolvovanie potrebujete aspoň 80%!";
                    resultOfQuiz.Margin = new Padding(((mainPanel.Width - resultOfQuiz.Width) / 2) - 350, 0, 0, 0);
                    retakeCourse.Margin = new Padding(45, 5, 10, 5);
                }

                mainPanel.Controls.Add(resultOfQuiz);
                panelForButtons.Controls.Add(retakeCourse);
            }
            else
            {
                if (langOfCourse == "eng")
                {
                    resultOfQuiz.Text = "You passed!";
                    resultOfQuiz.Margin = new Padding(((mainPanel.Width - resultOfQuiz.Width) / 2) - 160, 0, 0, 0);
                }
                else
                {
                    resultOfQuiz.Text = "Prešli ste!";
                    resultOfQuiz.Margin = new Padding(((mainPanel.Width - resultOfQuiz.Width) / 2) - 145, 0, 0, 0);
                    panelForButtons.Margin = new Padding(yourScoreLabel.Margin.Left - 45, 25, 0, 0);
                }
                
                mainPanel.Controls.Add(resultOfQuiz);
                panelForButtons.Controls.Add(retakeCourse);
                panelForButtons.Controls.Add(completeQuiz);
            }

            mainPanel.Controls.Add(panelForButtons);
        }

        private void retakeCourse_Click(int section)
        {
            string textToCheck = "";
            if (langOfCourse == "eng")
            {
                textToCheck = "Quiz";
            }
            else
            {
                textToCheck = "Kvíz";
            }

            if (textToCheck != "")
            {
                var quizCheckBox = listPanel.Controls.OfType<CheckBox>().FirstOrDefault(cb => cb.Text == textToCheck && (int)cb.Tag == section + 1);
                if (quizCheckBox != null)
                {
                    topicCheckBoxClick(quizCheckBox, section + 1, section, currentCourse.TopicTitles[section].Count);
                }
            }
        }

        private void completeQuiz_Click(int section)
        {
            deleteControls();
            string textToCheck = "";
            if (langOfCourse == "eng")
            {
                textToCheck = "Quiz";
            }
            else
            {
                textToCheck = "Kvíz";
            }

            var quizCheckBox = listPanel.Controls.OfType<CheckBox>().FirstOrDefault(cb => cb.Text == textToCheck && (int)cb.Tag == section + 1);

            if (quizCheckBox != null && !quizCheckBox.Checked && textToCheck != "")
            {
                quizCheckBox.Checked = true;

                string[] courses = currentUser.getCourseInfo(currentCourse.Link, "course");
                string[] progress = currentUser.getCourseInfo(currentCourse.Link, "progress");

                int indexOfCourse = Array.IndexOf(courses, currentCourse.Link);
                string[] currProgress = progress[indexOfCourse].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                currProgress[0] = (Int32.Parse(currProgress[0]) + 1).ToString();
                progress[indexOfCourse] = $"{currProgress[0]}/{currProgress[1]}";

                string result = courses.Length > 1 ? string.Join(";", progress) : $"{progress[0]}";

                Database.DBConnect();
                MySqlCommand cmd = new MySqlCommand($"UPDATE users SET Progress = '{result};' WHERE Email='{currentUser.Email}' ", Database.connection);
                cmd.ExecuteNonQuery();
                Database.DBClose();

                if (section == currentCourse.Questions.Count - 1)
                {
                    if (langOfCourse == "eng")
                    {
                        MessageBox.Show("You have completed the course!");
                    }
                    else
                    {
                        MessageBox.Show("Dokončili ste kurz!");
                    }
                    drawCompletedCourse();
                }
                else
                {
                    if (langOfCourse == "eng")
                    {
                        MessageBox.Show("You have completed the section!");
                    }
                    else
                    {
                        MessageBox.Show("Dokončili ste sekciu!");
                    }
                }
            }
            else if (quizCheckBox != null && quizCheckBox.Checked)
            {
                if (section == currentCourse.Questions.Count - 1)
                {
                    if (langOfCourse == "eng")
                    {
                        MessageBox.Show("You have completed the course!");
                    }
                    else
                    {
                        MessageBox.Show("Dokončili ste kurz!");
                    }
                    drawCompletedCourse();
                }
                else
                {
                    if (langOfCourse == "eng")
                    {
                        MessageBox.Show("You have already completed the section!");
                    }
                    else
                    {
                        MessageBox.Show("Túto sekciu ste už dokončili!");
                    }
                }
            }
        }
        
        private void confirmButton_Click(CheckBox clickedCheckBox, int currIndex, List<CheckBox> checkBoxes, Button confirmButton)
        {
            if (currIndex > 0)
            {
                CheckBox previousCheckBox = checkBoxes[currIndex - 1];

                if (!previousCheckBox.Checked)
                {
                    if (langOfCourse == "eng")
                    {
                        MessageBox.Show("You need to have checked previous topic(s)!");
                    }
                    else
                    {
                        MessageBox.Show("Musíte mať vypracované predchádzajúce témy!");
                    }

                    return;
                }
            }

            string[] courses = currentUser.getCourseInfo(currentCourse.Link, "course");
            string[] progress = currentUser.getCourseInfo(currentCourse.Link, "progress");

            int indexOfCourse = Array.IndexOf(courses, currentCourse.Link);
            string[] currProgress = progress[indexOfCourse].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            currProgress[0] = (Int32.Parse(currProgress[0]) + 1).ToString();
            progress[indexOfCourse] = $"{currProgress[0]}/{currProgress[1]}";

            string result = courses.Length > 1 ? string.Join(";", progress) : $"{progress[0]}";

            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"UPDATE users SET Progress = '{result};' WHERE Email='{currentUser.Email}' ", Database.connection);
            cmd.ExecuteNonQuery();
            Database.DBClose();

            clickedCheckBox.Checked = true;

            mainPanel.Controls.Remove(confirmButton);

            if (currIndex == 0)
            {
                listPanel.SuspendLayout();
                foreach (Control c in listPanel.Controls)
                {
                    listPanel.Controls.Remove(c);
                    c.Dispose();
                }
                listPanel.Controls.Clear();

                loadList(true);
                listPanel.ResumeLayout(true);
            }
        }

        private void drawCompletedCourse()
        {
            deleteControls();

            Label completedLabel = new Label
            {
                Text = "Tento kurz ste dokončili!",
                ForeColor = Color.FromArgb(0, 82, 165),
                Margin = new Padding(200, 250, 100, 50),
                AutoSize = true,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI Black", 30, FontStyle.Bold),
            };
            if (langOfCourse == "eng")
            {
                completedLabel.Text = "You have completed this course!";
            }

            mainPanel.Controls.Add(completedLabel);
        }

        private void deleteControls()
        {
            foreach (Control c in mainPanel.Controls)
            {
                mainPanel.Controls.Remove(c);
                c.Dispose();
            }
            mainPanel.Controls.Clear();

            if (topicNameDivider != null)
            {
                Controls.Remove(topicNameDivider);
                topicNameDivider.Dispose();
            }

            if (topicNamePanel != null)
            {
                Controls.Remove(topicNamePanel);
                topicNamePanel.Dispose();
            }
        }
    }
}