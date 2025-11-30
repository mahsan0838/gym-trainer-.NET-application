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
    public partial class gym_owner_trainer_reports : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R" + "\\" + "SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        int curr_owner_id;
        Gym_owner_dashboard unhide = null;
        public gym_owner_trainer_reports()
        {
            InitializeComponent();
        }
        public gym_owner_trainer_reports(Gym_owner_dashboard callingform, int curr_owner_id)
        {
            unhide = callingform as Gym_owner_dashboard;
            InitializeComponent();
            this.curr_owner_id = curr_owner_id;
            LoadTrainerDataIntoGrid();
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
        private void button2_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }




        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = @"
                                    SELECT t.*, g.gym_id
                                    FROM trainer_log$ t
                                    INNER JOIN trn_gym_rlt rlt ON t.trainer_id = rlt.trainer_id
                                    INNER JOIN gym g ON g.gym_id = rlt.gym_id
                                    WHERE g.owner_id = @owner_id
                                    ORDER BY t.experience;
                                    ";

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

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = @"
                                    SELECT t.*, g.gym_id
                                    FROM trainer_log$ t
                                    INNER JOIN trn_gym_rlt rlt ON t.trainer_id = rlt.trainer_id
                                    INNER JOIN gym g ON g.gym_id = rlt.gym_id
                                    WHERE g.owner_id = @owner_id
                                    ORDER BY t.ovr;
                                    ";

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

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = @"
                                        SELECT t.trainer_id,t.username,t.qualifications,t.ovr,t.speciality_area ,
                                        COUNT(DISTINCT m.user_id) AS NumberOfClients
                                        FROM member_logs m
                                        INNER JOIN sessions s ON m.user_id = s.user_id
                                        INNER JOIN trainer_log$ t ON t.trainer_id = s.trainer_id
                                        WHERE m.gym_id IN (SELECT gym_id FROM gym WHERE gym.owner_id = @owner_id)
                                        GROUP BY t.trainer_id,t.trainer_id,t.username,t.qualifications,t.ovr,t.speciality_area;
                                        ";
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
    }
}
