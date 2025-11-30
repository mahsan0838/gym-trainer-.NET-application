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
    public partial class trainer_registration : Form
    {
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";
        private trainer_login unhide = null;
        public trainer_registration()
        {
            InitializeComponent();
        }
        public trainer_registration(trainer_login callingform)
        {
            InitializeComponent();
            unhide = callingform as trainer_login;
        }

        private void trainer_registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBox3.Text;
                string password = textBox4.Text;
                string f_name = textBox1.Text;
                string l_n = textBox2.Text;
                string name = f_name + l_n;
                string email = textBox5.Text;
                int exp_lvl = int.Parse(textBox7.Text);
                string certificate = textBox6.Text;
                string speciality = textBox8.Text;
                float ovr = float.Parse(textBox9.Text);
                int trainer_id = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string getMaxtrainerIdQuery = "SELECT MAX(trainer_id) FROM trainer_log$";
                    using (SqlCommand getMaxDietIdCmd = new SqlCommand(getMaxtrainerIdQuery, conn))
                    {
                        trainer_id = (int)getMaxDietIdCmd.ExecuteScalar() + 1;
                    }
                    string query = "select count(*) from trainer_log$ where username = @username or password1 = @password";
                    SqlCommand command = new SqlCommand(query, conn);

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    MessageBox.Show(trainer_id.ToString());

                    int recordsFound = (int)command.ExecuteScalar();
                    if (recordsFound >= 1)
                    {
                        MessageBox.Show("This user already exists, please write unique username and password");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("before");
                        string inserting = "insert into trainer_log$ values (@trainerid, @email, @username, @certificate, @password, @ovr, @exp, @speciality)";
                        MessageBox.Show("after 75");
                        SqlCommand c2 = new SqlCommand(inserting, conn);
                        MessageBox.Show("after 77");

                        c2.Parameters.AddWithValue("@username", username);
                        c2.Parameters.AddWithValue("@password", password);
                        c2.Parameters.AddWithValue("@trainerid", trainer_id);
                        c2.Parameters.AddWithValue("@email", email);
                        c2.Parameters.AddWithValue("@certificate", certificate);
                        c2.Parameters.AddWithValue("@ovr", ovr);
                        c2.Parameters.AddWithValue("@exp", exp_lvl);
                        c2.Parameters.AddWithValue("@speciality", speciality);

                        c2.ExecuteNonQuery();
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


            
            MessageBox.Show("Registration Successful!");
            unhide.Show();
            this.Close();
            
        }
    }
}
