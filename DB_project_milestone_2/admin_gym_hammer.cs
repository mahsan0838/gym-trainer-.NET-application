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
    public partial class admin_gym_hammer : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private admin_dashboard unhide = null;
        public admin_gym_hammer()
        {

            InitializeComponent();
        }
        public admin_gym_hammer(admin_dashboard callingform)
        {
            unhide = callingform as admin_dashboard;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string location = textBox2.Text;
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please enter location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Parameterized query for location
                        string query = "SELECT * FROM gym WHERE loc = @location";
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@location", location);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("No records found for the specified location.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            int enteredID;
            if (!int.TryParse(textBox3.Text, out enteredID))
            {
                MessageBox.Show("Invalid ID. Please enter a valid ID.");

            }
            else
            {
                //here we find the if the entered is in the table or not
                bool exists = false;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "Select COUNT(*) from gym WHERE gym_id = @id"; // Replace with your actual table name and column name
                    SqlCommand com = new SqlCommand(query, conn);
                    com.Parameters.AddWithValue("@id", enteredID);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read() && reader.GetInt32(0) > 0)
                    {
                        exists = true;
                    }
                    reader.Close();
                }
                if (exists == true)
                {
                    //deleting the foreign keys and setting one table's some attributs to NULL
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            string query = "UPDATE member_logs SET gym_id = NULL, membership_type = NULL, joining_date = NULL WHERE gym_id = @enteredID";
                            SqlCommand command = new SqlCommand(query, conn);
                            command.Parameters.AddWithValue("@enteredID", enteredID);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Records updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //deletes the records if exist in the member_gym_reg_request table
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM member_gym_reg_request WHERE gym_id = @id"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", enteredID);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                    //deletes the records if exist in the trn_gym_reg_request table
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM trn_gym_reg_request WHERE gym_id = @id"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", enteredID);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                    //deletes the records if exist in the trn_gym_rlt table
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM trn_gym_rlt WHERE gym_id = @id"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", enteredID);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }

                    //deletes the record of the requested gym
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM gym WHERE gym_id = @id"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", enteredID);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select * from gym";
                conn.Open();
                SqlDataAdapter com = new SqlDataAdapter(query, conn);
                DataTable dtbl = new DataTable();
                com.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }
    }
}
