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
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DB_project_milestone_2
{
    public partial class remove_trainer_member : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        int curr_owner_id;
        private Gym_owner_dashboard unhide = null;
        public remove_trainer_member()
        {
            InitializeComponent();
        }
        public remove_trainer_member(Gym_owner_dashboard callingform, int curr_owner_id)
        {
            unhide = callingform as Gym_owner_dashboard;
            InitializeComponent();
            this.curr_owner_id = curr_owner_id;
            LoadTrainerDataIntoGrid();
            LoadMemberDataIntoGrid();
        }
        private void LoadTrainerDataIntoGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT trainer_log$.*,gym.gym_id FROM trainer_log$ " +
                                   "JOIN trn_gym_rlt ON trainer_log$.trainer_id = trn_gym_rlt.trainer_id " +
                                   "JOIN gym ON gym.gym_id = trn_gym_rlt.gym_id " +
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
        private void LoadMemberDataIntoGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT member_logs.* FROM member_logs " +
                                   "JOIN gym ON gym.gym_id = member_logs.gym_id " +
                                   "WHERE gym.owner_id = @owner_id;";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView2.DataSource = dataTable;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
        //removing Member from gym
        private void button2_Click(object sender, EventArgs e)
        {
            string memberID_txt = textBox2.Text;
            int memberID;
            if (int.TryParse(memberID_txt, out memberID))
            {
                bool exist = false;
                string checkQuery = "SELECT COUNT(*) FROM member_logs WHERE user_id = @memberID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                    checkCommand.Parameters.AddWithValue("@memberID", memberID);

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show($"Member ID {memberID} does not exist.", "Invalid Member", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        exist = true;
                    }
                }
                if (exist)
                {
                    string updateQuery = "UPDATE member_logs " +
                             "SET joining_date = null, gym_id = null, membership_type = null " +
                             "WHERE user_id = @memberID AND gym_id IN (SELECT gym_id FROM gym WHERE owner_id = @owner_id)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        try
                        {
                            SqlCommand command = new SqlCommand(updateQuery, conn);
                            command.Parameters.AddWithValue("@memberID", memberID);
                            command.Parameters.AddWithValue("@owner_id", this.curr_owner_id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected <= 0)
                            {
                                MessageBox.Show($"Member_ID Doesn't Exist In your GYM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Updated {rowsAffected} records in member_logs table.", "Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    MessageBox.Show($"Member ID {memberID} does not exist.", "Invalid Member", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //display specifce Member name
        private void button5_Click(object sender, EventArgs e)
        {
            string member_name = textBox4.Text;
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
                                       "JOIN gym ON gym.gym_id = member_logs.gym_id " +
                                       "WHERE gym.owner_id = @owner_id AND username LIKE @membername;";

                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@owner_id", owner_id_to_check);
                        command.Parameters.AddWithValue("@membername", "%" + member_name.Trim() + "%"); // Trim whitespaces and add wildcards

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView2.DataSource = dataTable;
                    
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //removing specifce trainer from gym
        private void button1_Click(object sender, EventArgs e)
        {
            string trainerID_txt = textBox1.Text;
            int trainerID;
            if (int.TryParse(trainerID_txt, out trainerID))
            {
                bool exist = false;
                string checkQuery = "SELECT COUNT(*) FROM trainer_log$ WHERE trainer_id = @trainerID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                    checkCommand.Parameters.AddWithValue("@trainerID", trainerID);

                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        exist = false;
                    }
                    else
                    {
                        exist = true;
                    }
                }
                if (exist)
                {
                    string updateQuery = "DELETE FROM trn_gym_rlt WHERE trainer_id = @trainerid AND gym_id IN(SELECT gym_id FROM gym WHERE owner_id = @owner_id); ";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        try
                        {
                            SqlCommand command = new SqlCommand(updateQuery, conn);
                            command.Parameters.AddWithValue("@trainerid", trainerID);
                            command.Parameters.AddWithValue("@owner_id", this.curr_owner_id);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected <= 0)
                            {
                                MessageBox.Show($"Trainer ID Doesn't Exist In your GYM", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Updated {rowsAffected} records in trn_gym_rlt table.", "Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    MessageBox.Show($"Trainer ID {trainerID} does not exist.", "Invalid Trainer", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Trianer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Display specifice Trainer name 
        private void button3_Click(object sender, EventArgs e)
        {
            string trainer_name = textBox3.Text;

            if (string.IsNullOrWhiteSpace(trainer_name))
            {
                MessageBox.Show("Please enter a trainer name to search.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        int owner_id_to_check = this.curr_owner_id;

                       
                        string query = "SELECT trainer_log$.*,gym.gym_id FROM trainer_log$ " +
                                       "JOIN trn_gym_rlt ON trainer_log$.trainer_id = trn_gym_rlt.trainer_id " +
                                       "JOIN gym ON gym.gym_id = trn_gym_rlt.gym_id " +
                                       "WHERE gym.owner_id = @owner_id AND username LIKE @trainer_name";  

                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@owner_id", owner_id_to_check);
                        command.Parameters.AddWithValue("@trainer_name", "%" + trainer_name.Trim() + "%"); // Trim whitespaces and add wildcards

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
}
