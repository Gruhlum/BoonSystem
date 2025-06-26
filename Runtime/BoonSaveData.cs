using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.BoonSystem
{
    [System.Serializable]
    public class BoonSaveData
    {
        public List<BoonEffect> boonEffects = new List<BoonEffect>();

        public BoonSaveData(List<BoonEffect> boonEffects)
        {
            this.boonEffects = boonEffects;
        }
    }
}