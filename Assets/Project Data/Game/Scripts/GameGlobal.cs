using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.GameModule;

namespace Watermelon
{
    
    public class GameGlobal : MonoBehaviour
    {
        public static GameGlobal Instance;
        private void Awake()
        {
            Instance = this;
        }
        
        GameModuleManager gameModuleManager = new GameModuleManager();
        
        public void Init()
        {
            gameModuleManager.Init();
        }

        public T GetModule<T>() where T : GameModuleBase
        {
            return gameModuleManager.GetModule<T>();
        }

        public void Update()
        {
            gameModuleManager.TickModule();
        }

        private bool isUpload = false;
        public void UploadRoleData()
        {
            if (isUpload)
            {
                return;
            }

            isUpload = true;
            var mdl = GetModule<RankModule>();
            StartCoroutine(OnlyOneTask(mdl.UploadRoleData()));
        }
        public IEnumerator OnlyOneTask(IEnumerator co)
        {
            yield return co;
            isUpload = false;
        }
    }
}