﻿using System.Threading;

namespace _17GroupTextRpgGame
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    //몬스터 클래스
    public class Monster
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Maxhp { get; }
        public int Exp { get; }
        public int Gold { get; }

        public Monster(string name, int level, int atk, int def, int hp, int maxhp, int exp, int gold)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Maxhp = maxhp;
            Exp = exp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        // 개선포인트 : Enum 활용
        public int Type { get; }

        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }


        public bool IsEquiped { get; set; }

        public static int ItemCnt = 0;

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquiped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquiped = isEquiped;
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            // 장착관리 전용
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquiped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));

            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"Hp {(Hp >= 0 ? "+" : "")}{Hp}");

            Console.Write(" | ");

            Console.WriteLine(Description);
        }

        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

    }

    internal class Program
    {
        static Character _player;
        static Item[] _items;
        static Monster _monster1;
        static Monster _monster2;
        static Monster _monster3;

        static void Main(string[] args)
        {
            /// 구성 
            /// 0. 초기화함
            /// 1. 스타팅 로고를 보여줌 (게임 처음 킬때만 보여줌)
            /// 2. 선택 화면을 보여줌 (기본 구현사항 - 상태 / 인벤토리)
            /// 3. 상태화면을 구현함 (필요 구현 요소 : 캐릭터, 아이템)
            /// 4. 인벤토리 화면을 구현함
            GameDataSetting();
            PrintStartLogo();
            StartMenu();
        }

        static void GameDataSetting()
        {
            _player = new Character("chad", "전사", 1, 10, 5, 100, 1500);

            //몬스터 3마리.
            _monster1 = new Monster("미니언", 2, 5, 0, 15, 15, 100, 1000);
            _monster2 = new Monster("공허충", 3, 9, 0, 10, 10, 200, 2000);
            _monster3 = new Monster("대포미니언", 5, 8, 0, 25, 25, 300, 3000);

            _items = new Item[10];
            AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));
            AddItem(new Item("골든 헬름", "희귀한 광석으로 만들어진 투구입니다.", 1, 0, 9, 0));
        }

        static void StartMenu()
        {
            /// 구성
            /// 0. 화면 정리
            /// 1. 선택 멘트를 줌
            /// 2. 선택 결과값을 검증함
            /// 3. 선택 결과에 따라 메뉴로 보내줌
            Console.Clear();

            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전 입장");
            Console.WriteLine("");
            // 1안 : 착한 유저들만 있을 경우
            // int keyInput = int.Parse(Console.ReadLine());

            // 2안 : 나쁜 유저들도 있는 경우
            // int keyInput;
            // bool result;
            // do
            // {
            //     Console.WriteLine("원하시는 행동을 입력해주세요.");
            //     result = int.TryParse(Console.ReadLine(), out keyInput);
            // } while (result == false || CheckIfValid(keyInput, min : 1, max : 2) == false);

            switch (CheckValidInput(1, 3))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 3:
                    DungeonMenu();
                    break;
            }
        }

        //던전 메뉴
        static void DungeonMenu()
        {
            Console.Clear();
            ShowHighlightedText("■던전에 입장했습니다.■");
            Console.WriteLine("몬스터가 나타났습니다.");
            Console.WriteLine();
            Console.WriteLine("0. 마을로 돌아가기.");
            Console.WriteLine("1. 싸우기");
            Console.WriteLine();

            switch (CheckValidInput(0, 1)) 

            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    StartOfTheBattle();
                    break;


                 
            }
        }

           
        static void StartOfTheBattle()
        {
            //랜덤하게 소환된 몬스터 1~3마리와 싸우기.  
        }







        static int CheckValidInput(int min, int max)
        {
            /// 설명
            /// 아래 두 가지 상황은 비정상 -> 재입력 수행
            /// (1) 숫자가 아닌 입력을 받은 경우
            /// (2) 숫자가 최소값 ~ 최대값의 범위를 넘는 경우
            int keyInput;
            bool result;
            do
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result == false || CheckIfValid(keyInput, min, max) == false);

            return keyInput;
        }

        static bool CheckIfValid(int checkable, int min, int max)
        {
            if (min <= checkable && checkable <= max) return true;
            return false;
        }

        static void AddItem(Item item)
        {
            if (Item.ItemCnt == 10) return;
            _items[Item.ItemCnt] = item;
            Item.ItemCnt++;
        }

        static void StatusMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 상태 보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("Lv. ", _player.Level.ToString("00"));
            Console.WriteLine("");
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

            int bonusAtk = getSumBonusAtk();
            PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");

            int bonusDef = getSumBonusDef();
            PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");

            int bonusHp = getSumBonusHp();
            PrintTextWithHighlights("체 력 : ", (_player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");

            PrintTextWithHighlights("Gold : ", _player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }
        }

        private static int getSumBonusAtk()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Atk;
            }
            return sum;
        }

        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Def;
            }
            return sum;
        }

        private static int getSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquiped) sum += _items[i].Hp;
            }
            return sum;
        }

        /* 고급 문법 : 리플렉션 적용 문법
         private static int GetSumBonusString(string propertyName)
        {
            int sum = 0;
            for (int i = 0; i < Item.itemCnt; i++)
            {
                if (items[i].IsEquiped)
                {
                    // 리플렉션을 사용하여 현재 아이템의 propertyName 속성 값을 가져옵니다.
                    var propertyInfo = items[i].GetType().GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        // 속성 값이 int 타입이라고 가정하고 값을 가져옵니다.
                        int value = (int)propertyInfo.GetValue(items[i], null);
                        sum += value;
                    }
                }
            }
            return sum;
        }
         */
        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        static void InventoryMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");
            switch (CheckValidInput(0, 1))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        static void EquipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1); // 1, 2, 3에 매핑하기 위해 +1
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, Item.ItemCnt);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    ToggleEquipStatus(keyInput - 1); // 유저가 입력하는건 1, 2, 3 : 실제 배열에는 0, 1, 2...
                    EquipMenu();
                    break;
            }
        }

        static void ToggleEquipStatus(int idx)
        {
            _items[idx].IsEquiped = !_items[idx].IsEquiped;
        }

        static void ShowHighlightedText(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        static void PrintStartLogo()
        {
            // ASCII ART GENERATED BY https://textkool.com/en/ascii-art-generator?hl=default&vl=default&font=Red%20Phoenix
            Console.WriteLine("=============================================================================");
            Console.WriteLine("        ___________________   _____  __________ ___________ _____    ");
            Console.WriteLine("       /   _____/\\______   \\ /  _  \\ \\______   \\\\__    ___//  _  \\   ");
            Console.WriteLine("       \\_____  \\  |     ___//  /_\\  \\ |       _/  |    |  /  /_\\  \\  ");
            Console.WriteLine("       /        \\ |    |   /    |    \\|    |   \\  |    | /    |    \\ ");
            Console.WriteLine("      /_______  / |____|   \\____|__  /|____|_  /  |____| \\____|__  / ");
            Console.WriteLine("              \\/                   \\/        \\/                  \\/  ");
            Console.WriteLine(" ________    ____ ___ _______     ________ ___________________    _______");
            Console.WriteLine(" \\______ \\  |    |   \\\\      \\   /  _____/ \\_   _____/\\_____  \\   \\      \\");
            Console.WriteLine("  |    |  \\ |    |   //   |   \\ /   \\  ___  |    __)_  /   |   \\  /   |   \\\r\n");
            Console.WriteLine("  |    |   \\|    |  //    |    \\\\    \\_\\  \\ |        \\/    |    \\/    |    \\\r\n");
            Console.WriteLine(" /_______  /|______/ \\____|__  / \\______  //_______  /\\_______  /\\____|__  /\r\n");
            Console.WriteLine("         \\/                  \\/         \\/         \\/         \\/         \\/");
            Console.WriteLine("=============================================================================");
            Console.WriteLine("                           PRESS ANYKEY TO START                             ");
            Console.WriteLine("=============================================================================");
            Console.ReadKey();
        }
    }

}