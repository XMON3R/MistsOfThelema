using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;


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

        private bool isInConversation = false;

        private DialogLoader diaLoad;
        private List<Button> choiceButtons = new List<Button>();
        private Label dialogLabel;
        private Label interactDetection;
        private PictureBox pictureBox2;
        private List<IInteractable> interactables; /*= new List<IInteractable>
        {
        new Houses("House1"),
        new Houses("House2"),
        new npc("Guard"),
        new npc("Merchant")
        };*/

        /* 
         this.npc1 = new MistsOfThelema.npc("Theo");
        this.playerExitHouse = new MistsOfThelema.Houses("your");
        */

        public Scene1()
        {
            InitializeComponent();
            InitializeInteractables();
            //cPlayer1.PlayerMoved += CPlayer1_PlayerMoved;

            diaLoad = new DialogLoader();
            diaLoad.LoadDialogFromJson("..\\..\\resources\\dialog\\day1.json");

            choiceButtons = new List<Button>();

            bool already_talked = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.interactLabel = new System.Windows.Forms.Label();
            this.collisionTimer = new System.Windows.Forms.Timer(this.components);
            this.cPlayer1 = new MistsOfThelema.cPlayer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dialogLabel = new System.Windows.Forms.Label();
            this.interactDetection = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.npc1 = new MistsOfThelema.npc("Theo");
            this.playerExitHouse = new MistsOfThelema.Houses("your");
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.interactLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.interactLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.interactLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.interactLabel.Location = new System.Drawing.Point(540, 931);
            this.interactLabel.Name = "interactLabel";
            this.interactLabel.Size = new System.Drawing.Size(348, 18);
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
            this.cPlayer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.cPlayer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
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
            // dialogLabel
            // 
            this.dialogLabel.AutoSize = true;
            this.dialogLabel.Location = new System.Drawing.Point(673, 885);
            this.dialogLabel.Name = "dialogLabel";
            this.dialogLabel.Size = new System.Drawing.Size(61, 13);
            this.dialogLabel.TabIndex = 9;
            this.dialogLabel.Text = "dialogLabel";
            // 
            // interactDetection
            // 
            this.interactDetection.AutoSize = true;
            this.interactDetection.Location = new System.Drawing.Point(853, 197);
            this.interactDetection.Name = "interactDetection";
            this.interactDetection.Size = new System.Drawing.Size(35, 13);
            this.interactDetection.TabIndex = 10;
            this.interactDetection.Text = "label3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(-5, 875);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1439, 227);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // npc1
            // 
            this.npc1.BackColor = System.Drawing.Color.Transparent;
            this.npc1.InstanceName = "Theo";
            this.npc1.Location = new System.Drawing.Point(986, 523);
            this.npc1.Name = "npc1";
            this.npc1.Size = new System.Drawing.Size(70, 88);
            this.npc1.TabIndex = 4;
            // 
            // playerExitHouse
            // 
            this.playerExitHouse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playerExitHouse.BackColor = System.Drawing.Color.Transparent;
            this.playerExitHouse.InstanceName = "Your House";
            this.playerExitHouse.Location = new System.Drawing.Point(1131, 292);
            this.playerExitHouse.Name = "playerExitHouse";
            this.playerExitHouse.Size = new System.Drawing.Size(225, 109);
            this.playerExitHouse.TabIndex = 5;
            // 
            // Scene1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::MistsOfThelema.Properties.Resources.townProto;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.interactDetection);
            this.Controls.Add(this.dialogLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.interactLabel);
            this.Controls.Add(this.cPlayer1);
            this.Controls.Add(this.npc1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playerExitHouse);
            this.Controls.Add(this.pictureBox2);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1440, 1080);
            this.MinimumSize = new System.Drawing.Size(1440, 1038);
            this.Name = "Scene1";
            this.Load += new System.EventHandler(this.Scene1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
            if (isInConversation) return;

            if (e.KeyCode == Core.KeyUp)
                Core.IsUp = true;
            if (e.KeyCode == Core.KeyDown)
                Core.IsDown = true;
            if (e.KeyCode == Core.KeyLeft)
                Core.IsLeft = true;
            if (e.KeyCode == Core.KeyRight)
                Core.IsRight = true;

            if (e.KeyCode == Core.Interact)
            {
                Core.IsInteracting = true;
                interactDetection.Text = "IN PROGRESS";
                HandleInteraction();
            }

            /* if(Core.IsInteracting)
             {
                 HandleInteraction();
             }*/
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (isInConversation) return;

            if (e.KeyCode == Core.KeyUp)
                Core.IsUp = false;
            if (e.KeyCode == Core.KeyDown)
                Core.IsDown = false;
            if (e.KeyCode == Core.KeyLeft)
                Core.IsLeft = false;
            if (e.KeyCode == Core.KeyRight)
                Core.IsRight = false;

            if (e.KeyCode == Core.Interact)
            {
                Core.IsInteracting = false;
                interactDetection.Text = "Not interacting";
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (isInConversation) return;*/

            if (Core.IsInteracting)
            {
                // HandleInteraction();
            }
        }

        private void CheckCollision()
        {
            label1.Text = playerExitHouse.GetBounds().ToString();
            label2.Text = cPlayer1.GetBounds().ToString();

            bool collision = false;

            foreach (var interactable in interactables)
            {
                Rectangle playerBounds = cPlayer1.GetBounds();
                Rectangle interactableBounds = interactable.GetBounds();

                //ensures that player does not need to get inside objects in order to interact
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


        private void HandleInteraction()
        {
            foreach (var interactable in interactables)
            {
                Rectangle playerBounds = cPlayer1.GetBounds();
                Rectangle interactableBounds = interactable.GetBounds();

                Rectangle expandedBounds = ExpandBoundsByRadius(interactableBounds, 50);

                if (playerBounds.IntersectsWith(expandedBounds) && interactable.InstanceName == "Your House")
                {
                    TransitionToScene2();
                    break;
                }

                if (playerBounds.IntersectsWith(expandedBounds) && interactable is npc && interactable.InstanceName != "Shopkeeper")
                {
                    //StartConversationWith("Theo", diaLoad);
                    isInConversation = true;
                    ResetPlayerMovement();
                    StartConversationWith(interactable.InstanceName, diaLoad);
                    break;
                }

                if (playerBounds.IntersectsWith(expandedBounds) && interactable is Houses)
                {
                    //somehow interact with house -> maybe stealing, introduce karma system for kills and stolen goods
                }
            }
        }

        //start a conversation with
        private void StartConversationWith(string NpcName, DialogLoader dl)
        {
            //DialogNode introNode = dl.GetDialogNode("Theo-Player", "intro");
            DialogNode introNode = dl.GetDialogNode(NpcName, "intro");
            if (introNode != null)
            {
                DisplayDialog(NpcName, introNode);
            }
        }

        // Method to display a dialog node
        private void DisplayDialog(string NpcName, DialogNode node)
        {
            pictureBox2.Visible = true;

            dialogLabel.Visible = true;
            dialogLabel.Text = node.text;

            //resets buttons for dialog choices
            foreach (var button in choiceButtons)
            {
                this.Controls.Remove(button);
            }
            choiceButtons.Clear();

            //ensures easier spacing
            int yPosition = dialogLabel.Bottom + 10;

            //iterate over choices from json dialog file and display them
            foreach (var choice in node.choices)
            {
                Button choiceButton = new Button();
                //choiceButton.BringToFront();
                choiceButton.Text = choice.Value.text;
                choiceButton.Location = new Point(dialogLabel.Left, yPosition);
                choiceButton.AutoSize = true;
                choiceButton.Click += (sender, args) => OnChoiceSelected(NpcName, choice.Value.next);
                this.Controls.Add(choiceButton);
                choiceButtons.Add(choiceButton);
                choiceButton.BringToFront();
                yPosition += choiceButton.Height + 5;
            }

            //end button 
            Button endButton = new Button();
            endButton.Text = "---- End Conversation";
            endButton.Location = new Point(dialogLabel.Left, yPosition);
            endButton.AutoSize = true;
            endButton.Click += (sender, args) => EndConversation();
            this.Controls.Add(endButton);
            choiceButtons.Add(endButton);
            endButton.BringToFront();

        }

        //player choices
        private void OnChoiceSelected(string NpcName, string nextNodeId)
        {
            if (!string.IsNullOrEmpty(nextNodeId))
            {
                DialogNode nextNode = diaLoad.GetDialogNode(NpcName, nextNodeId);
                if (nextNode != null)
                {
                    DisplayDialog(NpcName, nextNode);
                }
            }
        }

        private void EndConversation()
        {
            dialogLabel.Visible = false;
            foreach (var button in choiceButtons)
            {
                this.Controls.Remove(button);
            }
            choiceButtons.Clear();

            //ensures movement and interaction resets as it shoulds
            isInConversation = false;
            Core.IsInteracting = false;

            //clears dialog window
            dialogLabel.Text = "";
            pictureBox2.Visible = false;
        }

        private void ResetPlayerMovement()
        {
            Core.IsUp = false;
            Core.IsDown = false;
            Core.IsLeft = false;
            Core.IsRight = false;
        }

        private void TransitionToScene2()
        {
            this.Hide();
            Scene2 newScene = new Scene2(cPlayer1);
            newScene.Show();
            /*
            newScene.ShowDialog();
            this.Show(); */
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
