using RPGCore.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Fighting
{
    public class Opponent
    {
        Random rand = new Random();
        public Opponent(Creature c)
        {
            Character = c;
        }

        private Creature character;

        public Creature Character
        {
            get { return character; }
            set { character = value; }
        }
        public bool winner = false;
        public bool loser = false;

        #region Opponent Functions
        public AttackResult Attack(Opponent other)
        {
            var dmg = Character.Damage;
            var def = other.Character.Defense;
            var res = dmg - def;
            var ar = new AttackResult(AttackStatus.Missed, 0);
            if (rand.NextDouble() <= 0.96)
                if (res > 0)
                {
                    other.Character.CurHealth -= res;
                    ar = new AttackResult(AttackStatus.Dealt, res);
                    if (other.Character.CurHealth <= 0)
                    {
                        winner = true;
                        loser = false;
                        other.loser = true;
                        other.winner = false;
                        ar.deadly = true;
                        return ar;
                    }
                }
                else if (res <= 0)
                {
                    ar = new AttackResult(AttackStatus.Blocked, 0);
                }
            return ar;
        }
        #endregion

        public override string ToString()
        {
            return Character.Name;
        }
    }
}
