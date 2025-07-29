using MySqlConnector;

namespace CourseMaster
{
    public static class Database
    {
        public static MySqlConnection connection = new MySqlConnection("Server=;User ID=;Database=;Password=;Port=;AllowUserVariables=True;");

        public static void DBConnect()
        {
            try
            {
                connection.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public static void DBClose()
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
    }
}