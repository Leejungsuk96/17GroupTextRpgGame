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
        public int Gold { get; set; }

        public int Exp { get; set; }

        //죽음 여부 확인
        public bool CheckDeath => Hp <= 0;
        //if (_player.CheckDeath) return;

        //경험치 1000이상인지 확인
        public bool CheckExp => Exp >= 1000;
        public Character(string name, string job, int level, int atk, int def, int hp, int maxhp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Maxhp = maxhp;
            Gold = gold;
            Exp = 0;
        }
    }
}
