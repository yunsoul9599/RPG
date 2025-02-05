using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG__Speed_.Script
{
    public class Dungeon
    {
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        public static void EnterDungeon(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전 난이도를 선택하세요:");
                Console.WriteLine("1. 쉬움 (권장 방어력: 5)");
                Console.WriteLine("2. 보통 (권장 방어력: 11)");
                Console.WriteLine("3. 어려움 (권장 방어력: 17)");
                Console.WriteLine("0. 나가기");
                Console.Write(">> ");

                string input = Console.ReadLine();
                if (input == "0") break;

                Difficulty difficulty = Difficulty.Normal;
                int recommendedDefense = 0;

                switch (input)
                {
                    case "1":
                        difficulty = Difficulty.Easy;
                        recommendedDefense = 5;
                        break;
                    case "2":
                        difficulty = Difficulty.Normal;
                        recommendedDefense = 11;
                        break;
                    case "3":
                        difficulty = Difficulty.Hard;
                        recommendedDefense = 17;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 기본 난이도(보통)로 설정됩니다.");
                        difficulty = Difficulty.Normal;
                        recommendedDefense = 11;
                        break;
                }

                Console.WriteLine($"{difficulty} 난이도의 던전에 입장합니다!");

                if (player.DefensePower < recommendedDefense)
                {
                    Random random = new Random();
                    int failChance = random.Next(1, 101); 
                    if (failChance <= 40)
                    {
                        Console.WriteLine("던전 클리어에 실패했습니다...");
                        player.ReduceHealthByHalf();
                        Console.WriteLine($"체력이 {player.Health}로 감소했습니다.");

                        if (!player.IsAlive())
                        {
                            Game.GameOver();
                            return; 
                        }

                        Console.ReadKey();
                        continue; 
                    }
                }

                Console.WriteLine("던전을 클리어했습니다!");

                int baseHealthLoss = new Random().Next(20, 36); 
                int defenseDifference = player.DefensePower - recommendedDefense;
                int totalHealthLoss = Math.Max(baseHealthLoss - defenseDifference, 0); 

                player.ReduceHealth(totalHealthLoss); 
                Console.WriteLine($"체력이 {totalHealthLoss}만큼 감소했습니다. 현재 체력: {player.Health}");

                int baseReward = 0;
                int baseExp = 0; 
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        baseReward = 1000;
                        baseExp = 5; 
                        break;
                    case Difficulty.Normal:
                        baseReward = 1700;
                        baseExp = 10; 
                        break;
                    case Difficulty.Hard:
                        baseReward = 2500;
                        baseExp = 20; 
                        break;
                }

                Random rand = new Random();
                int attackBonusPercentage = rand.Next((int)player.AttackPower, (int)((player.AttackPower * 2) + 1)); // 공격력 ~ 공격력 * 2 사이의 %
                int totalReward = baseReward + (baseReward * attackBonusPercentage / 100);

                player.Gold += totalReward;
                player.GainExp(baseExp); // 경험치 획득
                Console.WriteLine($"{totalReward} G를 획득했습니다!");

                Console.WriteLine("\n던전을 다시 도전하시겠습니까? (Y/N)");
                Console.Write(">> ");
                string retryInput = Console.ReadLine().ToUpper();


                if (retryInput != "Y")
                {
                    break; 
                }
            }
        }
    }
}
