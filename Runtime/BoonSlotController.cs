using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.BoonSystem
{
    public class BoonSlotController : MonoBehaviour
    {
        [SerializeField] private BoonSelectWindow selectWindow = default;

        private List<BoonSlot> boonSlots = default;

        private BoonSlot activeSlot;

        private const string SAVE_KEY = "BOON_SLOTS";


        private void Start()
        {
            selectWindow.OnDeactivated += SelectWindow_OnDeactivated;

            boonSlots = GetComponentsInChildren<BoonSlot>().ToList();

            foreach (BoonSlot boonSlot in boonSlots)
            {
                boonSlot.OnClicked += BoonSlot_OnClicked;
            }
            LoadBoons();
        }

        private void OnDestroy()
        {
            SaveBoons();
        }

        public void SaveBoons()
        {
            BoonSaveData saveData = new BoonSaveData(GetActiveEffects());

            SaveSystem.SaveJSON(saveData, SAVE_KEY);
        }

        private void LoadBoons()
        {
            BoonSaveData saveData = SaveSystem.LoadJSON<BoonSaveData>(SAVE_KEY);

            if (saveData == null)
            {
                return;
            }
            if (saveData.boonEffects == null)
            {
                return;
            }
            if (saveData.boonEffects.Count > boonSlots.Count)
            {
                return;
            }

            if (saveData.boonEffects.Count != saveData.boonEffects.Distinct().Count())
            {
                return;
            }

            for (int i = 0; i < saveData.boonEffects.Count; i++)
            {
                if (boonSlots[i].IsValidEffect(saveData.boonEffects[i]))
                {
                    boonSlots[i].SetBoonEffect(saveData.boonEffects[i]);
                }
            }
        }

        public List<IDisplayable> GetActiveEffects()
        {
            List<IDisplayable> effects = new List<IDisplayable>();
            foreach (BoonSlot slot in boonSlots)
            {
                effects.Add(slot.BoonEffect);
            }

            return effects;
        }

        private void SelectWindow_OnDeactivated()
        {
            activeSlot = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (activeSlot != null)
                {
                    ClearActiveSlot();
                }
            }
        }

        private void BoonSlot_OnClicked(BoonSlot slot)
        {
            if (activeSlot == slot)
            {
                ClearActiveSlot();
                return;
            }
            activeSlot = slot;
            selectWindow.Setup(slot, GetActiveEffects());
        }

        private void ClearActiveSlot()
        {
            selectWindow.Deactivate();
            activeSlot = null;
        }
    }
}