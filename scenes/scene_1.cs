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
        private cPlayer cPlayer1;

        public Scene1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.DoubleBuffered = true;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cPlayer1 = new MistsOfThelema.cPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.InitialImage = global::MistsOfThelema.Properties.Resources.defPlayer;
            this.pictureBox1.Location = new System.Drawing.Point(189, 299);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 130);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cPlayer1
            // 
            this.cPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.cPlayer1.Location = new System.Drawing.Point(12, 279);
            this.cPlayer1.Name = "cPlayer1";
            this.cPlayer1.Size = new System.Drawing.Size(150, 150);
            this.cPlayer1.TabIndex = 0;
            this.cPlayer1.Load += new System.EventHandler(this.cPlayer1_Load_1);
            this.cPlayer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.cPlayer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            // 
            // Scene1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::MistsOfThelema.Properties.Resources.townProto;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cPlayer1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Scene1";
            this.Load += new System.EventHandler(this.Scene1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

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
    }
}
