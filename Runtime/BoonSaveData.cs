using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.BoonSystem
{
    [System.Serializable]
    public class BoonSaveData
    {
        public List<IDisplayable> boonEffects = new List<IDisplayable>();

        public BoonSaveData(List<IDisplayable> boonEffects)
        {
            this.boonEffects = boonEffects;
        }
    }
}