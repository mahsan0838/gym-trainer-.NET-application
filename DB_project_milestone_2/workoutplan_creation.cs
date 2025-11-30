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

namespace Login_Design
{
    public partial class workoutplan_creation : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        private Trainer_dashboard unhide = null;
        private int trainer_id = 0;
        public workoutplan_creation(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;



            try
            {
                conn.Open();

                // Query to retrieve exercises data
                string query1 = @"SELECT * FROM exercises";
                SqlCommand command1 = new SqlCommand(query1, conn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);

                // Query to retrieve routine data
                string query2 = @"SELECT * FROM routine";
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
    public workoutplan_creation()
        {
            InitializeComponent();
        }




        private void button1_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string exc_id = textBox1.Text;
            string routine_id = textBox2.Text;
            string experience = textBox3.Text;
            string goal = textBox4.Text;
            string role = "Trainer";
            string mbr_id = "NULL";

            if (string.IsNullOrWhiteSpace(goal) || string.IsNullOrWhiteSpace(exc_id) || string.IsNullOrWhiteSpace(experience) || string.IsNullOrWhiteSpace(routine_id))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    conn.Open();

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

                    // Retrieve the maximum plan_id value from the existing data
                    string getMaxPlanIdQuery = "SELECT MAX(plan_id) FROM workout_plans";
                    SqlCommand getMaxPlanIdCommand = new SqlCommand(getMaxPlanIdQuery, conn);
                    int maxPlanId = Convert.ToInt32(getMaxPlanIdCommand.ExecuteScalar());

                    // Increment the maximum plan_id value by 1 to get the new plan_id
                    int newPlanId = maxPlanId + 1;

                    // Insert the new row into the workout_plans table with the newPlanId
                    string query = "INSERT INTO workout_plans (plan_id, exp_lvl, creator_role, goal, Member_id, Trainer_id) VALUES (@plan_id, @experience, @role, @goal, NULL, @trainerid)";
                    SqlCommand command = new SqlCommand(query, conn);

                   // int memberId = m_id;

                    // Assuming you don't have a trainer ID available
                    //SqlParameter trainerIdParam = new SqlParameter("@trainerId", SqlDbType.Int);
                    //trainerIdParam.Value = DBNull.Value;

                    // Setting parameters
                    command.Parameters.AddWithValue("@experience", exp_lvl);
                    command.Parameters.AddWithValue("@role", role);
                    command.Parameters.AddWithValue("@goal", goal);
                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    command.Parameters.AddWithValue("@plan_id", newPlanId);
                    //command.Parameters.AddWithValue("@trainerId", trn_id); // Assuming trainerId is already converted
                    // Other parameters as needed

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

                    // Check if the combination of exc_id and routine_id already exists in exc_rtn_rlt table
                    string checkExcRtnRltQuery = "SELECT COUNT(*) FROM exc_rtn_rlt WHERE exc_id = @exerciseId AND routine_id = @routineId";
                    SqlCommand checkExcRtnRltCommand = new SqlCommand(checkExcRtnRltQuery, conn);
                    checkExcRtnRltCommand.Parameters.AddWithValue("@exerciseId", exerciseId);
                    checkExcRtnRltCommand.Parameters.AddWithValue("@routineId", routineId);

                    int count = Convert.ToInt32(checkExcRtnRltCommand.ExecuteScalar());

                    // If the combination does not exist, proceed with the insertion into exc_rtn_rlt table
                    if (count == 0)
                    {
                        // Inserting data into exc_rtn_rlt table
                        string insertExcRtnRltQuery = "INSERT INTO exc_rtn_rlt (exc_id, routine_id) VALUES (@exerciseId, @routineId)";
                        SqlCommand insertExcRtnRltCommand = new SqlCommand(insertExcRtnRltQuery, conn);

                        // Setting parameters for exc_rtn_rlt insertion
                        insertExcRtnRltCommand.Parameters.AddWithValue("@exerciseId", exerciseId);
                        insertExcRtnRltCommand.Parameters.AddWithValue("@routineId", routineId);

                        // Executing the query to insert into exc_rtn_rlt
                        int rowsAffectedExcRtnRlt = insertExcRtnRltCommand.ExecuteNonQuery();
                    }

                    // Check if all inserts were successful

                    else
                    {
                        MessageBox.Show("Failed to add workout plan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
