﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistsOfThelema
{
    public interface IgameItem
    {
        string Name { get; }
        int Id { get; }
        string Description { get; }
        int UsableTimes { get; set; }
        void Use();
    }


    public class Coin : IgameItem
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

        public void Use()
        {
            //extension
        }
    }

    public class Apple : IgameItem
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

        public void Use() //extension
        {
            cPlayer.HP += HealAmount;
            if (cPlayer.HP > 100)
            {
                cPlayer.HP = 100;
            }
        }
    }

    public class Knife : IgameItem
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

        public void Use()
        {
            //extension
        }
    }


    public class InvItem : IgameItem
    {
        public string Name { get; }
        public int Id { get; }
        public string Description { get; set; }
        public int UsableTimes { get; set; }
     
        public string Owner { get; set; }

        public virtual void Use()
        {
            UsableTimes--;
            if(UsableTimes == 0)
            {
                Description = "No longer usable";
            }
        }
    }
}
