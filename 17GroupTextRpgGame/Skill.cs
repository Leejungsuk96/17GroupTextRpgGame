using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17GroupTextRpgGame
{
    public class Skill
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int MpCost { get; set; }  //MP 소모량

        public List<Skill> Skills { get; set; }

        public Skill(string name, int damage, int mpcost)
        {
            Name = name;
            Damage = damage;
            MpCost = mpcost;

        }

    }
}
