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
        //private Timer gameTimer;
        //private int timeRemaining;
        //private const int dayDuration = 60000; // Délka dne v milisekundách (např. 60 sekund)

        //private PictureBox playerPictureBox;

        public Scene1()
        {
            InitializeComponent();

            //InitializeGame();
        }

        /*
        private void InitializeGame()
        {
            
            gameTimer = new Timer();
            gameTimer.Interval = 1000; 
            gameTimer.Tick += new EventHandler(OnTimerTick);
            timeRemaining = dayDuration / 1000;
            gameTimer.Start();

            InitializePlayer();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            timeRemaining--;
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                gameTimer.Stop();
                SwitchToEndOfDayScene();
            }
        }

        private void UpdateTimerDisplay()
        {
            this.Text = $"Zbývající čas: {timeRemaining} sekund";
        }

        private void InitializePlayer()
        {
            playerPictureBox = new PictureBox();
            playerPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            playerPictureBox.Location = new Point(player.X, player.Y); // Počáteční pozice hráče

            string imagePath = @"..\..\resources\defPlayer.png";
            playerPictureBox.Image = Image.FromFile(imagePath);

            
            //playerPictureBox.Image = Image.FromFile("MistsOfThelema/resources/defPlayer.png");
           
            this.Controls.Add(playerPictureBox);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            player.KeyDown(e.KeyCode); // Update key state
            MovePlayer();              // Update player's position based on key state
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            player.KeyUp(e.KeyCode); // Update key state
        }

        private void MovePlayer()
        {
            // Update player's position
            if (player.IsUp) player.Y -= player.Speed;
            if (player.IsDown) player.Y += player.Speed;
            if (player.IsLeft) player.X -= player.Speed;
            if (player.IsRight) player.X += player.Speed;

            // Update player's visual position on the form
            playerPictureBox.Location = new Point(player.X, player.Y);
        }

        private void SwitchToEndOfDayScene()
        {
            MessageBox.Show("Konec dne!");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Scene1
            // 
            this.ClientSize = new System.Drawing.Size(716, 428);
            this.Name = "Scene1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scene1_KeyDown);
            this.ResumeLayout(false);

        }
        */


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Scene1
            // 
            this.ClientSize = new System.Drawing.Size(716, 428);
            this.Name = "Scene1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scene1_KeyDown);
            this.ResumeLayout(false);

        }

        private void Scene1_KeyDown(object sender, KeyEventArgs e)
        {
            player.KeyDown(e.KeyCode); // Update key state
            //MovePlayer();
        }
    }
}
