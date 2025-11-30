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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DB_project_milestone_2
{
    public partial class Admin_login : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");
        private general_login_form unhide = null;
        public Admin_login()
        {
            InitializeComponent();
        }
        public Admin_login(general_login_form callingform)
        {
            InitializeComponent();
            unhide = callingform as general_login_form;

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string un = textBox1.Text;
            string pass = textBox2.Text;

            if (string.IsNullOrWhiteSpace(un) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    conn.Open();

                    // Parameterized query for username and password
                    string query = "SELECT * FROM admin_logs WHERE username = @username AND password1 = @password";
                    SqlCommand selectCommand = new SqlCommand(query, conn);
                    selectCommand.Parameters.AddWithValue("@username", un);
                    selectCommand.Parameters.AddWithValue("@password", pass);

                    // Execute the query and handle potential errors
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows) // Check if any rows were returned
                        {
                            MessageBox.Show("Login Successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            admin_dashboard adm_dsh = new admin_dashboard(this);
                            adm_dsh.ShowDialog();
                            adm_dsh = null;
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } // reader is automatically closed here
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Catch other general exceptions
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close(); // Ensure connection is closed even on exceptions
                }
            }


        }
        
    }
}
