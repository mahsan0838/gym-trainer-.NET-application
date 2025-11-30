using DB_project_milestone_2;
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
    public partial class Feedback : Form
    {
        private Trainer_dashboard unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public Feedback(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();
        }
        public Feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback_by_mbr_to_trn m = new Feedback_by_mbr_to_trn(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Feedback_specific_gym m = new Feedback_specific_gym(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Feedback_comb_rtg m = new Feedback_comb_rtg(this, trainer_id);
            m.ShowDialog();
            m = null;
            this.Show();
        }
    }
}
