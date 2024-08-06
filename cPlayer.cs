using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistsOfThelema
{
    internal class cPlayer : UserControl
    {
        private Timer timerUpdate;
        private System.ComponentModel.IContainer components;
        private TextBox hpInfo;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private PictureBox player;
        private Timer timer1;

        //new for intersect
        public event EventHandler PlayerMoved;

        public static int speed { get; set; } = 5;
        public static int HP { get; set; } = 100;

        //int[] borderCoord = {-40,1322,204,914};
        int[] borderCoord = { 5, 1350, 224, 914 };

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.player = new System.Windows.Forms.PictureBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.hpInfo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Image = global::MistsOfThelema.Properties.Resources.defPlayer;
            this.player.Location = new System.Drawing.Point(-34, -19);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(140, 150);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            this.player.Click += new System.EventHandler(this.playerClick);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 10;
            this.timerUpdate.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // hpInfo
            // 
            this.hpInfo.Location = new System.Drawing.Point(3, 3);
            this.hpInfo.Name = "hpInfo";
            this.hpInfo.Size = new System.Drawing.Size(55, 20);
            this.hpInfo.TabIndex = 1;
            this.hpInfo.TabStop = false;
            this.hpInfo.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // cPlayer
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.hpInfo);
            this.Controls.Add(this.player);
            this.Name = "cPlayer";
            this.Size = new System.Drawing.Size(66, 85);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public cPlayer()
        {
            InitializeComponent();
        }

        public Rectangle GetBounds()
        {
            return this.Bounds;
        }


        private void playerClick(object sender, EventArgs e)
        {
            if (hpInfo.Visible == false)
            {
                hpInfo.Visible = true;
            }
            else
            {
                hpInfo.Visible = false;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveOnlyWithingBorders();
            UpdateHP();
            UpdateLoc();
        }

        private void UpdateHP()
        {
            hpInfo.Text = HP.ToString();
        }

        private void UpdateLoc()
        {
            //locationInfo.Text = GetBounds().ToString();
            //locationInfo.Text = $"X: {Left}, Y: {Top}";
        }

        private void MoveOnlyWithingBorders()
        {

            int newTop = Top;
            int newLeft = Left;

            //vertical
            if (Core.IsUp && Top > borderCoord[2])
            {
                newTop -= speed;
                if (newTop < borderCoord[2])
                {
                    newTop = borderCoord[2];
                }
            }

            if (Core.IsDown && Top < borderCoord[3])
            {
                newTop += speed;
                if (newTop > borderCoord[3])
                {
                    newTop = borderCoord[3];
                }
            }

            //horizontal
            if (Core.IsLeft && Left > borderCoord[0])
            {
                newLeft -= speed;
                if (newLeft < borderCoord[0])
                {
                    newLeft = borderCoord[0];
                }
            }

            if (Core.IsRight && Left < borderCoord[1])
            {
                newLeft += speed;
                if (newLeft > borderCoord[1])
                {
                    newLeft = borderCoord[1];
                }
            }

            Top = newTop;
            Left = newLeft;

            /*
            if (Top >= borderCoord[2] && Top <= borderCoord[3])
            {
                if (Core.IsUp)
                    Top -= speed;
                if (Core.IsDown)
                    Top += speed;
            }
            else if (Top < borderCoord[2])
            {
                Top = borderCoord[2];
            }
            else if (Top > borderCoord[3])
            {
                Top = borderCoord[3];
            }

            if (Left >= borderCoord[0] && Left <= borderCoord[1])
            {
                if (Core.IsRight)
                    Left += speed;
                if (Core.IsLeft)
                    Left -= speed;
            }

            else if (Left < borderCoord[0])
            {
                Left = borderCoord[0];
            }

            else if (Left > borderCoord[1])
            {
                Left = borderCoord[1];
            }            
            */
        }

        private void locationInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
