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
    public partial class feedbackform : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;");
        private int m_id;
        private book_session_form unhide = null;
        public feedbackform()
        {
            InitializeComponent();
        }
        public feedbackform(book_session_form callingform, int x)
        {
            InitializeComponent();
            this.m_id = x;
            unhide = callingform as book_session_form;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            
            string trnID = textBox3.Text;
            string rating = textBox4.Text;

            if (!int.TryParse(trnID, out int trainerID))
            {
                MessageBox.Show("Invalid Trainer ID. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!float.TryParse(rating, out float ratingValue))
            {
                MessageBox.Show("Invalid rating. Please enter a valid float value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate rating to ensure it is less than or equal to 10.0
            if (ratingValue > 10.0f)
            {
                MessageBox.Show("Rating should be less than or equal to 10.0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                conn.Open();

                // Check if both m_id and trainerID exist in the sessions table
                string checkSessionsQuery = "SELECT COUNT(*) FROM sessions WHERE user_id = @userId AND trainer_id = @trainerId";
                SqlCommand checkSessionsCommand = new SqlCommand(checkSessionsQuery, conn);
                checkSessionsCommand.Parameters.AddWithValue("@userId", m_id);
                checkSessionsCommand.Parameters.AddWithValue("@trainerId", trainerID);
                int sessionsCount = Convert.ToInt32(checkSessionsCommand.ExecuteScalar());

                if (sessionsCount == 0)
                {
                    MessageBox.Show("You haven't booked a session with this trainer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Query to retrieve the maximum feedback_id from the feedback table
                string getMaxFeedbackIdQuery = "SELECT MAX(feedback_id) FROM feedback";
                SqlCommand getMaxFeedbackIdCommand = new SqlCommand(getMaxFeedbackIdQuery, conn);
                int maxFeedbackId = Convert.ToInt32(getMaxFeedbackIdCommand.ExecuteScalar());
                int newFeedbackId = maxFeedbackId + 1;

                // Query to insert feedback into the feedback table
                string insertFeedbackQuery = "INSERT INTO feedback (feedback_id, trainer_id, user_id, rating) VALUES (@feedbackId, @trainerId, @userId, @rating)";
                SqlCommand insertFeedbackCommand = new SqlCommand(insertFeedbackQuery, conn);

                // Setting parameters for the insert query
                insertFeedbackCommand.Parameters.AddWithValue("@feedbackId", newFeedbackId);
                insertFeedbackCommand.Parameters.AddWithValue("@userId", m_id); // Assuming m_id is already defined
                insertFeedbackCommand.Parameters.AddWithValue("@trainerId", trainerID);
                insertFeedbackCommand.Parameters.AddWithValue("@rating", ratingValue);

                // Executing the insert query
                int rowsAffected = insertFeedbackCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Feedback added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Retrieve the current value of 'ovr' from trainer_log$ table
                    string getOvrQuery = "SELECT ovr FROM trainer_log$ WHERE trainer_id = @trainerId";
                    SqlCommand getOvrCommand = new SqlCommand(getOvrQuery, conn);
                    getOvrCommand.Parameters.AddWithValue("@trainerId", trainerID);
                    float currentOvr = Convert.ToSingle(getOvrCommand.ExecuteScalar());

                    // Calculate the new 'ovr' value
                    float newOvr = (currentOvr + ratingValue) / 2;

                    // Update the 'ovr' value in the trainer_log$ table
                    string updateOvrQuery = "UPDATE trainer_log$ SET ovr = @newOvr WHERE trainer_id = @trainerId";
                    SqlCommand updateOvrCommand = new SqlCommand(updateOvrQuery, conn);
                    updateOvrCommand.Parameters.AddWithValue("@newOvr", newOvr);
                    updateOvrCommand.Parameters.AddWithValue("@trainerId", trainerID);
                    updateOvrCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Failed to add feedback.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while adding feedback: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unhide.Show();
            this.Close();
        }
    }
}
