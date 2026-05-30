using UnityEngine;
using UnityEngine.UI;
using Watermelon.GameModule;
using Watermelon.Message;

namespace Watermelon
{
    

    [RequireComponent(typeof(Canvas)), RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UIPage : MonoBehaviour
    {
        protected bool isPageDisplayed;
        public bool IsPageDisplayed { get => isPageDisplayed; set => isPageDisplayed = value; }

        protected Canvas canvas;
        public Canvas Canvas => canvas;

        protected GraphicRaycaster graphicRaycaster;
        public GraphicRaycaster GraphicRaycaster => graphicRaycaster;

        public void CacheComponents()
        {
            canvas = GetComponent<Canvas>();
            graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        public abstract void Initialise();

        public void EnableCanvas()
        {
            isPageDisplayed = true;

            canvas.enabled = true;
        }

        public void DisableCanvas()
        {
            isPageDisplayed = false;

            canvas.enabled = false;

            UIController.SetGameUIInputState(true);
        }

        
        protected T GetModule<T>() where T : GameModuleBase
        {
            return GameGlobal.Instance.GetModule<T>();
        }
        
        public abstract void PlayShowAnimation(object param = null);
        public abstract void PlayHideAnimation();

        public virtual void Unload()
        {
            isPageDisplayed = false;

            canvas.enabled = false;
        }

        public void ShowError(GameErrorCode err)
        {
            switch (err)
            {
                case GameErrorCode.Fail          : FloatingMessage.ShowMessage("请重新登陆"); break;
                case GameErrorCode.InvaildAccount: FloatingMessage.ShowMessage("请登录"); break;
                case GameErrorCode.PasswordError: FloatingMessage.ShowMessage("密码错误"); break;
                case GameErrorCode.InvaildUserId: FloatingMessage.ShowMessage("角色错误"); break;
                case GameErrorCode.InvaildToken: FloatingMessage.ShowMessage("开始登录"); break;
                case GameErrorCode.DBError: FloatingMessage.ShowMessage("服务器错误"); break;
                case GameErrorCode.IdCardLengthError: FloatingMessage.ShowMessage("证件号码长度错误"); break;
                case GameErrorCode.IdCardDigitError: FloatingMessage.ShowMessage("证件号码无效字符"); break;
                case GameErrorCode.IdCardAgeError: FloatingMessage.ShowMessage("证件号码年龄不合法"); break;
                case GameErrorCode.IdCardAreaError: FloatingMessage.ShowMessage("证件号码区域无效"); break;
                case GameErrorCode.IdCardCheckSumError: FloatingMessage.ShowMessage("证件号码不符合规范"); break;
                case GameErrorCode.NickNameIsNull: FloatingMessage.ShowMessage("昵称为空"); break;
                case GameErrorCode.HeadIconIsNull: FloatingMessage.ShowMessage("未选择头像"); break;
                case GameErrorCode.AccountExists: FloatingMessage.ShowMessage("账号已存在"); break;
                case GameErrorCode.UserOrPwdNull: FloatingMessage.ShowMessage("用户名和密码不能为空"); break;
                case GameErrorCode.AgeCannotLoginNow: NotifyDialog.NotifyClose(DialogState.Notice, "提示","    您当前处于防沉迷保护中，当前时段为未成年人限制在线时段，您暂时无法登录游戏。"); break;
                case GameErrorCode.AgeDayDuringMoreThanOneHour: NotifyDialog.NotifyClose(DialogState.Notice, "提示","    您当前处于防沉迷保护中，今日累计游戏1小时，已达到上限，您暂时无法登录游戏。"); break;
                    
                    
                
                case GameErrorCode.Succ: FloatingMessage.ShowMessage("成功"); break;
                default:
                    FloatingMessage.ShowMessage("错误码:" + err); break;
            }
        }
    }
}