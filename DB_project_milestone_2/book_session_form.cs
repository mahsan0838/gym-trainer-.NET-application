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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_dashboard_interface_forms
{
    public partial class book_session_form : Form
    {
        private member_dashboard unhide = null;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");
        private int m_id;
        public book_session_form()
        {
            InitializeComponent();
        }
        public book_session_form(member_dashboard callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as member_dashboard;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string trainerIDString = textBox1.Text;
            string dateString = textBox2.Text;
            string timeString = textBox3.Text;
            

            if ( !int.TryParse(trainerIDString, out int trainerID))
            {
                MessageBox.Show("Invalid user ID or trainer ID. Please enter valid integers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!DateTime.TryParse(dateString, out DateTime date))
            {
                MessageBox.Show("Invalid date. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TimeSpan.TryParse(timeString, out TimeSpan time))
            {
                MessageBox.Show("Invalid time. Please enter a valid time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();

                // Find the maximum booking ID
                string getMaxBookingIDQuery = "SELECT ISNULL(MAX(booking_id), 0) FROM sessions";
                SqlCommand getMaxBookingIDCommand = new SqlCommand(getMaxBookingIDQuery, conn);
                int maxBookingID = Convert.ToInt32(getMaxBookingIDCommand.ExecuteScalar());
                int newBookingID = maxBookingID + 1;

                // Check if the session already exists
                string checkQuery = "SELECT COUNT(*) FROM sessions WHERE user_id = @userID AND trainer_id = @trainerID ";
                SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
                checkCommand.Parameters.AddWithValue("@userID", m_id);
                checkCommand.Parameters.AddWithValue("@trainerID", trainerID);
            

                int existingSessionCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingSessionCount > 0)
                {
                    MessageBox.Show("A session already exists for the given user ID, trainer ID", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Make an entry in the sessions table
                    string insertQuery = "INSERT INTO sessions (booking_id, user_id, trainer_id, date1, time1) VALUES (@bookingID, @userID, @trainerID, @date, @time)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                    insertCommand.Parameters.AddWithValue("@bookingID", newBookingID);
                    insertCommand.Parameters.AddWithValue("@userID", m_id);
                    insertCommand.Parameters.AddWithValue("@trainerID", trainerID);
                    insertCommand.Parameters.AddWithValue("@date", date);
                    insertCommand.Parameters.AddWithValue("@time", time);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session entry added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add session entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }


        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            feedbackform fdbkfrm = new feedbackform(this, m_id);
            fdbkfrm.ShowDialog();
            fdbkfrm = null;
            this.Show();
        }
    }
}
