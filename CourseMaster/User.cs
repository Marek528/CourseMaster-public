using MySqlConnector;

namespace CourseMaster
{
    public class User
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private string emailCheck = "", passCheck = "";

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }

        public void addUser()
        {
            Database.DBConnect();

            MySqlCommand cmd = new MySqlCommand($"INSERT INTO users(Username, Email, Password) VALUES ('{this.Username}', '{this.Email}', '{this.Password}')", Database.connection);
            cmd.ExecuteNonQuery();

            Database.DBClose();
        }

        public bool checkUser()
        {
            emailCheck = "";

            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE Email='{this.Email}'", Database.connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    emailCheck = reader["Email"].ToString();
                }
            }
            Database.DBClose();

            if (emailCheck == this.Email)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool loginUser()
        {
            emailCheck = "";
            passCheck = "";

            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE Email='{this.Email}' AND Password='{this.Password}'", Database.connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    emailCheck = reader["Email"].ToString();
                    passCheck = reader["Password"].ToString();
                }
            }
            Database.DBClose();

            if (emailCheck == this.Email && passCheck == this.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void getUsername()
        {
            string tempUsername = "";
            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE Email='{this.Email}'", Database.connection);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tempUsername = reader["Username"].ToString();
                }
            }
            Database.DBClose();

            if (tempUsername != null || tempUsername != "")
            {
                Username = tempUsername;
            }
            else
            {
                MessageBox.Show($"No username; username: {tempUsername}");
            }
        }

        public bool checkCourses(Course course)
        {
            string tempCourses = "";
            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT Courses FROM users WHERE Email = '{this.Email}'", Database.connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tempCourses = reader["Courses"].ToString();
                }
            }
            Database.DBClose();

            string[] startedCourses = tempCourses.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in startedCourses)
            {
                if (s == course.Link)
                {
                    return true;
                }
            }

            return false;
        }

        public string[] getCourseInfo(string courseLink, string item = "specificProgress")
        {
            string tempProgress = "", tempCourses = "";
            Database.DBConnect();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM users WHERE Email = '{this.Email}'", Database.connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tempProgress = reader["Progress"].ToString();
                    tempCourses = reader["Courses"].ToString();
                }
            }
            Database.DBClose();

            string[] startedCourses = tempCourses.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] progress = tempProgress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int index = -1;

            if (item == "course")
            {
                return startedCourses;
            }
            else if (item == "progress")
            {
                return progress;
            }
            else if (item == "specificProgress")
            {
                for (int i = 0; i < startedCourses.Count(); i++)
                {
                    if (startedCourses[i] == courseLink)
                    {
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    return progress[index].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else
            {
                return null;
            }

            return null;
        }
    }
}
