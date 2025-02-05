using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Game
    {
        static Player player;
        static Inventory inventory = new Inventory();
        static Shop shop = new Shop();

        public static void Start()
        {
            while (true)
            {
                InitializeGame();
                MainGameLoop(); 

                Console.WriteLine("게임을 다시 시작하시겠습니까? (Y/N)");
                string input = Console.ReadLine().ToUpper();
                if (input != "Y")
                {
                    break; 
                }
            }
        }

        static void InitializeGame()
        {
            CreateCharacter();
        }

        static void MainGameLoop()
        {
            while (player.IsAlive()) 
            {
                Console.Clear();
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowStatus();
                        break;
                    case "2":
                        inventory.ShowInventory(player);
                        break;
                    case "3":
                        shop.OpenShop(player, inventory);
                        break;
                    case "4":
                        Dungeon.EnterDungeon(player);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        Console.ReadKey();
                        break;
                }
            }

            if (!player.IsAlive())
            {
                GameOver();
            }
        }

       public static void GameOver()
        {
            Console.Clear();
            Console.WriteLine(@"
  _____                         ____                 
 / ____|                       / __ \                
| |  __  __ _ _ __ ___   ___  | |  | |_   _____ _ __ 
| | |_ |/ _` | '_ ` _ \ / _ \ | |  | \ \ / / _ \ '__|
| |__| | (_| | | | | | |  __/ | |__| |\ V /  __/ |   
 \_____|\__,_|_| |_| |_|\___|  \____/  \_/ \___|_|   
        ");
            Console.WriteLine("체력이 0이 되어 게임 오버되었습니다...");
            Console.ReadKey();
        }

        static void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine(@"
               
 _____                                _____                      _                 
|_   _|                              /  ___|                    | |                
  | |    ___  __      __  ___  _ __  \ `--.  _ __    __ _  _ __ | |_   __ _  _ __  
  | |   / _ \ \ \ /\ / / / _ \| '__|  `--. \| '_ \  / _` || '__|| __| / _` || '_ \
  | |  | (_) | \ V  V / |  __/| |    /\__/ /| |_) || (_| || |   | |_ | (_| || | | |
  \_/   \___/   \_/\_/   \___||_|    \____/ | .__/  \__,_||_|    \__| \__,_||_| |_|
                                            | |                                    
                                            |_|                                    
                                               
            ");
            Console.WriteLine("스파르타 타워에 오신 여러분 환영합니다.");
            Console.Write("플레이어의 이름을 입력하세요: ");
            string name = Console.ReadLine();

            string job = "";
            int attack = 0;
            int defense = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("직업을 선택하세요:");
                Console.WriteLine("1. 사냥꾼 (공격력: 10, 방어력: 0)");
                Console.WriteLine("2. 군인 (공격력: 5, 방어력: 5)");
                Console.WriteLine("3. 요원 (공격력: 7, 방어력: 3)");
                Console.Write(">> ");

                string jobChoice = Console.ReadLine();

                switch (jobChoice)
                {
                    case "1":
                        job = "사냥꾼";
                        attack = 10;
                        defense = 0;
                        break;
                    case "2":
                        job = "군인";
                        attack = 5;
                        defense = 5;
                        break;
                    case "3":
                        job = "요원";
                        attack = 7;
                        defense = 3;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        Console.ReadKey();
                        continue;
                }
                break;
            }

            player = new Player(name, job, 1, attack, defense, 100, 1500);
            Console.WriteLine($"{job}으로 선택되었습니다. 공격력: {attack}, 방어력: {defense}");
            Console.WriteLine("아무 키나 눌러 계속하세요...");
            Console.ReadKey();
        }

        static void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.AttackPower}");
            Console.WriteLine($"방어력 : {player.DefensePower}");
            Console.WriteLine($"체 력 : {player.Health}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine("장착 아이템:");
            Console.WriteLine($"  무기: {player.EquippedWeapon?.Name ?? "없음"}");
            Console.WriteLine($"  방어구: {player.EquippedArmor?.Name ?? "없음"}");
            Console.WriteLine($"  회복 아이템: {player.EquippedHealingItem?.Name ?? "없음"}\n");
            Console.WriteLine("0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                return;
            }
        }
    }
}
