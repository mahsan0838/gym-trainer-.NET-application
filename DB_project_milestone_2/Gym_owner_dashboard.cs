using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_project_milestone_2
{
    public partial class Gym_owner_dashboard : Form
    {
        string  connectionString = "Data Source=DESKTOP-8H6V81R" + "\\" + "SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private gym_owner_login unhide = null;
        int curr_owner_id = -1;
        public Gym_owner_dashboard()
        {
            InitializeComponent();
        }
        public Gym_owner_dashboard(gym_owner_login callingform, int curr_owner_id)
        {
            unhide = callingform as gym_owner_login;
            InitializeComponent();
            this.curr_owner_id = curr_owner_id;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
        //kick out form
        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //remove_trainer_member rem_trn_mem = new remove_trainer_member(this,this.curr_owner_id);
            //rem_trn_mem.ShowDialog();
            //rem_trn_mem = null;
            //this.Show();
            bool exist = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT COUNT(*) FROM gym WHERE owner_id = @owner_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        exist = true;
                    }
                    else
                    {
                        MessageBox.Show("You don't have any Registered GYMs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            if (exist)
            {
                this.Hide();
                remove_trainer_member rem_trn_mem = new remove_trainer_member(this, this.curr_owner_id);
                rem_trn_mem.ShowDialog();
                rem_trn_mem = null;
                this.Show();
            }
        }
        //trainer_approver_req
        private void button3_Click(object sender, EventArgs e)
        {
                //this.Hide();
                //gym_owner_approval_trainer_req own_apr_trn_rq = new gym_owner_approval_trainer_req(this,this.curr_owner_id);
                //own_apr_trn_rq.ShowDialog();
                //own_apr_trn_rq = null;
                //this.Show();

                bool exist = false;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        int owner_id_to_check = this.curr_owner_id;

                        string query = "SELECT COUNT(*) FROM gym WHERE owner_id = @owner_id";
                        SqlCommand command = new SqlCommand(query, conn);
                        command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            exist = true;
                        }
                        else
                        {
                            MessageBox.Show("You don't have any Registered GYMs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (exist)
                {
                     this.Hide();
                     gym_owner_approval_trainer_req own_apr_trn_rq = new gym_owner_approval_trainer_req(this, this.curr_owner_id);
                     own_apr_trn_rq.ShowDialog();
                     own_apr_trn_rq = null;
                     this.Show();
                }
        }
        //register your gym
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            gym_owner_register_gym own_reg_gym = new gym_owner_register_gym(this, this.curr_owner_id);
            own_reg_gym.ShowDialog();
            own_reg_gym = null;
            this.Show();
        }
        //trainer_reports
        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //gym_owner_trainer_reports own_trn_rep = new gym_owner_trainer_reports(this,this.curr_owner_id);
            //own_trn_rep.ShowDialog();
            //own_trn_rep = null;
            //this.Show();
            bool exist = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT COUNT(*) FROM gym WHERE owner_id = @owner_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        exist = true;
                    }
                    else
                    {
                        MessageBox.Show("You don't have any Registered GYMs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (exist)
            {
                this.Hide();
                gym_owner_trainer_reports own_trn_rep = new gym_owner_trainer_reports(this, this.curr_owner_id);
                own_trn_rep.ShowDialog();
                own_trn_rep = null;
                this.Show();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //gym_owner_member_request_approval owner_apr_member_req = new gym_owner_member_request_approval(this, this.curr_owner_id);
            //owner_apr_member_req.ShowDialog();
            //owner_apr_member_req = null;
            //this.Show();
            bool exist = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    int owner_id_to_check = this.curr_owner_id;

                    string query = "SELECT COUNT(*) FROM gym WHERE owner_id = @owner_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@owner_id", owner_id_to_check);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        exist = true;
                    }
                    else
                    {
                        MessageBox.Show("You don't have any Registered GYMs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (exist)
            {
                this.Hide();
                gym_owner_member_request_approval owner_apr_member_req = new gym_owner_member_request_approval(this, this.curr_owner_id);
                owner_apr_member_req.ShowDialog();
                owner_apr_member_req = null;
                this.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
