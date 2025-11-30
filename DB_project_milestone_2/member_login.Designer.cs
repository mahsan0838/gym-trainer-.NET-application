namespace Login_Design
{
    partial class member_login
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
            label2 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            panel2 = new Panel();
            pictureBox3 = new PictureBox();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(221, 19);
            label2.Name = "label2";
            label2.Size = new Size(238, 55);
            label2.TabIndex = 29;
            label2.Text = "MEMBER";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.BackColor = Color.WhiteSmoke;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.IBeam;
            textBox2.Location = new Point(221, 301);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(262, 16);
            textBox2.TabIndex = 27;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = Color.WhiteSmoke;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Location = new Point(221, 173);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(262, 16);
            textBox1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Desktop;
            panel2.Location = new Point(175, 327);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 3);
            panel2.TabIndex = 25;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = SystemColors.AppWorkspace;
            pictureBox3.Image = DB_project_milestone_2.Properties.Resources.login_password_3;
            pictureBox3.Location = new Point(175, 286);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(40, 36);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 24;
            pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Desktop;
            panel1.Location = new Point(175, 199);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 3);
            panel1.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Arial Rounded MT Bold", 27.9403F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(268, 91);
            label1.Name = "label1";
            label1.Size = new Size(132, 44);
            label1.TabIndex = 22;
            label1.Text = "Log in";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.AppWorkspace;
            pictureBox1.Cursor = Cursors.SizeAll;
            pictureBox1.Image = DB_project_milestone_2.Properties.Resources.username_1;
            pictureBox1.Location = new Point(175, 158);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Black;
            button3.Font = new Font("Arial Narrow", 13.97015F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Location = new Point(420, 363);
            button3.Name = "button3";
            button3.Size = new Size(105, 38);
            button3.TabIndex = 34;
            button3.Text = "Login";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Black;
            button2.Font = new Font("Arial Narrow", 13.97015F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(175, 363);
            button2.Name = "button2";
            button2.Size = new Size(105, 38);
            button2.TabIndex = 33;
            button2.Text = "Back";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click_1;
            // 
            // button1
            // 
            button1.BackColor = Color.Black;
            button1.Font = new Font("Arial Narrow", 13.97015F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(299, 363);
            button1.Name = "button1";
            button1.Size = new Size(105, 38);
            button1.TabIndex = 32;
            button1.Text = "Sign Up";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // member_login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(700, 422);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(panel2);
            Controls.Add(pictureBox3);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "member_login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += member_login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox1;
        private Panel panel2;
        private PictureBox pictureBox3;
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}

