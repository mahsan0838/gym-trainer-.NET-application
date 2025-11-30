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

    public partial class workoutplan_report : Form
    {
        private Trainer_dashboard unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public workoutplan_report(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from workout_plans where trainer_id = @trainerid";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
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
        public workoutplan_report()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int client_id = int.Parse(textBox5.Text);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select member_logs.user_id,member_logs.username, trainer_log$.trainer_id,member_follows_workout_plan.perc_progress,workout_plans.* from member_logs join sessions on member_logs.user_id=sessions.user_id join trainer_log$ on trainer_log$.trainer_id=sessions.trainer_id join member_follows_workout_plan on sessions.user_id=member_follows_workout_plan.Member_id join workout_plans on workout_plans.plan_id=member_follows_workout_plan.Workout_plan_id where trainer_log$.trainer_id=@trainerid";
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            int workoutplan_id = int.Parse(textBox1.Text);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from workout_plans join exc_wp_rlt on workout_plans.plan_id=exc_wp_rlt.plan_id join exercises on exc_wp_rlt.exc_id=exercises.exc_id where workout_plans.Trainer_id= @trainerid and workout_plans.plan_id = @planid";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    command.Parameters.AddWithValue("@planid", workoutplan_id);

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string target_msl = textBox2.Text;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from workout_plans join exc_wp_rlt on workout_plans.plan_id=exc_wp_rlt.plan_id join exercises on exc_wp_rlt.exc_id=exercises.exc_id where workout_plans.Trainer_id= @trainerid and muscle_grp=@tgt_msl";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    command.Parameters.AddWithValue("@tgt_msl", target_msl);

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            string day = textBox3.Text;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from workout_plans join exc_wp_rlt on workout_plans.plan_id=exc_wp_rlt.plan_id join exercises on exc_wp_rlt.exc_id=exercises.exc_id where workout_plans.Trainer_id= @trainerid and day1=@day";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    command.Parameters.AddWithValue("@day", day);

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

        private void button4_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}
