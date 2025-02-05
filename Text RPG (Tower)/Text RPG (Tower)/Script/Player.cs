using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Player
    {
        public string Name { get; private set; }
        public string Job { get; private set; }
        public int Level { get; private set; } = 1;
        public int Exp { get; private set; } = 0;
        public float AttackPower { get; private set; }
        public int DefensePower { get; private set; }
        public int Health { get; private set; }
        public int Gold { get; set; }
        public Item EquippedWeapon { get; private set; } = null;
        public Item EquippedArmor { get; private set; } = null;
        public Item EquippedHealingItem { get; private set; } = null;

        
        public const int MaxHealth = 100;


        public List<string> OwnedItems { get; private set; } = new List<string>();


        public Player(string name, string job, int level, double attack, int defense, int health, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            AttackPower = (float)attack;
            DefensePower = defense;
            Health = health > MaxHealth ? MaxHealth : health;
            Gold = gold;
        }


        public void ReduceHealth(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }

        
        public void ReduceHealthByHalf()
        {
            Health /= 2;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth; // 체력이 최대치를 넘지 않도록 조정
            Console.WriteLine($"체력이 {amount}만큼 회복되었습니다. 현재 체력: {Health}");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }


        public void EquipItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    if (EquippedWeapon != null)
                    {
                        UnequipItem(EquippedWeapon); 
                    }

                    EquippedWeapon = item;
                    AttackPower += item.AttackBoost;
                    break;
                case ItemType.Armor:
                    if (EquippedArmor != null) UnequipItem(EquippedArmor); 
                    EquippedArmor = item;
                    DefensePower += item.DefenseBoost;
                    break;
                case ItemType.Healing:
                    if (EquippedHealingItem != null) UnequipItem(EquippedHealingItem); 
                    EquippedHealingItem = item;
                    break;
            }
            Console.WriteLine($"{item.Name}을(를) 장착했습니다!");
        }

        private void UnequipItem(string equippedWeapon)
        {
            throw new NotImplementedException();
        }

        public void UnequipItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    EquippedWeapon = null;
                    AttackPower -= item.AttackBoost;
                    break;
                case ItemType.Armor:
                    EquippedArmor = null;
                    DefensePower -= item.DefenseBoost;
                    break;
                case ItemType.Healing:
                    EquippedHealingItem = null;
                    break;
            }
            Console.WriteLine($"{item.Name}을(를) 장착 해제했습니다!");
        }



        public bool CanBuyItem(string itemName)
        {
            return !OwnedItems.Contains(itemName);
        }

        public void AddOwnedItem(string itemName)
        {
            OwnedItems.Add(itemName);
        }


        public void GainExp(int exp)
        {
            Exp += exp;
            Console.WriteLine($"{exp} EXP를 획득했습니다!");

            while (Exp >= Level * 10) 
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Exp = 0;
            AttackPower += (float)0.5; 
            DefensePower += 1; 
            Console.WriteLine($"레벨 업! 현재 레벨: {Level}");
            Console.WriteLine($"공격력이 {AttackPower}로, 방어력이 {DefensePower}로 증가했습니다!");
        }
    }
}

