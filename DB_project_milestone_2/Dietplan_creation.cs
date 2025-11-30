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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Login_Design
{
    public partial class Dietplan_creation : Form
    {
        private Trainer_dashboard unhide = null;
        string connectionString = "Data Source=DESKTOP-8H6V81R\\SQLEXPRESS;Initial Catalog=milestone_3;Integrated Security=True;";

        private int trainer_id = 0;
        public Dietplan_creation(Trainer_dashboard callingform, int id)
        {
            trainer_id = id;
            unhide = callingform as Trainer_dashboard;
            InitializeComponent();
        }
        public Dietplan_creation()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string objective = textBox12.Text;
            string diet_type = textBox14.Text;
            string nutrition = textBox18.Text;
            int nut = int.Parse(nutrition);

            string allergens_breakfast = comboBox4.SelectedItem.ToString();
            string allergens_lunch = comboBox5.SelectedItem.ToString();
            string allergens_dinner = comboBox6.SelectedItem.ToString();

            //breakfast
            string meal_breakfast = comboBox1.SelectedItem.ToString();
            string portion_breakfast = textBox1.Text;
            string carbs_breakfast = textBox6.Text;
            string protiens_breakfast = textBox5.Text;
            string fats_breakfast = textBox4.Text;
            string calories_breakfast = textBox15.Text;

            float port_br = float.Parse(portion_breakfast);
            float carbs_br = float.Parse(carbs_breakfast);
            float prot_br = float.Parse(protiens_breakfast);
            float fats_br = float.Parse(fats_breakfast);
            float cal_br = float.Parse(calories_breakfast);

            //lunch
            string meal_lunch = comboBox2.SelectedItem.ToString();
            string portion_lunch = textBox2.Text;
            string carbs_lunch = textBox7.Text;
            string protiens_lunch = textBox8.Text;
            string fats_lunch = textBox9.Text;
            string calories_lunch = textBox16.Text;

            float port_ln = float.Parse(portion_lunch);
            float carbs_ln = float.Parse(carbs_lunch);
            float prot_ln = float.Parse(protiens_lunch);
            float fats_ln = float.Parse(fats_lunch);
            float cal_ln = float.Parse(calories_lunch);

            //dinner
            string meal_dinner = comboBox3.SelectedItem.ToString();
            string portion_dinner = textBox3.Text;
            string carbs_dinner = textBox10.Text;
            string protiens_dinner = textBox11.Text;
            string fats_dinner = textBox12.Text;
            string calories_dinner = textBox17.Text;


            float port_dn = float.Parse(portion_dinner);
            float carbs_dn = float.Parse(carbs_dinner);
            float prot_dn = float.Parse(protiens_dinner);
            float fats_dn = float.Parse(fats_dinner);
            float cal_dn = float.Parse(calories_dinner);




            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    int diet_id = 0;
                    int meal_id = 0;

                    string getMaxDietIdQuery = "SELECT MAX(diet_plan_id) FROM diet_plan";
                    using (SqlCommand getMaxDietIdCmd = new SqlCommand(getMaxDietIdQuery, conn))
                    {
                        diet_id = (int)getMaxDietIdCmd.ExecuteScalar() + 1;
                    }

                    // Insert into diet_plan
                    string insertDietPlanQuery = "INSERT INTO diet_plan VALUES (@dietId, @obj, @typeOfDiet, @nutrition, 'Trainer', NULL, @trainerId)";
                    using (SqlCommand insertDietPlanCmd = new SqlCommand(insertDietPlanQuery, conn))
                    {
                        insertDietPlanCmd.Parameters.AddWithValue("@dietId", diet_id);
                        insertDietPlanCmd.Parameters.AddWithValue("@obj", objective);
                        insertDietPlanCmd.Parameters.AddWithValue("@typeOfDiet", diet_type);
                        insertDietPlanCmd.Parameters.AddWithValue("@nutrition", nut);

                        // Assuming trainerId is available from another source
                        insertDietPlanCmd.Parameters.AddWithValue("@trainerId", trainer_id);

                        insertDietPlanCmd.ExecuteNonQuery();
                    }

                    //get next available meal id
                    string getMaxMealIdQuery = "SELECT MAX(meal_id) FROM meals";
                    MessageBox.Show("Trainer_id: " + trainer_id);
                    using (SqlCommand getMaxMealIdCmd = new SqlCommand(getMaxMealIdQuery, conn))
                    {
                        meal_id = (int)getMaxMealIdCmd.ExecuteScalar() + 1;
                    }



                    // Insert into meals_breakfast

                    string insertMealQuery = "INSERT INTO Meals VALUES (@mealId,@mealname, @breakfast , @allergens, @portionSize, @carbs, @fats, @proteins, @calories)";
                    string breakfast = "breakfast";
                    using (SqlCommand insertMealCmd = new SqlCommand(insertMealQuery, conn))
                    {
                        insertMealCmd.Parameters.AddWithValue("@mealId", meal_id);
                        insertMealCmd.Parameters.AddWithValue("@mealname", meal_breakfast);

                        insertMealCmd.Parameters.AddWithValue("@breakfast", breakfast);
                        insertMealCmd.Parameters.AddWithValue("@allergens", allergens_breakfast);
                        insertMealCmd.Parameters.AddWithValue("@portionSize", port_br);
                        insertMealCmd.Parameters.AddWithValue("@carbs", carbs_br);
                        insertMealCmd.Parameters.AddWithValue("@fats", fats_br);
                        insertMealCmd.Parameters.AddWithValue("@proteins", prot_br);
                        insertMealCmd.Parameters.AddWithValue("@calories", cal_br);

                        insertMealCmd.ExecuteNonQuery();
                    }

                    //   MessageBox.Show("line 195");
                    //inserting into dp_mls_rlt table
                    string insertDPMlsRltQuery = "INSERT INTO DP_MLS_RLT VALUES (@dietId, @mealId)";
                    using (SqlCommand insertDPMlsRltCmd = new SqlCommand(insertDPMlsRltQuery, conn))
                    {
                        insertDPMlsRltCmd.Parameters.AddWithValue("@dietId", diet_id);
                        insertDPMlsRltCmd.Parameters.AddWithValue("@mealId", meal_id);

                        insertDPMlsRltCmd.ExecuteNonQuery();
                    }
                    // MessageBox.Show("line 206");

                    //increamenting meal_id
                    meal_id += 1;
                    //FOR LUNCH


                    // Insert into meals_lunch
                    string insertMealQuery_lunch = "INSERT INTO Meals VALUES (@mealId,@mealname, @lunch, @allergens, @portionSize, @carbs, @fats, @proteins, @calories)";
                    string lunch = "lunch";

                    using (SqlCommand insertMealCmd2 = new SqlCommand(insertMealQuery_lunch, conn))
                    {
                        insertMealCmd2.Parameters.AddWithValue("@mealId", meal_id);
                        insertMealCmd2.Parameters.AddWithValue("@mealname", meal_lunch);

                        insertMealCmd2.Parameters.AddWithValue("@lunch", lunch);

                        insertMealCmd2.Parameters.AddWithValue("@allergens", allergens_lunch);
                        insertMealCmd2.Parameters.AddWithValue("@portionSize", port_ln);
                        insertMealCmd2.Parameters.AddWithValue("@carbs", carbs_ln);
                        insertMealCmd2.Parameters.AddWithValue("@fats", fats_ln);
                        insertMealCmd2.Parameters.AddWithValue("@proteins", prot_ln);
                        insertMealCmd2.Parameters.AddWithValue("@calories", cal_ln);

                        insertMealCmd2.ExecuteNonQuery();
                    }

                    //inserting into dp_mls_rlt table
                    string insertDPMlsRltQuery_lunch = "INSERT INTO DP_MLS_RLT VALUES (@dietId, @mealId)";
                    using (SqlCommand insertDPMlsRltCmd = new SqlCommand(insertDPMlsRltQuery_lunch, conn))
                    {
                        insertDPMlsRltCmd.Parameters.AddWithValue("@dietId", diet_id);
                        insertDPMlsRltCmd.Parameters.AddWithValue("@mealId", meal_id);

                        insertDPMlsRltCmd.ExecuteNonQuery();
                    }

                    //FOR DINNER ----------------------------DINNER--------------------------------
                    meal_id += 1;


                    // Insert into meals_dinner
                    string insertMealQuery_dinner = "INSERT INTO Meals VALUES (@mealId,@mealname, @dinner , @allergens, @portionSize, @carbs, @fats, @proteins, @calories)";
                    string dinner = "dinner";
                    using (SqlCommand insertMealCmd3 = new SqlCommand(insertMealQuery_dinner, conn))
                    {
                        insertMealCmd3.Parameters.AddWithValue("@mealId", meal_id);
                        insertMealCmd3.Parameters.AddWithValue("@mealname", meal_dinner);

                        insertMealCmd3.Parameters.AddWithValue("@dinner", dinner);

                        insertMealCmd3.Parameters.AddWithValue("@allergens", allergens_dinner);
                        insertMealCmd3.Parameters.AddWithValue("@portionSize", port_dn);
                        insertMealCmd3.Parameters.AddWithValue("@carbs", carbs_dn);
                        insertMealCmd3.Parameters.AddWithValue("@fats", fats_dn);
                        insertMealCmd3.Parameters.AddWithValue("@proteins", prot_dn);
                        insertMealCmd3.Parameters.AddWithValue("@calories", cal_dn);

                        insertMealCmd3.ExecuteNonQuery();
                    }

                    //inserting into dp_mls_rlt table
                    string insertDPMlsRltQuery_dinner = "INSERT INTO DP_MLS_RLT VALUES (@dietId, @mealId)";
                    using (SqlCommand insertDPMlsRltCmd3 = new SqlCommand(insertDPMlsRltQuery_dinner, conn))
                    {
                        insertDPMlsRltCmd3.Parameters.AddWithValue("@dietId", diet_id);
                        insertDPMlsRltCmd3.Parameters.AddWithValue("@mealId", meal_id);

                        insertDPMlsRltCmd3.ExecuteNonQuery();
                    }

                }

            }

            catch (SqlException ex)
            {
                MessageBox.Show("Error occurred while creating diet plan");
                // Handle the exception appropriately (e.g., log the error or notify the user)
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            unhide.Show();
            this.Close();
        }
    }
}
