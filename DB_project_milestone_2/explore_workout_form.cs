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
    public partial class explore_workout_form : Form
    {
        private workout_plan_form unhide = null;
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        public explore_workout_form()
        {
            InitializeComponent();
            conn.Open();

            //   string objectives = textBox1.Text;
            string query = @"SELECT * FROM workout_plans order by plan_id";

            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand(query, conn);
            //   command.Parameters.AddWithValue("@obj", objectives);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView (optional)
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use
        }
        public explore_workout_form(workout_plan_form callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as workout_plan_form;

            conn.Open();

            //   string objectives = textBox1.Text;
            string query = @"SELECT * FROM workout_plans order by plan_id;";

            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand(query, conn);
            //   command.Parameters.AddWithValue("@obj", objectives);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView (optional)
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use

        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//creator

            conn.Open();

            string creator = comboBox1.SelectedItem?.ToString();
            int memberId = m_id; // Assuming m_id is defined elsewhere

            string query;
            SqlCommand command;

            // Check if the selected option in the combobox is specifically "Own"
            if (creator == "Own")
            {
                // Display only those records that have the Member_id equal to m_id
                query = @"SELECT * FROM workout_plans WHERE Member_id = @memberId;";
                command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@creatorRole", creator);
                command.Parameters.AddWithValue("@memberId", memberId);
            }
            else
            {
                // Display all records where creator_role matches the selected option
                query = @"SELECT * FROM workout_plans WHERE creator_role = @creatorRole;";
                command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@creatorRole", creator);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use



        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();

            string experience = comboBox3.SelectedItem?.ToString();

            string query = @"SELECT * FROM workout_plans WHERE exp_lvl = @exp;";
            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@exp", experience);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView (optional)
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();

            string objectives = textBox1.Text;
            string query = @"SELECT * FROM workout_plans WHERE goal LIKE '%' + @obj + '%'";

            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@obj", objectives);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView (optional)
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();

            string t_id = textBox2.Text;
            string query = @"SELECT * FROM workout_plans WHERE Trainer_id=@tid";

            // Create a new SqlCommand object
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@tid", t_id);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Display results in a DataGridView (optional)
            dataGridView1.DataSource = dataTable;

            conn.Close(); // Close the connection after use
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Assuming conn is declared elsewhere in your class
                conn.Open();

                // Get input values from textboxes
                string planIdText = textBox3.Text;

                // Validate and convert plan ID to integer
                if (!int.TryParse(planIdText, out int planId))
                {
                    MessageBox.Show("Invalid plan ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Define the SQL query to check if the plan ID exists in workout_plans table
                string checkPlanQuery = @"
        SELECT COUNT(*)
        FROM workout_plans
        WHERE plan_id = @planId;
    ";

                // Create a new SqlCommand object with the query to check the plan ID
                SqlCommand checkPlanCommand = new SqlCommand(checkPlanQuery, conn);
                checkPlanCommand.Parameters.AddWithValue("@planId", planId);

                // Execute the query to check if the plan ID exists
                int planCount = (int)checkPlanCommand.ExecuteScalar();

                if (planCount == 0)
                {
                    // Plan ID doesn't exist in workout_plans table, display message and return
                    MessageBox.Show("The entered plan ID doesn't exist.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Define the SQL query to check if the record already exists in member_follows_workout_plan table
                string checkQuery = @"
        SELECT COUNT(*)
        FROM member_follows_workout_plan
        WHERE Member_id = @memberId AND Workout_plan_id = @workoutId;
    ";

                // Create a new SqlCommand object with the query to check
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);

                // Assuming m_id is declared elsewhere in your class
                // Add parameters for the member ID and workout ID
                checkCommand.Parameters.AddWithValue("@memberId", m_id);
                checkCommand.Parameters.AddWithValue("@workoutId", planId);

                // Execute the query to check if the record exists
                int count = (int)checkCommand.ExecuteScalar();

                if (count > 0)
                {
                    // Record already exists, display a message and exit
                    MessageBox.Show("This member is already following the selected workout plan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // If the record doesn't exist and the plan ID exists in workout_plans, proceed with insertion

                // Define the SQL query to insert values into the table
                string insertQuery = @"
        INSERT INTO member_follows_workout_plan (Member_id, Workout_plan_id, perc_progress)
        VALUES (@memberId, @workoutId, @percProgress);
    ";

                // Create a new SqlCommand object with the query to insert
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);

                // Add parameter values to the command
                insertCommand.Parameters.AddWithValue("@memberId", m_id);
                insertCommand.Parameters.AddWithValue("@workoutId", planId);
                insertCommand.Parameters.AddWithValue("@percProgress", 0.0); // Default value for perc_progress

                // Execute the command to insert the values into the table
                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to insert record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }


        }
    }
}
