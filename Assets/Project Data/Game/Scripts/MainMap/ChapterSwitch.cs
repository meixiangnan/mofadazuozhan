using UnityEngine;
using UnityEngine.UI;

namespace Watermelon.MainMap
{
    public class ChapterSwitch : MonoBehaviour
    {
        public int ChapterIndex = 0;
        public Sprite bg;
        public Button btn;
        
        public delegate void SelectAction(int index, Sprite bg);
        public event SelectAction OnSelect;

        public void SetSelect()
        {
            OnSelect?.Invoke(ChapterIndex, bg);
        }

        void Start()
        {
            btn.onClick.AddListener(() =>
            {
                OnSelect?.Invoke(ChapterIndex, bg);
            });
        }
    }
}