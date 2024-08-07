using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Numerics;


namespace MistsOfThelema
{
    public partial class Scene1 : Form
    {
        private Label interactLabel;
        private Label dialogLabel;
        private Label playInventory;

        private PictureBox pictureBox1;
        private PictureBox dialogBox;

        private Timer collisionTimer;
        private Timer afterDialogTimer;
        private Timer endOfDayTimer;

        private npc npc1;
        private npc weirdMan;
        private npc shopkeeper;

        private Houses playerExitHouse;

        private cPlayer cPlayer1;

        private List<string> talkedToList = new List<string>();
        private List<Button> choiceButtons = new List<Button>();
        private List<IInteractable> interactables;

        private DialogLoader diaLoad;

        private System.ComponentModel.IContainer components;

        private bool isInConversation = false;

        public Scene1()
        {
            InitializeComponent();
            InitializeInteractables();
            //cPlayer1.PlayerMoved += CPlayer1_PlayerMoved;

            diaLoad = new DialogLoader();
            diaLoad.LoadDialogFromJson("..\\..\\resources\\dialog\\day1.json");

            choiceButtons = new List<Button>();
            talkedToList = new List<string>();

            afterDialogTimer = new Timer();
            afterDialogTimer.Interval = 2000;
            afterDialogTimer.Tick += dialogTimer_Tick;

            endOfDayTimer = new Timer();
            endOfDayTimer.Interval = 300000;
            endOfDayTimer.Tick += endOfDay_Tick;
            endOfDayTimer.Start();

            npc1.InstanceName = "Theo";
            weirdMan.InstanceName = "Weird man";
            playerExitHouse.InstanceName = "Your House";
            shopkeeper.InstanceName = "Shopkeeper";

            PlayerStarterInventory(cPlayer1);
            UpdateInventoryList();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.interactLabel = new System.Windows.Forms.Label();
            this.collisionTimer = new System.Windows.Forms.Timer(this.components);
            this.dialogLabel = new System.Windows.Forms.Label();
            this.dialogBox = new System.Windows.Forms.PictureBox();
            this.afterDialogTimer = new System.Windows.Forms.Timer(this.components);
            this.endOfDayTimer = new System.Windows.Forms.Timer(this.components);
            this.playInventory = new System.Windows.Forms.Label();
            this.shopkeeper = new MistsOfThelema.npc();
            this.weirdMan = new MistsOfThelema.npc();
            this.cPlayer1 = new MistsOfThelema.cPlayer();
            this.npc1 = new MistsOfThelema.npc();
            this.playerExitHouse = new MistsOfThelema.Houses();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dialogBox)).BeginInit();
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
            // 
            // interactLabel
            // 
            this.interactLabel.AutoSize = true;
            this.interactLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.interactLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.interactLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.interactLabel.Location = new System.Drawing.Point(526, 854);
            this.interactLabel.Name = "interactLabel";
            this.interactLabel.Size = new System.Drawing.Size(348, 18);
            this.interactLabel.TabIndex = 6;
            this.interactLabel.Text = "Interact with ------ by pressing E";
            this.interactLabel.Visible = false;
            // 
            // collisionTimer
            // 
            this.collisionTimer.Enabled = true;
            this.collisionTimer.Interval = 50;
            this.collisionTimer.Tick += new System.EventHandler(this.collisionTimer_Tick);
            // 
            // dialogLabel
            // 
            this.dialogLabel.AutoSize = true;
            this.dialogLabel.BackColor = System.Drawing.Color.SeaGreen;
            this.dialogLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dialogLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dialogLabel.Location = new System.Drawing.Point(227, 889);
            this.dialogLabel.Name = "dialogLabel";
            this.dialogLabel.Size = new System.Drawing.Size(118, 18);
            this.dialogLabel.TabIndex = 9;
            this.dialogLabel.Text = "dialogLabel";
            this.dialogLabel.Visible = false;
            // 
            // dialogBox
            // 
            this.dialogBox.BackColor = System.Drawing.Color.Black;
            this.dialogBox.Location = new System.Drawing.Point(-5, 875);
            this.dialogBox.Name = "dialogBox";
            this.dialogBox.Size = new System.Drawing.Size(1439, 227);
            this.dialogBox.TabIndex = 11;
            this.dialogBox.TabStop = false;
            this.dialogBox.Visible = false;
            // 
            // playInventory
            // 
            this.playInventory.AutoSize = true;
            this.playInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.playInventory.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playInventory.Location = new System.Drawing.Point(696, 152);
            this.playInventory.Name = "playInventory";
            this.playInventory.Size = new System.Drawing.Size(178, 18);
            this.playInventory.TabIndex = 14;
            this.playInventory.Text = "PLAYER INVENTORY:";
            this.playInventory.Click += new System.EventHandler(this.playInventory_Click);
            this.playInventory.Visible = false;
            // 
            // shopkeeper
            // 
            this.shopkeeper.BackColor = System.Drawing.Color.Transparent;
            this.shopkeeper.InstanceName = "Shopkeeper";
            this.shopkeeper.Location = new System.Drawing.Point(602, 703);
            this.shopkeeper.Name = "shopkeeper";
            this.shopkeeper.Size = new System.Drawing.Size(70, 88);
            this.shopkeeper.TabIndex = 13;
            // 
            // weirdMan
            // 
            this.weirdMan.BackColor = System.Drawing.Color.Transparent;
            this.weirdMan.InstanceName = null;
            this.weirdMan.Location = new System.Drawing.Point(207, 364);
            this.weirdMan.Name = "weirdMan";
            this.weirdMan.Size = new System.Drawing.Size(70, 88);
            this.weirdMan.TabIndex = 12;
            // 
            // cPlayer1
            // 
            this.cPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.cPlayer1.Location = new System.Drawing.Point(34, 841);
            this.cPlayer1.Name = "cPlayer1";
            this.cPlayer1.Size = new System.Drawing.Size(61, 86);
            this.cPlayer1.TabIndex = 0;
            this.cPlayer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.cPlayer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.cPlayer1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
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
            this.Controls.Add(this.playInventory);
            this.Controls.Add(this.shopkeeper);
            this.Controls.Add(this.weirdMan);
            this.Controls.Add(this.dialogLabel);
            this.Controls.Add(this.interactLabel);
            this.Controls.Add(this.cPlayer1);
            this.Controls.Add(this.npc1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playerExitHouse);
            this.Controls.Add(this.dialogBox);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1440, 1080);
            this.MinimumSize = new System.Drawing.Size(1440, 1038);
            this.Name = "Scene1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dialogBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //add to interactables so it can detect and start dialogs
        private void InitializeInteractables()
        {
            interactables = new List<IInteractable>
            {
                playerExitHouse,
                npc1,
                weirdMan,
                shopkeeper
            };
        }

        //player movement and interaction
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
                HandleInteraction(cPlayer1);
            }

            if(e.KeyCode == Core.Inventory)
            {
                playInventory.Visible = true;
            }
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
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        //player-interactable collision detection
        private void CheckCollision()
        {
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

                    //special case for house that exits to scene 2
                    if(interactable.InstanceName == "Your House")
                    {
                        interactLabel.Text = $"Press E to END THE DAY by entering {interactable.InstanceName}";
                    }

                    else
                    {
                        interactLabel.Text = $"Press E to interact with {interactable.InstanceName}";
                    }
                    
                    collision = true;
                    break;
                }

                //otherwise hide interact label
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

        //for interaction (E is clicked)
        private void HandleInteraction(cPlayer player)
        {
            foreach (var interactable in interactables)
            {
                Rectangle playerBounds = cPlayer1.GetBounds();
                Rectangle interactableBounds = interactable.GetBounds();

                Rectangle expandedBounds = ExpandBoundsByRadius(interactableBounds, 50);

                //your house scenario
                if (playerBounds.IntersectsWith(expandedBounds) && interactable.InstanceName == "Your House")
                {
                    TransitionToScene2();
                    break;
                }

                //check if intersected object is npc (whether to start conversation or not)
                if (playerBounds.IntersectsWith(expandedBounds) && interactable is npc)
                {
                    //do not repeat conversations on the same day
                    if (talkedToList.Contains(interactable.InstanceName))
                    {
                        dialogLabel.Visible = true;
                        dialogBox.Visible = true;
                        dialogLabel.Text = "Sorry, nothing left to say.";
                        afterDialogTimer.Start();
                        
                        if(interactable.InstanceName == "Weird man")
                        {
                            //possible extension: given in second "secret" dialog
                            player.AddItem(new Knife("Knife", "A basic knife.", 3, 4));
                            UpdateInventoryList();
                        }
                        return;
                    }

                    else
                    {
                        //StartConversationWith("Theo", diaLoad);
                        isInConversation = true;
                        ResetPlayerMovement();

                        StartConversationWith(interactable.InstanceName, diaLoad);
                        break;
                    }
                }

                if (playerBounds.IntersectsWith(expandedBounds) && interactable is Houses)
                {
                    //extension: somehow interact with other houses -> maybe stealing, introduce karma system for kills and stolen goods
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
            dialogBox.Visible = true;

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

            //numbers for choices
            int choice_number = 1;

            //iterate over choices from json dialog file and display them
            foreach (var choice in node.choices)
            {
                Button choiceButton = new Button();
                //choiceButton.BringToFront();
                choiceButton.Text = choice_number++ + ") " + choice.Value.text;
                choiceButton.Location = new Point(dialogLabel.Left, yPosition);
                choiceButton.AutoSize = true;
                choiceButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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
            endButton.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            endButton.Click += (sender, args) => EndConversation();
            this.Controls.Add(endButton);
            choiceButtons.Add(endButton);
            endButton.BringToFront();

            //ensure you cant repeat the conversation
            talkedToList.Add(NpcName);

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
            dialogBox.Visible = false;
        }

        //ensures player does not move during dialog
        private void ResetPlayerMovement()
        {
            Core.IsUp = false;
            Core.IsDown = false;
            Core.IsLeft = false;
            Core.IsRight = false;
        }


        //inventory functions bellow
        private void AddItemToPlayer(IgameItem item)
        {
            cPlayer1.AddItem(item);
            UpdateInventoryList();
        }

        private void UpdateInventoryList()
        {
            StringBuilder inventoryText = new StringBuilder();

            inventoryText.AppendLine("Your inventory: (click to close)");

            foreach (var item in cPlayer1.Inventory)
            {
                inventoryText.AppendLine($"{item.Name} - {item.Description}");
            }

            playInventory.Text = inventoryText.ToString();
        }
       
        private void PlayerStarterInventory(cPlayer player)
        {
            var coin = new Coin("Coin", "A shiny gold coin.", 1, 10, 1);
            var apple = new Apple("Apple", "Restores full health.", 2, 100,1);

            player.AddItem(coin);
            player.AddItem(apple);
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

        private void collisionTimer_Tick(object sender, EventArgs e)
        {
            CheckCollision();
        }

        private void dialogTimer_Tick(object sender, EventArgs e)
        {
            dialogLabel.Visible = false;
            dialogBox.Visible = false;
            afterDialogTimer.Stop();
        }

        private void endOfDay_Tick(object sender, EventArgs e)
        {
            TransitionToScene2();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void playInventory_Click(object sender, EventArgs e)
        {
            playInventory.Visible = false;
        }
    }
}
