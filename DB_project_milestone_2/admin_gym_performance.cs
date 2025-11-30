using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_project_milestone_2
{
    public partial class admin_gym_performance : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private admin_dashboard unhide = null;
        public admin_gym_performance()
        {
            InitializeComponent();
        }
        public admin_gym_performance(admin_dashboard callingform)
        {
            unhide = callingform as admin_dashboard;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    SELECT g.gym_id, g.loc, g.gym_name, SUM(CASE
                        WHEN m.membership_type = 'basic' THEN mp.basic_price
                        WHEN m.membership_type = 'standard' THEN mp.standard_price
                        WHEN m.membership_type = 'premium' THEN mp.premium_price
                        ELSE 0
                    END) AS total_income
                    FROM Gym g
                    LEFT JOIN member_logs m ON g.gym_id = m.gym_id
                    INNER JOIN mbr_price mp ON g.Mbr_price_id = mp.Mbr_price_id
                    GROUP BY g.gym_id, g.loc, g.gym_name;
                ";

                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display results in a DataGridView (optional)
                    dataGridView1.DataSource = dataTable;

                    // Alternatively, process the data further here
                    if (dataTable.Rows.Count > 0)
                    {
                        /*foreach (DataRow row in dataTable.Rows)
                        {
                            int gymId = Convert.ToInt32(row["gym_id"]);
                            string loc = row["loc"].ToString();
                            string gymName = row["gym_name"].ToString();
                            decimal totalIncome = Convert.ToDecimal(row["total_income"]);

                            // Process or display gym income information here
                            MessageBox.Show($"Gym ID: {gymId}, Location: {loc}, Name: {gymName}, Total Income: {totalIncome}");
                        }*/
                    }
                    else
                    {
                        MessageBox.Show("No income data found for any gyms.");
                    }
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                    SELECT g.gym_id, g.loc, g.gym_name,
                           COUNT(DISTINCT m.user_id) AS member_count,
                           COUNT(DISTINCT t.trainer_id) AS trainer_count
                    FROM Gym g
                    LEFT JOIN member_logs m ON g.gym_id = m.gym_id
                    LEFT JOIN trn_gym_rlt t ON g.gym_id = t.gym_id
                    LEFT JOIN trainer_log$ tln ON t.trainer_id = tln.trainer_id  -- Assuming $ is a typo for actual table name
                    GROUP BY g.gym_id, g.loc, g.gym_name;
                ";

                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Display results in a DataGridView (optional)
                    dataGridView1.DataSource = dataTable;

                    // Alternatively, process the data further here
                    if (dataTable.Rows.Count > 0)
                    {
                        /*foreach (DataRow row in dataTable.Rows)
                        {
                            int gymId = Convert.ToInt32(row["gym_id"]);
                            string loc = row["loc"].ToString();
                            string gymName = row["gym_name"].ToString();
                            decimal totalIncome = Convert.ToDecimal(row["total_income"]);

                            // Process or display gym income information here
                            MessageBox.Show($"Gym ID: {gymId}, Location: {loc}, Name: {gymName}, Total Income: {totalIncome}");
                        }*/
                    }
                    else
                    {
                        MessageBox.Show("No income data found for any gyms.");
                    }
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
    }
}
