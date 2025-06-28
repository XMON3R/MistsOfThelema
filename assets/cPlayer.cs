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
    public class CPlayer : UserControl
    {
        private Timer timerUpdate;
        private System.ComponentModel.IContainer components;
        private TextBox hpInfo;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private PictureBox player;
        private Timer timer1;

        //new for intersect
        //public event EventHandler PlayerMoved;

        public static int Speed { get; set; } = 5;
        public int HP { get; set; } = 100;
        public List<IIgameItem> Inventory {  get; private set; } 

        //int[] borderCoord = {-40,1322,204,914};
        int[] borderCoord = { 5, 1365, 224, 965 };

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
            this.player.Click += new System.EventHandler(this.PlayerClick);
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 10;
            this.timerUpdate.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // hpInfo
            // 
            this.hpInfo.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.hpInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.hpInfo.ForeColor = System.Drawing.SystemColors.Window;
            this.hpInfo.Location = new System.Drawing.Point(3, 3);
            this.hpInfo.Name = "hpInfo";
            this.hpInfo.Size = new System.Drawing.Size(55, 20);
            this.hpInfo.TabIndex = 1;
            this.hpInfo.TabStop = false;
            this.hpInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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

        public CPlayer()
        {
            InitializeComponent();
            HP = 100;
            Inventory = new List<IIgameItem>();
        }

        public void AddItem(IIgameItem item1)
        {
            Inventory.Add(item1);
        }

        public void UseItem(int itemId)
        {
            var item = Inventory.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                item.Use();
                if (item.UsableTimes <= 0)
                {
                    Inventory.Remove(item);
                }
            }
        }

        public Rectangle GetBounds()
        {
            return this.Bounds;
        }

        private void PlayerClick(object sender, EventArgs e)
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


        private void Timer1_Tick(object sender, EventArgs e)
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
                newTop -= Speed;
                if (newTop < borderCoord[2])
                {
                    newTop = borderCoord[2];
                }
            }

            if (Core.IsDown && Top < borderCoord[3])
            {
                newTop += Speed;
                if (newTop > borderCoord[3])
                {
                    newTop = borderCoord[3];
                }
            }

            //horizontal
            if (Core.IsLeft && Left > borderCoord[0])
            {
                newLeft -= Speed;
                if (newLeft < borderCoord[0])
                {
                    newLeft = borderCoord[0];
                }
            }

            if (Core.IsRight && Left < borderCoord[1])
            {
                newLeft += Speed;
                if (newLeft > borderCoord[1])
                {
                    newLeft = borderCoord[1];
                }
            }

            Top = newTop;
            Left = newLeft;
        }

        private void LocationInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
