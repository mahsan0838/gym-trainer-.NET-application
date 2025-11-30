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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_project_milestone_2
{
    public partial class gym_owner_member_request_approval : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private Gym_owner_dashboard unhide = null;
        int curr_owner_id = -1;
        public gym_owner_member_request_approval()
        {
            InitializeComponent();
        }
        public gym_owner_member_request_approval(Gym_owner_dashboard callingform, int curr_owner_id)
        {
            InitializeComponent();
            unhide = callingform as Gym_owner_dashboard;
            this.curr_owner_id = curr_owner_id;
            LoadMemberDataIntoGrid();
        }
        private void LoadMemberDataIntoGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT member_logs.* FROM member_logs " +
                                   "JOIN member_gym_reg_request ON member_gym_reg_request.member_id = member_logs.user_id " +
                                   "JOIN gym ON gym.gym_id=member_gym_reg_request.gym_id " +
                                   "WHERE gym.owner_id = @owner_id;";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void gym_owner_member_request_approval_Load(object sender, EventArgs e)
        {

        }
        //Display Specifice Member data based on the name entered
        private void button1_Click(object sender, EventArgs e)
        {
            string member_name = textBox1.Text;


            if (string.IsNullOrEmpty(member_name))
            {
                MessageBox.Show("Please enter a Member name to search.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        int owner_id_to_check = this.curr_owner_id;

                        string query = "SELECT member_logs.* FROM member_logs " +
                                   "JOIN member_gym_reg_request ON member_gym_reg_request.member_id = member_logs.user_id " +
                                   "JOIN gym ON gym.gym_id=member_gym_reg_request.gym_id " +
                                    "WHERE gym.owner_id = @owner_id AND username LIKE @membername;";

                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@owner_id", owner_id_to_check);
                        command.Parameters.AddWithValue("@membername", "%" + member_name.Trim() + "%"); // Trim whitespaces and add wildcards

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        //search by member age
        private void button2_Click(object sender, EventArgs e)
        {
            string member_age_txt = textBox2.Text;
            int member_age;

            if (string.IsNullOrEmpty(member_age_txt))
            {
                MessageBox.Show("Please enter a Member Age to search.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                if (int.TryParse(member_age_txt, out member_age))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            int owner_id_to_check = this.curr_owner_id;

                            string query = "SELECT member_logs.* FROM member_logs " +
                                           "JOIN member_gym_reg_request ON member_gym_reg_request.member_id = member_logs.user_id " +
                                           "JOIN gym ON gym.gym_id=member_gym_reg_request.gym_id " +
                                           "WHERE gym.owner_id = @owner_id AND age = @memberage;";

                            SqlCommand command = new SqlCommand(query, conn);
                            command.Parameters.AddWithValue("@owner_id", owner_id_to_check);
                            command.Parameters.AddWithValue("@memberage", member_age); // Trim whitespaces and add wildcards

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView1.DataSource = dataTable;

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


            }
        }

        //approve member request
        private void button3_Click(object sender, EventArgs e)
        {

            string entered_member_id_str = textBox3.Text;

            // Check for empty input 
            if (string.IsNullOrWhiteSpace(entered_member_id_str))
            {
                MessageBox.Show("Please enter a trainer ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                int entered_member_id;

                // Attempt to convert trainer ID to integer.
                if (int.TryParse(entered_member_id_str, out entered_member_id))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Check if Member ID exists in member_gym_reg_request table
                            string checkQuery = "SELECT TOP 1 gym_id, membership_type " +
                                                 "FROM member_gym_reg_request " +
                                                 "WHERE member_id = @member_id " +
                                                 "AND gym_id IN (SELECT gym_id FROM gym WHERE owner_id = @owner_id_to_check);";

                            SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                            checkCommand.Parameters.AddWithValue("@member_id", entered_member_id);
                            checkCommand.Parameters.AddWithValue("@owner_id_to_check", this.curr_owner_id);

                            SqlDataReader reader = checkCommand.ExecuteReader(); // Use ExecuteReader for multiple columns

                            if (reader.HasRows)
                            {
                                reader.Read(); // Read the first (and hopefully only) row

                                int extracted_gym_id = reader.GetInt32(0); // Access "gym_id" by index (0)
                                string extracted_membership_type = reader.GetString(1); // Access "membership_type" by index (1)
                                DateTime currentDate = DateTime.Now; // Get current date and time
                                reader.Close();

                                string updateQuery = "UPDATE member_logs " +
                                                     "SET gym_id = @gym_id, membership_type = @membership_type, joining_date = @joining_date " +
                                                     "WHERE user_id = @member_id;";

                                SqlCommand updateCommand = new SqlCommand(updateQuery, conn);
                                updateCommand.Parameters.AddWithValue("@member_id", entered_member_id);
                                updateCommand.Parameters.AddWithValue("@gym_id", extracted_gym_id);
                                updateCommand.Parameters.AddWithValue("@membership_type", extracted_membership_type);
                                updateCommand.Parameters.AddWithValue("@joining_date", currentDate);

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Member record updated in member_logs table. Gym ID: {extracted_gym_id}, Membership Type: {extracted_membership_type}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //delets the entry from trn_gym_reg_request table
                                    string deleteQuery = "DELETE FROM member_gym_reg_request WHERE member_id = @member_id AND gym_id = @extracted_gym_id";
                                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                                    deleteCommand.Parameters.AddWithValue("@member_id", entered_member_id);
                                    deleteCommand.Parameters.AddWithValue("@extracted_gym_id", extracted_gym_id);
                                    deleteCommand.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show("No member record found to update. Consider inserting a new record.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                

                              
                            }
                            else // Trainer ID not found in trn_gym_reg_request
                            {
                                MessageBox.Show("Please Enter a valid  Trainer ID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for trainer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
