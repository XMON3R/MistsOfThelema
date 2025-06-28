using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistsOfThelema
{
    /// <summary>
    /// Defines the contract for any interactive item within the game's inventory system.
    /// All game items must implement this interface.
    /// </summary>
    public interface IIgameItem
    {
        string Name { get; }
        int Id { get; }
        string Description { get; }
        int UsableTimes { get; set; }
        void Use();
    }

    /// <summary>
    /// Represents a Coin item in the game, a form of currency.
    /// </summary>
    public class Coin : IIgameItem
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Description { get; private set; }
        public int UsableTimes { get; set; }
        public int Value { get; private set; }

        public Coin(string name, string description, int id, int value, int usableTimes)
        {
            Name = name;
            Description = description;
            Id = id;
            UsableTimes = usableTimes;
            Value = value;
        }

        /// <summary>
        /// Defines the action when the coin is used.
        /// This method can be extended to add money to the player's currency, for example.
        /// </summary>
        public void Use()
        {
            // TODO: Implement logic for what happens when a coin is used, e.g., adding to a player's wallet.
        }
    }

    /// <summary>
    /// Represents a consumable Apple item that can heal the player.
    /// </summary>
    public class Apple : IIgameItem
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Description { get; private set; }
        public int UsableTimes { get; set; }
        public int HealAmount { get; private set; }

        public Apple(string name, string description, int id, int healA, int usableTimes)
        {
            Name = name;
            Description = description;
            Id = id;
            HealAmount = healA;
            UsableTimes = usableTimes;
        }

        /// <summary>
        /// Defines the action when the apple is used.
        /// This should increase the player's health.
        /// </summary>
        public void Use()
        {
            // TODO: Implement the healing logic. This commented-out code is a good starting point.
            /*
            // Restore player's HP by the HealAmount.
            cPlayer.HP += HealAmount;
            // Ensure the player's HP does not exceed the maximum health.
            if (cPlayer.HP > 100)
            {
                cPlayer.HP = 100;
            }
            */
            // Decrease the usable count after use. This logic is handled by CPlayer.UseItem.
        }
    }

    /// <summary>
    /// Represents a Knife item, which could be a weapon or a tool.
    /// </summary>
    public class Knife : IIgameItem
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Description { get; private set; }
        public int UsableTimes { get; set; }

        public Knife(string name, string description, int id, int usableTimes)
        {
            Name = name;
            Description = description;
            Id = id;
            UsableTimes = usableTimes;
        }

        /// <summary>
        /// Defines the action when the knife is used.
        /// This could involve attacking, cutting, or solving a puzzle.
        /// </summary>
        public void Use()
        {
            // TODO: Implement logic for using the knife, e.g., for attacking or as a tool.
        }
    }


    /// <summary>
    /// A base class for inventory items that provides a default 'Use' implementation.
    /// </summary>
    public class InvItem : IIgameItem
    {
        public string Name { get; }
        // The unique ID of the item.
        public int Id { get; }
        // The description of the item.
        public string Description { get; set; }
        // The number of times the item can be used.
        public int UsableTimes { get; set; }
        // A property to track the owner of the item, if applicable.
        public string Owner { get; set; }

        /// <summary>
        /// Decrements the usable count of the item.
        /// If the item runs out of uses, its description is updated.
        /// This method is virtual and can be overridden by derived classes for specific logic.
        /// </summary>
        public virtual void Use()
        {
            UsableTimes--;
            if (UsableTimes == 0)
            {
                Description = "No longer usable";
            }
        }
    }
}