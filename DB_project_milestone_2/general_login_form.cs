using DB_dashboard_interface_forms;
using Login_Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_project_milestone_2
{
    public partial class general_login_form : Form
    {
        public general_login_form()
        {
            InitializeComponent();


        }



        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            trainer_login trainer_lg = new trainer_login(this);
            trainer_lg.ShowDialog();
            trainer_lg = null;
            this.Show();

        }


        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            member_login loginForm = new member_login(this);
            loginForm.ShowDialog();
            //whenever clicked on cross button form2 back to form1
            loginForm = null;
            this.Show();


        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            gym_owner_login gym_lg = new gym_owner_login(this);
            gym_lg.ShowDialog();
            gym_lg = null;
            this.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_login admin_lg = new Admin_login(this);
            admin_lg.ShowDialog();
            admin_lg = null;
            this.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            reportform1 rep1 = new reportform1(this);
            rep1.ShowDialog();
            rep1 = null;
            this.Show();
        }
    }
}
