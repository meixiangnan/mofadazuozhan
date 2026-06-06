using System;
using System.Collections;
using System.Collections.Generic;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Watermelon.MainMap;
using Watermelon.Map;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Watermelon.MainMap
{
    


public class MainMap : MonoBehaviour
{
    private int StartLevel = 1;
    private int EndLevel = 100;
    

    private readonly int MaxChapterLevelNum = 100; 
    
    private int GroupCnt = 0;
    
    public Image chapterBg;
    [SerializeField] private Sprite[] randomBackgrounds;
    [SerializeField] private int maxRandomBackgroundIndex = 15;
    
    public ScrollRect scrollRect;
    public LoopListView2 scrollView;
    
    public List<ChapterSwitch> chapterSwitches;
    
    public void Initialized()
    {
        for (int i = 0; i < chapterSwitches.Count; i++)
        {
            chapterSwitches[i].OnSelect += (index, bg) =>
            {
                this.SwitchChapter(index, bg);
            };
        }
        
        scrollView.InitListView(GroupCnt, GetItemCount);
        
        chapterSwitches[0].SetSelect();
    }

    public void Refresh()
    {
        RandomizeBackground();
        scrollView.RefreshAllShownItem();
    }


    public void SwitchChapter(int chapterIndex, Sprite bg)
    {
        StartLevel = 1 + (chapterIndex - 1) * MaxChapterLevelNum;
        EndLevel = StartLevel + MaxChapterLevelNum - 1;
        GroupCnt = ((EndLevel - StartLevel) / ChapterLevelGroup.LevelMaxNum) + 1;
        
        scrollRect.StopMovement();
        scrollView.SetListItemCount(-1, true);
        scrollView.SetListItemCount(GroupCnt,true);
        scrollView.ResetListView();
        scrollView.RefreshAllShownItemWithFirstIndex(0);

        //将bg应用给chapterBg
        chapterBg.sprite = bg;
        
    }
    private void RandomizeBackground()
    {
        if (chapterBg == null || randomBackgrounds == null || randomBackgrounds.Length == 0)
        {
            return;
        }

        Sprite randomSprite = randomBackgrounds[UnityEngine.Random.Range(0, randomBackgrounds.Length)];
        if (randomSprite != null)
        {
            chapterBg.sprite = randomSprite;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 1; i <= maxRandomBackgroundIndex; i++)
        {
            string assetPath = $"Assets/Project Data/Game/Images_new/main_ui/mainui-{i}.png";
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite != null)
            {
                sprites.Add(sprite);
            }
        }

        randomBackgrounds = sprites.ToArray();
    }
#endif


    private LoopListViewItem2 GetItemCount(LoopListView2 loopListView, int index)
    {
        if (index < 0 || index >= EndLevel) return null;

        int startLevel = StartLevel + index * ChapterLevelGroup.LevelMaxNum;
        int endLevel = startLevel + ChapterLevelGroup.LevelMaxNum;
        if (endLevel > EndLevel)
        {
            endLevel = EndLevel;
        }

        LoopListViewItem2 item = loopListView.NewListViewItem("ChapterLevelGroup");
        var _itemInfo = item.GetComponent<ChapterLevelGroup>();
        _itemInfo.SetData(startLevel, endLevel);
        return item;
    }

    public void Show()
    {
    }

    public void Hide()
    {
        
    }
}


}