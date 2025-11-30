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
    public partial class gym_owner_register_gym : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R" + "\\" + "SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        int curr_owner_id = -1;
        private Gym_owner_dashboard unhide = null;
        public gym_owner_register_gym()
        {
            InitializeComponent();
        }
        public gym_owner_register_gym(Gym_owner_dashboard callingform, int owner_id)
        {
            unhide = callingform as Gym_owner_dashboard;
            this.curr_owner_id = owner_id;
            InitializeComponent();
            LoadFacilitiesDataIntoGrid(); // Call method to load data on form load (even with parameters)
            LoadMemberShipPriceDataIntoGrid();
        }

        private void LoadFacilitiesDataIntoGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Replace "SELECT *" with your desired columns from facilities table
                    string query = "SELECT * FROM fascilities";
                    SqlCommand command = new SqlCommand(query, conn);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
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
        private void LoadMemberShipPriceDataIntoGrid()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Replace "SELECT *" with your desired columns from facilities table
                    string query = "SELECT * FROM mbr_price";
                    SqlCommand command = new SqlCommand(query, conn);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView2.DataSource = dataTable;
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
        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gym_name = textBox1.Text;
            string loc = textBox2.Text;
            string business_plans = textBox3.Text;
            int fascility_id;
            int Mbr_price_id;
            if (string.IsNullOrWhiteSpace(gym_name) || string.IsNullOrWhiteSpace(loc) || string.IsNullOrWhiteSpace(business_plans) ||
         string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) ||
         !int.TryParse(textBox4.Text, out fascility_id) || !int.TryParse(textBox5.Text, out Mbr_price_id))
            {
                MessageBox.Show("Please fill in all fields and ensure facility ID and membership price ID are valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            else
            {

                bool exists = false;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "Select COUNT(*) from fascilities WHERE facility_id = @id"; // Replace with your actual table name and column name
                    SqlCommand com = new SqlCommand(query, conn);
                    com.Parameters.AddWithValue("@id", fascility_id);

                    conn.Open();
                    SqlDataReader reader = com.ExecuteReader();

                    if (reader.Read() && reader.GetInt32(0) > 0)
                    {
                        exists = true;
                    }
                    reader.Close();
                }
                if (exists)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "Select COUNT(*) from mbr_price WHERE Mbr_price_id = @id1"; // Replace with your actual table name and column name
                        SqlCommand com = new SqlCommand(query, conn);
                        com.Parameters.AddWithValue("@id1", Mbr_price_id);

                        conn.Open();
                        SqlDataReader reader = com.ExecuteReader();

                        if (reader.Read() && reader.GetInt32(0) > 0)
                        {
                            exists = true;
                        }
                        else
                        {
                            exists = false;
                        }
                        reader.Close();
                    }
                    if (exists)
                    {
                        int request_id = -1;
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conn.Open();

                                string query_1 = "SELECT ISNULL(MAX(request_id), 0) + 1 AS new_request_id FROM request_registration";
                                SqlCommand getIdCommand = new SqlCommand(query_1, conn);
                                request_id = Convert.ToInt32(getIdCommand.ExecuteScalar());
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the function on database error
                            }
                        }
                        if (request_id > -1)
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                try
                                {
                                    conn.Open();

                                    string insertQuery = "INSERT INTO request_registration (request_id, business_plans,loc, facility_id, gym_name, Gym_owner_id, Mbr_price_id) VALUES (@request_id, @business_plan, @location, @facility_id, @gym_name,@Gym_owner_id, @membership_price_id)";
                                    SqlCommand insertCommand = new SqlCommand(insertQuery, conn);

                                    insertCommand.Parameters.AddWithValue("@request_id", request_id);
                                    insertCommand.Parameters.AddWithValue("@business_plan", business_plans);
                                    insertCommand.Parameters.AddWithValue("@location", loc);
                                    insertCommand.Parameters.AddWithValue("@facility_id", fascility_id);
                                    insertCommand.Parameters.AddWithValue("@gym_name", gym_name);
                                    insertCommand.Parameters.AddWithValue("@Gym_owner_id", this.curr_owner_id);
                                    insertCommand.Parameters.AddWithValue("@membership_price_id", Mbr_price_id);

                                    insertCommand.ExecuteNonQuery();

                                    MessageBox.Show("Request Submitted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                   
                                    
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to generate request ID. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("MemberShip Price ID  is Invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Facility ID  is Invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
