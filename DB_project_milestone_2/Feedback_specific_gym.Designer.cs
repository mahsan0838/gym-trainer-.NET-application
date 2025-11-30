namespace DB_project_milestone_2
{
    partial class Feedback_specific_gym
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button1 = new Button();
            textBox13 = new TextBox();
            label11 = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(75, 96);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 57;
            dataGridView1.Size = new Size(663, 209);
            dataGridView1.TabIndex = 49;
            // 
            // button2
            // 
            button2.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(471, 347);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(153, 46);
            button2.TabIndex = 48;
            button2.Text = "Get Feedback";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(194, 347);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(95, 46);
            button1.TabIndex = 47;
            button1.Text = "Back";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // textBox13
            // 
            textBox13.Location = new Point(399, 36);
            textBox13.Margin = new Padding(4, 5, 4, 5);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(225, 30);
            textBox13.TabIndex = 46;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Black;
            label11.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(185, 44);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(132, 22);
            label11.TabIndex = 45;
            label11.Text = "Enter Gym ID";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.gym_admin_dashboard_background1;
            pictureBox2.Location = new Point(-49, -89);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(901, 629);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 44;
            pictureBox2.TabStop = false;
            // 
            // Feedback_specific_gym
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox13);
            Controls.Add(label11);
            Controls.Add(pictureBox2);
            Name = "Feedback_specific_gym";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Feedback_specific_gym";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button2;
        private Button button1;
        private TextBox textBox13;
        private Label label11;
        private PictureBox pictureBox2;
    }
}