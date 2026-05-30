using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;

namespace Watermelon
{
    public class ShowUICompleteParam
    {
        public List<PUType> rewards = null;
    }

    public class UIComplete : UIPage
    {
        [SerializeField] RectTransform safeAreaTransform;

        [Space]
        [SerializeField] UIFadeAnimation backgroundFade;
        [SerializeField] UIScaleAnimation levelCompleteLabel;



        [Header("Buttons")]
        [SerializeField] UIFadeAnimation multiplyRewardButtonFade;
        [SerializeField] UIScaleAnimation homeButtonScaleAnimation;
        [SerializeField] UIScaleAnimation nextLevelButtonScaleAnimation;
        [SerializeField] Button multiplyRewardButton;
        [SerializeField] Button closeButton;
        [SerializeField] Button homeButton;
        [SerializeField] Button nextLevelButton;

        public Image reward1;
        public Image reward2;
        public Image reward3;


        private TweenCase noThanksAppearTween;

        private int coinsHash = FloatingCloud.StringToHash("Coins");
        private int currentReward;

        public override void Initialise()
        {
            homeButton.onClick.AddListener(HomeButton);
            nextLevelButton.onClick.AddListener(NextLevelButton);
            closeButton.onClick.AddListener(HomeButton);

            //coinsPanelUI.Initialise();

            NotchSaveArea.RegisterRectTransform(safeAreaTransform);
        }

        #region Show/Hide
        public override void PlayShowAnimation(object param = null)
        {
            if (isPageDisplayed)
                return;

            isPageDisplayed = true;
            canvas.enabled = true;

            if (null != param && param is ShowUICompleteParam)
            {
                var inParam = param as ShowUICompleteParam;
                for (int i = 0; i < inParam.rewards.Count; i++)
                {
                    var puType =  inParam.rewards[i];

                    var puInst = PUController.GetPowerUpBehavior(puType);
                    if (0 == i)
                    {
                        reward1.sprite = puInst.Settings.Icon;
                        
                    }else if (1 == i)
                    {
                        reward2.sprite = puInst.Settings.Icon;
                    }
                    else if (2 == i)
                    {
                        reward3.sprite = puInst.Settings.Icon;
                    }
                }

            }

            multiplyRewardButtonFade.Hide(immediately: true);
            multiplyRewardButton.interactable = false;

            backgroundFade.Show(duration: 0.3f);
            levelCompleteLabel.Show();


            currentReward = LevelController.CurrentReward;

            ShowRewardLabel(currentReward, false, 0.3f, delegate
            {
            });
        }

        public override void PlayHideAnimation()
        {
            if (!isPageDisplayed)
                return;

            backgroundFade.Hide(0.25f);

            Tween.DelayedCall(0.25f, delegate
            {
                canvas.enabled = false;
                isPageDisplayed = false;

                UIController.OnPageClosed(this);
            });
        }


        #endregion

        #region RewardLabel

        public void ShowRewardLabel(float rewardAmounts, bool immediately = false, float duration = 0.3f, Action onComplted = null)
        {
           
        }

        #endregion

        #region Buttons
        

        public void NextLevelButton()
        {
            AudioController.PlaySound(AudioController.Sounds.buttonSound);

            UIController.HidePage<UIComplete>(() =>
            {
                GameController.LoadNextLevel();
            });
        }

        public void HomeButton()
        {
            AudioController.PlaySound(AudioController.Sounds.buttonSound);

            UIController.HidePage<UIComplete>(() =>
            {
                GameController.ReturnToMenu();
            });

            LivesManager.AddLife();
        }

        #endregion
    }
}
