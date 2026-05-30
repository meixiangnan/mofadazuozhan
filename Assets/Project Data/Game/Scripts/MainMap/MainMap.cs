using System;
using System.Collections;
using System.Collections.Generic;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Watermelon.MainMap;
using Watermelon.Map;

namespace Watermelon.MainMap
{
    


public class MainMap : MonoBehaviour
{
    private int StartLevel = 1;
    private int EndLevel = 100;
    

    private readonly int MaxChapterLevelNum = 100; 
    
    private int GroupCnt = 0;
    
    public Image chapterBg;
    
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