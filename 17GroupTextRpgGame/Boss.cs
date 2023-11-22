using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17GroupTextRpgGame
{
    public class Boss  //보스 클래스
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; set; }
        public int Maxhp { get; }
        public int Exp { get; }
        public int Gold { get; }

        public static int BossCnt = 0;

        public bool CheckDeath => Hp <= 0;

        public Boss(string name, int level, int atk, int def, int hp, int maxhp, int exp, int gold)
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

        public void ReceiveDamage(int damage)
        {
            // 보스의 체력 감소
            Hp -= damage;

            // 체력이 음수가 되지 않도록 보정
            if (Hp < 0)
            {
                Hp = 0;
            }
        }
    }
}
