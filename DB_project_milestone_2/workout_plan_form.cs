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

namespace DB_dashboard_interface_forms
{
    public partial class workout_plan_form : Form
    {
        private member_dashboard unhide = null;
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        public workout_plan_form()
        {
            InitializeComponent();
        }

        public workout_plan_form(member_dashboard callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as member_dashboard;

            dataGridView1.AutoGenerateColumns = true;
            conn.Open();

            // Query to retrieve exercises data
            string query1 = @"
                SELECT * 
                FROM member_follows_workout_plan AS MFWP
                JOIN workout_plans AS WP ON WP.plan_id = MFWP.Workout_plan_id
                WHERE MFWP.Member_id = @memberId order by WP.plan_id;";
            SqlCommand command1 = new SqlCommand(query1, conn);
            command1.Parameters.AddWithValue("@memberId", m_id);
            SqlDataAdapter adapter1 = new SqlDataAdapter(command1);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            dataGridView1.DataSource = dataTable1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            create_workout_form create_Workout = new create_workout_form(this, m_id);
            create_Workout.ShowDialog();
            create_Workout = null;
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            explore_workout_form explore_Workout = new explore_workout_form(this, m_id);
            explore_Workout.ShowDialog();
            explore_Workout = null;
            this.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
