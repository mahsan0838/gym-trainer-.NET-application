using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace DB_project_milestone_2
{
    public partial class gym_owner_registration : Form
    {
        string connectionString = "Data Source=LENOVO-ALI\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private gym_owner_login unhide = null;
        public gym_owner_registration()
        {
            InitializeComponent();

        }
        public gym_owner_registration(gym_owner_login callingform)
        {
            unhide= callingform as gym_owner_login;
            InitializeComponent();

        }
        //button1 allows new gym owner to log in to the application
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text.Trim();
            string pass = textBox4.Text.Trim();
            string ema = textBox5.Text.Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(ema))
            {
                MessageBox.Show("Please enter all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Get the maximum owner_id
                        string query = "SELECT ISNULL(MAX(owner_id), 0) + 1 AS new_owner_id FROM gym_owner_log";
                        SqlCommand getIdCommand = new SqlCommand(query, conn);
                        int newOwnerId = Convert.ToInt32(getIdCommand.ExecuteScalar());

                        // Insert into gym_owner_log with the new owner_id
                        string insertQuery = "INSERT INTO gym_owner_log (owner_id, username, password, email) VALUES (@ownerId, @username, @password, @email)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, conn);

                        insertCommand.Parameters.AddWithValue("@ownerId", newOwnerId);
                        insertCommand.Parameters.AddWithValue("@username", username);
                        insertCommand.Parameters.AddWithValue("@password", pass); 
                        insertCommand.Parameters.AddWithValue("@email", ema);

                        insertCommand.ExecuteNonQuery();
                        insertCommand.Dispose();
                        MessageBox.Show($"Registration Successful! Your owner ID is: {newOwnerId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                finally
                {
                    
                }

            }
        }
    }
}
