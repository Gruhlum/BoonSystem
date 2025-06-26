using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.TooltipSystem;
using TMPro;
using UnityEngine;

namespace HexTecGames.BoonSystem
{
    public class BoonSelectWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text tooltipGUI = default;
        [SerializeField] private MoveableWindow moveableWindow = default;

        [SerializeField] private SetupSpawner<BoonIcon, BoonEffect> boonIconSpawner = default;

        private List<BoonIcon> activeIcons = new List<BoonIcon>();

        private BoonSlot activeSlot;

        public event Action OnDeactivated;

        public void Setup(BoonSlot slot, List<BoonEffect> activeEffects)
        {
            this.activeSlot = slot;

            foreach (var activeIcon in activeIcons)
            {
                activeIcon.OnClicked -= Icon_OnClicked;
                activeIcon.OnHoverStarted -= Icon_OnHoverStarted;
                activeIcon.OnHoverEnded -= Icon_OnHoverEnded;
            }

            activeIcons = boonIconSpawner.DeactivateAllAndSpawnAndSetup(slot.BoonGroup.Items);

            foreach (var icon in activeIcons)
            {
                icon.OnClicked += Icon_OnClicked;
                icon.OnHoverStarted += Icon_OnHoverStarted;
                icon.OnHoverEnded += Icon_OnHoverEnded;
            }

            if (activeEffects != null && activeEffects.Count > 0)
            {
                foreach (var icon in activeIcons)
                {
                    if (icon.BoonEffect == slot.BoonEffect)
                    {
                        icon.IsSelected = true;
                        icon.IsInactive = false;
                        continue;
                    }
                    
                    if (activeEffects.Contains(icon.BoonEffect))
                    {
                        icon.IsInactive = true;
                        icon.IsSelected = true;
                    }
                    else
                    {
                        icon.IsSelected = false;
                        icon.IsInactive = false;
                    } 
                }
            }
            moveableWindow.Setup(slot.RectTransform, true);
        }
        public void Deactivate()
        {
            gameObject.SetActive(false);
            OnDeactivated?.Invoke();
        }
        private void Icon_OnHoverEnded(BoonIcon icon)
        {
            tooltipGUI.text = string.Empty;
        }

        private void Icon_OnHoverStarted(BoonIcon icon)
        {
            tooltipGUI.text = icon.BoonEffect.description;
        }

        private void Icon_OnClicked(BoonIcon icon)
        {
            foreach (var activeIcon in activeIcons)
            {
                if (activeIcon.IsSelected && !activeIcon.IsInactive)
                {
                    activeIcon.IsSelected = false;
                }
            }
            icon.IsSelected = !icon.IsSelected;
            activeSlot.SetBoonEffect(icon.BoonEffect);
        }
    }
}