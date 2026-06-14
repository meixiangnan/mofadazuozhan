using System.Collections.Generic;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Watermelon
{
    [RequireComponent(typeof(Canvas)), RequireComponent(typeof(GraphicRaycaster))]
    public class UIHeroBook : UIPage
    {
        [SerializeField] Button returnBtn;
        [SerializeField] LoopGridView heroGridView;
        [SerializeField] string itemPrefabName = "HeroBookItem";
        [SerializeField] HeroBookData[] heroDatas;
        [SerializeField] int maxAutoHeroId = 30;

        [Header("Hero Detail")]
        [SerializeField] GameObject heroPanel;
        [SerializeField] Image heroImage;
        [SerializeField] Button heroPanelReturnBtn;

        public override void Initialise()
        {
            returnBtn.onClick.AddListener(OnReturn);
            if (heroPanelReturnBtn != null)
            {
                heroPanelReturnBtn.onClick.AddListener(HideHeroPanel);
            }
            if (heroPanel != null)
            {
                heroPanel.SetActive(false);
            }
            heroGridView.InitGridView(GetHeroCount(), GetHeroItem);
        }

        public override void PlayShowAnimation(object param = null)
        {
            HideHeroPanel();
            heroGridView.SetListItemCount(GetHeroCount());
            heroGridView.RefreshAllShownItem();
            UIController.OnPageOpened(this);
        }

        public override void PlayHideAnimation()
        {
            UIController.OnPageClosed(this);
        }

        private int GetHeroCount()
        {
            return heroDatas == null ? 0 : heroDatas.Length;
        }

        private LoopGridViewItem GetHeroItem(LoopGridView gridView, int index, int row, int column)
        {
            if (index < 0 || index >= heroDatas.Length)
            {
                return null;
            }

            HeroBookData data = heroDatas[index];
            LoopGridViewItem item = gridView.NewListViewItem(itemPrefabName);
            UIHeroBookItem heroItem = item.GetComponent<UIHeroBookItem>();
            heroItem.OnClickHero = OnClickHeroItem;
            heroItem.SetData(data, IsHeroUnlocked(data.heroId, data.defaultUnlocked));
            return item;
        }

        private bool IsHeroUnlocked(int heroId, bool defaultUnlocked)
        {
            return PlayerPrefs.GetInt(GetHeroUnlockKey(heroId), defaultUnlocked ? 1 : 0) == 1;
        }

        public static string GetHeroUnlockKey(int heroId)
        {
            int userId = -1;
            RoleModule roleModule = GameGlobal.Instance.GetModule<RoleModule>();
            if (roleModule != null && roleModule.userData != null)
            {
                userId = roleModule.userData.UserId;
            }

            if (userId > 0)
            {
                return $"HeroUnlocked_{userId}_{heroId}";
            }

            return $"HeroUnlocked_{heroId}";
        }

        private void OnReturn()
        {
            UIController.HidePage<UIHeroBook>();
        }

        private void OnClickHeroItem(HeroBookData data)
        {
            if (data == null)
            {
                return;
            }

            if (heroImage != null)
            {
                heroImage.sprite = data.heroDetailSprite != null ? data.heroDetailSprite : data.heroSprite;
            }

            ShowHeroPanel();
        }

        private void ShowHeroPanel()
        {
            if (heroPanel != null)
            {
                heroPanel.SetActive(true);
            }
        }

        private void HideHeroPanel()
        {
            if (heroPanel != null)
            {
                heroPanel.SetActive(false);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            List<HeroBookData> datas = new List<HeroBookData>();
            for (int i = 1; i <= maxAutoHeroId; i++)
            {
                string cardAssetPath = $"Assets/Project Data/Game/Images_new/hero_card/Collection_{i}.png";
                string detailAssetPath = $"Assets/Project Data/Game/Images_new/hero/hero_{i}.png";
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(cardAssetPath);
                Sprite detailSprite = AssetDatabase.LoadAssetAtPath<Sprite>(detailAssetPath);
                if (sprite == null)
                {
                    continue;
                }

                datas.Add(new HeroBookData
                {
                    heroId = i,
                    heroName = $"英雄{i}",
                    heroSprite = sprite,
                    heroDetailSprite = detailSprite,
                    defaultUnlocked = false
                });
            }

            heroDatas = datas.ToArray();
        }
#endif
    }
}
