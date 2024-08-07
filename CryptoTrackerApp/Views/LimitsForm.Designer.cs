namespace CryptoTrackerApp.Views
{
    partial class LimitsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            label2 = new Label();
            btnHome = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Sans Serif Collection", 10.2F);
            label1.ForeColor = Color.FromArgb(56, 152, 213);
            label1.Location = new Point(69, 29);
            label1.Name = "label1";
            label1.Size = new Size(263, 35);
            label1.TabIndex = 0;
            label1.Text = "Change the actual limits";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(0, 26, 43);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.ForeColor = Color.FromArgb(56, 152, 213);
            textBox1.Location = new Point(142, 148);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(64, 228, 175);
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Segoe UI", 9F);
            button1.ForeColor = Color.FromArgb(0, 18, 30);
            button1.Location = new Point(257, 268);
            button1.Name = "button1";
            button1.Size = new Size(79, 28);
            button1.TabIndex = 2;
            button1.Text = "Update";
            button1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.FromArgb(56, 152, 213);
            label2.Location = new Point(248, 148);
            label2.Name = "label2";
            label2.Size = new Size(23, 21);
            label2.TabIndex = 3;
            label2.Text = "%";
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(64, 228, 175);
            btnHome.Cursor = Cursors.Hand;
            btnHome.Font = new Font("Segoe UI", 9F);
            btnHome.ForeColor = Color.FromArgb(0, 18, 30);
            btnHome.Location = new Point(69, 268);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(79, 28);
            btnHome.TabIndex = 4;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = false;
            btnHome.Click += button2_Click;
            // 
            // LimitsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 18, 30);
            ClientSize = new Size(420, 416);
            Controls.Add(btnHome);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            ForeColor = Color.White;
            Name = "LimitsForm";
            Text = "Change Limits";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private Label label2;
        private Button btnHome;

    }


}
