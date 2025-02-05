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

                // 던전 클리어 가능 여부 판단
                if (player.DefensePower < recommendedDefense)
                {
                    Random random = new Random();
                    int failChance = random.Next(1, 101); // 1 ~ 100 사이의 랜덤 값
                    if (failChance <= 40) // 40% 확률로 실패
                    {
                        Console.WriteLine("던전 클리어에 실패했습니다...");
                        player.ReduceHealthByHalf(); // 체력 절반 감소
                        Console.WriteLine($"체력이 {player.Health}로 감소했습니다.");
                        Console.ReadKey();
                        continue; // 다시 던전 난이도 선택으로 돌아감
                    }
                }

                // 던전 클리어 성공
                Console.WriteLine("던전을 클리어했습니다!");

                // 체력 감소 계산
                int baseHealthLoss = new Random().Next(20, 36); // 20 ~ 35 사이의 랜덤 값
                int defenseDifference = player.DefensePower - recommendedDefense;
                int totalHealthLoss = Math.Max(baseHealthLoss - defenseDifference, 0); // 체력 감소량은 0보다 작을 수 없음

                player.ReduceHealth(totalHealthLoss); // 체력 감소
                Console.WriteLine($"체력이 {totalHealthLoss}만큼 감소했습니다. 현재 체력: {player.Health}");

                // 보상 계산
                int baseReward = 0;
                int baseExp = 0; // 기본 경험치
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        baseReward = 1000;
                        baseExp = 5; // 쉬움 던전 경험치
                        break;
                    case Difficulty.Normal:
                        baseReward = 1700;
                        baseExp = 10; // 보통 던전 경험치
                        break;
                    case Difficulty.Hard:
                        baseReward = 2500;
                        baseExp = 20; // 어려움 던전 경험치
                        break;
                }

                // 추가 보상 계산 (공격력에 따른 %)
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
                    break; // 던전 선택 메뉴로 돌아감
                }
            }
        }
    }
}
