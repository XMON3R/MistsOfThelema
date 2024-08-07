using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    internal class npc : UserControl, IInteractable
    {
        private PictureBox pictureBox1;
        public string InstanceName { get; set; }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MistsOfThelema.Properties.Resources.basicVillager;
            this.pictureBox1.InitialImage = global::MistsOfThelema.Properties.Resources.defPlayer;
            this.pictureBox1.Location = new System.Drawing.Point(-36, -19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // npc
            // 
            this.Controls.Add(this.pictureBox1);
            this.Name = "npc";
            this.Size = new System.Drawing.Size(70, 88);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        public npc()
        {
            InitializeComponent();
        }

        public Rectangle GetBounds()
        {
            return this.Bounds;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
