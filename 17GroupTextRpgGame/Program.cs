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
        static Boss[] _bosses;
        static Boss _boss;

        static void Main(string[] args)
        {
            PrintStartLogo();
            GameDataSetting();
            MonsterSpawn();
            StartMenu();
        }

        static void GameDataSetting()
        {
            //닉네임 설정
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("스파르타 게임에 오신 것을 환영합니다.");
            Console.WriteLine();
            Console.WriteLine("플레이어의 닉네임을 적어주세요. ");
            Console.Write(">> ");
            string playerName = Console.ReadLine();

            //직업 선택
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("1. 전사  |  2. 법사  |  3. 도적");
            Console.WriteLine();
            Console.WriteLine("원하는 직업을 선택해주세요.");
            Console.Write(">> ");
            string jobChoice = Console.ReadLine();
            if (jobChoice == "1") _player = new Character($"{playerName}", "전사", 1, 10, 5, 200, 200, 30, 30, 1500);
            else if (jobChoice == "2") _player = new Character($"{playerName}", "법사", 1, 20, 5, 100, 100, 50, 50, 1500);
            else if (jobChoice == "3") _player = new Character($"{playerName}", "도적", 1, 15, 5, 150, 150, 40, 40, 1500);
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.WriteLine("플레이어의 정보를 초기화 합니다.");
                Console.ResetColor();
                Console.ReadLine();
                GameDataSetting();
            }

            //몬스터 3마리
            _monsters = new Monster[5];
            AddMonster(new Monster("미니언", 2, 4, 0, 150, 150, 100, 1000));
            AddMonster(new Monster("공허충", 3, 10, 0, 100, 100, 150, 1500));
            AddMonster(new Monster("대포미니언", 5, 8, 0, 300, 300, 300, 3000));
            _monster1 = new Monster("미니언", 2, 4, 0, 150, 150, 100, 1000);
            _monster2 = new Monster("공허충", 3, 10, 0, 100, 100, 200, 2000);
            _monster3 = new Monster("대포미니언", 5, 8, 0, 300, 300, 300, 3000);

            _bosses = new Boss[5];
            AddBoss(new Boss("바론", 30, 35, 35, 300, 300, 3000, 7000));
            _boss = new Boss("바론", 30, 35, 35, 300, 300, 3000, 7000);

            _items = new Item[10];
            AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 0, 50));
            AddItem(new Item("골든 헬름", "희귀한 광석으로 만들어진 투구입니다.", 1, 0, 0, 100));

            //직업에 맞는 무기 지급
            if (_player.Job == "전사") AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 10, 0, 0));
            else if (_player.Job == "법사") AddItem(new Item("낡은 스태프", "쉽게 구할 수 있는 낡은 스태프입니다.", 1, 10, 0, 0));
            else if (_player.Job == "도적") AddItem(new Item("낡은 단검", "쉽게 볼 수 있는 낡은 단검입니다.", 1, 10, 0, 0));
        }

        static void StartMenu()
        {
            Console.Clear();

            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("■                                                        ■");
            Console.WriteLine("■        스파르타 마을에 오신 여러분 환영합니다!         ■");
            Console.WriteLine("■  이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.  ■");
            Console.WriteLine("■                                                        ■");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전 입장");
            Console.WriteLine("4. 보스방 입장");
            Console.WriteLine("5. 체력회복하기(1000골드 소모)");
            Console.WriteLine("6. 마나회복하기(1000골드 소모)");

            Console.WriteLine();

            switch (CheckValidInput(1, 6))
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
                case 4:
                    BossDungeonMenu();
                    break;
                case 5:
                    PlayerHpHeal();
                    break;
                case 6:
                    PlayerMpHeal();
                    break;
            }
        }

        private static void PlayerMpHeal() //플레이어 마나 회복
        {
            Console.Clear();
            if (_player.Hp == _player.Maxhp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("더 이상 마나를 회복할 수 없습니다.");
                Console.ResetColor();
            }
            else
            {
                _player.Mp = _player.Maxmp;
                _player.Gold -= 1000;
                Console.WriteLine("1000골드를 사용하여 마나를 회복하였습니다.");
            }
            Console.WriteLine("마을로 돌아갑니다.");
            Console.ReadLine();
            StartMenu();
        }
        private static void PlayerHpHeal() //플레이어 체력 회복
        {
            Console.Clear();
            if (_player.Hp == _player.Maxhp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("더 이상 체력을 회복할 수 없습니다.");
                Console.ResetColor();
            }
            else
            {
                _player.Hp = _player.Maxhp;
                _player.Gold -= 1000;
                Console.WriteLine("1000골드를 사용하여 체력을 회복하였습니다.");
            }
            Console.WriteLine("마을로 돌아갑니다.");
            Console.ReadLine();
            StartMenu();
        }

        static void BossDungeonMenu()
        {
            Console.Clear();

            ShowHighlightedText("■보스방에 입장했습니다.■");
            Console.WriteLine();
            Console.WriteLine("보스가 나타났습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 싸우러 가기");
            Console.WriteLine();
            Console.WriteLine("0. 마을로 돌아가기.");
            Console.WriteLine();

            switch (CheckValidInput(0, 1))
            {
                case 1:
                    if (_player.Hp <= 0)
                    {
                        Console.WriteLine("체력이 너무 낮습니다. 치료 후 도전 해주세요");
                        Console.ReadKey();
                        BossDungeonMenu();
                    }
                    BossBattle();
                    break;
                case 0:
                    StartMenu();
                    break;
            }
        }

        static void BossBattle()
        {
            Console.Clear();
            ShowHighlightedText("!! Boss Bettle !!");
            Console.WriteLine();            

            while (_boss.Hp > 0 && _player.Hp > 0)
            {
                DisplayBossInfo();

                Console.WriteLine("\n[내정보]");
                PrintPlayerInfo();

                static void PrintPlayerInfo()
                {
                    Console.WriteLine($"Lv.{_player.Level} {_player.Name} ({_player.Job})");
                    Console.WriteLine($"HP {_player.Hp}/{_player.Maxhp}");
                    Console.WriteLine($"MP {_player.Mp}/{_player.Maxmp}"); ;
                    Console.WriteLine();
                    Console.WriteLine("1. 공격하기");
                    Console.WriteLine("2. 스킬");
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                }

                int keyInput = CheckValidInput(0, 2);

                switch (keyInput)
                {
                    case 0:
                        BossDungeonMenu();
                        break;
                    case 1:
                        // 플레이어가 공격을 선택한 경우
                        Console.WriteLine("적을 공격합니다.");
                        EnemyPhase2(keyInput);
                        break;
                    case 2:
                        // 플레이어가 스킬을 선택한 경우
                        ChooseSkill();
                        break;
                }

                //플레이어의 행동 후에도 보스가 생존하면 보스의 턴을 진행. 보스 턴 자동 진행.
                if (_boss.Hp > -0)
                {
                    BossTurn();
                }

                //플레이어나 보스 중 하나의 체력이 0이하이면 전투 종료.
                if (_player.Hp <= 0)
                {
                    Console.WriteLine("전투에서 패배했습니다.");
                }
                else if (_boss.Hp <= 0)
                {
                    Console.WriteLine("바론을 쓰러트렸습니다.");
                }
            }

            // 보스의 턴 동작을 처리하는 메서드
            static void BossTurn()
            {
                // 보스의 특화된 턴 동작을 구현
                // 예시로 간단하게 랜덤한 값을 이용하여 플레이어를 공격
                int bossDamage = new Random().Next(10, 21);
                _player.ReceiveDamage(bossDamage);

                Console.WriteLine($"바론이 플레이어를 공격했습니다. 피해: {bossDamage}");
                Console.WriteLine();
            }

            // 플레이어가 스킬을 선택하고 사용할 수 있도록 메서드를 추가합니다.
            static void ChooseSkill()
            {
                // 플레이어의 직업에 따라 스킬 초기화
                List<Skill> skills = _player.Skills;

                // 스킬 목록 출력
                Console.WriteLine("스킬을 선택하세요:");

                for (int i = 0; i < skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {skills[i].Name}");
                }

                int skillInput = CheckValidInput(1, skills.Count);

                // 선택한 스킬에 따라 다른 동작 수행
                Skill selectedSkill = skills[skillInput - 1];

                // 스킬 사용
                _player.UseSkill(selectedSkill, _boss);

                //화면 초기화
                Console.Clear();
                
                Console.WriteLine();
                Console.WriteLine($"플레이어가 {selectedSkill.Name}(으)로 {selectedSkill.Damage}의 피해를 입혔습니다.");                                
                Console.WriteLine();
            }

            //보스 정보 출력을 별도로 분리.
            static void DisplayBossInfo()
            {
                if (_boss.Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    
                    Console.WriteLine("[보스 정보]");
                    Console.WriteLine($"Lv.{_boss.Level} {_boss.Name}");
                    Console.WriteLine($"HP {_boss.Hp}/{_boss.Maxhp} (Dead)");                    
                }
                else
                {                    
                    Console.WriteLine("[보스 정보]");
                    Console.WriteLine($"Lv.{_boss.Level} {_boss.Name}");
                    Console.WriteLine($"HP {_boss.Hp}/{_boss.Maxhp}");                    
                }
            }
        }

        static void EnemyPhase2(int keyInput)
        {
            Console.Clear();

            //플레이어의 일반 공격.
            int playerDamage = _player.Atk;

            //보스에게 피해 입힘.
            _boss.ReceiveDamage(playerDamage);

            ////보스의 체력 출력
            Console.WriteLine($"보스의 체력 : {_boss.Hp}/{_boss.Maxhp}");
            Console.WriteLine();

            //플레이어의 MP 출력
            Console.WriteLine($"{_player.Name}의 HP: {_player.Hp}/{_player.Maxhp}");
            Console.WriteLine($"{_player.Name}의 MP: {_player.Mp}/{_player.Maxmp}");
            Console.WriteLine();

            //보스의 체력이 0이하인지 체크
            if (_boss.Hp <= 0)
            {
                Console.WriteLine($"플레이어가 {_boss.Name}을 처치했습니다.");
                return;
            }

            // 보스의 턴 후 플레이어가 패배했는지 확인
            if (_player.Hp <= 0)
            {
                Console.WriteLine("전투에서 패배했습니다.");
            }

            switch (keyInput)
            {
                case 1:                    
                    // 플레이어의 공격 구현
                    int damage = _player.Atk;
                    _boss.ReceiveDamage(damage);

                    Console.Clear();
                    Console.WriteLine($"플레이어가 바론을 공격했습니다. 피해: {damage}");
                    Console.WriteLine();
                    break;

                default:
                    Console.WriteLine("올바른 행동을 선택하세요.");
                    break;
            }

            //보스 정보 출력을 별도로 분리.
            static void DisplayBossInfo()
            {
                if (_boss.Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv.{_boss.Level} {_boss.Name}");
                    Console.WriteLine($"HP {_boss.Hp}/{_boss.Maxhp} (Dead)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Lv.{_boss.Level} {_boss.Name}");
                    Console.WriteLine($"HP {_boss.Hp}/{_boss.Maxhp}");
                }
            }

            //보스의 턴.
            static void BossAtkToPlayer(int keyInput)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                _player.Hp -= _bosses[keyInput - 1].Atk;
                Console.WriteLine(_bosses[keyInput - 1].Atk + "의 데미지로 " + _bosses[keyInput - 1].Name + "의 공격");
                Console.ResetColor();
                Console.Write(_player.Name + "의 체력이 " + _player.Hp + "만큼 남았습니다.");
            }

            //일반 공격을 할 경우.
            static void PlayerAtkToBoss(int keyInput)
            {
                Console.Clear();
                int randomCorrectAtk = new Random().Next(1, 11);
                int randomChangeAtk = new Random().Next(1, 3);

                if (randomCorrectAtk >= 1 && randomCorrectAtk < 10)
                {
                    _bosses[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();

                    Console.WriteLine(_player.Atk + getSumBonusAtk() + "의 데미지로 " + _player.Name + "의 공격");
                    Console.Write(_bosses[keyInput - 1].Name + "의");

                    //보스의 체력 출력.
                    if (_bosses[keyInput - 1].Hp < 0)
                    {
                        _bosses[keyInput - 1].Hp = 0;
                        Console.WriteLine("체력이 " + _bosses[keyInput - 1].Hp + "만큼 남았습니다.");
                    }
                    else
                    {
                        Console.WriteLine("체력이 " + _bosses[keyInput - 1].Hp + "만큼 남았습니다.");
                    }
                }

                else
                {
                    double value = (double)(_player.Atk + getSumBonusAtk()) / 10;
                    if (randomChangeAtk == 2)
                    {
                        _bosses[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();
                        Console.WriteLine(_player.Atk + getSumBonusAtk() + Math.Ceiling(value) + "의 데미지로 " + _player.Name + "의 공격");
                        Console.Write(_bosses[keyInput - 1].Name);
                        Console.WriteLine();
                        if (_bosses[keyInput - 1].Hp < 0)
                        {
                            _bosses[keyInput - 1].Hp = 0;
                            Console.WriteLine(_bosses[keyInput - 1].Hp);
                        }
                        else
                        {
                            Console.WriteLine(_bosses[keyInput - 1].Hp);
                        }
                    }

                    else if (randomChangeAtk == 1)
                    {
                        _bosses[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();
                        Console.WriteLine(_player.Atk + getSumBonusAtk() - Math.Ceiling(value) + "의 데미지로 " + _player.Name + "의 공격");
                        Console.Write(_bosses[keyInput - 1].Name);

                        if (_bosses[keyInput - 1].Hp < 0)
                        {
                            _bosses[keyInput - 1].Hp = 0;
                            Console.WriteLine(_bosses[keyInput - 1].Hp);
                        }
                    }
                }
            }
        }

        //던전 메뉴
        static void DungeonMenu()
        {
            Console.Clear();
            ShowHighlightedText("■던전에 입장했습니다.■");
            Console.WriteLine();
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
                    if (_player.Hp <= 0)
                    {
                        Console.WriteLine("체력이 너무 낮습니다. 치료 후 도전해주세요");
                        Console.ReadKey();
                        DungeonMenu();
                    }
                    StartOfTheBattle();
                    break;
            }
        }

        static void MonsterSpawn()
        {
            //1에서 4 사이의 몬스터를 무작위로 생성
            int numberOfMonsters = new Random().Next(1, 5);
            Monster.MonsterCnt = numberOfMonsters;

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

                if (_monsters[i].Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp}");
                }
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

            for (int i = 0; i < Monster.MonsterCnt; i++)
            {

                if (_monsters[i].Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp} (Dead)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}. Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp}");
                }
            }

            Console.WriteLine("\n[내정보]");
            PrintPlayerInfo();

            static void PrintPlayerInfo()
            {
                Console.WriteLine($"Lv.{_player.Level} {_player.Name} ({_player.Job})");
                // 장비의 추가 HP가 적용이 안되서 getSumBonusHp()함수 추가
                Console.WriteLine($"HP {(_player.Hp + getSumBonusHp())}/{_player.Maxhp + getSumBonusHp()}");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
            }

            int keyInput = CheckValidInput(0, Monster.MonsterCnt); // 생성된 몬스터 번호를 입력시 그 몬스터와 배틀

            switch (keyInput)
            {
                case 0:
                    if (MonsterAllDead())
                    {
                        MonsterSpawn();
                    }
                    DungeonMenu();
                    break;

                default:
                    if (_player.Hp <= 0)
                    {
                        Console.WriteLine("체력이 너무 낮습니다. 싸움보단 치료를 우선시 해주세요");
                        Console.ReadKey();
                        StartOfTheBattle();

                    }
                    EnemyPhase(keyInput);
                    break;
            }
        }
        //랜덤하게 소환된 몬스터 1~3마리와 싸우기.
        private static void EnemyPhase(int keyInput)
        {
            Console.Clear();
            Console.Write(_monsters[keyInput - 1].Name);
            Console.WriteLine(_monsters[keyInput - 1].Hp);

            do
            {
                Console.ReadKey();
                Console.WriteLine();
                PlayerAtkToMonster(keyInput);
                Console.WriteLine();
                if (_monsters[keyInput - 1].Hp <= 0)
                {
                    Console.WriteLine("몬스터를 처치했습니다.");
                    //눈에 잘띄게 색 변경 및 경험치 골드 보상 지급
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"경험치 {_monsters[keyInput - 1].Exp}");
                    Console.ResetColor();
                    Console.Write(", ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"골드 {_monsters[keyInput - 1].Gold}");
                    Console.ResetColor();
                    Console.WriteLine(" 획득 !!!");
                    _player.Exp += _monsters[keyInput - 1].Exp;
                    _player.Gold += _monsters[keyInput - 1].Gold;
                    //레벨할 수 있는 경험치량 확인 후 레벨업
                    if (_player.CheckExp)
                    {
                        Console.WriteLine();
                        _player.Level += 1;
                        _player.Maxhp += 10;
                        _player.Atk += 2;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Level.Up !!!");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine($"HP가 10 증가하였습니다.");
                        Console.WriteLine($"ATK이 2 증가하였습니다.");
                        _player.Exp = 0;  //경험치 초기화
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 이어서 전투하기");
                    Console.WriteLine();
                    switch (CheckValidInput(0, 0))

                    {
                        case 0:
                            StartOfTheBattle();
                            break;
                    }
                }
                Console.ReadKey();
                MonsterAtkToPlayer(keyInput);
                Console.WriteLine();
            }

            while (_monsters[keyInput - 1].Hp > 0 && (_player.Hp + getSumBonusHp()) > 0);
            
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartOfTheBattle();
                    break;
            }

            Console.ReadKey();

            static void MonsterAtkToPlayer(int keyInput)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                _player.Hp -= _monsters[keyInput - 1].Atk;
                Console.WriteLine(_monsters[keyInput - 1].Atk + "의 데미지로 " + _monsters[keyInput - 1].Name + "의 공격");
                Console.ResetColor();
                if (_player.Hp < 0)
                {
                    _player.Hp = 0;
                    Console.Write(_player.Name + "의 체력이 " + (_player.Hp + getSumBonusHp()) + "만큼 남았습니다.");
                    Console.WriteLine();
                    Console.WriteLine("패배했습니다.");
                }
                else
                {
                    Console.Write(_player.Name + "의 체력이 " + (_player.Hp + getSumBonusHp()) + "만큼 남았습니다.");
                }
            }

            static void PlayerAtkToMonster(int keyInput)
            {
                Console.Clear();
                int randomCorrectAtk = new Random().Next(1, 11);
                int randomChangeAtk = new Random().Next(1, 3);

                if (randomCorrectAtk >= 1 && randomCorrectAtk < 10)
                {
                    _monsters[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();
                    Console.WriteLine(_player.Atk + getSumBonusAtk() + "의 데미지로 " + _player.Name + "의 공격");
                    Console.Write(_monsters[keyInput - 1].Name + "의");
                    if (_monsters[keyInput - 1].Hp < 0)
                    {
                        _monsters[keyInput - 1].Hp = 0;
                        Console.WriteLine("체력이 " + _monsters[keyInput - 1].Hp + "만큼 남았습니다.");
                    }
                    else
                    {
                        Console.WriteLine("체력이 " + _monsters[keyInput - 1].Hp + "만큼 남았습니다.");
                    }
                }

                else
                {
                    double value = (double)(_player.Atk + getSumBonusAtk()) / 10;
                    if (randomChangeAtk == 2)
                    {
                        _monsters[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();
                        Console.WriteLine(_player.Atk + getSumBonusAtk() + Math.Ceiling(value) + "의 데미지로 " + _player.Name + "의 공격");
                        Console.Write(_monsters[keyInput - 1].Name);
                        Console.WriteLine();
                        if (_monsters[keyInput - 1].Hp < 0)
                        {
                            _monsters[keyInput - 1].Hp = 0;
                            Console.WriteLine(_monsters[keyInput - 1].Hp);
                        }
                        else
                        {
                            Console.WriteLine(_monsters[keyInput - 1].Hp);
                        }
                    }

                    else if (randomChangeAtk == 1)
                    {
                        _monsters[keyInput - 1].Hp -= _player.Atk + getSumBonusAtk();
                        Console.WriteLine(_player.Atk + getSumBonusAtk() - Math.Ceiling(value) + "의 데미지로 " + _player.Name + "의 공격");
                        Console.Write(_monsters[keyInput - 1].Name);
                        if (_monsters[keyInput - 1].Hp < 0)
                        {
                            _monsters[keyInput - 1].Hp = 0;
                            Console.WriteLine(_monsters[keyInput - 1].Hp);
                        }
                    }
                }
            }
        }
        static bool MonsterAllDead()
        {
            for (int i = 0; i < Monster.MonsterCnt; i++)
            {
                if (_monsters[i].Hp > 0)
                {
                    return false;
                }
            }
            return true;
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

        static void AddBoss(Boss boss)
        {
            if (Boss.BossCnt == 5) return;
            _bosses[Boss.BossCnt] = boss;
            Boss.BossCnt++;
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

            PrintTextWithHighlights("마 력 : ", _player.Mp.ToString()); //캐릭터 정보에 mp 추가.
            PrintTextWithHighlights("최대마력 : ", _player.Maxmp.ToString());

            PrintTextWithHighlights("Gold : ", _player.Gold.ToString());
            Console.WriteLine();
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription();
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1); // 1, 2, 3에 매핑하기 위해 +1
            }
            Console.WriteLine();
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

        static void Victory()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("  :::     :::      :::::::::::       ::::::::      :::::::::::      ::::::::       :::::::::       :::   ::: ");
            Console.WriteLine("  :+:     :+:          :+:          :+:    :+:         :+:         :+:    :+:      :+:    :+:      :+:   :+: ");
            Console.WriteLine("  +:+     +:+          +:+          +:+                +:+         +:+    +:+      +:+    +:+       +:+ +:+  ");
            Console.WriteLine("  +#+     +:+          +#+          +#+                +#+         +#+    +:+      +#++:++#:         +#++:   ");
            Console.WriteLine("   +#+   +#+           +#+          +#+                +#+         +#+    +#+      +#+    +#+         +#+    ");
            Console.WriteLine("    #+#+#+#            #+#          #+#    #+#         #+#         #+#    #+#      #+#    #+#         #+#    ");
            Console.WriteLine("      ###          ###########       ########          ###          ########       ###    ###         ###    ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                ::::::::::          ::::    :::          ::::::::: ");
            Console.WriteLine("                               :+:                 :+:+:   :+:          :+:    :+: ");
            Console.WriteLine("                              +:+                 :+:+:+  +:+          +:+    +:+  ");
            Console.WriteLine("                             +#++:++#            +#+ +:+ +#+          +#+    +:+   ");
            Console.WriteLine("                            +#+                 +#+  +#+#+#          +#+    +#+    ");
            Console.WriteLine("                           #+#                 #+#   #+#+#          #+#    #+#     ");
            Console.WriteLine("                          ##########          ###    ####          #########       ");
            Console.WriteLine();
            Console.WriteLine("===============================================================================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 마을로 가기");
            Console.WriteLine();            

            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;                
            }
        }

        static void PrintStartLogo()
        {
            Console.Clear();
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