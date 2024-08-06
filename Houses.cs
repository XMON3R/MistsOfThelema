using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    internal class Houses: UserControl
    {
        private PictureBox housik;
        public event EventHandler PlayerInteracted;

        private void InitializeComponent()
        {
            this.housik = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.housik)).BeginInit();
            this.SuspendLayout();
            // 
            // housik
            // 
            this.housik.Image = global::MistsOfThelema.Properties.Resources.basicHouse;
            this.housik.Location = new System.Drawing.Point(-348, -281);
            this.housik.Name = "housik";
            this.housik.Size = new System.Drawing.Size(1021, 650);
            this.housik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.housik.TabIndex = 0;
            this.housik.TabStop = false;
            this.housik.WaitOnLoad = true;
            this.housik.Click += new System.EventHandler(this.housik_Click);
            // 
            // Houses
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.housik);
            this.DoubleBuffered = true;
            this.Name = "Houses";
            this.Size = new System.Drawing.Size(235, 114);
            ((System.ComponentModel.ISupportInitialize)(this.housik)).EndInit();
            this.ResumeLayout(false);

        }

        public Houses()             ///KURVA DOPRDELE DŮLEŽITÉ!
        {
            InitializeComponent();
        }

        private void housik_Click(object sender, EventArgs e)
        {

        }
    }
}
