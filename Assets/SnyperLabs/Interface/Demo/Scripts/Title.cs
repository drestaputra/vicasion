using System.Collections;
using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Interface.Demo.Scripts
{
    public class Title : BasePanel
    {
        [Tooltip("Wait for user to click to proceed from the title screen")]
        [SerializeField] private bool clickToContinue = true;
        
        [Tooltip("The text hint to display when ClickToContinue is enabled")]
        [SerializeField] private Text textClickToContinue;
        
        [Tooltip("The title image to display at Title screen. Can be the Game Title image")]
        [SerializeField] private Image imageTitle;
        
        public void Show()
        {
            Show(CanvasSide.Centre);
            if (imageTitle)
            {
                if (textClickToContinue)
                {
                    var color = textClickToContinue.color;
                    textClickToContinue.color = new Color(color.r, color.g, color.b, 0);
                }
                imageTitle.color = Color.clear;
                imageTitle.transform.localPosition = Vector3.down * 200;
                imageTitle.TweenGraphicColor(Color.white, 1);
                imageTitle.TweenLocalPosition(Vector3.zero, 1).SetEase(EaseType.ExpoOut);
                StartCoroutine(ShowAccount());
            }
            else
            {
                MainMenu.Login.Show();
            }
        }

        private IEnumerator ShowAccount()
        {
            yield return new WaitForSeconds(1);
            
            if (clickToContinue)
            {
                if (textClickToContinue)
                    textClickToContinue.TweenGraphicAlpha(1, 0.1f);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
            else
            {
                yield return new WaitForSeconds(3);
            }

            imageTitle.TweenGraphicAlpha(0, 0.5f);
            textClickToContinue.TweenGraphicAlpha(0, 0.5f);
            // MainMenu.Login.Show();

            MainMenu.MainMenuT.ShowGameMenu();
            // SceneManager.LoadScene( "vrscene");
        }
    }
}