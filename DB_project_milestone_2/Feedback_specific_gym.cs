using Login_Design;
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

namespace DB_project_milestone_2
{
    public partial class Feedback_specific_gym : Form
    {
        private Feedback unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public Feedback_specific_gym(Feedback callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Feedback;
            InitializeComponent();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from gym join trn_gym_rlt ON gym.gym_id=trn_gym_rlt.gym_id where trn_gym_rlt.trainer_id=@trainerid";

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
        public Feedback_specific_gym()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            int gym_id = int.Parse(textBox13.Text);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from feedback join member_logs on feedback.user_id=member_logs.user_id where feedback.trainer_id=@trainerid and member_logs.gym_id=@gym_id";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@gym_id", gym_id);

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

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}
