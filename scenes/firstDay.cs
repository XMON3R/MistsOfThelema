using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks; // Potřebné pro Task
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Numerics;


namespace MistsOfThelema
{
    public partial class FirstDay : Form
    {
        private Label interactLabel;
        private Label dialogLabel;
        private Label playInventory;

        private PictureBox pictureBox1;
        private PictureBox dialogBox;

        private Timer collisionTimer;
        private Timer afterDialogTimer;
        private Timer endOfDayTimer;

        private Npc npc1;
        private Npc weirdMan;
        private Npc shopkeeper;

        private Houses playerExitHouse;

        private CPlayer cPlayer1;

        private List<string> talkedToList = new List<string>();
        private List<Button> choiceButtons = new List<Button>();
        private List<IInteractable> interactables;

        private DialogLoader diaLoad;

        private System.ComponentModel.IContainer components;

        private bool isInConversation = false;

        public FirstDay()
        {
            InitializeComponent();
            // cPlayer1.PlayerMoved += CPlayer1_PlayerMoved; // Zakomentováno, jelikož není v poskytnutém kódu definováno

            // --- Krok 1: Inicializace DialogLoaderu ---
            diaLoad = new DialogLoader();

            // --- Krok 2: Přihlášení k události DialogsLoaded ---
            diaLoad.DialogsLoaded += OnDialogsLoaded;

            // --- Krok 3: Spuštění asynchronního načítání dialogů ---
            // Namísto synchronního volání, teď voláme asynchronní metodu.
            // Použijeme '_' pro potlačení varování, že Task není awaitován,
            // protože chceme, aby se načítání provedlo na pozadí.

            //_ = diaLoad.LoadDialogsFromJsonAsync("..\\..\\resources\\dialog\\day1.json");
            _ = diaLoad.LoadDialogsFromJsonAsync("resources\\dialog\\day1.json");

            // Volání InitializeInteractables by mělo být až PO inicializaci všech komponent.
            // V tvém kódu se volá před přiřazením objektů k npc1, weirdMan atd.
            // Proto jsem přesunul volání na konec konstruktoru.
            // Nechávám to zde, ale ideálně by InitializeInteractables mělo být voláno po new npc() atd.
            InitializeInteractables();

            choiceButtons = new List<Button>();
            talkedToList = new List<string>();

            afterDialogTimer = new Timer
            {
                Interval = 2000
            };
            afterDialogTimer.Tick += DialogTimer_Tick;

            endOfDayTimer = new Timer
            {
                Interval = 300000
            };
            endOfDayTimer.Tick += EndOfDay_Tick;
            endOfDayTimer.Start();

            // Tyto řádky by měly být volány až po inicializaci komponent v InitializeComponent()
            // a po inicializaci 'npc1', 'weirdMan', 'playerExitHouse', 'shopkeeper'
            // což se děje v InitializeComponent. Takže toto je správné místo.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstDay));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.interactLabel = new System.Windows.Forms.Label();
            this.collisionTimer = new System.Windows.Forms.Timer(this.components);
            this.dialogLabel = new System.Windows.Forms.Label();
            this.dialogBox = new System.Windows.Forms.PictureBox();
            this.afterDialogTimer = new System.Windows.Forms.Timer(this.components);
            this.endOfDayTimer = new System.Windows.Forms.Timer(this.components);
            this.playInventory = new System.Windows.Forms.Label();
            this.shopkeeper = new MistsOfThelema.Npc(); // Inicializace shopkeeper
            this.weirdMan = new MistsOfThelema.Npc();   // Inicializace weirdMan
            this.cPlayer1 = new MistsOfThelema.CPlayer(); // Inicializace cPlayer1
            this.npc1 = new MistsOfThelema.Npc();       // Inicializace npc1
            this.playerExitHouse = new MistsOfThelema.Houses(); // Inicializace playerExitHouse
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
            this.collisionTimer.Tick += new System.EventHandler(this.CollisionTimer_Tick);
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
            this.playInventory.Click += new System.EventHandler(this.PlayInventory_Click);
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

        // --- Krok 4: Metoda pro zpracování události DialogsLoaded ---
        private void OnDialogsLoaded(bool success, string errorMessage)
        {
            // !!! DŮLEŽITÉ !!!
            // Tato metoda může být volána z jiného vlákna (než UI vlákno),
            // proto je nutné ověřit a případně přesunout volání na UI vlákno.
            if (this.InvokeRequired)
            {
                this.Invoke(new DialogLoader.DialogsLoadedEventHandler(OnDialogsLoaded), new object[] { success, errorMessage });
                return;
            }

            // Kód níže se spustí vždy na UI vlákně
            if (success)
            {
                // Dialogy byly úspěšně načteny. Nyní můžeš bezpečně přistupovat k diaLoad.Dialogs
                // a pokračovat v inicializaci hry, která závisí na dialozích.
                // Například, pokud bys měl nějakou úvodní dialogovou sekvenci,
                // mohl bys ji spustit zde.
                //MessageBox.Show("Dialogy byly úspěšně načteny!", "Načítání dokončeno", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Můžeš zde třeba skrýt načítací obrazovku, pokud ji máš.
                // Povolit interakce, které závisí na načtených dialozích.
            }
            else
            {
                // Došlo k chybě při načítání dialogů.
                MessageBox.Show($"Dialog Error: {errorMessage}\nError Loading Dialog", "Dialog Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Zde můžeš implementovat logiku pro zpracování chyby, např.
                // - Zobrazit chybovou zprávu a ukončit aplikaci.
                // - Načíst záložní dialogy.
                // - Umožnit hráči pokračovat bez dialogů (pokud je to možné).
                Application.Exit(); // Příklad: Ukončení hry v případě kritické chyby
            }
        }


        //add to interactables so it can detect and start dialogs
        private void InitializeInteractables()
        {
            // Ujistěte se, že objekty playerExitHouse, npc1, weirdMan, shopkeeper
            // jsou již inicializovány (což jsou v InitializeComponent).
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

            if (e.KeyCode == Core.Inventory)
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
                    if (interactable.InstanceName == "Your House")
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
                if (!collision) // Toto by mělo být vně cyklu, aby se label skryl až po kontrole všech objektů
                {
                    interactLabel.Visible = false;
                }
            }
            // Opravená logika pro skrytí interactLabelu
            if (!collision)
            {
                interactLabel.Visible = false;
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
        private void HandleInteraction(CPlayer player)
        {
            foreach (var interactable in interactables)
            {
                Rectangle playerBounds = cPlayer1.GetBounds();
                Rectangle interactableBounds = interactable.GetBounds();

                Rectangle expandedBounds = ExpandBoundsByRadius(interactableBounds, 50);

                //your house scenario
                if (playerBounds.IntersectsWith(expandedBounds) && interactable.InstanceName == "Your House")
                {
                    TransitionToEndOfDay();
                    break;
                }

                //check if intersected object is npc (whether to start conversation or not)
                if (playerBounds.IntersectsWith(expandedBounds) && interactable is Npc)
                {
                    //do not repeat conversations on the same day
                    if (talkedToList.Contains(interactable.InstanceName))
                    {
                        dialogLabel.Visible = true;
                        dialogBox.Visible = true;
                        dialogLabel.Text = "Sorry, nothing left to say.";
                        afterDialogTimer.Start();

                        if (interactable.InstanceName == "Weird man")
                        {
                            //possible extension: given in second "secret" dialog
                            player.AddItem(new Knife("Knife", "A basic knife.", 3, 4));
                            UpdateInventoryList();
                        }
                        return;
                    }

                    else
                    {
                        isInConversation = true;
                        ResetPlayerMovement();

                        StartConversationWith(interactable.InstanceName, diaLoad);
                        break;
                    }
                }

                if (playerBounds.IntersectsWith(expandedBounds) && interactable is Houses && interactable.InstanceName != "Your House") // Added check for other houses
                {
                    //extension: somehow interact with other houses -> maybe stealing, introduce karma system for kills and stolen goods
                    // Zde by mohla být logika pro interakci s jinými domy
                    // Například: dialogLabel.Text = "Seems locked."; afterDialogTimer.Start();
                }
            }
        }

        //start a conversation with
        private void StartConversationWith(string NpcName, DialogLoader dl)
        {
            // Před spuštěním konverzace je dobré zkontrolovat, zda jsou dialogy načteny
            if (dl.Dialogs == null)
            {
                dialogLabel.Visible = true;
                dialogBox.Visible = true;
                dialogLabel.Text = "Dialog error?";
                afterDialogTimer.Start();
                isInConversation = false; // Umožnit hráči pohyb
                return;
            }

            DialogNode introNode = dl.GetDialogNode(NpcName, "intro");
            if (introNode != null)
            {
                DisplayDialog(NpcName, introNode);
            }
            else
            {
                // Pokud pro dané NPC neexistuje "intro" uzel
                dialogLabel.Visible = true;
                dialogBox.Visible = true;
                dialogLabel.Text = $"Cannot find dialog for {NpcName}.";
                afterDialogTimer.Start();
                isInConversation = false;
            }
        }

        // Method to display a dialog node
        private void DisplayDialog(string NpcName, DialogNode node)
        {
            dialogBox.Visible = true;

            dialogLabel.Visible = true;
            dialogLabel.Text = node.Text;

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
            if (node.Choices != null) // Zkontrolujte, zda existují volby
            {
                foreach (var choice in node.Choices)
                {
                    Button choiceButton = new Button
                    {
                        //choiceButton.BringToFront();
                        Text = choice_number++ + ") " + choice.Value.Text,
                        Location = new Point(dialogLabel.Left, yPosition),
                        AutoSize = true,
                        Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)))
                    };
                    choiceButton.Click += (sender, args) => OnChoiceSelected(NpcName, choice.Value.Next);
                    this.Controls.Add(choiceButton);
                    choiceButtons.Add(choiceButton);
                    choiceButton.BringToFront();
                    yPosition += choiceButton.Height + 5;
                }
            }


            //end button 
            Button endButton = new Button
            {
                Text = "---- End Conversation",
                Location = new Point(dialogLabel.Left, yPosition),
                AutoSize = true,
                Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)))
            };
            endButton.Click += (sender, args) => EndConversation();
            this.Controls.Add(endButton);
            choiceButtons.Add(endButton);
            endButton.BringToFront();

            //ensure you cant repeat the conversation
            if (!talkedToList.Contains(NpcName)) // Přidáno ověření, aby se NPC nepřidávalo opakovaně
            {
                talkedToList.Add(NpcName);
            }
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
                else
                {
                    // Pokud nextNodeId vede na neexistující uzel
                    dialogLabel.Text = "Chyba dialogu: Následující uzel neexistuje.";
                    // Můžeš se rozhodnout konverzaci ukončit nebo zobrazit jen tuto zprávu
                }
            }
            else
            {
                // Pokud nextNodeId je prázdné, znamená to konec větve dialogu
                EndConversation();
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
        private void AddItemToPlayer(IIgameItem item) // Změnil jsem na IgameItem, předpokládám, že takový interface máš
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

        private void PlayerStarterInventory(CPlayer player)
        {
            var coin = new Coin("Coin", "A shiny gold coin.", 1, 10, 1);
            var apple = new Apple("Apple", "Restores full health.", 2, 100, 1);

            player.AddItem(coin);
            player.AddItem(apple);
        }

        private void TransitionToEndOfDay()
        {
            this.Hide();
            EndOfDay newScene = new EndOfDay(cPlayer1);
            newScene.Show();
            /*
            newScene.ShowDialog();
            this.Show(); */
        }

        private void CollisionTimer_Tick(object sender, EventArgs e)
        {
            CheckCollision();
        }

        private void DialogTimer_Tick(object sender, EventArgs e)
        {
            dialogLabel.Visible = false;
            dialogBox.Visible = false;
            afterDialogTimer.Stop();
        }

        private void EndOfDay_Tick(object sender, EventArgs e)
        {
            TransitionToEndOfDay();
        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void PlayInventory_Click(object sender, EventArgs e)
        {
            playInventory.Visible = false;
        }
    }
}