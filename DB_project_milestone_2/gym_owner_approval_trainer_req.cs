using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DB_project_milestone_2
{
    public partial class gym_owner_approval_trainer_req : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        int curr_owner_id;
        Gym_owner_dashboard unhide = null;
        public gym_owner_approval_trainer_req()
        {
            InitializeComponent();
        }
        public gym_owner_approval_trainer_req(Gym_owner_dashboard callingform, int curr_owner_id)
        {
            unhide = callingform as Gym_owner_dashboard;
            InitializeComponent();
            this.curr_owner_id = curr_owner_id;
            LoadTrainerRequestDataIntoGrid();
        }
        private void LoadTrainerRequestDataIntoGrid()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT trainer_log$.*, gym.* FROM trainer_log$ " +
                                   "JOIN trn_gym_reg_request ON trainer_log$.trainer_id = trn_gym_reg_request.trainer_id " +
                                   "JOIN gym ON gym.gym_id = trn_gym_reg_request.gym_id " +
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
        //this shows the request by specifice trainer's name
        private void button1_Click(object sender, EventArgs e)
        {
            string trainer_name = textBox1.Text;

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

                        // Use parameterized queries to prevent SQL injection
                        string query = "SELECT trainer_log$.*, gym.* FROM trainer_log$ " +
                                       "JOIN trn_gym_reg_request ON trainer_log$.trainer_id = trn_gym_reg_request.trainer_id " +
                                       "JOIN gym ON gym.gym_id = trn_gym_reg_request.gym_id " +
                                       "WHERE gym.owner_id = @owner_id AND username LIKE @trainer_name";  // Use LIKE for name search

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

        //this shows the request by specifice trainer's experience
        private void button2_Click(object sender, EventArgs e)
        {
            string trainer_exp = textBox2.Text;

            // Check for empty trainer experience 
            if (string.IsNullOrWhiteSpace(trainer_exp))
            {
                MessageBox.Show("Please enter a trainer's experience (in years) ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {


                int trn_exp;


                if (int.TryParse(trainer_exp, out trn_exp))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            int owner_id_to_check = this.curr_owner_id;


                            string query = "SELECT trainer_log$.*, gym.* FROM trainer_log$ " +
                                           "JOIN trn_gym_reg_request ON trainer_log$.trainer_id = trn_gym_reg_request.trainer_id " +
                                           "JOIN gym ON gym.gym_id = trn_gym_reg_request.gym_id " +
                                           "WHERE gym.owner_id = @owner_id AND trainer_log$.experience = @trainer_exp";

                            SqlCommand command = new SqlCommand(query, conn);
                            command.Parameters.AddWithValue("@owner_id", owner_id_to_check);
                            command.Parameters.AddWithValue("@trainer_exp", trn_exp);

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
                else
                {

                    MessageBox.Show("Please enter a valid number for trainer experience (in years).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
            //trainer registered to the gym request approval
            //does input vaidation on trainer_id and against that trainer_id checks gym_id if of the current logged in gym_owner or not
            //inserts trainer_id and gym_id into trn_gym_rlt table
            //increments the reg_trainer count in gym table
            //removes the request from trn_gym_reg_request
        private void button3_Click(object sender, EventArgs e)
        {
            string entered_trainer_id_str = textBox3.Text; 

            // Check for empty input 
            if (string.IsNullOrWhiteSpace(entered_trainer_id_str))
            {
                MessageBox.Show("Please enter a trainer ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }

            int entered_trainer_id;

            // Attempt to convert trainer ID to integer.
            if (int.TryParse(entered_trainer_id_str, out entered_trainer_id))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Check if trainer ID exists in trn_gym_reg_request table
                        string checkQuery = "SELECT TOP 1 gym_id " +
                                               "FROM trn_gym_reg_request " +
                                               "WHERE trainer_id = @trainer_id " +
                                               "AND gym_id IN (SELECT gym_id FROM gym WHERE owner_id = @owner_id_to_check)";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                        checkCommand.Parameters.AddWithValue("@trainer_id", entered_trainer_id);
                        checkCommand.Parameters.AddWithValue("@owner_id_to_check", this.curr_owner_id);
                        object result = checkCommand.ExecuteScalar();
                        if (result != null) // Trainer ID exists and gym belongs to the owner 
                        {
                            int extracted_gym_id = (int)result;
                            MessageBox.Show($"Trainer's registration request (first record) is for gym ID: {extracted_gym_id}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Insert trainer and gym ID into trn_gym_rlt 
                            string insertQuery = "INSERT INTO trn_gym_rlt (gym_id, trainer_id) VALUES (@gym_id, @trainer_id)";
                                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                                insertCommand.Parameters.AddWithValue("@gym_id", extracted_gym_id);
                                insertCommand.Parameters.AddWithValue("@trainer_id", entered_trainer_id);

                                int rowsInserted = insertCommand.ExecuteNonQuery();

                                if (rowsInserted > 0) // Insertion successful
                                {
                                    MessageBox.Show("Trainer successfully Registered to the gym.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                //delets the entry from trn_gym_reg_request table
                                string deleteQuery = "DELETE FROM trn_gym_reg_request WHERE trainer_id = @trainer_id AND gym_id = @extracted_gym_id";
                                SqlCommand deleteCommand = new SqlCommand(deleteQuery, conn);
                                deleteCommand.Parameters.AddWithValue("@trainer_id", entered_trainer_id);
                                deleteCommand.Parameters.AddWithValue("@extracted_gym_id", extracted_gym_id);
                                deleteCommand.ExecuteNonQuery();

                                // Update reg_trainers count in gym table for the extracted gym ID 
                                string updateQuery = "UPDATE gym SET reg_trainers = reg_trainers + 1 WHERE gym_id = @gym_id";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, conn);
                                    updateCommand.Parameters.AddWithValue("@gym_id", extracted_gym_id);
                                    updateCommand.ExecuteNonQuery();

                            }
                                else
                                {
                                    MessageBox.Show("An error occurred while inserting data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
