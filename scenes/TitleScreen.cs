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

    /*    private void Form1_Load(object sender, EventArgs e)
        {
        }*/

        private void InitializeComponent()
        {
            this.StartButton = new System.Windows.Forms.Button();
            this.gameLogo = new System.Windows.Forms.PictureBox();
            this.introBox = new System.Windows.Forms.Label();
            this.go = new System.Windows.Forms.Button();
            this.smallerLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gameLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallerLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // 
            // introBox
            // 
            this.introBox.BackColor = System.Drawing.Color.Black;
            this.introBox.ForeColor = System.Drawing.SystemColors.Info;
            this.introBox.Location = new System.Drawing.Point(88, 28);
            this.introBox.Name = "introBox";
            this.introBox.Size = new System.Drawing.Size(1184, 532);
            this.introBox.TabIndex = 5;
            this.introBox.Visible = false;
            // 
            // go
            // 
            this.go.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.go.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.go.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.go.Location = new System.Drawing.Point(335, 835);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(678, 117);
            this.go.TabIndex = 6;
            this.go.Text = "go";
            this.go.UseVisualStyleBackColor = false;
            this.go.Visible = false;
            this.go.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // smallerLogo
            // 
            this.smallerLogo.Image = global::MistsOfThelema.Properties.Resources.thelemaW;
            this.smallerLogo.Location = new System.Drawing.Point(559, 554);
            this.smallerLogo.Name = "smallerLogo";
            this.smallerLogo.Size = new System.Drawing.Size(253, 196);
            this.smallerLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.smallerLogo.TabIndex = 7;
            this.smallerLogo.TabStop = false;
            this.smallerLogo.Visible = false;
            // 
            // TitleScreen
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.smallerLogo);
            this.Controls.Add(this.go);
            this.Controls.Add(this.gameLogo);
            this.Controls.Add(this.introBox);
            this.Controls.Add(this.StartButton);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "TitleScreen";
            ((System.ComponentModel.ISupportInitialize)(this.gameLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallerLogo)).EndInit();
            this.ResumeLayout(false);

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
            introBox.Font = new Font("Courier New", 40, FontStyle.Regular);
            introBox.ForeColor = Color.White;

            smallerLogo.Visible = true;
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
