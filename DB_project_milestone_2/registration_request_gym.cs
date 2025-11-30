using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_project_milestone_2
{
    public partial class registration_request_gym : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R"+"\\"+"SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        private admin_dashboard unhide = null;

        public registration_request_gym()
        {
            InitializeComponent();
        }
        public registration_request_gym(admin_dashboard callingform)
        {
            unhide = callingform as admin_dashboard;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
        //button 4 basically displays the request_registration table
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select * from request_registration";
                conn.Open();
                SqlDataAdapter com = new SqlDataAdapter(query, conn);
                DataTable dtbl = new DataTable();
                com.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
        }
        //button2 is for sorting record based on their location
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
                        string query = "SELECT * FROM request_registration WHERE loc = @location";
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

        //button1 is for accepting the request id
        //inserting the relevant records into gym table
        //and deleting the record from registration request table

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
                    string query = "Select COUNT(*) from request_registration WHERE request_id = @id"; // Replace with your actual table name and column name
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
                    //here find the next max gym_id for the new gym record to be inserted
                    int maxId = 1;
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT MAX(gym_id) AS maxId FROM gym"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        conn.Open();
                        // Execute the query and get the single result (max ID)
                        object result = command.ExecuteScalar();
                        // Check if a value was returned (no records might exist)
                        if (result != null)
                        {
                            // Convert the result to integer (assuming the column is an integer type)
                             maxId = Convert.ToInt32(result);
                            // Now you can use the maxId variable
                            maxId++;

                        }
                        else
                        {
                             maxId = 1;  // Handle no records scenario
                        }
                    }
                    //here we extract all the necessary details from request tables
                    //so that they can inserted as a new record in gym table
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        // Extract data from requests_registration
                        string selectQuery = "SELECT * FROM request_registration WHERE request_id = @enteredID";
                        SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
                        selectCommand.Parameters.AddWithValue("@enteredID", enteredID); //this is for security risk basically binds the enteredID value

                        SqlDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read()) // Check if a record exists with entered ID
                        {
                           
                            string loc = reader.GetString(2);
                            int facilityId = reader.GetInt32(3);
                            string gymName = reader.GetString(4);
                            int gymOwnerId = reader.GetInt32(5);
                            int mbrPriceId = reader.GetInt32(6);
                            reader.Close();
                            string message = $"Extracted Data:\n" +
                                      $"Location: {loc}\n" +
                                      $"Facility ID: {facilityId}\n" +
                                      $"Owner Name: {gymName}\n" +
                                      $"Gym Owner ID: {gymOwnerId}\n"+
                                      $"Membership Price ID: {mbrPriceId}\n" ;
                            int reg_trainer = 0;
                            MessageBox.Show(message);
                            // Insert data into Gym table
                            string insertQuery = "INSERT INTO Gym (gym_id,loc, gym_name, reg_trainers, owner_id, facility_id, Mbr_price_id) VALUES (@maxid,@loc, @gymName, @regtrainers, @gymOwnerId, @facilityId, @mbrPriceId)"; // Assuming reg_trainers is NULL
                            SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                            insertCommand.Parameters.AddWithValue("@maxid", maxId);
                            insertCommand.Parameters.AddWithValue("@loc", loc);
                            insertCommand.Parameters.AddWithValue("@gymName",gymName);
                            insertCommand.Parameters.AddWithValue("@regtrainers", reg_trainer);
                            insertCommand.Parameters.AddWithValue("@gymOwnerId", gymOwnerId);
                            insertCommand.Parameters.AddWithValue("@facilityId", facilityId);
                            insertCommand.Parameters.AddWithValue("@mbrPriceId", mbrPriceId);

                            insertCommand.ExecuteNonQuery();

                            MessageBox.Show("Data transferred successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the entered ID.");
                            reader.Close();
                        }
                        
                    }
                        //delets the gym registration request from request table of admin
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM request_registration WHERE request_id = @id"; // Replace with your actual table name and column name
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@id", enteredID);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Doesn't Exist. Please enter a valid ID.");
                }
                
            }
           
        }
    }
}
