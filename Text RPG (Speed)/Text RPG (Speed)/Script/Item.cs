using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Item
    {
        public static List<ItemInfo> ItemList { get; private set; } = new List<ItemInfo>
    {
        new ItemInfo("자동소총", 20, 0, 700, ItemType.Weapon),
        new ItemInfo("기관단총", 15, 0, 500, ItemType.Weapon),
        new ItemInfo("자동권총", 10, 0, 300, ItemType.Weapon),
        new ItemInfo("리볼버", 5, 0, 100, ItemType.Weapon),
        new ItemInfo("방탄플레이트", 0, 15, 600, ItemType.Armor),
        new ItemInfo("전투복", 0, 10, 300, ItemType.Armor),
        new ItemInfo("운동복", 0, 3, 100, ItemType.Armor),
        new ItemInfo("붕대", 0, 0, 100, ItemType.Healing),
        new ItemInfo("진통제", 0, 0, 200, ItemType.Healing),
        new ItemInfo("수술키트", 0, 0, 300, ItemType.Healing)
    };

        public string Name { get; private set; }
        public int AttackBoost { get; private set; }
        public int DefenseBoost { get; private set; }
        public int Price { get; private set; }
        public ItemType Type { get; private set; }

        public Item(string name, int attackBoost, int defenseBoost, int price, ItemType type)
        {
            Name = name;
            AttackBoost = attackBoost;
            DefenseBoost = defenseBoost;
            Price = price;
            Type = type;
        }

        public Item(ItemInfo itemInfo)
        {
            Name = itemInfo.Name;
            AttackBoost = itemInfo.AttackBoost;
            DefenseBoost = itemInfo.DefenseBoost;
            Price = itemInfo.Price;
            Type = itemInfo.Type;
        }

        public static implicit operator string(Item item)
        {
            return item.Name; // Item의 Name을 반환
        }

        public static bool operator ==(Item item1, Item item2)
        {
            if (ReferenceEquals(item1, item2)) return true;
            if (item1 is null || item2 is null) return false;
            return item1.Name == item2.Name &&
                   item1.AttackBoost == item2.AttackBoost &&
                   item1.DefenseBoost == item2.DefenseBoost &&
                   item1.Price == item2.Price &&
                   item1.Type == item2.Type;
        }

        public static bool operator !=(Item item1, Item item2)
        {
            return !(item1 == item2);
        }

        
        public override bool Equals(object obj)
        {
            if (obj is Item other)
            {
                return this == other;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ AttackBoost.GetHashCode() ^ DefenseBoost.GetHashCode() ^ Price.GetHashCode() ^ Type.GetHashCode();
        }


    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Healing
    }
}


