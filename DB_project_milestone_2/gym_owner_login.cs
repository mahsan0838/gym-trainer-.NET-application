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
    public partial class gym_owner_login : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R" + "\\" + "SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        private general_login_form unhide = null;
        public gym_owner_login()
        {
            InitializeComponent();
        }
        public gym_owner_login(general_login_form callingform)
        {
            InitializeComponent();
            unhide = callingform as general_login_form;
            textBox1.Size = new Size(textBox1.Width, 50);

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            this.Hide();
            gym_owner_registration gym_own_reg = new gym_owner_registration(this);
            gym_own_reg.ShowDialog();
            gym_own_reg = null;
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Gym_owner_dashboard gym_own_dshbrd = new Gym_owner_dashboard(this, 505); // Pass ownerId to dashboard
            //gym_own_dshbrd.ShowDialog();
            //gym_own_dshbrd = null;
            //this.Show();

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

                    // query for username and password
                    string query = "SELECT owner_id FROM gym_owner_log WHERE username = @username AND password = @password";
                    SqlCommand selectCommand = new SqlCommand(query, conn);
                    selectCommand.Parameters.AddWithValue("@username", un);
                    selectCommand.Parameters.AddWithValue("@password", pass);

                    // Execute the query and get the owner_id 
                    int ownerId = -1; 
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read()) 
                            {
                                ownerId = reader.GetInt32(0); // Get the owner_id from the first column (index 0)
                            }
                            MessageBox.Show("Login Successful!");
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } 

                   
                    if (ownerId > -1)
                    {
                        this.Hide();
                        Gym_owner_dashboard gym_own_dshbrd = new Gym_owner_dashboard(this, ownerId); // Pass ownerId to dashboard
                        gym_own_dshbrd.ShowDialog();
                        gym_own_dshbrd = null;
                        this.Show();
                    }
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
