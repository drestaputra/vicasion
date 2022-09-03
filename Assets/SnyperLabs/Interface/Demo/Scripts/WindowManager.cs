using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Demo.Scripts
{
    public class WindowManager : MonoBehaviour
    {
        [Header("Button List")]
        [Tooltip("The buttons corresponding to each window index")]
        public List<Button> buttons = new List<Button>();
        
        [Header("Panel list")]
        [Tooltip("The windows corresponding to each button index")]
        public List<CanvasGroup> windows = new List<CanvasGroup>();
        
        private CanvasGroup currentPanel;
        private CanvasGroup nextPanel;

        [Header("SETTINGS")]
        [Tooltip("The starting panel index")]
        public int currentPanelIndex;

        private void Start()
        {
            // Hide all panels in the beginning
            foreach (var window in windows) 
                window.Hide(0);

            currentPanel = windows[currentPanelIndex];
            currentPanel.Show();

            Load();
        }

        private void Load()
        {
            if (buttons.Count != windows.Count)
            {
                Debug.LogError("The window count does not match buttons");
                return;
            }
            
            for (var i = 0; i < windows.Count; i++)
            {
                var panelIndex = i;
                buttons[i].onClick.AddListener(() => PanelAnim(panelIndex));
            }
        }

        private void PanelAnim(int newPanel)
        {
            if (newPanel != currentPanelIndex)
            {
                currentPanel = windows[currentPanelIndex];

                currentPanelIndex = newPanel;
                nextPanel = windows[currentPanelIndex];
                
                currentPanel.Hide();
                nextPanel.Show();
            }
        }
    }
}