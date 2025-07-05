using System;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames.BoonSystem
{
    public class BoonIcon : MonoBehaviour, ISetup<IDisplayable>, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private Image background = default;
        [Space]
        [SerializeField] private Color normalColor = Color.black;
        [SerializeField] private Color inactiveColor = Color.gray;
        [SerializeField] private Color selectedColor = Color.green;
        [SerializeField] private Color hoverColor = Color.white;


        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                SetBackgroundColor();
            }
        }
        private bool isSelected;

        public bool IsInactive
        {
            get
            {
                return isInactive;
            }
            set
            {
                isInactive = value;
                SetBackgroundColor();
            }
        }
        private bool isInactive;

        private bool isHovering;

        public IDisplayable BoonEffect
        {
            get
            {
                return boonEffect;
            }
            private set
            {
                boonEffect = value;
            }
        }
        private IDisplayable boonEffect;


        public event Action<BoonIcon> OnHoverStarted;
        public event Action<BoonIcon> OnHoverEnded;
        public event Action<BoonIcon> OnClicked;

        public void Setup(IDisplayable boonEffect)
        {
            this.BoonEffect = boonEffect;
            icon.sprite = boonEffect.Icon;
            SetBackgroundColor();
        }

        private void SetBackgroundColor()
        {
            background.color = GetBackgroundColor();
        }

        private Color GetBackgroundColor()
        {
            if (!IsSelected && !IsInactive && !isHovering)
            {
                return normalColor;
            }

            ColorMixer colorMixer = new ColorMixer();

            if (IsSelected)
            {
                colorMixer.Add(selectedColor);
            }
            if (IsInactive)
            {
                colorMixer.Add(inactiveColor);
            }
            if (isHovering)
            {
                colorMixer.Add(hoverColor, 0.5f);
            }
            Color color = colorMixer.Mix();
            return color;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsInactive)
            {
                return;
            }
            OnClicked?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
            SetBackgroundColor();
            OnHoverStarted?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
            SetBackgroundColor();
            OnHoverEnded?.Invoke(this);
        }
    }
}