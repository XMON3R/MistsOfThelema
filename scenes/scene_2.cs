using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MistsOfThelema
{
    public partial class Scene2 : Form
    {
        private cPlayer player_s2;
        private Panel ItemButtonsPanel;
        private Label ScenarioTextLabel;
        private DialogLoader diaLo;
        private Timer resultTimer;
        private System.ComponentModel.IContainer components;
        private string currentScenario;
        private Timer exitGameTimer;
        private Timer introTimer;
        private string resultText;

        public Scene2(cPlayer player)
        {
            InitializeComponent();
            this.player_s2 = player;

            // Initialize DialogLoader
            diaLo = new DialogLoader();
            diaLo.LoadDialogFromJson("..\\..\\resources\\dialog\\day1.json");

            //intro timer
            introTimer = new Timer
            {
                Interval = 5000
            };
            introTimer.Tick += IntroTimer_Tick;

            //story timer
            resultTimer = new Timer
            {
                Interval = 7000
            };
            resultTimer.Tick += ResultTimer_Tick;

            //exit timer
            exitGameTimer = new Timer
            {
                Interval = 8000
            };
            exitGameTimer.Tick += ExitGameTimer_Tick;

            // Start the intro scenario
            IntroScenario();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene2));
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
            // Scene2
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
            this.Name = "Scene2";
            this.Text = "Scene2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void IntroScenario()
        {
            ScenarioTextLabel.Text = "They say strange things happen here at night... What will happen tonight?";
            introTimer.Start();
        }

        private void IntroTimer_Tick(object sender, EventArgs e)
        {
            introTimer.Stop();
            StartScenario();
        }

        private void StartScenario()
        {
            bool hasCoin = player_s2.Inventory.Any(item => item.Name == "Coin");
            bool hasKnife = player_s2.Inventory.Any(item => item.Name == "Knife");

            string[] scenarios = { "killerInHouse", "peacefulSleep", "payOrDie" };

            Random random = new Random();
            int index = random.Next(scenarios.Length);
            currentScenario = scenarios[index];

            if (currentScenario == "killerInHouse" && !(hasKnife))
            {
                ScenarioTextLabel.Text = "You don't have anything to defend yourself. The killer is drawing near.";
                resultText = "The killer mercilessly watched the life drift from your eyes. DEAD";
                resultTimer.Start();
                return;
            }

            if (currentScenario == "payOrDie" && !hasCoin)
            {
                ScenarioTextLabel.Text = "You don't even have a dime. You failed to satisfy the spirit.";
                resultText = "The spirits tears your body apart until only dust remains. DEAD";
                resultTimer.Start();
                return;
            }

            DisplayScenario(currentScenario, diaLo);
        }

        private void DisplayScenario(string scenario, DialogLoader diaLo)
        {
            ItemButtonsPanel.Controls.Clear();

            var scenarioNode = diaLo.GetDialogNode("dayEnd", scenario);
            if (scenarioNode == null)
            {
                ScenarioTextLabel.Text = "Scenario not found.";
                return;
            }

            ScenarioTextLabel.Text = scenarioNode.text;

            if (scenarioNode.choices != null && scenarioNode.choices.Any())
            {
                foreach (var choice in scenarioNode.choices)
                {
                    Button itemButton = new Button
                    {
                        Text = choice.Value.text,
                        Tag = choice.Key,
                        Size = new Size(100, 30)
                    };
                    itemButton.Click += ItemButton_Click;
                    ItemButtonsPanel.Controls.Add(itemButton);
                }
            }
            else
            {
                exitGameTimer.Start();
                //ScenarioTextLabel.Text += " No choices available.";
            }
        }

        private void ItemButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string itemKey = (string)button.Tag;

            if (currentScenario == "killerInHouse")
            {
                if (itemKey == "knife")
                {
                    resultText = "You live... For now... ALIVE";
                    resultTimer.Start();
                    DisplayScenario("killerResolved", diaLo);
                }
                else
                {
                    resultText = "The killer laughed at you and stabbed you to death. DEAD";
                    resultTimer.Start();
                }
            }
            else if (currentScenario == "payOrDie")
            {
                if (itemKey == "coin")
                {
                    resultText = "You live... For now... ALIVE";
                    resultTimer.Start();
                    DisplayScenario("payResolved", diaLo);
                }
                else
                {
                    resultText = "There is nothing else that the spirit wanted. DEAD";
                    resultTimer.Start();
                }
            }
            else if (currentScenario == "peacefulSleep")
            {
                resultText = "You sleep peacefully through the night. ALIVE";
                resultTimer.Start();
            }
        }

        private void ResultTimer_Tick(object sender, EventArgs e)
        {
            resultTimer.Stop();
            ScenarioTextLabel.Text = resultText;
            exitGameTimer.Start();
        }

        private void ExitGameTimer_Tick(object sender, EventArgs e)
        {
            exitGameTimer.Stop();
            Application.Exit();
        }
    }
}
