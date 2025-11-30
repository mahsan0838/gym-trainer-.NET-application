using Login_Design;
using System.Data.SqlClient;

namespace DB_dashboard_interface_forms
{
    public partial class member_dashboard : Form
    {
        private member_login unhide = null;
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        public member_dashboard()
        {
            InitializeComponent();

        }
        public member_dashboard(member_login callingform, int x)
        {
            this.unhide = callingform as member_login;
            this.m_id = x;
            InitializeComponent();

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            meals_option meal_opt = new meals_option(this, m_id);
            meal_opt.ShowDialog();
            meal_opt = null;
            this.Show();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gym_reg_form gym_reg = new Gym_reg_form(this, m_id);
            gym_reg.ShowDialog();
            gym_reg = null;
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            workout_plan_form workout = new workout_plan_form(this, m_id);
            workout.ShowDialog();
            workout = null;
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            book_session_form session = new book_session_form(this,m_id);
            session.ShowDialog();
            session = null;
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}