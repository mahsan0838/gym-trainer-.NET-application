using Login_Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_project_milestone_2
{
    public partial class member_registration : Form
    {
        member_login unhide = null;
        //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";


        public member_registration()
        {
            InitializeComponent();
        }
        public member_registration(member_login callingform)
        {
            InitializeComponent();
            unhide = callingform as member_login;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text.Trim();
            string pass = textBox4.Text.Trim();
            string ema = textBox5.Text.Trim();
            string age = textBox6.Text.Trim();
            int int_age;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(ema) || string.IsNullOrWhiteSpace(age))
            {
                MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!int.TryParse(age, out int_age))
            {
                MessageBox.Show("Invalid age. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Check if the username or email already exists in the database
                        string checkDuplicateQuery = "SELECT COUNT(*) FROM member_logs WHERE username = @username OR email = @email";
                        SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, conn);
                        checkDuplicateCommand.Parameters.AddWithValue("@username", username);
                        checkDuplicateCommand.Parameters.AddWithValue("@email", ema);
                        int duplicateCount = Convert.ToInt32(checkDuplicateCommand.ExecuteScalar());

                        if (duplicateCount > 0)
                        {
                            MessageBox.Show("Username or email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Get the maximum user_id
                        string getMaxUserIdQuery = "SELECT ISNULL(MAX(user_id), 0) + 1 AS new_user_id FROM member_logs";
                        SqlCommand getIdCommand = new SqlCommand(getMaxUserIdQuery, conn);
                        int newuserId = Convert.ToInt32(getIdCommand.ExecuteScalar());

                        // Insert into member_logs with the new user_id
                        string insertQuery = "INSERT INTO member_logs (username, user_id, password1, gym_id, membership_type, joining_date, email, age) " +
                                             "VALUES (@username, @USERId, @password, NULL, NULL, NULL, @email, @age)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, conn);

                        insertCommand.Parameters.AddWithValue("@USERId", newuserId);
                        insertCommand.Parameters.AddWithValue("@username", username);
                        insertCommand.Parameters.AddWithValue("@password", pass);
                        insertCommand.Parameters.AddWithValue("@email", ema);
                        insertCommand.Parameters.AddWithValue("@age", int_age);

                        insertCommand.ExecuteNonQuery();
                        MessageBox.Show($"Registration Successful! Your user ID is: {newuserId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }

}

