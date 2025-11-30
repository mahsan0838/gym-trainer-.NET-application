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
    public partial class Admin_registration : Form
    {
        private Admin_login unhide = null;
        public Admin_registration()
        {
            InitializeComponent();
        }
        public Admin_registration(Admin_login callingform)
        {
            unhide = callingform as Admin_login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Registration Successful!");
            unhide.Show();
            this.Close();
        }
    }
}
