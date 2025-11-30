using DB_project_milestone_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login_Design
{
    public partial class Register_in_gym : Form
    {
        private Trainer_dashboard unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public Register_in_gym(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from gym where gym_id NOT IN (select gym.gym_id from gym join trn_gym_rlt on gym.gym_id=trn_gym_rlt.gym_id join trainer_log$ on trn_gym_rlt.trainer_id=trainer_log$.trainer_id where trainer_log$.trainer_id=@trainerid)";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    //command.Parameters.AddWithValue("@tgt_msl", target_msl);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display results in a DataGridView (optional)
                    dataGridView1.DataSource = dataTable;

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
        public Register_in_gym()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gymid_string = textBox1.Text;

            if (string.IsNullOrWhiteSpace(gymid_string))
            {
                MessageBox.Show("Please enter Gym Id to get registered to a gym.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Parse the gym_id string to an integer
                    int gym_id = int.Parse(gymid_string);
                    //int trainer_id = 1;
                    int count = 0;
                    //MessageBox.Show("" + gym_id.ToString());
                    // Create a SqlConnection object
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Open the connection
                        conn.Open();
                        // checks connection
                        MessageBox.Show("Connection Established");

                        // Check if gym_id exists in the gym table
                        string checkQuery = "select * from gym where gym.gym_id = @gym_id";

                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@gym_id", gym_id);

                            using (SqlDataReader reader = checkCmd.ExecuteReader())  // Use a nested using statement
                            {
                                 count = reader.HasRows ? 1 : 0;  // Check if any rows exist
                            }
                            MessageBox.Show("" + count.ToString());
                            if (count == 0)
                            {
                                MessageBox.Show("The Gym Id does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                             // Prepare the SQL query with a parameterized query to avoid SQL injection
                    string query = "INSERT INTO trn_gym_reg_request VALUES (@trainer_id, @gym_id)";

                        // Create a SqlCommand object with the query and connection
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Add the parameter for gym_id
                            cmd.Parameters.AddWithValue("@trainer_id", trainer_id);

                            cmd.Parameters.AddWithValue("@gym_id", gym_id);

                            // Execute the query
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any rows were affected
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Registration request submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to submit registration request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Gym Id. Please enter a valid integer Gym Id.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while processing the request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
