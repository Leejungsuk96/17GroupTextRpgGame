namespace _17GroupTextRpgGame
{
    
    internal class Program
    {
        static Character _player;
        static Item[] _items;
        static Monster _monster;

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

            switch (CheckValidInput(1, 2))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
            }
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