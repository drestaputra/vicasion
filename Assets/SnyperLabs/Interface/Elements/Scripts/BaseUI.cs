using UnityEngine;

namespace Interface.Elements.Scripts
{
    public abstract class BaseUI : MonoBehaviour
    {
        private Color originalSecondaryColor;
        protected bool Interactable = true;
        
        /// <summary>
        /// Is pointer on this item
        /// </summary>
        protected bool onPointer;
        
        /// <summary>
        /// The height of this element (divided by 2)
        /// </summary>
        protected float Height;

        public Color primaryColor = Color.white;

        public Color secondaryColor = Color.white;
        
        [Tooltip("Only applicable when differentTextColorOnHighlight is true")]
        [SerializeField] protected Color highlightTextColor = Color.black;
        
        [Tooltip("Text color will change to highlightTextColor on highlight")]
        [SerializeField] protected bool differentTextColorOnHighlight;


        protected virtual void Start()
        {
            originalSecondaryColor = secondaryColor;
            Height = GetComponent<RectTransform>().rect.height / 2f;

            Normal();
        }
        

        /// <summary>
        /// Called when the button is normal
        /// </summary>
        public virtual void Normal()
        {
            if (!Interactable) return;
            
            //Debug.Log(name + " Normal");
            if (differentTextColorOnHighlight)
                secondaryColor = originalSecondaryColor;
        }
        
        /// <summary>
        /// Called when the button is highlighted
        /// </summary>
        public virtual void Highlight()
        {
            if (!Interactable) return;
            
            //Debug.Log(name + " Highlight");
            if (differentTextColorOnHighlight)
            {
                secondaryColor = highlightTextColor; //NegativeColor(secondaryColor);
            }
            onPointer = true;
        }

        /// <summary>
        /// Called when the button is pressed
        /// </summary>
        public virtual void Press()
        {
            if (!Interactable) return;
            
            //Debug.Log(name + " Pressed");
            if (differentTextColorOnHighlight)
                secondaryColor = originalSecondaryColor;
        }

        public virtual void PointerExit()
        {
            //Debug.Log(name + " Pointer Exit");
            onPointer = false;
        }
        
        /// <summary>
        /// Returns the negative color
        /// </summary>
        /// <param name="color">The input color</param>
        /// <returns></returns>
        private static Color NegativeColor(Color color)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);
            float negativeH = (H + 0.5f) % 1f;
            var negativeColor = Color.HSVToRGB(negativeH, S, V);
            return negativeColor;
        }
    }
}