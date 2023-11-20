using System.Runtime.CompilerServices;
using System.Threading;

namespace _17GroupTextRpgGame
{
    internal class Program
    {
        static Character _player;
        static Item[] _items;
        static Monster[] _monsters;
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

        static void GameDataSetting() // 최대체력 추가해줬어요.
        {
            _player = new Character("chad", "전사", 1, 10, 5, 100, 100, 1500);


            //몬스터 3마리.
            _monsters = new Monster[5];
            AddMonster(new Monster("미니언", 2, 5, 0, 15, 15, 100, 1000));
            AddMonster(new Monster("공허충", 3, 9, 0, 10, 10, 200, 2000));
            AddMonster(new Monster("대포미니언", 5, 8, 0, 25, 25, 300, 3000));
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
         
        static void MonsterSpawn()
        {
            //1에서 4 사이의 몬스터를 무작위로 생성
            int numberOfMonsters = new Random().Next(1, 5);
            Monster.MonsterCnt = numberOfMonsters;
            //Monster[] monsters = new Monster[numberOfMonsters];

            //생성된 몬스터 배열 저장
            _monsters = new Monster[numberOfMonsters];
            for (int i = 0; i < numberOfMonsters; i++)
            {
                _monsters[i] = GenerateRandomMonster();
            }
            //몬스터 배열 정렬
            Array.Sort(_monsters, (m1, m2) => m2.Level.CompareTo(m1.Level));

            //몬스터 상태 표시
            for (int i = 0; i < numberOfMonsters; i++)
            {
                Console.WriteLine($"{i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp}");
            }

            Console.WriteLine("\n[내정보]");
            PrintPlayerInfo();

            static void PrintPlayerInfo()
            {
                Console.WriteLine($"Lv.{_player.Level} {_player.Name} ({_player.Job})");
                Console.WriteLine($"HP {_player.Hp}/{_player.Hp}");
                Console.WriteLine("\n0. 공격\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            }
        }

        private static Monster GenerateRandomMonster()
        {
            //필요에 따라 몬스터 추가
            Monster[] possibleMonsters = { _monster1, _monster2, _monster3 };

            //몬스터 배열에서 몬스터 무작위 선택
            int randomIndex = new Random().Next(possibleMonsters.Length);
            Monster selectedMonster = possibleMonsters[randomIndex];

            //몬스터 인스턴스 생성
            return new Monster(selectedMonster.Name, selectedMonster.Level, selectedMonster.Atk,
                selectedMonster.Def, selectedMonster.Hp, selectedMonster.Maxhp, selectedMonster.Exp,
                selectedMonster.Gold);
        }

        static void StartOfTheBattle()
        {
            Console.Clear();
            ShowHighlightedText("!! Bettle !!");
            Console.WriteLine();
            MonsterSpawn();

            int keyInput = CheckValidInput(0, Monster.MonsterCnt); // 생성된 몬스터 번호를 입력시 그 몬스터와 배틀

            switch (keyInput)

            {
                case 0:
                    DungeonMenu();
                    break;

                default:
                    EnemyPhase(keyInput);
                    break;
            }



            VictoryBattle(); // 승리조건을 스타트배틀에서 끌어다 쓸거 같아서 넣어뒀어요!
            //랜덤하게 소환된 몬스터 1~3마리와 싸우기.  
        }

        private static void EnemyPhase(int keyInput)
        {
            Console.WriteLine(_monsters[keyInput - 1].Name);
            Console.WriteLine(_monsters[keyInput-1].Hp);
            Console.WriteLine(_monsters[keyInput-1].Level);

            int randomCorrectAtk = new Random().Next(1, 11);
            int randomChangeAtk = new Random().Next(1, 3);

            if (randomCorrectAtk >= 1 && randomCorrectAtk < 10)
            {
                Console.WriteLine(_player.Atk + getSumBonusAtk() + "의 데미지로 공격");
            }

            else
            {
                double value = (double)(_player.Atk + getSumBonusAtk()) / 10;
                if (randomChangeAtk == 2)
                {
                    Console.WriteLine(_player.Atk + getSumBonusAtk() + Math.Ceiling(value) + "의 데미지로 공격");

                }

                else if (randomChangeAtk == 1)
                {
                    Console.WriteLine(_player.Atk + getSumBonusAtk() - Math.Ceiling(value) + "의 데미지로 공격");
                }
            }
        }
        static void VictoryBattle() //승리조건 구현 현재까진 몬스터가 1마리만 있을때만 했어요! 3마리는 좀더 고민해보겠습니다.
        {
            if (_player.Hp <= 0 || _monster1.Hp <= 0)
            {
                if (_player.Hp <= 0)
                {
                    Console.WriteLine("패배!");
                }
                else
                {
                    Console.WriteLine("승리!");
                }
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

        static void AddMonster(Monster monster)
        {
            if (Monster.MonsterCnt == 5) return;
            _monsters[Monster.MonsterCnt] = monster;
            Monster.MonsterCnt++;
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

            PrintTextWithHighlights("최대체력 : ", (_player.Maxhp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");

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

        static void ToggleMonsterAtk(int idx)
        {
            
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