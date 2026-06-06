using System;
using UnityEngine;

namespace Watermelon
{
    [Serializable]
    public class HeroBookData
    {
        public int heroId;
        public string heroName;
        public Sprite heroSprite;
        public Sprite heroDetailSprite;
        public bool defaultUnlocked;
    }
}
