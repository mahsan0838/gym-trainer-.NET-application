using DB_dashboard_interface_forms;
using Login_Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;

namespace DB_project_milestone_2
{
    public partial class reportform1 : Form
    {
        private general_login_form unhide = null;
        //  SqlConnection conn = new SqlConnection("Data Source=DESKTOP-3JR2DDU\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        public reportform1()
        {
            InitializeComponent();
            InitializeLabelTooltips();
        }
        public reportform1(general_login_form callingform)
        {
            InitializeComponent();
            InitializeLabelTooltips();
            unhide = callingform as general_login_form;
        }
        private void InitializeLabelTooltips()
        {
            // Associate labels with tooltips
            toolTip1.SetToolTip(label2, "Details of members of one specific gym that get training from 1 specific trainer.");
            toolTip1.SetToolTip(label3, "Details of members from one specific gym that follow a specific diet plan.");
            toolTip1.SetToolTip(label4, "Details of members across all gyms of a specific trainer that follow a specific diet plan.");
            toolTip1.SetToolTip(label5, "Count of members who will be using specific machines on a given day in a specific gym.");
            toolTip1.SetToolTip(label6, "List of Diet plans that have less than 500 calorie meals as breakfast.");
            toolTip1.SetToolTip(label7, "List of diet plans in which total carbohydrate intake is less than 300 grams.");
            toolTip1.SetToolTip(label8, "List of workout plans that don’t require using a specific machine.");
            toolTip1.SetToolTip(label9, "List of diet plans which doesn’t have peanuts as allergens.");
            toolTip1.SetToolTip(label10, "New membership data in last 3 months (Gym Owner).");
            toolTip1.SetToolTip(label11, "Comparison of total members in multiple gyms, in the past 6 months.");
            toolTip1.SetToolTip(label12, "workoutplans followed by a specific member");
            toolTip1.SetToolTip(label13, "List of workout plans that don't require any machine");
            toolTip1.SetToolTip(label14, "Clients of each trainer");
            toolTip1.SetToolTip(label15, "Members Who don’t follow any workout plan");
            toolTip1.SetToolTip(label16, "Members who don’t follow any diet plan");
            toolTip1.SetToolTip(label17, "gym owner of specific gym");
            toolTip1.SetToolTip(label18, "show client of specific trainer");
            toolTip1.SetToolTip(label19, "Trainers who are not in any gym");
            toolTip1.SetToolTip(label20, "Members who are not in any gym");
            toolTip1.SetToolTip(label21, "Members who are not in any session");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string gymid=textBox1.Text.Trim();
        //    string trainerid=textBox2.Text.Trim();

        //}

        private void button2_Click(object sender, EventArgs e)
        {
            //  string connectionString = "Data Source=DESKTOP-3JR2DDU\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
            string gymId = textBox1.Text.Trim();
            string trainerId = textBox2.Text.Trim();

            string query = @"
        SELECT member_logs.*
        FROM member_logs
        JOIN sessions ON member_logs.user_id = sessions.user_id
        JOIN trainer_log$ ON trainer_log$.trainer_id = sessions.trainer_id
        WHERE trainer_log$.trainer_id = @trainerId AND member_logs.gym_id = @gymId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@trainerId", trainerId);
                        command.Parameters.AddWithValue("@gymId", gymId);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gymId = textBox3.Text.Trim();
            string dietId = textBox4.Text.Trim();

            string query = @"
        SELECT member_logs.*
        FROM member_logs
        JOIN member_follow_diet_plan ON member_follow_diet_plan.Member_id = member_logs.user_id
        WHERE member_logs.gym_id = @gymId AND member_follow_diet_plan.diet_plan_id = @dietId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gymId", gymId);
                        command.Parameters.AddWithValue("@dietId", dietId);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //    string connectionString = "Data Source=DESKTOP-3JR2DDU\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True";
            string trainerId = textBox5.Text.Trim();
            string dietId = textBox6.Text.Trim();

            string query = @"
        SELECT member_logs.*
        FROM member_logs
        JOIN sessions ON member_logs.user_id = sessions.user_id
        JOIN trainer_log$ ON trainer_log$.trainer_id = sessions.trainer_id
        JOIN member_follow_diet_plan ON member_logs.user_id = member_follow_diet_plan.Member_id
        WHERE trainer_log$.trainer_id = @trainerId AND member_follow_diet_plan.diet_plan_id = @dietId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@trainerId", trainerId);
                        command.Parameters.AddWithValue("@dietId", dietId);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }




        //private void button4_Click(object sender, EventArgs e)
        //{
        //    string gymid = textBox7.Text;
        //    string mname= textBox8.Text;
        //    string day = comboBox1.SelectedValue?.ToString();


        //}
        private void button4_Click(object sender, EventArgs e)
        {
            //   string connectionString = "Data Source=DESKTOP-3JR2DDU\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True";
            string gymId = textBox7.Text.Trim();
            string machineName = textBox8.Text.Trim();
            string day = textBox11.Text.Trim();

            string query = @"
        SELECT COUNT(DISTINCT member_logs.user_id) AS MemberCount
        FROM member_logs
        JOIN member_follows_workout_plan ON member_logs.user_id = member_follows_workout_plan.Member_id
        JOIN exc_wp_rlt ON member_follows_workout_plan.Workout_plan_id = exc_wp_rlt.plan_id
        JOIN exercises ON exercises.exc_id = exc_wp_rlt.exc_id
        WHERE exercises.day1 = @day AND exercises.machine = @machine AND member_logs.gym_id = @gymId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@day", day);
                        command.Parameters.AddWithValue("@machine", machineName);
                        command.Parameters.AddWithValue("@gymId", gymId);

                        connection.Open();

                        // ExecuteScalar returns the first column of the first row in the result set
                        object result = command.ExecuteScalar();

                        // Check if the result is not null and can be converted to an int
                        if (result != null && int.TryParse(result.ToString(), out int memberCount))
                        {
                            // Update the DataGridView with the member count
                            dataGridView1.DataSource = new[] { new { MemberCount = memberCount } };
                        }
                        else
                        {
                            MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT diet_plan.*, meals.meal_id
        FROM diet_plan
        JOIN DP_MLS_RLT ON diet_plan.diet_plan_id = DP_MLS_RLT.diet_plan_id
        JOIN meals ON DP_MLS_RLT.meal_id = meals.meal_id
        WHERE meals.type_of_meal = 'Breakfast' AND meals.calories < 500
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT diet_plan.*, rnt_tbl.total_carbs
        FROM diet_plan
        JOIN (
            SELECT diet_plan.diet_plan_id, SUM(meals.carbs) AS total_carbs
            FROM diet_plan
            JOIN DP_MLS_RLT ON diet_plan.diet_plan_id = DP_MLS_RLT.diet_plan_id
            JOIN meals ON DP_MLS_RLT.meal_id = meals.meal_id
            GROUP BY diet_plan.diet_plan_id
        ) AS rnt_tbl ON diet_plan.diet_plan_id = rnt_tbl.diet_plan_id
        WHERE rnt_tbl.total_carbs < 300
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string machineName = textBox9.Text;

            string query = @"
        SELECT DISTINCT workout_plans.plan_id, workout_plans.exp_lvl, workout_plans.creator_role, 
                        workout_plans.goal, workout_plans.Member_id, workout_plans.Trainer_id
        FROM workout_plans
        JOIN exc_wp_rlt ON workout_plans.plan_id = exc_wp_rlt.plan_id
        JOIN exercises ON exercises.exc_id = exc_wp_rlt.exc_id
        WHERE exercises.machine != @machineName
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@machineName", machineName);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT diet_plan.*
        FROM diet_plan
        WHERE diet_plan.diet_plan_id NOT IN (
            SELECT diet_plan.diet_plan_id
            FROM diet_plan
            JOIN DP_MLS_RLT ON diet_plan.diet_plan_id = DP_MLS_RLT.diet_plan_id
            JOIN meals ON DP_MLS_RLT.meal_id = meals.meal_id
            WHERE meals.allergens = 'Peanuts'
        )
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string gymOwnerIdText = textBox10.Text.Trim();
            int gymOwnerId;

            if (!int.TryParse(gymOwnerIdText, out gymOwnerId))
            {
                MessageBox.Show("Invalid gym owner ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string query = @"
        SELECT member_logs.*
        FROM member_logs
        JOIN member_trigger_log ON member_logs.user_id = member_trigger_log.member_id
        JOIN gym ON gym.gym_id = member_trigger_log.gym_id
        WHERE gym.owner_id = @gymOwnerId
        AND member_trigger_log.member_join_app_data >= DATEADD(MONTH, -3, GETDATE())
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gymOwnerId", gymOwnerId);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT member_trigger_log.gym_id, COUNT(member_trigger_log.member_id) AS No_of_members
        FROM member_trigger_log
        WHERE member_trigger_log.member_join_app_data >= DATEADD(MONTH, -6, GETDATE())
        GROUP BY member_trigger_log.gym_id
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string t_id = textBox12.Text;

            string tIdText = textBox12.Text.Trim();

            // Parse the trainer ID input to an integer
            if (!int.TryParse(tIdText, out int tId))
            {
                MessageBox.Show("Invalid trainer ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQL query to retrieve clients of a specific trainer
            string query = @"
        SELECT member_logs.*
        FROM member_logs
        JOIN sessions ON member_logs.user_id = sessions.user_id
        JOIN trainer_log$ ON trainer_log$.trainer_id = sessions.trainer_id
        WHERE trainer_log$.trainer_id = @TrainerId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add trainer ID parameter
                        command.Parameters.AddWithValue("@TrainerId", tId);

                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string gidText = textBox13.Text.Trim();

            // Parse the gym ID input to an integer
            if (!int.TryParse(gidText, out int gid))
            {
                MessageBox.Show("Invalid gym ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQL query to retrieve gym owners of a specific gym
            string query = @"
        SELECT gym_owner_log.*
        FROM gym_owner_log
        JOIN gym ON gym_owner_log.owner_id = gym.owner_id
        WHERE gym.gym_id = @GymId
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add gym ID parameter
                        command.Parameters.AddWithValue("@GymId", gid);

                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            string userIDText = textBox14.Text.Trim();

            // Parse the user ID input to an integer
            if (!int.TryParse(userIDText, out int userID))
            {
                MessageBox.Show("Invalid user ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // SQL query to retrieve workout plans followed by a specific member
            string query = @"
        SELECT workout_plans.*
        FROM workout_plans
        JOIN member_follows_workout_plan ON workout_plans.plan_id = member_follows_workout_plan.Workout_plan_id
        JOIN member_logs ON member_logs.user_id = member_follows_workout_plan.Member_id
        WHERE member_logs.user_id = @UserID
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add user ID parameter
                        command.Parameters.AddWithValue("@UserID", userID);

                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            // SQL query to retrieve workout plans that don’t require using a specific machine
            string query = @"
        SELECT workout_plans.*
        FROM workout_plans
        WHERE workout_plans.plan_id IN (
            SELECT rnt_wp.plan_id
            FROM (
                SELECT workout_plans.plan_id, COUNT(workout_plans.plan_id) AS number_exercises
                FROM workout_plans
                JOIN exc_wp_rlt ON workout_plans.plan_id = exc_wp_rlt.plan_id
                GROUP BY workout_plans.plan_id
            ) AS rnt_wp
            JOIN (
                SELECT workout_plans.plan_id, COUNT(workout_plans.plan_id) AS number_exercises
                FROM workout_plans
                JOIN exc_wp_rlt ON workout_plans.plan_id = exc_wp_rlt.plan_id
                JOIN exercises ON exercises.exc_id = exc_wp_rlt.exc_id
                WHERE exercises.machine IS NULL
                GROUP BY workout_plans.plan_id
            ) AS rnt_wp_1 ON rnt_wp.plan_id = rnt_wp_1.plan_id
            WHERE rnt_wp.number_exercises = rnt_wp_1.number_exercises
        )
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT trainer_log$.trainer_id, COUNT(sessions.user_id) AS No_of_clients
        FROM trainer_log$
        JOIN sessions ON trainer_log$.trainer_id = sessions.trainer_id
        GROUP BY trainer_log$.trainer_id
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT member_logs.*
        FROM member_logs
        WHERE member_logs.user_id NOT IN (
            SELECT member_follows_workout_plan.Member_id
            FROM member_follows_workout_plan
        )
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT member_logs.*
        FROM member_logs
        WHERE member_logs.user_id NOT IN (
            SELECT member_follow_diet_plan.Member_id
            FROM member_follow_diet_plan
        )
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // SQL query to select trainers who are not associated with any gym
            string query = @"
        SELECT trainer_log$.*
        FROM trainer_log$
        WHERE trainer_log$.trainer_id NOT IN (
            SELECT trn_gym_rlt.trainer_id
            FROM trn_gym_rlt
        )
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // SQL query to select members who don't have a gym ID
            string query = @"
        SELECT member_logs.*
        FROM member_logs
        WHERE member_logs.gym_id IS NULL AND member_logs.membership_type is null
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT member_logs.*
        FROM member_logs
        WHERE member_logs.user_id NOT IN (SELECT sessions.user_id FROM sessions)
    ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        // Execute the query and fill the results into a DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the dataGridView1
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

