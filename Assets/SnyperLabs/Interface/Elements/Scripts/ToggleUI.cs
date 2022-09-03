using System;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Elements.Scripts
{
    public class ToggleUI : BaseUI
    {
        private const float Duration1 = 0.4f;
        private const float Duration2 = 0.15f;

        private bool isOn;
        private bool isDragging;
        private float startingX;
        private RectTransform rect;
        private Toggle toggle;
        
        public Image background;
        public Image outline;
        public Image highlighter;

        [Space]

        public Text onText;
        public Image onImage;
        
        [Space]

        public Text offText;
        public Image offImage;

        [Space] 
        
        [Tooltip("The highlighter on the left signifies ON")] 
        public bool leftIsOn;


        private void OnValidate()
        {
            background.color = primaryColor;
            outline.color = primaryColor;
            highlighter.color = secondaryColor;

            differentTextColorOnHighlight = true;
        }
        

        protected override void Start()
        {
            rect = GetComponent<RectTransform>();
            startingX = highlighter.transform.localPosition.x;
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(SetValue);
            isOn = toggle.isOn;
            
            base.Start();
            Press();
        }

        private void Update()
        {
            if (isDragging) Drag();
        }

        private void SetValue(bool on)
        {
            isOn = on;
            Press();
        }

        private void Drag()
        {
            var clamp = rect.rect.width / 4;

            var pos = highlighter.transform.localPosition;
            if (pos.x > Math.Abs(clamp))
                return;

            var dragPos = Input.mousePosition.x - transform.position.x;
            dragPos = Mathf.Clamp(dragPos, -clamp, clamp);
            highlighter.transform.localPosition = new Vector3(dragPos, pos.y, pos.z);

            if (leftIsOn)
            {
                toggle.isOn = dragPos < startingX;
            }
            else
            {
                toggle.isOn = dragPos > startingX;
            }
        }

        public void BeginDrag()
        {
            isDragging = true;
        }

        public void EndDrag()
        {
            isDragging = false;
            Press();
        }

        public override void Press()
        {
            base.Press();

            if (isDragging) return;

            
            var width = rect.rect.width / 4;
            float to;
            
            if (isOn)
            {
                if (leftIsOn)
                {
                    to = startingX - width;
                }
                else
                {
                    to = startingX + width;
                }
            }
            else
            {
                if (leftIsOn)
                {
                    to = startingX + width;
                }
                else
                {
                    to = startingX - width;
                }
            }
            
            highlighter.TweenLocalPositionX(to, Duration1).SetEase(EaseType.ExpoInOut);

            if (isOn)
            {
                background.TweenGraphicColor(primaryColor, Duration2);
                onText.TweenGraphicAlpha(1, Duration2);
                onImage.TweenGraphicAlpha(1, Duration2);
                offText.TweenGraphicAlpha(0, Duration2);
                offImage.TweenGraphicAlpha(0, Duration2);
            }
            else
            {
                background.TweenGraphicColor(highlightTextColor, Duration2);
                onText.TweenGraphicAlpha(0, Duration2);
                onImage.TweenGraphicAlpha(0, Duration2);
                offText.TweenGraphicAlpha(1, Duration2);
                offImage.TweenGraphicAlpha(1, Duration2);
            }
        }
    }
}