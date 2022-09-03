using UnityEngine;
using UnityEngine.UI;

namespace Interface.Demo.Scripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// The CanvasGroup component of this object
        /// </summary>
        protected CanvasGroup cg;
        
        [Tooltip("The blurred background for this panel. Leave blank if not applicable")]
        [SerializeField] private Image blur;

        protected virtual void Start()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public virtual void Show(CanvasSide side)
        {
            cg.Show(side);
            if (blur) blur.enabled = true;
        }

        public virtual void Hide(CanvasSide side)
        {
            cg.Hide(side);
            if (blur) blur.enabled = false;
        }
    }
}