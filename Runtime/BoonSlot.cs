using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.BoonSystem
{
    public class BoonSlot : MonoBehaviour
    {
        public RectTransform RectTransform
        {
            get
            {
                return rectTransform;
            }
            private set
            {
                rectTransform = value;
            }
        }
        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private TMP_Text textGUI = default;
        [SerializeField] private Image icon = default;
        [Space]
        [SerializeField] private Sprite emptyIcon = default;
        [Space]
        [SerializeField] private BoonEffectGroup boonGroup = default;
        public BoonEffectGroup BoonGroup
        {
            get
            {
                return this.boonGroup;
            }
            private set
            {
                this.boonGroup = value;
            }
        }

        public BoonEffect BoonEffect
        {
            get
            {
                return this.boonEffect;
            }
            private set
            {
                this.boonEffect = value;
            }
        }
        private BoonEffect boonEffect;

        public event Action<BoonSlot> OnClicked;
        public event Action<BoonSlot, BoonEffect> OnBoonEffectChanged;


        public void SetBoonEffect(BoonEffect boonEffect)
        {
            this.BoonEffect = boonEffect;
            if (boonEffect != null)
            {
                icon.sprite = boonEffect.icon;
                textGUI.text = boonEffect.name;
            }
            else
            {
                icon.sprite = emptyIcon;
                textGUI.text = string.Empty;
            }
            OnBoonEffectChanged?.Invoke(this, boonEffect);
        }

        public bool IsValidEffect(BoonEffect effect)
        {
            if (!BoonGroup.Items.Contains(effect))
            {
                return false;
            }
            return true;
        }

        public void ClearBoon()
        {
            SetBoonEffect(null);
        }

        public void Clicked()
        {
            OnClicked?.Invoke(this);
        }
    }
}