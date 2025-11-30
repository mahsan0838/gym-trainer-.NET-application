namespace Login_Design
{
    partial class Trainer_dashboard
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
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            button8 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Font = new Font("Arial Rounded MT Bold", 10.74627F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Cyan;
            button1.Location = new Point(10, 11);
            button1.Name = "button1";
            button1.Size = new Size(165, 35);
            button1.TabIndex = 2;
            button1.Text = "Register in Gym";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Black;
            button2.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Cyan;
            button2.Location = new Point(10, 60);
            button2.Name = "button2";
            button2.Size = new Size(165, 32);
            button2.TabIndex = 3;
            button2.Text = "Appointments";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Black;
            button4.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.Cyan;
            button4.Location = new Point(12, 209);
            button4.Name = "button4";
            button4.Size = new Size(163, 34);
            button4.TabIndex = 5;
            button4.Text = "workoutplan reports";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Black;
            button5.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.Cyan;
            button5.Location = new Point(10, 157);
            button5.Name = "button5";
            button5.Size = new Size(165, 34);
            button5.TabIndex = 6;
            button5.Text = "Dietplan creation";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.Black;
            button6.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button6.ForeColor = Color.Cyan;
            button6.Location = new Point(10, 261);
            button6.Name = "button6";
            button6.Size = new Size(165, 34);
            button6.TabIndex = 7;
            button6.Text = "dietplan reports";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.Black;
            button7.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button7.ForeColor = Color.Cyan;
            button7.Location = new Point(12, 316);
            button7.Name = "button7";
            button7.Size = new Size(163, 34);
            button7.TabIndex = 8;
            button7.Text = "Feedback";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Black;
            button3.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.Cyan;
            button3.Location = new Point(10, 108);
            button3.Name = "button3";
            button3.Size = new Size(165, 34);
            button3.TabIndex = 4;
            button3.Text = "Workout plan creation";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = DB_project_milestone_2.Properties.Resources.trainer_dashboard_pic;
            pictureBox1.Location = new Point(181, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(516, 410);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button8
            // 
            button8.BackColor = Color.Black;
            button8.Font = new Font("Arial Rounded MT Bold", 10.20895F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button8.ForeColor = Color.Cyan;
            button8.Location = new Point(12, 375);
            button8.Name = "button8";
            button8.Size = new Size(163, 34);
            button8.TabIndex = 9;
            button8.Text = "Back";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // Trainer_dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(692, 410);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Trainer_dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trainer_dashboard";
            Load += Trainer_dashboard_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private Button button8;
    }
}