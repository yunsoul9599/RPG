using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Shop
    {
        public void OpenShop(Player player, Inventory inventory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("Gold: " + player.Gold + " G\n"); 
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();
                if (input == "0") break;

                switch (input)
                {
                    case "1":
                        BuyItem(player, inventory);
                        break;
                    case "2":
                        SellItem(player, inventory);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void BuyItem(Player player, Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("구매할 아이템을 선택하세요:");
            for (int i = 0; i < Item.ItemList.Count; i++)
            {
                string itemInfo = $"{i + 1}. {Item.ItemList[i].Name}";
                switch (Item.ItemList[i].Type)
                {
                    case ItemType.Weapon:
                        itemInfo += $" (공격력: {Item.ItemList[i].AttackBoost})";
                        break;
                    case ItemType.Armor:
                        itemInfo += $" (방어력: {Item.ItemList[i].DefenseBoost})";
                        break;
                    case ItemType.Healing:
                        itemInfo += $" (회복량: 20)";
                        break;
                }
                itemInfo += $" - {Item.ItemList[i].Price} G";
                if (Item.ItemList[i].Type == ItemType.Weapon || Item.ItemList[i].Type == ItemType.Armor)
                {
                    if (player.OwnedItems.Contains(Item.ItemList[i].Name))
                    {
                        itemInfo += " (구매 완료)";
                    }
                }
                Console.WriteLine(itemInfo);
            }
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");

            string input = Console.ReadLine();
            if (input == "0") return;

            if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= Item.ItemList.Count)
            {
                ItemInfo selectedItem = Item.ItemList[itemIndex - 1];

                // Weapon 또는 Armor 타입 아이템은 중복 구매 방지
                if ((selectedItem.Type == ItemType.Weapon || selectedItem.Type == ItemType.Armor) &&
                    player.OwnedItems.Contains(selectedItem.Name))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ReadKey();
                    return;
                }

                if (player.Gold >= selectedItem.Price)
                {
                    player.Gold -= selectedItem.Price;
                    inventory.AddItem(new Item(selectedItem));
                    player.AddOwnedItem(selectedItem.Name); 
                    Console.WriteLine($"{selectedItem.Name}을(를) 구매했습니다!");
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadKey();
        }

        private void SellItem(Player player, Inventory inventory)
        {
            Console.Clear();
            Console.WriteLine("판매할 아이템을 선택하세요:");
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {inventory.Items[i].Name} - {inventory.Items[i].Price / 2} G");
            }
            Console.WriteLine("0. 나가기");
            Console.Write(">> ");

            string input = Console.ReadLine();
            if (input == "0") return;

            if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= inventory.Items.Count)
            {
                Item selectedItem = inventory.Items[itemIndex - 1];
                player.Gold += selectedItem.Price / 2;
                inventory.Items.Remove(selectedItem);
                Console.WriteLine($"{selectedItem.Name}을(를) 판매했습니다!");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadKey();
        }
    }
}
