using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Design
{
    public partial class Trainer_dashboard : Form
    {
        private trainer_login unhide = null;
        private int trainer_id = 0;
        public Trainer_dashboard()
        {
            InitializeComponent();
        }
        public Trainer_dashboard(trainer_login callingform, int id)
        {
            unhide = callingform as trainer_login;
            trainer_id = id;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            workoutplan_creation m = new workoutplan_creation(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dietplan_creation m = new Dietplan_creation(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void Trainer_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register_in_gym m = new Register_in_gym(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointment m = new Appointment(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback m = new Feedback(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            workoutplan_report m = new workoutplan_report(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dietplan_report m = new Dietplan_report(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}
