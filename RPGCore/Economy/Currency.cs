using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCore.Economy
{
    /// <summary>
    /// <para>A class that is the base for economy system.</para>
    /// </summary>
    [ImplementPropertyChanged]
    public class Currency
    {
        public const int _copperRate = 8;
        public const int _silverRate = 800;
        /// <summary>
        /// <para>Create new instance of Currency class with 0 coins.</para>
        /// </summary>
        public Currency() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Amount of stone coins to be converted into an instance of Currency class.</param>
        public Currency(int amount)
        {
            allCoins = amount;
        }
        #region Basic Fields
        private int allCoins;
        /// <summary>
        /// <para>Gets or sets the overall amount of money held by this instance of the Currency class.</para>
        /// </summary>
        [AlsoNotifyFor("StoneCoins", "CopperCoins", "SilverCoins")]
        public int AllCoins { get { return allCoins; } set { allCoins = value; } }
        public int StoneCoins { get { return (allCoins % _silverRate) % _copperRate; } }
        public int CopperCoins { get { return (allCoins % _silverRate) / _copperRate; } }
        public int SilverCoins { get { return allCoins / _silverRate; } }
        #endregion

        #region Basic Functions
        /// <summary>
        /// <para>Add money to this instance</para>
        /// </summary>
        /// <param name="amount">Amount of stone coins to add</param>
        public void AddMoney(int amount)
        {
            AllCoins += amount;
        }
        /// <summary>
        /// <para>Add money to this instance</para>
        /// </summary>
        /// <param name="stone">Amount of stone coins to add</param>
        /// <param name="copper">Amount of copper coins to add</param>
        /// <param name="silver">Amount of silver coins to add</param>
        public void AddMoney(int stone, int copper, int silver)
        {
            AllCoins += stone;
            AllCoins += copper * _copperRate;
            AllCoins += silver * _silverRate;
        }
        /// <summary>
        /// <para>Remove money from current instance</para>
        /// </summary>
        /// <param name="amount">Amount to remove (in stone coins)</param>
        public void SubtractMoney(int amount)
        {
            if (AllCoins >= amount)
                AllCoins -= amount;
            else AllCoins = 0;
        }
        /// <summary>
        /// <para>Removes an amount of money from the Currency instance and transfers it to the 'other' instance.</para>
        /// </summary>
        /// <param name="amount">Amount in stone coins</param>
        /// <param name="other">The other instance of Currency class.</param>
        /// <returns>True if success; false if not enough funds</returns>
        public bool PayMoney(int amount, Currency other=null)
        {
            if (amount >= AllCoins)
            {
                AllCoins -= amount;
                if (other != null)
                    other.AllCoins += amount;
                return true;
            }
            else return false;
        }
        #endregion

        public override string ToString()
        {
            return String.Format("({0}): {1}; {2}; {3};", allCoins, StoneCoins, CopperCoins, SilverCoins);
        }
    }
}
