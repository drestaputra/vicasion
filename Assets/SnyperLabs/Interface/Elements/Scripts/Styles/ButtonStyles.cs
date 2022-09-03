using System;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;

namespace Interface.Elements.Scripts.Styles
{
    public static class ButtonStyles
    {
        private const float Duration1 = 0.15f;
        private const float Duration2 = 0.2f;
        
        /// <summary>
        /// The button style to transition to when the button is in normal state
        /// </summary>
        /// <param name="button">Reference to the button component</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Normal(ButtonUI button)
        {
            var primary = button.primaryColor;
            var secondary = button.secondaryColor;
            var style = button.style;
            var colorGrey = new Color(0.8f, 0.8f, 0.8f);

            if (button.mainText) button.mainText.TweenGraphicColor(secondary, Duration1);
            button.images[0].TweenGraphicAlpha(0.1f, Duration1);

            switch (style)
            {
                case Styles.Style1:
                    button.images[1].TweenGraphicColor(primary, Duration1);
                    break;
                
                case Styles.Style2:
                    button.images[1].TweenGraphicAlpha(0, Duration1);
                    break;
                
                case Styles.Style3:
                    button.images[1].TweenGraphicAlpha(0, Duration1);
                    button.slider.TweenValueFloat(0, Duration2, f =>
                    {
                        button.slider.value = f;
                    });
                    break;
                
                case Styles.Style4:
                    button.images[1].TweenGraphicColor(colorGrey, Duration1);
                    break;

                case Styles.Style5:
                    button.images[1].TweenGraphicColor(colorGrey, Duration1);
                    button.images[1].TweenLocalScale(Vector3.one, Duration1);
                    break;

                case Styles.Style6:
                    button.images[1].TweenGraphicColor(colorGrey, Duration1);
                    button.images[2].TweenGraphicAlpha(0.7f, Duration1);
                    break;
                
                case Styles.Style7:
                    button.images[1].TweenGraphicColor(colorGrey, Duration1);
                    button.images[1].TweenLocalScale(Vector3.one, Duration1);
                    button.images[2].TweenGraphicAlpha(0.7f, Duration1);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }


        /// <summary>
        /// The button style to transition to when the button is highlighted
        /// </summary>
        /// <param name="button">Reference to the button component</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Highlight(ButtonUI button)
        {
            var primary = button.primaryColor;
            var secondary = button.secondaryColor;
            var style = button.style;
            var scale = new Vector3(1.05f, 1.05f, 1.05f);

            if (button.mainText) button.mainText.TweenGraphicColor(secondary, Duration1);

            switch (style)
            {
                case Styles.Style1:
                    button.images[0].TweenGraphicAlpha(0.5f, Duration1);
                    break;
                
                case Styles.Style2:
                    button.images[0].TweenGraphicAlpha(0.2f, Duration1);
                    button.images[1].TweenGraphicAlpha(0.2f, Duration1);
                    break;
                
                case Styles.Style3:
                    button.images[0].TweenGraphicAlpha(0.1f, Duration1);
                    button.images[1].TweenGraphicAlpha(0.1f, Duration1);
                    button.slider.TweenValueFloat(1, Duration2, f =>
                    {
                        button.slider.value = f;
                    }).SetEase(EaseType.ExpoOut);
                    break;
                
                case Styles.Style4:
                    button.images[0].TweenGraphicAlpha(0.5f, Duration1);
                    button.images[1].TweenGraphicColor(Color.white, Duration1);
                    break;

                case Styles.Style5:
                    button.images[1].TweenGraphicColor(Color.white, Duration1);
                    button.images[1].TweenLocalScale(scale, Duration1);
                    break;

                case Styles.Style6:
                    button.images[0].TweenGraphicAlpha(0.5f, Duration1);
                    button.images[1].TweenGraphicColor(Color.white, Duration1);
                    button.images[2].TweenGraphicAlpha(1, Duration1);
                    break;
                
                case Styles.Style7:
                    button.images[1].TweenGraphicColor(Color.white, Duration1);
                    button.images[1].TweenLocalScale(scale, Duration1);
                    button.images[2].TweenGraphicAlpha(1, Duration1);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }


        /// <summary>
        /// The button style to transition to when the button is pressed
        /// </summary>
        /// <param name="button">Reference to the button component</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Press(ButtonUI button)
        {
            var primary = button.primaryColor;
            var secondary = button.secondaryColor;
            var style = button.style;

            if (button.mainText) button.mainText.color = secondary;
            
            switch (style)
            {
                case Styles.Style1:
                    primary.a = 0.9f;
                    button.images[0].color = primary;
                    break;
                
                case Styles.Style2:
                    primary.a = 0.9f;
                    button.images[0].color = primary;
                    button.images[1].color = primary;
                    break;
                
                case Styles.Style3:
                    button.images[1].color = primary;
                    break;
                
                case Styles.Style4:
                    button.images[0].color = primary;
                    button.images[1].color = Color.clear;
                    break;

                case Styles.Style5:
                    button.images[0].color = primary;
                    button.images[1].color = Color.clear;
                    break;

                case Styles.Style6:
                    button.images[0].color = primary;
                    button.images[1].color = Color.clear;
                    break;
                
                case Styles.Style7:
                    button.images[0].color = primary;
                    button.images[1].color = Color.clear;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
            
            Highlight(button);
        }

        
        


        public enum Styles
        {
            /// <summary>
            /// Normal: Outline and transparent background
            /// Highlight: Background increased alpha
            /// Press: Background full alpha
            /// </summary>
            Style1,
            
            /// <summary>
            /// Normal: Light backround (small alpha) and transparent outline
            /// Highlight: Background and outline increased alpha
            /// Pressed: Background and outline full alpha
            /// </summary>
            Style2,
            
            /// <summary>
            /// Normal: No outline, Light background and slider is set to 0%
            /// Highlight: Slider fills to 100%, Slider color is light alpha
            /// Press: Slider color is full alpha
            /// </summary>
            Style3,
            
            /// <summary>
            /// Normal: Light background and grey foreground
            /// Highlight: Background increased alpha, white foreground
            /// Press: Background full alpha, forground transparent
            /// </summary>
            Style4,
            
            /// <summary>
            /// Normal: Light background and grey foreground
            /// Highlight: white foreground and increased size
            /// Press: Background full alpha, forground transparent
            /// </summary>
            Style5,
            
            /// <summary>
            /// Normal: Light background and grey foreground, small highlight
            /// Highlight: Background increased alpha, white foreground, highlight height increased
            /// Press: Background full alpha, forground transparent
            /// </summary>
            Style6,
            
            /// <summary>
            /// Normal: Light background and grey foreground, dark highlight
            /// Highlight: wBackground increased alpha, white foreground, light highlight
            /// Press: Background full alpha, forground transparent
            /// </summary>
            Style7
        }
    }
}