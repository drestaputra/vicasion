using Interface.Elements.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Demo.Scripts
{
    public class Reset : BasePanel
    {
        [SerializeField] private TextUI txtTitle;
        
        [Space] 
        
        [SerializeField] private InputField inpCode;
        [SerializeField] private InputField inpNewPass;

        [Space] 
        
        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnBack;

        protected override void Start()
        {
            base.Start();
            
            btnContinue.onClick.AddListener(Launch);
            btnBack.onClick.AddListener(Launch);
        }

        private void Launch()
        {
            Hide(CanvasSide.Left);
            MainMenu.Login.Show(CanvasSide.Right);
        }

        public override void Show(CanvasSide side)
        {
            base.Show(side);
            txtTitle.StartAnimation();
        }
    }
}