using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17GroupTextRpgGame
{
    public class Character // 최대체력 구현하기
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; }
        public int Hp { get; set; }

        public int Maxhp { get; set; }

        //플레이어 mp
        public int Mp { get; set; }
        public int Maxmp { get; }


        public int Gold { get; set; }

        public int Exp { get; set; }

        //죽음 여부 확인
        public bool CheckDeath => Hp <= 0;
        //if (_player.CheckDeath) return;

        //스킬 리스트
        public List<Skill> Skills { get; set; }
        public bool UsedSkillThisTurn { get; set; } // 스킬 사용 여부


        //경험치 1000이상인지 확인
        public bool CheckExp => Exp >= 1000;
        public Character(string name, string job, int level, int atk, int def, int hp, int maxhp, int mp, int maxmp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Maxhp = maxhp;
            Mp = mp;        //플레이어 mp
            Maxmp = maxmp;
            Gold = gold;
            Exp = 0;

            //직업에 따라 스킬 초키화
            Skills = new List<Skill>();
            if (job == "전사")
            {
                Skills.Add(new Skill("강력한 일격", 20, 5));
                Skills.Add(new Skill("참수", 40, 5));
            }
            else if (job == "법사")
            {
                Skills.Add(new Skill("파이어", 40, 10));
                Skills.Add(new Skill("블라자드", 40, 10));
            }
            else if (job == "도적")
            {
                Skills.Add(new Skill("난도질", 20, 10));
                Skills.Add(new Skill("마무리 일격", 30, 10));
            }
        }

        public void UseSkill(Skill skill, Boss boss)
        {
            //스킬 사용 전에 MP 확인
            if (Mp < skill.MpCost)
            {
                Console.WriteLine("마나가 부족합니다.");
                return;
            }

            //스킬 사용
            Console.WriteLine($"{Name}이(가) {skill.Name} 스킬을 사용합니다.");
            boss.ReceiveDamage(skill.Damage);

            //플레이어의 MP 감소
            Mp -= skill.MpCost;
            if (Mp < 0)
            {
                Mp = 0;
            }
        }

        public void ReceiveDamage(int damage)
        {
            // 플레이어가 피해를 받을 때 호출되는 메서드
            Hp -= damage;
            if (Hp < 0)
            {
                Hp = 0; // 체력이 음수로 가지 않도록 보정
            }

            Console.WriteLine($"플레이어가 {damage}의 피해를 받았습니다. 현재 체력: {Hp}/{Maxhp}");
        }
    }
}
