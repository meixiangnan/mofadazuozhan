using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class UINoticePanel : MonoBehaviour
    {
        public Button closeBtn;
        public ScrollRect scrollRect;
        public TextMeshProUGUI title;
        public TextMeshProUGUI content;
        //public Text content;
        
        private bool isInit = false;

        public void SetData(string _title, string _content)
        {
            if (!isInit)
            {
                isInit = true;
                closeBtn.onClick.AddListener(this.OnClickCloseBtn);
            }
            
            title.text = _title;
            content.text = _content;
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 1f);
        }
        
        

        private void OnClickCloseBtn()
        {
            this.gameObject.SetActive(false);
        }
    }
}