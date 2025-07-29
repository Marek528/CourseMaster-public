using MySqlConnector;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CourseMaster.Forms
{
    public partial class AdminPanelForm : Form
    {
        private string defaultFtpURL = "";
        private string username = "";
        private string password = "";

        private User user;
        private string[] avaibleCourses;
        private RichTextBox? contentEditableRTB;
        private Label? labelLoadedXmlFile;
        private TextBox? nameTextBox;
        private RadioButton? radioButtonEng, radioButtonSvk;
        private PictureBox? pictureBox;
        private string currentCourseLoaded = "";
        private int xAxis = 400;

        public AdminPanelForm(User user, string[] courses)
        {
            InitializeComponent();
            this.user = user;
            this.avaibleCourses = courses;
        }

        private void activateButton(object sender)
        {
            Button? currButton = sender as Button;

            if (currButton != null)
            {
                disableButton();
                currButton.BackColor = this.BackColor;
                currButton.ForeColor = Color.FromArgb(0, 82, 165);
                currButton.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            }
        }

        private void disableButton()
        {
            foreach (Button bt in panelMenu.Controls.OfType<Button>())
            {
                bt.BackColor = Color.FromArgb(178, 255, 102);
                bt.ForeColor = Color.FromArgb(0, 82, 165);
                bt.Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point);
            }
        }

        private void changePanelContent(object sender, string headerText)
        {
            foreach (Control c in mainPanel.Controls)
            {
                mainPanel.Controls.Remove(c);
                c.Dispose();
            }
            mainPanel.Controls.Clear();

            activateButton(sender);
            labelWelcome.Hide();
            labelName.Text = headerText;
        }

        private void buttonAddCourse_Click(object sender, EventArgs e)
        {
            if (File.Exists("temp.xml"))
            {
                File.Delete("temp.xml");
            }

            if (File.Exists("temp_image.png"))
            {
                File.Delete("temp_image.png");
            }

            if (File.Exists("temp_image.jpg"))
            {
                File.Delete("temp_image.jpg");
            }

            changePanelContent(sender, buttonAddCourse.Text);

            //
            // Select file
            //
            Label labelSelectFile = new Label
            {
                Text = "Nahrajte kurz (XML súbor):",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(xAxis, 10),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            Button buttonSelectFile = new Button
            {
                Location = new Point(labelSelectFile.Location.X + labelSelectFile.Size.Width + 190, 10),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Nahrať",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            labelLoadedXmlFile = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(buttonSelectFile.Location.X + buttonSelectFile.Size.Width + 20, 15),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            buttonSelectFile.Click += (sender, e) => buttonSelectFileClick(labelLoadedXmlFile);

            mainPanel.Controls.Add(labelSelectFile);
            mainPanel.Controls.Add(buttonSelectFile);
            mainPanel.Controls.Add(labelLoadedXmlFile);

            //
            // Set name
            //
            Label labelSetName = new Label
            {
                Text = "Názov kurzu:",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(xAxis, labelSelectFile.Location.Y + 50),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            nameTextBox = new TextBox
            {
                Location = new Point(labelSetName.Location.X + labelSetName.Width + 65, labelSelectFile.Location.Y + 50 + 4),
                BackColor = mainPanel.BackColor,
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = "",
                PlaceholderText = "nazov",
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            mainPanel.Controls.Add(labelSetName);
            mainPanel.Controls.Add(nameTextBox);

            //
            // Choose language
            //
            Label labelChooseLang = new Label
            {
                Text = "Vyberte jazyk:",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(xAxis, labelSetName.Location.Y + 50),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            radioButtonEng = new RadioButton
            {
                AutoSize = true,
                Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(labelChooseLang.Location.X + labelChooseLang.Size.Width + 70, labelSetName.Location.Y + 54),
                Size = new Size(62, 25),
                Text = "ENG",
                UseVisualStyleBackColor = true
            };

            radioButtonSvk = new RadioButton
            {
                AutoSize = true,
                Font = new Font("Segoe UI Black", 12F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(labelChooseLang.Location.X + labelChooseLang.Size.Width + 70, labelSetName.Location.Y + 54 + 22),
                Size = new Size(62, 25),
                Text = "SVK",
                UseVisualStyleBackColor = true
            };

            mainPanel.Controls.Add(labelChooseLang);
            mainPanel.Controls.Add(radioButtonEng);
            mainPanel.Controls.Add(radioButtonSvk);

            //
            // Choose picture
            //
            Label labelChoosePic = new Label
            {
                Text = "Nahrajte obrázok:",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(xAxis, radioButtonSvk.Location.Y + 50),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            pictureBox = new PictureBox
            {
                Location = new Point(xAxis + 30, labelChoosePic.Location.Y + labelChoosePic.Size.Height + 20),
                Size = new Size(320, 160),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.Fixed3D,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pictureBox.Location = new Point((this.Width - pictureBox.Width) / 2, pictureBox.Location.Y);

            Button buttonLoadPic = new Button
            {
                Location = new Point(labelChoosePic.Location.X + labelChoosePic.Size.Width + 100, labelChoosePic.Location.Y),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Nahrať",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            buttonLoadPic.Click += (sender, e) => buttonLoadPicClick();

            mainPanel.Controls.Add(labelChoosePic);
            mainPanel.Controls.Add(buttonLoadPic);
            mainPanel.Controls.Add(pictureBox);

            //
            // Last button to upload all the data
            //
            Button buttonUploadCourse = new Button
            {
                Location = new Point(xAxis + 30, pictureBox.Location.Y + pictureBox.Size.Height + 50),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = false,
                Size = new Size(300, 40),
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Pridať kurz",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            buttonUploadCourse.Location = new Point((this.Width - buttonUploadCourse.Width) / 2, buttonUploadCourse.Location.Y);

            buttonUploadCourse.Click += (sender, e) => buttonUploadCourseClick();

            mainPanel.Controls.Add(buttonUploadCourse);
        }

        private void buttonEditCourse_Click(object sender, EventArgs e)
        {
            avaibleCourses = MainForm.loadAvaibleCourses();

            if (File.Exists("temp.xml"))
            {
                File.Delete("temp.xml");
            }

            if (File.Exists("temp_image.png"))
            {
                File.Delete("temp_image.png");
            }

            if (File.Exists("temp_image.jpg"))
            {
                File.Delete("temp_image.jpg");
            }

            changePanelContent(sender, buttonEditCourse.Text);

            // load all courses
            Label labelSelect = new Label
            {
                Text = "Vyberte kurz:",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(15, 5),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            ComboBox listOfCourses = new ComboBox
            {
                Width = 200,
                Location = new Point(15 + 5, labelSelect.Size.Height + 15),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            listOfCourses.Items.Add("");
            listOfCourses.Items.AddRange(avaibleCourses);
            listOfCourses.SelectedIndex = 0;
            
            Button buttonLoad = new Button
            {
                Location = new Point(15 + 5, listOfCourses.Size.Height + 60),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Načítaj kurz",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            buttonLoad.Click += (sender, e) => buttonLoadClick();

            contentEditableRTB = new RichTextBox
            {
                Text = "",
                ForeColor = Color.Black,
                BackColor = mainPanel.BackColor,
                Font = new Font("Segoe UI", 10),
                Location = new Point(listOfCourses.Size.Width + 50, 15),
                Width = mainPanel.Width - 280,
                Height = mainPanel.Height - 100,
                ReadOnly = false,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };

            Button buttonUpload = new Button
            {
                Location = new Point(contentEditableRTB.Location.X, contentEditableRTB.Size.Height + 30),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Uložiť zmeny",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            buttonUpload.Click += (sender, e) => buttonUploadClick();

            Button buttonDiscard = new Button
            {
                Location = new Point(buttonUpload.Location.X + buttonUpload.Size.Width + 50, contentEditableRTB.Size.Height + 30),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = true,
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Zahodiť zmeny",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            buttonDiscard.Click += (sender, e) => buttonDiscardClick();

            mainPanel.Controls.Add(labelSelect);
            mainPanel.Controls.Add(listOfCourses);
            mainPanel.Controls.Add(contentEditableRTB);
            mainPanel.Controls.Add(buttonLoad);
            mainPanel.Controls.Add(buttonUpload);
            mainPanel.Controls.Add(buttonDiscard);
        }

        private void buttonDeleteCourse_Click(object sender, EventArgs e)
        {
            avaibleCourses = MainForm.loadAvaibleCourses();

            if (File.Exists("temp.xml"))
            {
                File.Delete("temp.xml");
            }

            if (File.Exists("temp_image.png"))
            {
                File.Delete("temp_image.png");
            }

            if (File.Exists("temp_image.jpg"))
            {
                File.Delete("temp_image.jpg");
            }

            changePanelContent(sender, buttonDeleteCourse.Text);

            Label labelSelect = new Label
            {
                Text = "Vyberte kurz:",
                ForeColor = Color.FromArgb(0, 82, 165),
                Location = new Point(xAxis + 15, this.Height / 4 - 20),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Black", 15, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            ComboBox listOfCourses = new ComboBox
            {
                Width = 200,
                Location = new Point(labelSelect.Location.X + labelSelect.Size.Width + 50, labelSelect.Location.Y + 3),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            listOfCourses.Items.Add("");
            listOfCourses.Items.AddRange(avaibleCourses);
            listOfCourses.SelectedIndex = 0;

            mainPanel.Controls.Add(labelSelect);
            mainPanel.Controls.Add(listOfCourses);

            Button buttonDelete = new Button
            {
                Location = new Point(xAxis + 30, listOfCourses.Location.Y + listOfCourses.Size.Height + 50),
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(178, 255, 102),
                ForeColor = Color.FromArgb(0, 82, 165),
                AutoSize = false,
                Size = new Size(300, 40),
                Font = new Font("Segoe UI Black", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Odstrániť kurz",
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            buttonDelete.Location = new Point((this.Width - buttonDelete.Width) / 2, buttonDelete.Location.Y);
            
            buttonDelete.Click += (sender, e) => buttonDeleteClick();

            mainPanel.Controls.Add(buttonDelete);
        }

        private void buttonLoadClick()
        {
            ComboBox? tempComboBox = mainPanel.Controls.OfType<ComboBox>().FirstOrDefault();

            if (tempComboBox == null)
            {
                MessageBox.Show("Vyskytla sa neočakávaná chyba!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedCourse = tempComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCourse))
            {
                MessageBox.Show("Žiadny kurz nebol vybraný", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string courseToChange = getCourseFromList(selectedCourse);

            if (contentEditableRTB != null && courseToChange != null)
            {
                formatXML(courseToChange, contentEditableRTB);
                currentCourseLoaded = courseToChange;
            }
        }

        private void buttonUploadClick()
        {
            if (contentEditableRTB == null)
            {
                MessageBox.Show("Vyskytla sa neočakávaná chyba!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentCourseLoaded))
            {
                MessageBox.Show("Žiadny kurz nebol vybraný", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning); // NOTE: change all warnings to this type of mbox
                return;
            }

            if (contentEditableRTB.Text == "")
            {
                MessageBox.Show("Textové pole je prázdne!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            File.WriteAllText("temp.xml", contentEditableRTB.Text, Encoding.UTF8);
            if (uploadFileViaFTP("temp.xml", $"{defaultFtpURL}/{currentCourseLoaded}", username, password))
            {
                MessageBox.Show("Úspešne upravený kurz!");
                if (File.Exists("temp.xml"))
                {
                    File.Delete("temp.xml");
                }
            }

            currentCourseLoaded = "";
            buttonEditCourse_Click(null, null);
        }

        private void buttonDiscardClick()
        {
            if (contentEditableRTB == null)
            {
                MessageBox.Show("Vyskytla sa neočakávaná chyba!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentCourseLoaded))
            {
                MessageBox.Show("Žiadny kurz nebol vybraný", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NOTE: set text to empty string than again load from combobox the course that is chosen (basically call func => buttonLoadClick() )
            contentEditableRTB.Text = "";
            buttonLoadClick();
        }

        private void buttonSelectFileClick(Label l)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Vyberte XML súbor";
                ofd.Filter = "XML files (*.xml)|*.xml";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    XDocument doc = XDocument.Load(filePath);
                    string formattedXML = doc.ToString();
                    File.WriteAllText("temp.xml", formattedXML);
                    if (l != null)
                    {
                        l.Text = Path.GetFileName(filePath);
                    }
                }
            }
        }

        private void buttonLoadPicClick()
        {
            if (pictureBox == null)
            {
                MessageBox.Show("Neexistuje miesto pre náhľad obrázka", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Vyberte obrázok";
                ofd.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    
                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                    }

                    pictureBox.Image = Image.FromFile(filePath);
                    File.Copy(filePath, "temp_image" + Path.GetExtension(filePath), true);
                }
            }
        }

        private void buttonUploadCourseClick()
        {
            if (nameTextBox == null || radioButtonEng == null || radioButtonSvk == null || pictureBox == null)
            {
                MessageBox.Show("Vyskytla sa neočakávaná chyba!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (labelLoadedXmlFile.Text == "")
            {
                MessageBox.Show("Prosím nahrajte XML súbor!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nameTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Prosím zadajte názov kurzu!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!radioButtonSvk.Checked && !radioButtonEng.Checked)
            {
                MessageBox.Show("Prosím vyberte jazyk kurzu!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pictureBox.Image == null)
            {
                MessageBox.Show("Prosím nahrajte obrázok!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // create a name of the course
            string language = "";
            if (radioButtonEng.Checked)
            {
                language = "eng";
            }
            else if (radioButtonSvk.Checked)
            {
                language = "svk";
            }
              
            if (language != "")
            {
                string newCourseNameFile = $"{nameTextBox.Text}_{language}.xml";

                // check if the name of the course already exists
                string tempCourse = "";
                Database.DBConnect();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM courses WHERE Course='{newCourseNameFile}'", Database.connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempCourse = reader["Course"].ToString();
                    }
                }
                Database.DBClose();

                if (tempCourse == newCourseNameFile)
                {
                    MessageBox.Show("Takýto kurz už existuje!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // upload xml file and picture
                if (uploadFileViaFTP("temp.xml", $"{defaultFtpURL}/{newCourseNameFile}", username, password))
                {
                    File.Delete("temp.xml");
                }

                if (File.Exists("temp_image.png"))
                {
                    string filename = $"{nameTextBox.Text}_{language}.png";
                    if (uploadFileViaFTP("temp_image.png", $"{defaultFtpURL}/img/{filename}", username, password))
                    {
                        File.Delete("temp_image.png");
                    }
                }
                else if (File.Exists("temp_image.jpg"))
                {
                    string filename = $"{nameTextBox.Text}_{language}.jpg";
                    if (uploadFileViaFTP("temp_image.jpg", $"{defaultFtpURL}/img/{filename}", username, password))
                    {
                        File.Delete("temp_image.jpg");
                    }
                }

                // add to the database
                Database.DBConnect();
                MySqlCommand cmd2 = new MySqlCommand($"INSERT INTO courses(Course) VALUES ('{newCourseNameFile}')", Database.connection);
                cmd2.ExecuteNonQuery();
                Database.DBClose();

                // reset all data
                labelLoadedXmlFile.Text = "";
                nameTextBox.Text = "";
                radioButtonEng.Checked = false;
                radioButtonSvk.Checked = false;
                pictureBox.Image = null;

                MessageBox.Show("Kurz úspešne pridaný!");
            }
        }

        private void buttonDeleteClick()
        {
            ComboBox? tempComboBox = mainPanel.Controls.OfType<ComboBox>().FirstOrDefault();

            if (tempComboBox == null)
            {
                MessageBox.Show("Vyskytla sa neočakávaná chyba!", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedCourse = tempComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedCourse))
            {
                MessageBox.Show("Žiadny kurz nebol vybraný", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string courseToChange = getCourseFromList(selectedCourse);

            if (courseToChange != null)
            {
                deleteFileFromFTP($"{defaultFtpURL}/{courseToChange}", username, password);

                string courseToChangeWithoutExtension = Path.GetFileNameWithoutExtension(courseToChange);
                try
                {
                    deleteFileFromFTP($"{defaultFtpURL}/img/{courseToChangeWithoutExtension}.jpg", username, password);
                }
                catch (Exception e)
                {
                    deleteFileFromFTP($"{defaultFtpURL}/img/{courseToChangeWithoutExtension}.png", username, password);
                }

                Database.DBConnect();
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM courses WHERE Course='{courseToChange}'", Database.connection);
                cmd.ExecuteNonQuery();
                Database.DBClose();

                MessageBox.Show("Kurz úspešne odstránený!");

                buttonDeleteCourse_Click(null, null);
            }
        }

        private bool uploadFileViaFTP(string filePath, string ftpUrl, string username, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = false;

                byte[] fileContents = File.ReadAllBytes(filePath);
                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Chyba pri nahrávaní: {e.Message}");
                return false;
            }
        }

        private void deleteFileFromFTP(string ftpUrl, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(username, password);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return;
            }
        }

        private string getCourseFromList(string course)
        {
            int index = Array.IndexOf(avaibleCourses, course);

            if (index == -1)
            {
                MessageBox.Show("Takýto kurz nebol nájdený", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return avaibleCourses[index];
        }

        private void formatXML(string XMLFile, Control c)
        {
            Course tempCourse = new Course(XMLFile);
            XDocument doc = XDocument.Parse(tempCourse.RawXMLtext);

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = false,
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
                {
                    doc.Save(xmlWriter);
                }

                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    c.Text = reader.ReadToEnd();
                }
            }
        }
    }
}
