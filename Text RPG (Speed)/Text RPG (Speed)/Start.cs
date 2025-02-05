using System;
using System.Collections.Generic;
using Text_RPG__Speed_.Script;



class Start
{
    static void Main()
    {
        Game.Start();
    }
}




public struct ItemInfo
{
    public string Name;
    public int AttackBoost;
    public int DefenseBoost;
    public int Price;
    public ItemType Type;

    public ItemInfo(string name, int attackBoost, int defenseBoost, int price, ItemType type)
    {
        Name = name;
        AttackBoost = attackBoost;
        DefenseBoost = defenseBoost;
        Price = price;
        Type = type;
    }
}


