using ElRaccoone.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Elements.Scripts
{
    public class InputUI : BaseUI
    {
        private const float Duration1 = 0.15f;
        
        private InputField inputField;
        private bool isTweening;
        private Vector3 placeholderPos;

        [Tooltip("Hide the placeholder text when selected. If false, the text will move up")]
        public bool hidePlaceholderOnSelect;
        
        [Tooltip("The placeholder text component")]
        public Text placeholder;
        
        [Tooltip("The text output")]
        public Text text;
        
        [Tooltip("Background image of the input field")]
        public Image background;


        protected override void Start()
        {
            base.Start();
            
            inputField = GetComponent<InputField>();
            placeholderPos = placeholder.transform.localPosition;
        }

        private void Update()
        {
            if (inputField.text.Length == 0) Focus(inputField.isFocused);

        }


        private void OnValidate()
        {
            var lightPrimary = primaryColor;
            lightPrimary.a = 0.7f;
            var lightSecondary = secondaryColor;
            lightSecondary.a = 0.7f;
            
            if (placeholder)
            {
                placeholder.color = lightSecondary;
            }

            if (text)
            {
                text.color = secondaryColor;
            }

            if (background)
            {
                background.color = lightPrimary;
            }
        }
        
        private void Focus(bool isFocussed)
        {
            if (isTweening) return;
            
            if (isFocussed)
            {
                if (hidePlaceholderOnSelect)
                {
                    placeholder.TweenGraphicAlpha(0f, 0.1f);
                }
                else
                {
                    isTweening = true;
                    placeholder.TweenLocalPositionY(placeholderPos.y + (Height / 1.5f), 0.1f).SetOnComplete(() => isTweening = false);
                    placeholder.TweenLocalScale(Vector3.one * 0.7f, 0.1f);
                    placeholder.TweenGraphicAlpha(0.5f, 0.1f);
                }
            }
            else
            {
                if (hidePlaceholderOnSelect)
                {
                    placeholder.TweenGraphicAlpha(1, 0.1f);
                }
                else
                {
                    isTweening = true;
                    placeholder.TweenLocalPositionY(placeholderPos.y, 0.1f).SetOnComplete(() => isTweening = false);
                    placeholder.TweenLocalScale(Vector3.one, 0.1f);
                    placeholder.TweenGraphicAlpha(1, 0.1f);
                }
            }
        }
        
        /// <summary>
        /// Highlight when the input field is being edit
        /// </summary>
        public override void Normal()
        {
            base.Normal();
            
            text.TweenGraphicColor(secondaryColor, Duration1);
            background.TweenGraphicAlpha(0.6f, Duration1);
        }

        /// <summary>
        /// Highlight when the input field has text
        /// </summary>
        public override void Highlight()
        {
            if (inputField.text.Length == 0) return;
            
            base.Highlight();
            
            text.TweenGraphicColor(secondaryColor, Duration1);
            background.TweenGraphicAlpha(0.9f, Duration1);
        }

        /// <summary>
        /// Not applicable for input fields
        /// </summary>
        public override void Press()
        {
            
        }



        public override void PointerExit()
        {
            base.PointerExit();
            Normal();
        }
    }
}