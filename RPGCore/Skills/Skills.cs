using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Skills
{
    [ImplementPropertyChanged]
    public class SkillBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int manaCost;
        
        public int ManaCost
        {
            get { return manaCost; }
            set { manaCost = value; }
        }

        public Action skillAction;
    }
    [ImplementPropertyChanged]
    public class HealingSkill : SkillBase
    {
        private int healithAmmount;

        public int HealthAmmount
        {
            get { return healithAmmount; }
            set { healithAmmount = value; }
        }

        public HealingSkill(string name, int manaCost, int health)
        {
            Name = name;
            ManaCost = manaCost;
            HealthAmmount = health;
            skillAction = () =>
            {

            };
        }
 
    }
}
