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
    public partial class Gym_reg_form : Form
    {
        private member_dashboard unhide = null;
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-3JR2DDU\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        public Gym_reg_form()
        {
            InitializeComponent();
        }
        public Gym_reg_form(member_dashboard callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as member_dashboard;

            dataGridView1.AutoGenerateColumns = true;

            conn.Open();

            // Query to retrieve exercises data
            string query1 = @"SELECT * FROM gym ORDER BY gym_id;";
            SqlCommand command1 = new SqlCommand(query1, conn);
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);

            dataGridView1.DataSource = dataTable1;

            conn.Close();
        }
        private void Gym_reg_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Convert gymID to integer
            if (int.TryParse(textBox1.Text, out int gymID))
            {
                // Validate gymID
                if (gymID <= 0)
                {
                    MessageBox.Show("Please enter a valid positive gym ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string membership = comboBox1.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(membership))
                {
                    try
                    {
                        conn.Open();

                        // Check if the gymID exists in the gym table
                        string checkGymIdQuery = "SELECT COUNT(*) FROM gym WHERE gym_id = @gymId";
                        SqlCommand checkGymIdCommand = new SqlCommand(checkGymIdQuery, conn);
                        checkGymIdCommand.Parameters.AddWithValue("@gymId", gymID);
                        int gymCount = (int)checkGymIdCommand.ExecuteScalar();

                        if (gymCount == 0)
                        {
                            MessageBox.Show("The specified gym ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method without proceeding further
                        }

                        // Check if the combination of m_id and gymID already exists in member_logs
                        string checkQuery = "SELECT COUNT(*) FROM member_logs WHERE user_id = @userId AND gym_id = @gymId";
                        SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                        checkCommand.Parameters.AddWithValue("@userId", m_id);
                        checkCommand.Parameters.AddWithValue("@gymId", gymID);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("You are already registered in this gym.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method without proceeding further
                        }

                        // Check if the m_id already exists in member_gym_reg_request
                        string checkMemberGymRegQuery = "SELECT COUNT(*) FROM member_gym_reg_request WHERE member_id = @memberId";
                        SqlCommand checkMemberGymRegCommand = new SqlCommand(checkMemberGymRegQuery, conn);
                        checkMemberGymRegCommand.Parameters.AddWithValue("@memberId", m_id);
                        int memberGymRegCount = (int)checkMemberGymRegCommand.ExecuteScalar();

                        if (memberGymRegCount > 0)
                        {
                            MessageBox.Show("You have already sent a request for gym registration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method without proceeding further
                        }

                        // Insert query for member_gym_reg_request table
                        string query = @"INSERT INTO member_gym_reg_request (member_id, gym_id,membership_type ) VALUES (@memberId, @gymId,@mtype)";

                        // Create a SqlCommand object
                        SqlCommand command = new SqlCommand(query, conn);

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@memberId", m_id); // Assuming m_id is the member_id
                        command.Parameters.AddWithValue("@gymId", gymID);
                        command.Parameters.AddWithValue("@mtype", membership);
                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Your request has been sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to send request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select an option from the ComboBox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid gym ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
