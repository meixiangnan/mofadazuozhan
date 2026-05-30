using System;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon.elems
{
    public class HeadIcon : MonoBehaviour
    {
        public Button button;
        public Image ImageHead;
        public GameObject SelectMask;
        
        private string HeadName;

        private bool canSelect = true;
        
        public Action<string> OnSelect;
        
        public void Start()
        {
            button.onClick.AddListener(() =>
            {
                
                SetSelected(true);
                OnSelect?.Invoke(HeadName);
            });
        }

        public void SetData(string headName, bool isSelect = false)
        {
            if (string.IsNullOrEmpty(headName) || headName.Length < 5) 
            {
                return;
            }

            HeadName = headName;
            ImageHead.sprite = GetSprite(headName);
            SelectMask.SetActive(isSelect);
        }

        public void SetCanSelect(bool can)
        {
            this.canSelect = can;
        }

        public void SetSelected(bool isSelect)
        {
            if (canSelect)
            {
                SelectMask.SetActive(isSelect);
            }
            else
            {
                SelectMask.SetActive(false);
            }
        }
        
        private Sprite GetSprite(string headName)
        {
            return HeadIconController.GetHeadIcon(headName);
        }
    }
}