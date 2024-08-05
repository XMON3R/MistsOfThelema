﻿using System;
using System.Collections.Generic;
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
        private TextBox locationInfo;
        private ContextMenuStrip contextMenuStrip2;
        private PictureBox player;

        public static int speed { get; set; } = 5;
        public static int HP { get; set; } = 100;

        

        int[] borderCoord = {-40,1322,204,914};

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.player = new System.Windows.Forms.PictureBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.hpInfo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.locationInfo = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.player.Image = global::MistsOfThelema.Properties.Resources.defPlayer;
            this.player.Location = new System.Drawing.Point(0, 0);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(150, 150);
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
            this.hpInfo.Location = new System.Drawing.Point(21, 113);
            this.hpInfo.Name = "hpInfo";
            this.hpInfo.Size = new System.Drawing.Size(100, 20);
            this.hpInfo.TabIndex = 1;
            this.hpInfo.Visible = false;
            this.hpInfo.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // locationInfo
            // 
            this.locationInfo.Location = new System.Drawing.Point(21, 0);
            this.locationInfo.Name = "locationInfo";
            this.locationInfo.Size = new System.Drawing.Size(100, 20);
            this.locationInfo.TabIndex = 2;
            this.locationInfo.ReadOnly = true;
            this.locationInfo.TabStop = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // cPlayer
            // 
            this.Controls.Add(this.locationInfo);
            this.Controls.Add(this.hpInfo);
            this.Controls.Add(this.player);
            this.Name = "cPlayer";
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public cPlayer()
        {
            InitializeComponent();
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
            locationInfo.Text = $"X: {Left}, Y: {Top}";
        }

        private void MoveOnlyWithingBorders()
        {
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
        }

        private void locationInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
