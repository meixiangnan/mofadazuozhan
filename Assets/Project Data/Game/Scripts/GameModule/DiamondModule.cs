using System;
using UnityEngine;
using Watermelon.GameModule;

namespace Watermelon
{
    public class DiamondModule : GameModuleBase
    {
        private const string DIAMOND_KEY = "PlayerDiamond";
        private const string MONTHLY_RECHARGE_MONTH_KEY = "MonthlyRechargeMonth";
        private const string MONTHLY_RECHARGE_AMOUNT_KEY = "MonthlyRechargeAmount";
        
        private int diamondCount;
        private int monthlyRechargeAmount;
        public int DiamondCount => diamondCount;
        public int MonthlyRechargeAmount
        {
            get
            {
                RefreshRechargeMonth();
                return monthlyRechargeAmount;
            }
        }

        public event System.Action OnDiamondChanged;

        public override void Init(GameModuleManager mngr)
        {
            base.Init(mngr);
            LoadDiamond();
        }

        private void LoadDiamond()
        {
            diamondCount = PlayerPrefs.GetInt(DIAMOND_KEY, 0);
            RefreshRechargeMonth();
        }

        private void RefreshRechargeMonth()
        {
            string currentMonth = DateTime.Now.ToString("yyyyMM");
            string savedMonth = PlayerPrefs.GetString(MONTHLY_RECHARGE_MONTH_KEY, "");
            if (savedMonth != currentMonth)
            {
                monthlyRechargeAmount = 0;
                PlayerPrefs.SetString(MONTHLY_RECHARGE_MONTH_KEY, currentMonth);
                PlayerPrefs.SetInt(MONTHLY_RECHARGE_AMOUNT_KEY, monthlyRechargeAmount);
                PlayerPrefs.Save();
                return;
            }

            monthlyRechargeAmount = PlayerPrefs.GetInt(MONTHLY_RECHARGE_AMOUNT_KEY, 0);
        }

        public void RecordRecharge(int yuanAmount)
        {
            RefreshRechargeMonth();
            monthlyRechargeAmount += yuanAmount;
            PlayerPrefs.SetInt(MONTHLY_RECHARGE_AMOUNT_KEY, monthlyRechargeAmount);
            PlayerPrefs.Save();
        }

        private void SaveDiamond()
        {
            PlayerPrefs.SetInt(DIAMOND_KEY, diamondCount);
            PlayerPrefs.Save();
        }

        public void AddDiamond(int amount)
        {
            diamondCount += amount;
            SaveDiamond();
            OnDiamondChanged?.Invoke();
        }

        public bool SpendDiamond(int amount)
        {
            if (diamondCount >= amount)
            {
                diamondCount -= amount;
                SaveDiamond();
                OnDiamondChanged?.Invoke();
                return true;
            }
            return false;
        }
    }
}
