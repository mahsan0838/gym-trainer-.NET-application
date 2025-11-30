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
    public partial class meals_option : Form
    {
        private int m_id;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");

        private member_dashboard unhide = null;
        public meals_option()
        {
            InitializeComponent();
        }
        public meals_option(member_dashboard callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as member_dashboard;

            try
            {
                conn.Open();

                string query = @"SELECT dp.diet_plan_id, dp.obj, dp.type_of_diet, dp.nutritions, dp.creator_role, dp.user_id, dp.trainer_id, MFDP.perc_progress
                FROM diet_plan as dp
                left outer join member_follow_diet_plan as MFDP 
                ON MFDP.diet_plan_id=dp.diet_plan_id
                WHERE Member_id = @memberId
                order by dp.diet_plan_id ;";

                // Create a new SqlCommand object
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@memberId", m_id);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Display results in a DataGridView (optional)
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close(); // Close the connection after use
            }


        }

        private void meals_option_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            create_meal_form meal_creation = new create_meal_form(this, m_id);
            meal_creation.ShowDialog();
            meal_creation = null;
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            explore_meal_plan meal_explore = new explore_meal_plan(this, m_id);
            meal_explore.ShowDialog();
            meal_explore = null;
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
