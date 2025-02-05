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
        public string EquippedWeapon { get; private set; } = null;
        public string EquippedArmor { get; private set; } = null;
        public string EquippedHealingItem { get; private set; } = null;

        
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
            if (Health > MaxHealth) Health = MaxHealth;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }


        // 아이템 장착 메서드
        public void EquipItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    if (EquippedWeapon != null)
                    {
                        UnequipItem(EquippedWeapon); // 이미 장착된 무기가 있다면 해제
                    }

                    EquippedWeapon = item;
                    AttackPower += item.AttackBoost;
                    break;
                case ItemType.Armor:
                    if (EquippedArmor != null) UnequipItem(EquippedArmor); // 이미 장착된 방어구가 있다면 해제
                    EquippedArmor = item;
                    DefensePower += item.DefenseBoost;
                    break;
                case ItemType.Healing:
                    if (EquippedHealingItem != null) UnequipItem(EquippedHealingItem); // 이미 장착된 회복 아이템이 있다면 해제
                    EquippedHealingItem = item;
                    break;
            }
            Console.WriteLine($"{item.Name}을(를) 장착했습니다!");
        }

        private void UnequipItem(string equippedWeapon)
        {
            throw new NotImplementedException();
        }

        // 아이템 해제 메서드
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

            // 레벨 업 조건 확인
            while (Exp >= Level * 10) // 레벨업에 필요한 EXP: 현재 레벨 * 10
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Exp = 0; // 경험치 초기화
            AttackPower += (float)0.5; // 공격력 0.5 증가
            DefensePower += 1; // 방어력 1 증가
            Console.WriteLine($"레벨 업! 현재 레벨: {Level}");
            Console.WriteLine($"공격력이 {AttackPower}로, 방어력이 {DefensePower}로 증가했습니다!");
        }
    }
}

