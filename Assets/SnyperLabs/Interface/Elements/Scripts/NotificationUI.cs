using ElRaccoone.Tweens;
using ElRaccoone.Tweens.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Interface.Elements.Scripts
{
    public class NotificationUI : MonoBehaviour
    {
        private NotifPosition position;
        
        /// <summary>
        /// The time for the notification to show and hide
        /// </summary>
        private const float Duration = 0.65f;
        
        /// <summary>
        /// How far the notification will be from the edge of the screen
        /// </summary>
        private const float EdgeDistance = 20;
        
        [SerializeField] private Image background;

        [Space] 
        
        [Tooltip("The title text")]
        [SerializeField] private Text title;
        
        [Tooltip("The description text")]
        [SerializeField] private Text description;

        [Space]
        
        [Tooltip("The round icon group parent")]
        [SerializeField] private GameObject roundIcon;
        
        [Tooltip("The round icon image")]
        [SerializeField] private Image roundIconImage;
        
        [Tooltip("The round outline image")]
        [SerializeField] private Image roundOutlineImage;

        [Tooltip("The square icon group parent")]
        [SerializeField] private GameObject squareIcon;

        [Tooltip("The square icon image")]
        [SerializeField] private Image squareIconImage;

        [Tooltip("The square outline image")]
        [SerializeField] private Image squareOutlineImage;

        [Space]
        
        [Tooltip("The buttons group parent")]
        [SerializeField] private GameObject buttonField;

        [Tooltip("The positive reaction button")]
        [SerializeField] private Button positiveButton;
        [Tooltip("The positive button text")]
        [SerializeField] private Text positiveText;
        
        [Tooltip("The negative reaction button")]
        [SerializeField] private Button negativeButton;
        [Tooltip("The negative button text")]
        [SerializeField] private Text negativeText;


        public void SetText(string title, string description)
        {
            this.title.text = title;
            this.description.text = description;
        }
        
        public void SetIcon(bool isRound, Sprite sprite, Color outlineColor)
        {
            if (sprite == null)
            {
                roundIcon.SetActive(false);
                squareIcon.SetActive(false);
                return;
            }
            
            roundIconImage.sprite = sprite;
            squareIconImage.sprite = sprite;

            roundOutlineImage.color = outlineColor;
            squareOutlineImage.color = outlineColor;
            
            roundIcon.SetActive(isRound);
            squareIcon.SetActive(!isRound);
        }

        public void SetButtons(bool enable, string positive, UnityAction onPositive, string negative, UnityAction onNegative)
        {
            buttonField.SetActive(enable);

            if (onPositive == null || onNegative == null)
            {
                return;
            }

            positiveText.text = positive;
            positiveButton.onClick.RemoveAllListeners();
            negativeButton.onClick.RemoveAllListeners();

            negativeText.text = negative;
            positiveButton.onClick.AddListener(onPositive);
            negativeButton.onClick.AddListener(onNegative);
        }

        public void Animate(NotifPosition position)
        {
            this.position = position;
            var localPosition = transform.localPosition;
            var rect = GetComponent<RectTransform>().rect;
            var x = localPosition.x;
            var y = localPosition.y;
            var width = rect.width + EdgeDistance;
            var height = rect.height + EdgeDistance;
            
            // Animate left
            if (position == NotifPosition.TopLeft
                || position == NotifPosition.MidLeft
                || position == NotifPosition.BottomLeft)
            {
                transform.TweenLocalPositionX(x + width, Duration).SetEase(EaseType.ExpoOut);
            }
            
            // Animate right
            if (position == NotifPosition.TopRight
                || position == NotifPosition.MidRight
                || position == NotifPosition.BottomRight)
            {
                transform.TweenLocalPositionX(x - width, Duration).SetEase(EaseType.ExpoOut);
            }

            // Animate top center
            if (position == NotifPosition.TopCenter)
            {
                transform.TweenLocalPositionY(y - height, Duration).SetEase(EaseType.ExpoOut);
            }

            // Animate mid center
            if (position == NotifPosition.MidCenter)
            {
                transform.localScale = Vector3.zero;
                transform.TweenLocalScale(Vector3.one, Duration).SetEase(EaseType.ExpoOut);
            }

            // Animate bottom center
            if (position == NotifPosition.BottomCenter)
            {
                transform.TweenLocalPositionY(y + height, Duration).SetEase(EaseType.ExpoOut);
            }

        }

        public void Hide()
        {
            var transform = this.transform;
            var rect = GetComponent<RectTransform>().rect;
            var x = transform.position.x;
            var y = transform.position.y;
            var width = rect.width + EdgeDistance;
            var height = rect.height + EdgeDistance;

            Tween<float> tween = null;
            
            // Animate left
            if (position == NotifPosition.TopLeft
                || position == NotifPosition.MidLeft
                || position == NotifPosition.BottomLeft)
            {
                tween = transform.TweenLocalPositionX(x - width, Duration).SetEase(EaseType.ExpoIn);
            }
            
            // Animate right
            if (position == NotifPosition.TopRight
                || position == NotifPosition.MidRight
                || position == NotifPosition.BottomRight)
            {
                tween = transform.TweenLocalPositionX(x + width, Duration).SetEase(EaseType.ExpoIn);
            }

            // Animate top center
            if (position == NotifPosition.TopCenter)
            {
                tween = transform.TweenLocalPositionY(y + height, Duration).SetEase(EaseType.ExpoIn);
            }

            // Animate mid center
            if (position == NotifPosition.MidCenter)
            {
                transform.TweenLocalScale(Vector3.zero, Duration).SetEase(EaseType.ExpoIn).SetOnComplete(Destroy);
            }

            // Animate bottom center
            if (position == NotifPosition.BottomCenter)
            {
                tween = transform.TweenLocalPositionY(y - height, Duration).SetEase(EaseType.ExpoIn);
            }

            if (tween != null)
            {
                tween.SetOnComplete(Destroy);
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}