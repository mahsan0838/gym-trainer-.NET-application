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
    public partial class Dietplan_report : Form
    {
        private Trainer_dashboard unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public Dietplan_report(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select member_logs.user_id, member_logs.username from member_logs join sessions on member_logs.user_id=sessions.user_id join trainer_log$ on sessions.trainer_id=trainer_log$.trainer_id where trainer_log$.trainer_id=@trainerid";

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
        public Dietplan_report()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int diet_id = int.Parse(textBox2.Text);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from diet_plan join DP_MLS_RLT ON diet_plan.diet_plan_id=dp_mls_rlt.diet_plan_id join meals on meals.meal_id=DP_MLS_RLT.meal_id where trainer_id=@trainerid";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                   // command.Parameters.AddWithValue("@diet_id", diet_id);

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

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string allergens = textBox3.Text;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from diet_plan join DP_MLS_RLT ON diet_plan.diet_plan_id=dp_mls_rlt.diet_plan_id join meals on meals.meal_id=DP_MLS_RLT.meal_id where trainer_id=@trainerid and meals.allergens=@allergens";

                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@trainerid", trainer_id);
                    command.Parameters.AddWithValue("@allergens", allergens);

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

        private void button10_Click_1(object sender, EventArgs e)
        {
            //int client_id = int.Parse(textBox5.Text);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select member_logs.user_id,member_logs.username, trainer_log$.trainer_id,member_follow_diet_plan.perc_progress,diet_plan.* from member_logs join sessions on member_logs.user_id=sessions.user_id join trainer_log$ on trainer_log$.trainer_id=sessions.trainer_id join member_follow_diet_plan on sessions.user_id=member_follow_diet_plan.Member_id join diet_plan on diet_plan.diet_plan_id=member_follow_diet_plan.diet_plan_id where trainer_log$.trainer_id=2";
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

        private void button7_Click_1(object sender, EventArgs e)
        {

            unhide.Show();
            this.Close();
        }
    }
}
