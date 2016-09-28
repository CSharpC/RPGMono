using PropertyChanged;
using RPGCore.Economy;
using RPGCore.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Creatures
{
    public class ExperienceTable
    {
        public static Dictionary<int, int> table;

        /// <summary>
        /// <para>Initialize the experience table used by all in-game creatures.</para>
        /// </summary>
        /// <param name="baseExp">Base EXP to level up</param>
        /// <param name="multiplier">By how much should the current level threshold be multiplied for the next level</param>
        /// <param name="increase">How much should be added to EXP curve each time</param>
        /// <param name="maxLevel">Max Level</param>
        public static void InitializeLevelTable(int baseExp, float multiplier, int increase, int maxLevel)
        {
            table = new Dictionary<int, int>();
            float lastExp = 0f;
            for (int i = 1; i <= maxLevel; i++)
            {
                table.Add(i, (int)lastExp);
                lastExp = (lastExp * multiplier) + baseExp + increase;
            }
        }
    }

    [ImplementPropertyChanged]
    public class Creature
    {
        private Random rand = new Random();
        public string Name { get; set; }
        public Creature()
        {
            baseHP = 10;
            Equipment = new Inventory(new List<ItemBase>(), new Currency());
        }
        public Creature(string name, int _exp, int _str, int _agi, int _luk, int _wis, int _vit, int _bHP, int _HPpl, int _bMP, int _MPpl, int _bDmg, int _bDef)
        {
            Name = name;
            Experience = _exp;
            STR = _str;
            AGI = _agi;
            LUK = _luk;
            WIS = _wis;
            VIT = _vit;
            baseHP = _bHP;
            healthPerLevel = _HPpl;
            baseMP = _bMP;
            manaPerLevel = _MPpl;
            baseDmg = _bDmg;
            baseDef = _bDef;
            curHealth = MaxHealth;
            curMana = MaxMana;
            Equipment = new Inventory(new List<ItemBase>(), new Currency());
        }

        #region Health Fields
        private int baseHP;
        private int healthPerLevel;
        [AlsoNotifyFor("HealthText")]
        public int MaxHealth { get { return baseHP + (VIT/2) + (Level * healthPerLevel); } }
        private int curHealth;
        [AlsoNotifyFor("HealthText")]
        public int CurHealth
        {
            get { return curHealth; }
            set { curHealth = value; }
        }
        public string HealthText { get { return CurHealth + "/" + MaxHealth; } }
        #endregion
        #region Mana Fields
        private int baseMP;
        private int manaPerLevel;
        public int MaxMana { get { return baseMP + (WIS / 3) + (Level * manaPerLevel); } }
        [AlsoNotifyFor("ManaText")]
        private int curMana;

        public int CurMana
        {
            get { return curMana; }
            set { curMana = value; }
        }
        public string ManaText { get { return CurMana + "/" + MaxMana; } }
        #endregion
        #region Experience & Level
        [AlsoNotifyFor("Level", "BottomExpThreshold", "TopExpThreshold", "CurrentLevelExp", "ExpToNextLevel", "StatPoints")]
        private int experience =0;
        public int Experience { get { return experience; } set { experience = value; } }
        public int Level { get { return ExperienceTable.table.Where(o => o.Value <= Experience).Last().Key; } }
        public int CurrentLevelExp { get { return Experience - BottomExpThreshold; } }
        public int ExpToLevelUp { get { return ExperienceTable.table[Level + 1] - Experience; } }
        public int BottomExpThreshold { get { return ExperienceTable.table[Level]; } }
        public int TopExpThreshold { get { return ExperienceTable.table[Level + 1]; } }
        #endregion
        #region Basic Stats
        private int str;

        [AlsoNotifyFor("Damage")]
        public int STR
        {
            get { return str; }
            set { str = value; }
        }
        private int agi;

        [AlsoNotifyFor("Defense", "CriticalHitChance", "CriticalHitChanceText")]
        public int AGI
        {
            get { return agi; }
            set { agi = value; }
        }
        private int wis;

        [AlsoNotifyFor("MaxMana")]
        public int WIS
        {
            get { return wis; }
            set { wis = value; }
        }
        private int luk;
        
        [AlsoNotifyFor("CriticalHitChance", "CriticalHitChanceText")]
        public int LUK
        {
            get { return luk; }
            set { luk = value; }
        }
        private int vit;

        public int VIT
        {
            get { return vit; }
            set { vit = value; }
        }
        private int statPoints;

        public int StatPoints
        {
            get { return statPoints; }
            set { statPoints = value; }
        }
        private Inventory equipment;

        public Inventory Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }
        public float CriticalHitChance { get { return 0.12f + (0.0088f * LUK) + (0.0021f * AGI); } }
        public string CriticalHitChanceText { get { return Math.Round(CriticalHitChance * 100) + "%"; } }
        #endregion
        #region Damage & Defense
        private int baseDmg;
        public int Damage { get { var dmgs = baseDmg + (STR / 4) + rand.Next(0, STR / 6); if(rand.NextDouble() <= CriticalHitChance) dmgs += dmgs/2; return dmgs; } }
        public string DamageText { get { return (baseDmg + (STR / 4)) + " - " + (baseDmg + (STR / 4) + (STR / 6)); } }
        private int baseDef;
        public int Defense { get { return baseDef + (AGI / 4) + rand.Next(0, AGI / 6); } }
        public string DefenseText { get { return (baseDef + (AGI/4)) + " - " + (baseDef + (AGI / 4) + (AGI / 6)); } }
        #endregion

        #region Creature Functions
        //TODO: Fight
        private void OnLevelUp(bool autoAssignPoints=false)
        {
            CurHealth = MaxHealth;
            CurMana = MaxMana;
            StatPoints += 3;
        }
        public void AddExperience(int amount)
        {
            var oldLv = Level;
            Experience += amount;
            if (Level > oldLv)
                OnLevelUp();
        }
        public void ForceLevelUp(int levels=1, bool autoAssignPoints=false)
        {
            for (int i = 0; i < levels; i++)
            {
                Experience = ExperienceTable.table[Level + 1];
                OnLevelUp();
            }
            if(autoAssignPoints)
                for(int i = 0; i< StatPoints; i++)
                {
                    var stat = rand.Next(0, 5);
                    switch(stat)
                    {
                        case 0:
                            STR++;
                            break;
                        case 1:
                            AGI++;
                            break;
                        case 2:
                            WIS++;
                            break;
                        case 3:
                            LUK++;
                            break;
                        case 4:
                            VIT++;
                            break;
                    }
                }
        }

        public void RestoreStats()
        {
            CurHealth = MaxHealth;
            CurMana = MaxMana;
        }
        #endregion
    }
}