using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Fighting
{
    public class AttackResult
    {
        public AttackResult(AttackStatus s, int dmg) {
            Status = s;
            Damage = dmg;
        }
        private AttackStatus status;

        public AttackStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        int damage;
        public int Damage { get { return damage; } set { damage = value; } }
        public bool deadly = false;
    }

    public enum AttackStatus { Dealt, Blocked, Missed}
}
