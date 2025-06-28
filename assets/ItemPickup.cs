using System;
using System.Drawing;
using System.Windows.Forms;

namespace MistsOfThelema
{
    // ItemPickup represents a visual item on the map that the player can pick up.
    public class ItemPickup : UserControl, IInteractable
    {
        public IIgameItem Item { get; set; }

        private PictureBox itemPictureBox;

        public ItemPickup()
        {
            InitializeComponent();
        }

        public string InstanceName
        {
            get { return Item?.Name ?? "Item"; } // Returns the item's name if available, otherwise "Item".
            set { }
        }

        public Rectangle GetBounds()
        {
            return this.Bounds;
        }

        public Image Image
        {
            get { return itemPictureBox.Image; }
            set { itemPictureBox.Image = value; }
        }

        private void InitializeComponent()
        {
            this.itemPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.itemPictureBox)).BeginInit();
            this.SuspendLayout();

            // 
            // itemPictureBox
            // 
            this.itemPictureBox.Dock = DockStyle.Fill;
            this.itemPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.itemPictureBox.Location = new Point(0, 0);
            this.itemPictureBox.Name = "itemPictureBox";
            this.itemPictureBox.TabStop = false;

            // 
            // ItemPickup
            // 
            this.Controls.Add(this.itemPictureBox);
            this.Name = "ItemPickup";
            this.BackColor = Color.Transparent;
            this.Size = new Size(50, 50);
            ((System.ComponentModel.ISupportInitialize)(this.itemPictureBox)).EndInit();
            this.ResumeLayout(false);
        }
    }
}