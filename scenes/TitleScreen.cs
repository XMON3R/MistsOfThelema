using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    public partial class TitleScreen : Form
    {
        public TitleScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.gameLogo = new System.Windows.Forms.PictureBox();
            this.introBox = new System.Windows.Forms.TextBox();
            this.go = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StartButton.Font = new System.Drawing.Font("Victorian LET", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(335, 835);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(678, 117);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "START GAME";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // gameLogo
            // 
            this.gameLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gameLogo.Image = global::MistsOfThelema.Properties.Resources.thelemaC;
            this.gameLogo.InitialImage = global::MistsOfThelema.Properties.Resources.thelemaC;
            this.gameLogo.Location = new System.Drawing.Point(119, 44);
            this.gameLogo.Name = "gameLogo";
            this.gameLogo.Size = new System.Drawing.Size(1137, 687);
            this.gameLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameLogo.TabIndex = 4;
            this.gameLogo.TabStop = false;
            this.gameLogo.Click += new System.EventHandler(this.pictureBox1_Click_2);
            // 
            // introBox
            // 
            this.introBox.Location = new System.Drawing.Point(88, 28);
            this.introBox.Multiline = true;
            this.introBox.Name = "introBox";
            this.introBox.ReadOnly = true;
            this.introBox.Size = new System.Drawing.Size(1184, 532);
            this.introBox.TabIndex = 5;
            this.introBox.Visible = false;
            // 
            // go
            // 
            this.go.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.go.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.go.Font = new System.Drawing.Font("Victorian LET", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.go.Location = new System.Drawing.Point(335, 835);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(678, 117);
            this.go.TabIndex = 6;
            this.go.Text = "go";
            this.go.UseVisualStyleBackColor = false;
            this.go.Visible = false;
            this.go.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // TitleScreen
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.go);
            this.Controls.Add(this.gameLogo);
            this.Controls.Add(this.introBox);
            this.Controls.Add(this.StartButton);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "TitleScreen";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.gameLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                    }

        private void StartButton_Click(object sender, EventArgs e)
        {
            introBox.Visible = true;
            gameLogo.Visible = false;
            StartButton.Visible = false;
            go.Visible = true;

            DialogLoader diLo = new DialogLoader();
            string introDialog = diLo.LoadSingleDialog("..\\..\\resources\\dialog\\intro.txt");

            introBox.Text = introDialog;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            introBox.Hide();
            go.Hide();

            Scene1 s1 = new Scene1();
            s1.Show();

            this.Hide();
        }
    }
} 
