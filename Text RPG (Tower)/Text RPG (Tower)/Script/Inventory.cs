using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Inventory
{
    public List<Item> Items { get; private set; } = new List<Item>();

    public void ShowInventory(Player player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            if (Items.Count == 0)
            {
                Console.WriteLine("소지한 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    string itemInfo = $"{i + 1}. {Items[i].Name}";
                    switch (Items[i].Type)
                    {
                        case ItemType.Weapon:
                            itemInfo += $" (공격력: {Items[i].AttackBoost})";
                            break;
                        case ItemType.Armor:
                            itemInfo += $" (방어력: {Items[i].DefenseBoost})";
                            break;
                        case ItemType.Healing:
                            itemInfo += $" (회복량: 20)";
                            break;
                    }
                    Console.WriteLine(itemInfo);
                }
            }
            Console.WriteLine("0. 나가기");
            Console.Write("장착/해제할 아이템의 번호를 입력해주세요.\n>> ");

            string input = Console.ReadLine();
            if (input == "0") return;

            if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= Items.Count)
            {
                Item selectedItem = Items[itemIndex - 1];

                bool isEquipped = player.EquippedWeapon == selectedItem ||
                                  player.EquippedArmor == selectedItem ||
                                  player.EquippedHealingItem == selectedItem;

                if (isEquipped)
                {
                    player.UnequipItem(selectedItem); 
                }
                else
                {
                    player.EquipItem(selectedItem);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadKey();
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
}
}
