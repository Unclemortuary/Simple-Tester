using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Controls;
//sing System.Windows.Markup;
using System.Windows.Forms;



namespace AccreditationTest
{
    public partial class Form2 : Form
    {
        private Label label1;
        private TextBox r;

        public Form2(Form1 f)
        {
            InitializeComponent();
            for (int i = 0; i < f.wAnswers.Count; i++)
                r.Text += f.wTopics[i] + " : " + f.wQuestions[i] + " : " + f.wAnswers[i] + "\r\n" + "\r\n";
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.r = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(201, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Правильные ответы";
            // 
            // r
            // 
            this.r.Location = new System.Drawing.Point(13, 57);
            this.r.Multiline = true;
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(579, 193);
            this.r.TabIndex = 1;
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(604, 262);
            this.Controls.Add(this.r);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(620, 300);
            this.MinimumSize = new System.Drawing.Size(620, 300);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r.SelectionStart = 0;
            r.ScrollBars = ScrollBars.Vertical;
        }
    }
}
