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
    public partial class admin_dashboard : Form
    {
        private Admin_login unhide = null;
        public admin_dashboard()
        {
            InitializeComponent();
        }
        public admin_dashboard(Admin_login callingform)
        {
            unhide = callingform as Admin_login;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            registration_request_gym reg_req_gym = new registration_request_gym(this);
            reg_req_gym.ShowDialog();
            reg_req_gym = null;
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_gym_hammer gym_rem = new admin_gym_hammer(this);
            gym_rem.ShowDialog();
            gym_rem = null;
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin_gym_performance adm_gym_prfm = new admin_gym_performance(this);
            adm_gym_prfm.ShowDialog();
            adm_gym_prfm = null;
            this.Show();
        }
    }
}
