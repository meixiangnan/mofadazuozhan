using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class UIHeroBookItem : MonoBehaviour
    {
        [SerializeField] Image heroIcon;
        [SerializeField] GameObject lockObj;
        [SerializeField] TextMeshProUGUI heroNameText;
        [SerializeField] TextMeshProUGUI heroIdText;

        private int heroId;
        public int HeroId => heroId;

        public void SetData(HeroBookData data, bool unlocked)
        {
            heroId = data.heroId;

            if (heroIcon != null)
            {
                heroIcon.sprite = data.heroSprite;
            }

            if (lockObj != null)
            {
                lockObj.SetActive(!unlocked);
            }

            if (heroNameText != null)
            {
                heroNameText.text = data.heroName;
            }

            if (heroIdText != null)
            {
                heroIdText.text = data.heroId.ToString();
            }
        }
    }
}
