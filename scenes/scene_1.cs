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
        private Timer collisionTimer;
        private System.ComponentModel.IContainer components;
        private Label label1;
        private Label label2;
        private cPlayer cPlayer1;

        private List<IInteractable> interactables; /*= new List<IInteractable>
        {
        new Houses("House1"),
        new Houses("House2"),
        new npc("Guard"),
        new npc("Merchant")
        };*/

    public Scene1()
        {
            InitializeComponent();
            InitializeInteractables();
            //cPlayer1.PlayerMoved += CPlayer1_PlayerMoved;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.interactLabel = new System.Windows.Forms.Label();
            this.collisionTimer = new System.Windows.Forms.Timer(this.components);
            this.cPlayer1 = new MistsOfThelema.cPlayer();
            this.npc1 = new MistsOfThelema.npc("Teo");
            this.playerExitHouse = new MistsOfThelema.Houses("your hut");
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // interactLabel
            // 
            this.interactLabel.AutoSize = true;
            this.interactLabel.Location = new System.Drawing.Point(700, 176);
            this.interactLabel.Name = "interactLabel";
            this.interactLabel.Size = new System.Drawing.Size(152, 13);
            this.interactLabel.TabIndex = 6;
            this.interactLabel.Text = "Interact with ------ by pressing E";
            this.interactLabel.Visible = false;
            this.interactLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // collisionTimer
            // 
            this.collisionTimer.Enabled = true;
            this.collisionTimer.Interval = 50;
            this.collisionTimer.Tick += new System.EventHandler(this.collisionTimer_Tick);
            // 
            // cPlayer1
            // 
            this.cPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.cPlayer1.Location = new System.Drawing.Point(34, 841);
            this.cPlayer1.Name = "cPlayer1";
            this.cPlayer1.Size = new System.Drawing.Size(61, 86);
            this.cPlayer1.TabIndex = 0;
            this.cPlayer1.Load += new System.EventHandler(this.cPlayer1_Load_1);
            this.cPlayer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.cPlayer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            // 
            // npc1
            // 
            this.npc1.BackColor = System.Drawing.Color.Transparent;
            this.npc1.Location = new System.Drawing.Point(986, 523);
            this.npc1.Name = "npc1";
            this.npc1.Size = new System.Drawing.Size(70, 88);
            this.npc1.TabIndex = 4;
            // 
            // playerExitHouse
            // 
            this.playerExitHouse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playerExitHouse.BackColor = System.Drawing.Color.Transparent;
            this.playerExitHouse.Location = new System.Drawing.Point(1131, 292);
            this.playerExitHouse.Name = "playerExitHouse";
            this.playerExitHouse.Size = new System.Drawing.Size(225, 109);
            this.playerExitHouse.TabIndex = 5;
            this.playerExitHouse.InstanceName = "Your House";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1051, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "no info yet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1195, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "no info yet";
            // 
            // Scene1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::MistsOfThelema.Properties.Resources.townProto;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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

        private void InitializeInteractables()
        {
            interactables = new List<IInteractable>
            {
                playerExitHouse,
                npc1
            };
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


        //private void CPlayer1_PlayerMoved(object sender, Rectangle playerBounds)
        private void CheckCollision()
        {
            label1.Text = playerExitHouse.GetBounds().ToString();
            label2.Text = cPlayer1.GetBounds().ToString();

            bool collision = false;

            foreach (var interactable in interactables)
            {
                Rectangle playerBounds = cPlayer1.GetBounds();
                Rectangle interactableBounds = interactable.GetBounds();

                Rectangle expandedBounds = ExpandBoundsByRadius(interactableBounds, 30);

                if (playerBounds.IntersectsWith(expandedBounds))
                {
                    interactLabel.Visible = true;
                    interactLabel.Text = $"Press E to interact with {interactable.InstanceName}";
                    collision = true;
                    break;
                }

                if (!collision)
                {
                    interactLabel.Visible = false;
                }

                //OLD VERSION FOR HOUSE-PLAYER SCENARIO
                /*
                if (playerExitHouse.GetBounds().IntersectsWith(cPlayer1.GetBounds()))
                {
                    // Show interaction label or trigger interaction
                    interactLabel.Visible = true;
                    interactLabel.Text = "Press E to enter " + playerExitHouse.InstanceName + " to END THE DAY";
                }
                else
                {
                    // Hide interaction label if the player moves away
                    interactLabel.Visible = false;
                }*/
            }


        } 
        private Rectangle ExpandBoundsByRadius(Rectangle bounds, int radius)
        {
            return new Rectangle(
                bounds.X - radius,
                bounds.Y - radius,
                bounds.Width + 2 * radius,
                bounds.Height + 2 * radius
            );
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

        private void collisionTimer_Tick(object sender, EventArgs e)
        {
            CheckCollision();
        }
    }
}
