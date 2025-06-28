// Import necessary libraries for the Windows Forms application.
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks; // Necessary for Task
using System.IO;

// Define the namespace for the application.
namespace MistsOfThelema
{
    // Define the partial class for the EndOfDay form, inheriting from Form.
    public partial class EndOfDay : Form
    {
        // Private fields to hold references to game objects and UI components.
        private CPlayer player_s2;
        private Panel ItemButtonsPanel;
        private Label ScenarioTextLabel;
        private DialogLoader diaLo;
        private Timer resultTimer;
        private System.ComponentModel.IContainer components;
        private string currentScenario;
        private Timer exitGameTimer;
        private Timer introTimer;
        private string resultText;

        // Constructor for the EndOfDay form. It takes a CPlayer object as an argument.
        public EndOfDay(CPlayer player)
        {
            InitializeComponent();
            // Store the player object passed into the constructor.
            this.player_s2 = player;

            // --- Step 1: Initialize DialogLoader ---
            diaLo = new DialogLoader();

            // --- Step 2: Subscribe to the DialogsLoaded event ---
            // Subscribe to the DialogsLoaded event. The OnDialogsLoaded method will be called when the event is triggered.
            diaLo.DialogsLoaded += OnDialogsLoaded;

            // --- Step 3: Start asynchronous dialog loading ---
            // Start loading in the background. We don't want the constructor to wait.
            // Asynchronously load dialogs from the JSON file. Using `_ =` discards the Task, so the constructor doesn't wait for it to complete.
            _ = diaLo.LoadDialogsFromJsonAsync($"resources\\dialog\\day{GameManager.CurrentDay}.json");

            // Initialize the intro timer.
            introTimer = new Timer
            {
                Interval = 5000 
            };
            introTimer.Tick += IntroTimer_Tick; 

            // Initialize the story timer (resultTimer).
            resultTimer = new Timer
            {
                Interval = 7000
            };
            resultTimer.Tick += ResultTimer_Tick; 

            // Initialize the exit game timer.
            exitGameTimer = new Timer
            {
                Interval = 8000
            };
            exitGameTimer.Tick += ExitGameTimer_Tick;

            this.FormClosing += EndOfDay_FormClosing;
        }

        // Auto-generated method for component initialization.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndOfDay));
            this.ItemButtonsPanel = new System.Windows.Forms.Panel();
            this.ScenarioTextLabel = new System.Windows.Forms.Label();
            this.resultTimer = new System.Windows.Forms.Timer(this.components);
            this.exitGameTimer = new System.Windows.Forms.Timer(this.components);
            this.introTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ItemButtonsPanel
            // 
            this.ItemButtonsPanel.BackColor = System.Drawing.Color.Black;
            this.ItemButtonsPanel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ItemButtonsPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ItemButtonsPanel.Location = new System.Drawing.Point(570, 851);
            this.ItemButtonsPanel.Name = "ItemButtonsPanel";
            this.ItemButtonsPanel.Size = new System.Drawing.Size(380, 100);
            this.ItemButtonsPanel.TabIndex = 0;
            // 
            // ScenarioTextLabel
            // 
            this.ScenarioTextLabel.AutoSize = true;
            this.ScenarioTextLabel.BackColor = System.Drawing.Color.IndianRed;
            this.ScenarioTextLabel.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ScenarioTextLabel.Location = new System.Drawing.Point(147, 675);
            this.ScenarioTextLabel.Name = "ScenarioTextLabel";
            this.ScenarioTextLabel.Size = new System.Drawing.Size(1181, 30);
            this.ScenarioTextLabel.TabIndex = 1;
            this.ScenarioTextLabel.Text = "They say strange things happen here at night... What will happen tonight?";
            // 
            // resultTimer
            // 
            this.resultTimer.Interval = 7000;
            // 
            // exitGameTimer
            // 
            this.exitGameTimer.Interval = 8000;
            // 
            // introTimer
            // 
            this.introTimer.Interval = 5000;
            // 
            // EndOfDay
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::MistsOfThelema.Properties.Resources.fireHome;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1424, 1041);
            this.Controls.Add(this.ScenarioTextLabel);
            this.Controls.Add(this.ItemButtonsPanel);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1440, 1080);
            this.MinimumSize = new System.Drawing.Size(1440, 1038);
            this.Name = "EndOfDay";
            this.Text = "EndOfDay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        // --- Step 4: Method to handle the DialogsLoaded event ---
        // This method is an event handler for when dialogs are successfully loaded.
        private void OnDialogsLoaded(bool success, string errorMessage)
        {
            // This method can be called from a thread other than the UI thread,
            // so it is necessary to check and, if necessary, move the call to the UI thread.
            // Check if the method is being called from a different thread than the UI thread.
            if (this.InvokeRequired)
            {
                // If so, marshal the call to the UI thread using Invoke.
                this.Invoke(new DialogLoader.DialogsLoadedEventHandler(OnDialogsLoaded), new object[] { success, errorMessage });
                return; // Exit the method after invoking.
            }

            // The code below will always run on the UI thread
            if (success)
            {
                // If dialogs loaded successfully, start the intro scenario.
                IntroScenario();
            }
            else
            {
                // An error occurred while loading the dialogs.
                // If there was an error, show a message box and exit the application.
                MessageBox.Show($"Dialog Error: {errorMessage}\nError Loading Dialog", "Dialog Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit(); // Exit the game
            }
        }

        // Method to start the introductory scenario.
        private void IntroScenario()
        {
            // Displays the introductory scenario text.
            // The text is the same, we just make sure it displays after the dialogs are loaded.
            // Display the introductory text for the scenario.
            ScenarioTextLabel.Text = "They say strange things happen here at night... What will happen tonight?";
            introTimer.Start(); // Start the intro timer.
        }

        // Event handler for the intro timer's Tick event.
        private void IntroTimer_Tick(object sender, EventArgs e)
        {
            introTimer.Stop(); 
            StartScenario(); 
        }

        // Method to start the main scenario for the end of the day.
        private void StartScenario()
        {
            // We check if the dialogs are loaded before trying to use them.
            // Check if dialogs have been loaded before trying to use them.
            if (diaLo.Dialogs == null)
            {
                // If not, display an error message and start the exit timer.
                ScenarioTextLabel.Text = "Dialog error?";
                // Here you can choose whether to wait, repeat, or end the game.
                // For now, we will just display a message.
                exitGameTimer.Start(); // You might consider an immediate exit if dialogs are not available
                return;
            }

            // Check the player's inventory for specific items.
            bool hasCoin = player_s2.Inventory.Any(item => item.Name == "Coin");
            bool hasKnife = player_s2.Inventory.Any(item => item.Name == "Knife");

            // Define a list of possible scenarios.
            string[] scenarios = { "killerInHouse", "peacefulSleep", "payOrDie" };

            // Select a random scenario from the list.
            Random random = new Random();
            int index = random.Next(scenarios.Length);
            currentScenario = scenarios[index];

            // Handle the "killerInHouse" scenario if the player doesn't have a knife.
            if (currentScenario == "killerInHouse" && !(hasKnife))
            {
                // Display the text for the death scenario.
                ScenarioTextLabel.Text = "You don't have anything to defend yourself. The killer is drawing near.";
                resultText = "The killer mercilessly watched the life drift from your eyes. DEAD";
                resultTimer.Start(); // Start the result timer to show the outcome.
                return; // Exit the method.
            }

            // Handle the "payOrDie" scenario if the player doesn't have a coin.
            if (currentScenario == "payOrDie" && !hasCoin)
            {
                // Display the text for the death scenario.
                ScenarioTextLabel.Text = "You don't even have a dime. You failed to satisfy the spirit.";
                resultText = "The spirits tears your body apart until only dust remains. DEAD";
                resultTimer.Start(); // Start the result timer to show the outcome.
                return; // Exit the method.
            }

            // Display the selected scenario.
            DisplayScenario(currentScenario, diaLo);
        }

        // Method to display a specific scenario from the loaded dialogs.
        private void DisplayScenario(string scenario, DialogLoader diaLo)
        {
            ItemButtonsPanel.Controls.Clear(); // Clear any existing buttons from the panel.

            // We make sure that diaLo.Dialogs is initialized before calling GetDialogNode.
            // Ensure that diaLo.Dialogs is initialized before calling GetDialogNode.
            if (diaLo.Dialogs == null)
            {
                // If not, display an error message and return.
                ScenarioTextLabel.Text = "Error: Dialogs not loaded.";
                return;
            }

            // Get the dialog node for the specified scenario.
            var scenarioNode = diaLo.GetDialogNode("dayEnd", scenario);
            if (scenarioNode == null)
            {
                // If the scenario node is not found, display an error message and return.
                ScenarioTextLabel.Text = "Scenario not found.";
                return;
            }

            // Set the scenario text label to the text from the dialog node.
            ScenarioTextLabel.Text = scenarioNode.Text;

            // Check if there are any choices for this scenario.
            if (scenarioNode.Choices != null && scenarioNode.Choices.Any())
            {
                // If there are choices, create a button for each one.
                foreach (var choice in scenarioNode.Choices)
                {
                    // Create a new Button instance.
                    Button itemButton = new Button
                    {
                        Text = choice.Value.Text, // Set the button's text to the choice's text.
                        Tag = choice.Key, // Store the choice's key in the Tag property for later use.
                        Size = new Size(100, 30) // Set the button's size.
                    };
                    itemButton.Click += ItemButton_Click; // Subscribe to the Click event.
                    ItemButtonsPanel.Controls.Add(itemButton); // Add the button to the panel.
                }
            }
            else
            {
                // If no options are available, we start the exit timer
                // If there are no choices, start the exit timer to end the day.
                exitGameTimer.Start();
            }
        }

        // Event handler for the button clicks in the ItemButtonsPanel.
        private void ItemButton_Click(object sender, EventArgs e)
        {
            // Cast the sender object back to a Button.
            Button button = (Button)sender;
            // Get the item key from the button's Tag property.
            string itemKey = (string)button.Tag;

            // Check the current scenario to handle the choice.
            if (currentScenario == "killerInHouse")
            {
                if (itemKey == "knife")
                {
                    // To make sure the result is displayed correctly
                    // If the player uses the knife, they survive.
                    resultText = "You live... For now... ALIVE";
                    resultTimer.Start(); // Start the timer to show the result.
                    DisplayScenario("killerResolved", diaLo); // Displays the resolution text
                }
                else
                {
                    // If another item is used, the player dies.
                    resultText = "The killer laughed at you and stabbed you to death. DEAD";
                    // Show a game over message and exit the application.
                    MessageBox.Show($"You survived for {GameManager.CurrentDay - 1} days.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0); //Exit
                }
            }
            else if (currentScenario == "payOrDie")
            {
                if (itemKey == "coin")
                {
                    // To make sure the result is displayed correctly
                    // If the player uses the coin, they survive.
                    resultText = "You live... For now... ALIVE";
                    resultTimer.Start();
                    DisplayScenario("payResolved", diaLo); // Displays the resolution text
                }
                else
                {
                    resultText = "There is nothing else that the spirit wanted. DEAD";
                    MessageBox.Show($"You survived for {GameManager.CurrentDay - 1} days.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0); //Exit
                }
            }
            else if (currentScenario == "peacefulSleep")
            {
                // For "peacefulSleep" there is no choice, so it falls here directly
                // after the scenario starts.
                // For "peacefulSleep" there is no choice, so it falls here directly after starting the scenario.
                resultText = "You sleep peacefully through the night. ALIVE";
                resultTimer.Start();
            }
        }

        // Event handler for the result timer's Tick event.
        private void ResultTimer_Tick(object sender, EventArgs e)
        {
            resultTimer.Stop(); // Stop the timer.
            ScenarioTextLabel.Text = resultText; // Display the final result
            exitGameTimer.Start(); // Start the exit timer to either close the game or proceed to the next day.
        }

        // Event handler for the exit game timer's Tick event.
        private void ExitGameTimer_Tick(object sender, EventArgs e)
        {
            exitGameTimer.Stop(); // Stop the timer.

            // Check if the player's HP is 0, indicating they died.
            if (player_s2.HP == 0)
            {
                Environment.Exit(0); // Player is dead, exit the game
            }
            else
            {
                // Increment the global day counter for the next day.
                GameManager.CurrentDay++;

                // Construct the path for the next day's dialog file.
                string nextDayDialogPath = Path.Combine(Application.StartupPath, $"resources\\dialog\\day{GameManager.CurrentDay}.json");

                // Check if the JSON file for the next day exists.
                if (File.Exists(nextDayDialogPath))
                {
                    // If it exists, proceed to the next day.
                    NextDay nextDayScene = new NextDay(player_s2); // Pass the player object to the next scene.
                    nextDayScene.Show(); // Show the next day's form.
                    this.Close(); // Close the current form.
                }
                else
                {
                    // If the file does not exist, it means there are no more days defined.
                    MessageBox.Show($"You did it! You survived all {GameManager.CurrentDay - 1} days.", "Win", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit(); // Exit the application.
                }
            }
        }

        /// <summary>
        /// Handles the FormClosing event to stop and dispose of all timers used in this form.
        /// </summary>
        private void EndOfDay_FormClosing(object sender, FormClosingEventArgs e)
        {
            // It's good practice to check if the timer objects exist before trying to stop them.
            // Stop the intro timer.
            if (introTimer != null)
            {
                introTimer.Stop();
                introTimer.Dispose();
                introTimer = null; // Set to null to indicate it's been disposed.
            }

            // Stop the result timer.
            if (resultTimer != null)
            {
                resultTimer.Stop();
                resultTimer.Dispose();
                resultTimer = null;
            }

            // Stop the exit game timer.
            if (exitGameTimer != null)
            {
                exitGameTimer.Stop();
                exitGameTimer.Dispose();
                exitGameTimer = null;
            }
        }
    }
}