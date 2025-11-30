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

namespace DB_dashboard_interface_forms
{
    public partial class create_workout_form : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");
        private int m_id;
        private workout_plan_form unhide = null;
        public create_workout_form()
        {
            InitializeComponent();
        }
        public create_workout_form(workout_plan_form callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as workout_plan_form;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;


           
            try
            {
                conn.Open();

                // Query to retrieve exercises data
                string query1 = @"SELECT * FROM exercises order by exc_id";
                SqlCommand command1 = new SqlCommand(query1, conn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);

                // Query to retrieve routine data
                string query2 = @"SELECT * FROM routine order by routine_id ";
                SqlCommand command2 = new SqlCommand(query2, conn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);
                DataTable dataTable2 = new DataTable();
                adapter2.Fill(dataTable2);

                // Display results in DataGridViews
                dataGridView1.DataSource = dataTable1;
                dataGridView2.DataSource = dataTable2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close(); // Close the connection if it's still open
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void create_workout_form_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string goal = textBox1.Text;
                string exc_id = textBox2.Text;
                string routine_id = textBox3.Text;
                string experience = textBox4.Text;
                string role = "Member";
                string trn_id = "NULL";

                if (string.IsNullOrWhiteSpace(goal) || string.IsNullOrWhiteSpace(exc_id) || string.IsNullOrWhiteSpace(experience) || string.IsNullOrWhiteSpace(routine_id))
                {
                    MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert exc_id to integer
                int exerciseId;
                if (!int.TryParse(exc_id, out exerciseId))
                {
                    MessageBox.Show("Invalid exercise ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert routine_id to integer
                int routineId;
                if (!int.TryParse(routine_id, out routineId))
                {
                    MessageBox.Show("Invalid routine ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int exp_lvl;
                if (!int.TryParse(experience, out exp_lvl))
                {
                    MessageBox.Show("Invalid experience", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (exp_lvl < 1 || exp_lvl > 3)
                {
                    MessageBox.Show("Invalid experience level. Please enter a value between 1 and 3.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if exc_id exists in the exercises table
                string checkExcIdQuery = "SELECT COUNT(*) FROM exercises WHERE exc_id = @exerciseId";
                SqlCommand checkExcIdCommand = new SqlCommand(checkExcIdQuery, conn);
                checkExcIdCommand.Parameters.AddWithValue("@exerciseId", exerciseId);
                int excCount = Convert.ToInt32(checkExcIdCommand.ExecuteScalar());

                if (excCount == 0)
                {
                    MessageBox.Show("Invalid exercise ID. The specified exercise ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if routine_id exists in the routine table
                string checkRoutineIdQuery = "SELECT COUNT(*) FROM routine WHERE routine_id = @routineId";
                SqlCommand checkRoutineIdCommand = new SqlCommand(checkRoutineIdQuery, conn);
                checkRoutineIdCommand.Parameters.AddWithValue("@routineId", routineId);
                int routineCount = Convert.ToInt32(checkRoutineIdCommand.ExecuteScalar());

                if (routineCount == 0)
                {
                    MessageBox.Show("Invalid routine ID. The specified routine ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if the combination of exc_id and routine_id already exists in exc_rtn_rlt table
                string checkExcRtnRltQuery = "SELECT COUNT(*) FROM exc_rtn_rlt WHERE exc_id = @exerciseId AND routine_id = @routineId";
                SqlCommand checkExcRtnRltCommand = new SqlCommand(checkExcRtnRltQuery, conn);
                checkExcRtnRltCommand.Parameters.AddWithValue("@exerciseId", exerciseId);
                checkExcRtnRltCommand.Parameters.AddWithValue("@routineId", routineId);

                int count = Convert.ToInt32(checkExcRtnRltCommand.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("The specified combination of exercise ID and routine ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Proceed with inserting data

                // Retrieve the maximum plan_id value from the existing data
                string getMaxPlanIdQuery = "SELECT MAX(plan_id) FROM workout_plans";
                SqlCommand getMaxPlanIdCommand = new SqlCommand(getMaxPlanIdQuery, conn);
                int maxPlanId = Convert.ToInt32(getMaxPlanIdCommand.ExecuteScalar());

                // Increment the maximum plan_id value by 1 to get the new plan_id
                int newPlanId = maxPlanId + 1;

                // Insert the new row into the workout_plans table with the newPlanId
                string query = "INSERT INTO workout_plans (plan_id, exp_lvl, creator_role, goal, Member_id, Trainer_id) VALUES (@plan_id, @experience, @role, @goal, @memberId, NULL)";
                SqlCommand command = new SqlCommand(query, conn);

                int memberId = m_id;

                // Setting parameters
                command.Parameters.AddWithValue("@experience", exp_lvl);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@goal", goal);
                command.Parameters.AddWithValue("@memberId", memberId);
                command.Parameters.AddWithValue("@plan_id", newPlanId);

                // Executing the query
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Workout plan added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to add workout plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Inserting data into exc_wp_rlt table
                string insertExcWpRltQuery = "INSERT INTO exc_wp_rlt (plan_id, exc_id) VALUES (@plan_id, @exerciseId)";
                SqlCommand insertExcWpRltCommand = new SqlCommand(insertExcWpRltQuery, conn);

                // Setting parameters for exc_wp_rlt insertion
                insertExcWpRltCommand.Parameters.AddWithValue("@plan_id", newPlanId);
                insertExcWpRltCommand.Parameters.AddWithValue("@exerciseId", exerciseId);

                // Executing the query to insert into exc_wp_rlt
                int rowsAffectedExcWpRlt = insertExcWpRltCommand.ExecuteNonQuery();

                // Check if all inserts were successful
                if (rowsAffectedExcWpRlt > 0)
                {
                    // Inserting data into exc_rtn_rlt table
                    string insertExcRtnRltQuery = "INSERT INTO exc_rtn_rlt (exc_id, routine_id) VALUES (@exerciseId, @routineId)";
                    SqlCommand insertExcRtnRltCommand = new SqlCommand(insertExcRtnRltQuery, conn);

                    // Setting parameters for exc_rtn_rlt insertion
                    insertExcRtnRltCommand.Parameters.AddWithValue("@exerciseId", exerciseId);
                    insertExcRtnRltCommand.Parameters.AddWithValue("@routineId", routineId);

                    // Executing the query to insert into exc_rtn_rlt
                    int rowsAffectedExcRtnRlt = insertExcRtnRltCommand.ExecuteNonQuery();

                    // Check if insertion into exc_rtn_rlt was successful
                    if (rowsAffectedExcRtnRlt == 0)
                    {
                        // If insertion fails, display error message
                        MessageBox.Show("Failed to add workout plan. Error inserting into exc_rtn_rlt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Display error message if exc_wp_rlt insertion fails
                    MessageBox.Show("Failed to add workout plan. Error inserting into exc_wp_rlt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while adding workout plan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }



        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
