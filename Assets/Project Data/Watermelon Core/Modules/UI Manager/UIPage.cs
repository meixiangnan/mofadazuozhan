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
                case GameErrorCode.IdCardLengthError: FloatingMessage.ShowMessage("请输入正确的身份证号码"); break;
                case GameErrorCode.IdCardDigitError: FloatingMessage.ShowMessage("请输入正确的身份证号码"); break;
                case GameErrorCode.IdCardAgeError: FloatingMessage.ShowMessage("请输入正确的身份证号码"); break;
                case GameErrorCode.IdCardAreaError: FloatingMessage.ShowMessage("请输入正确的身份证号码"); break;
                case GameErrorCode.IdCardCheckSumError: FloatingMessage.ShowMessage("请输入正确的身份证号码"); break;
                case GameErrorCode.NickNameIsNull: FloatingMessage.ShowMessage("昵称为空"); break;
                case GameErrorCode.HeadIconIsNull: FloatingMessage.ShowMessage("未选择头像"); break;
                case GameErrorCode.AccountExists: FloatingMessage.ShowMessage("账号已存在"); break;
                case GameErrorCode.UserOrPwdNull: FloatingMessage.ShowMessage("用户名和密码不能为空"); break;
                case GameErrorCode.AgeCannotLoginNow: NotifyDialog.NotifyClose(DialogState.Notice, "防沉迷提示","        您当前登录的是未成年人帐号，已被纳入防沉迷系统。根据国家新闻出版署《关于防止未成年人沉迷网络游戏的通知》与《关于进一步严格管理 切实防止未成年人沉迷网络游戏的通知》，您可在周五、周六、周日和法定节假日的20：00-21：00登入游戏。\n        您当日剩余游戏时长已不足15分钟，请注意您的游戏时长。"); break;
                case GameErrorCode.AgeDayDuringMoreThanOneHour: NotifyDialog.NotifyClose(DialogState.Notice, "防沉迷提示","        您当前登录的是未成年人帐号，已被纳入防沉迷系统。根据国家新闻出版署《关于防止未成年人沉迷网络游戏的通知》与《关于进一步严格管理 切实防止未成年人沉迷网络游戏的通知》，您可在周五、周六、周日和法定节假日的20：00-21：00登入游戏。\n        您当日剩余游戏时长已不足15分钟，请注意您的游戏时长。"); break;
                    
                    
                
                case GameErrorCode.Succ: FloatingMessage.ShowMessage("成功"); break;
                default:
                    FloatingMessage.ShowMessage("错误码:" + err); break;
            }
        }
    }
}