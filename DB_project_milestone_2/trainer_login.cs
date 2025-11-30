using DB_project_milestone_2;
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

namespace Login_Design
{
    public partial class trainer_login : Form
    {
        private int trainer_id = 0;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        public trainer_login()
        {
            InitializeComponent();
        }
        private general_login_form unhide = null;
        public trainer_login(general_login_form callingform)
        {
            InitializeComponent();
            unhide = callingform as general_login_form;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            trainer_registration trainer_reg = new trainer_registration(this);
            trainer_reg.ShowDialog();
            trainer_reg = null;
            this.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {



            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT TOP 1 trainer_id FROM trainer_log$ WHERE username = @username AND password1 = @password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            trainer_id = (int)result;
                            MessageBox.Show("Login Successful. Trainer ID: " + trainer_id.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                    }
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while running SqlCommand: " + ex.Message);
                
            }

            this.Hide();
            Trainer_dashboard trn_dshbrd = new Trainer_dashboard(this, trainer_id);
            trn_dshbrd.ShowDialog();
            trn_dshbrd = null;
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
