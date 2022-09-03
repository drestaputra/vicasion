using System.Collections.Generic;
using ElRaccoone.Tweens;
using Interface.Elements.Scripts.Styles;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

namespace Interface.Elements.Scripts
{
    public class ButtonUI : BaseUI
    {

        private Vector3 originalTextPos;
        [Tooltip("Show the second text on highlight")]
        [SerializeField] private bool showSecondText;
        
        [Space]
        
        [Tooltip("The button style")]
        public ButtonStyles.Styles style;
        
        [Tooltip("Pilih Scene")]
        public string StrScene = "";

        [Space] 
        
        [Tooltip("The text that will show up at start and normal")]
        public Text mainText;

        [Tooltip("The text that will show up when the button is highlighted (and ShowSecondText is enabled)")]
        public Text secondText;
        
        [Tooltip("The image components managed by this Button. Leave empty if not applicable")]
        public List<Image> images = new List<Image>();

        [Tooltip("The slider effect on buttons. Only applicable for Style 3")]
        public Slider slider;

        [HideInInspector] public Button button;

        [Header("Sounds")] 
        [SerializeField] private AudioClip onHover;
        [SerializeField] private AudioClip onClick;


        private void Awake()
        {
            button = GetComponent<Button>();
        }

        protected override void Start()
        {
            if (mainText) originalTextPos = mainText.transform.localPosition;
            
            button.onClick.AddListener(delegate{PressCustom();});

            base.Start();
        }

        private void Update()
        {
            if (button.interactable && !Interactable)
            {
                Interactable = true;
            }
            else if (!button.interactable && Interactable)
            {
                Interactable = false;
                foreach (var image in images)
                {
                    image.TweenGraphicAlpha(0.5f, 0.1f);
                }
            }
        }

        private void OnValidate()
        {
            if (mainText) mainText.color = secondaryColor;
            if (secondText) secondText.color = new Color(secondaryColor.r, secondaryColor.g, secondaryColor.b, 0);

            if (slider) slider.value = 0;

            var lightPrimaryColor = primaryColor;
            lightPrimaryColor.a = 0.1f;
            if (images.Count > 0)
            {
                images[0].color = lightPrimaryColor;
            }
        }


        public override void Normal()
        {
            base.Normal();
            
            ButtonStyles.Normal(this);

            if (showSecondText)
            {
                mainText.TweenLocalPosition(originalTextPos, 0.2f);
                mainText.TweenGraphicAlpha(1, 0.18f);
                secondText.TweenLocalPositionY(originalTextPos.y + Height, 0.2f);
                secondText.TweenGraphicAlpha(0, 0.18f);
            }
            
        }

        public override void Highlight()
        {
            HighlightNoSound();
            AudioManager.Play(onHover);
        }

        public void HighlightNoSound()
        {
            base.Highlight();
            
            ButtonStyles.Highlight(this);

            if (showSecondText)
            {
                mainText.TweenLocalPositionY(originalTextPos.y - Height, 0.2f);
                mainText.TweenGraphicAlpha(0, 0.18f);
                secondText.TweenLocalPosition(originalTextPos, 0.2f);
                secondText.TweenGraphicAlpha(1, 0.18f);
            }
        }

        public override void Press()
        {
            base.Press();
            
            ButtonStyles.Press(this);
            
            if (showSecondText)
            {
                mainText.TweenLocalPosition(originalTextPos, 0.2f);
                mainText.color = secondaryColor;
                secondText.TweenLocalPositionY(originalTextPos.y + Height, 0.2f);
                secondText.TweenGraphicAlpha(0, 0.18f);
            }

            AudioManager.Play(onClick);
        }
        public void PressCustom()
        {
            base.Press();
            // ButtonStyles.Press(this);
            
            // if (StrScene == "vrscene"){
            //     // SceneManager.LoadScene( "vrscene");
            //     // SceneManager.SetActiveScene( SceneManager.GetSceneByName( "vrscene" ) );
            // }else if(StrScene == "inolabs"){
            //     SceneManager.LoadScene( "inolabs");
            // }
            if (showSecondText)
            {
                mainText.TweenLocalPosition(originalTextPos, 0.2f);
                mainText.color = secondaryColor;
                secondText.TweenLocalPositionY(originalTextPos.y + Height, 0.2f);
                secondText.TweenGraphicAlpha(0, 0.18f);
            }

            AudioManager.Play(onClick);
        }

        public override void PointerExit()
        {
            base.PointerExit();
            Normal();
        }
    }
}