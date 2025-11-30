using DB_dashboard_interface_forms;
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
    public partial class member_login : Form
    {
        private general_login_form unhide = null;
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        public member_login(general_login_form callingform)
        {
            InitializeComponent();
            unhide = callingform as general_login_form;


        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            member_registration mem_reg = new member_registration(this);
            mem_reg.ShowDialog();
            mem_reg = null;
            this.Show();

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

                    string query = "SELECT user_id FROM member_logs WHERE username='" + textBox1.Text + "' AND password1='" + textBox2.Text + "'";

                    using (SqlDataAdapter com = new SqlDataAdapter(query, conn))
                    {
                        DataTable dtable = new DataTable();
                        com.Fill(dtable);

                        if (dtable.Rows.Count > 0)
                        {
                            // Assign member_id value when the user successfully logs in
                            m_id = Convert.ToInt32(dtable.Rows[0]["user_id"]);

                            MessageBox.Show("Login Successful. Your User ID is: " + m_id.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            member_dashboard mem_dshbrd = new member_dashboard(this, m_id);
                            mem_dshbrd.ShowDialog();
                            mem_dshbrd = null;
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            com.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred While running SqlAdapter: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void member_login_Load(object sender, EventArgs e)
        {

        }
    }
}
