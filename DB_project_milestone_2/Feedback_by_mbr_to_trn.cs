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
    public partial class Feedback_by_mbr_to_trn : Form
    {
        private Feedback unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        private int trainer_id = 0;
        public Feedback_by_mbr_to_trn(Feedback callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Feedback;
            InitializeComponent();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select user_id from feedback where trainer_id=@trainerid";

                    SqlCommand command = new SqlCommand(query, conn);
                    // command.Parameters.AddWithValue("@member_id", member_id);

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
        public Feedback_by_mbr_to_trn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int member_id = int.Parse(textBox13.Text);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "select * from feedback where user_id = @member_id and trainer_id=@trainerid";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@member_id", member_id);

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}
