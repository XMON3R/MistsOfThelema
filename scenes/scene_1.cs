using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MistsOfThelema
{
    public partial class Scene1 : Form
    {
        private PictureBox pictureBox1;
        private npc npc1;
        private Houses playerExitHouse;
        private Label interactLabel;
        private cPlayer cPlayer1;

        public Scene1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.playerExitHouse = new MistsOfThelema.Houses();
            this.npc1 = new MistsOfThelema.npc();
            this.cPlayer1 = new MistsOfThelema.cPlayer();
            this.interactLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.InitialImage = global::MistsOfThelema.Properties.Resources.defPlayer;
            this.pictureBox1.Location = new System.Drawing.Point(194, 322);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 130);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // playerExitHouse
            // 
            this.playerExitHouse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playerExitHouse.BackColor = System.Drawing.Color.Transparent;
            this.playerExitHouse.Location = new System.Drawing.Point(1131, 292);
            this.playerExitHouse.Name = "playerExitHouse";
            this.playerExitHouse.Size = new System.Drawing.Size(225, 109);
            this.playerExitHouse.TabIndex = 5;
            // 
            // npc1
            // 
            this.npc1.BackColor = System.Drawing.Color.Transparent;
            this.npc1.Location = new System.Drawing.Point(986, 523);
            this.npc1.Name = "npc1";
            this.npc1.Size = new System.Drawing.Size(70, 88);
            this.npc1.TabIndex = 4;
            // 
            // cPlayer1
            // 
            this.cPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.cPlayer1.Location = new System.Drawing.Point(34, 841);
            this.cPlayer1.Name = "cPlayer1";
            this.cPlayer1.Size = new System.Drawing.Size(61, 123);
            this.cPlayer1.TabIndex = 0;
            this.cPlayer1.Load += new System.EventHandler(this.cPlayer1_Load_1);
            this.cPlayer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.cPlayer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            // 
            // interactLabel
            // 
            this.interactLabel.AutoSize = true;
            this.interactLabel.Location = new System.Drawing.Point(700, 176);
            this.interactLabel.Name = "interactLabel";
            this.interactLabel.Size = new System.Drawing.Size(152, 13);
            this.interactLabel.TabIndex = 6;
            this.interactLabel.Text = "Interact with ------ by pressing E";
            this.interactLabel.Click += new System.EventHandler(this.label1_Click);
            this.interactLabel.Visible = false;
            // 
            // Scene1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::MistsOfThelema.Properties.Resources.townProto;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.interactLabel);
            this.Controls.Add(this.cPlayer1);
            this.Controls.Add(this.npc1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playerExitHouse);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1440, 1080);
            this.MinimumSize = new System.Drawing.Size(1440, 1038);
            this.Name = "Scene1";
            this.Load += new System.EventHandler(this.Scene1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Core.KeyUp)
                Core.IsUp = true;
            if (e.KeyCode == Core.KeyDown)
                Core.IsDown = true;
            if (e.KeyCode == Core.KeyLeft)
                Core.IsLeft = true;
            if (e.KeyCode == Core.KeyRight)
                Core.IsRight = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Core.KeyUp)
                Core.IsUp = false;
            if (e.KeyCode == Core.KeyDown)
                Core.IsDown = false;
            if (e.KeyCode == Core.KeyLeft)
                Core.IsLeft = false;
            if (e.KeyCode == Core.KeyRight)
                Core.IsRight = false;

        }

        private void cPlayer1_Load(object sender, EventArgs e)
        {

        }

        private void cPlayer2_Load(object sender, EventArgs e)
        {

        }

        private void Scene1_Load(object sender, EventArgs e)
        {

        }

        private void npc1_Load(object sender, EventArgs e)
        {

        }

        private void cPlayer1_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void houses2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
