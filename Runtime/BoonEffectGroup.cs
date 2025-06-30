using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.BoonSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/BoonEffectGroup")]
    public class BoonEffectGroup : SerializableCollection<ScriptableObject, IDisplayable>
    {
        
    }
}