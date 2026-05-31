using UnityEngine;
using Watermelon.GameModule;

namespace Watermelon
{
    public class DiamondModule : GameModuleBase
    {
        private const string DIAMOND_KEY = "PlayerDiamond";
        
        private int diamondCount;
        public int DiamondCount => diamondCount;

        public event System.Action OnDiamondChanged;

        public override void Init(GameModuleManager mngr)
        {
            base.Init(mngr);
            LoadDiamond();
        }

        private void LoadDiamond()
        {
            diamondCount = PlayerPrefs.GetInt(DIAMOND_KEY, 0);
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
