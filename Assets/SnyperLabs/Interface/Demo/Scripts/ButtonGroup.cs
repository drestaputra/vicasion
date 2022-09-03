using System.Collections.Generic;
using Interface.Elements.Scripts;
using UnityEngine;

namespace Interface.Demo.Scripts
{
    public class ButtonGroup : MonoBehaviour
    {
        /// <summary>
        /// The selection changed the previous frame and needs to be updated
        /// </summary>
        private bool selectionChanged;
        
        [Tooltip("The buttons that are grouped together")]
        public List<ButtonUI> buttons;

        [Tooltip("The selected buttons index on start. Set -1 for none selected")] 
        [SerializeField] private List<int> selectedIndex = new List<int>();

        [Tooltip("Does this have multiple selection")]
        [SerializeField] private bool multipleSelection;

        [Tooltip("Can have 0 selections")] 
        [SerializeField] private bool canHaveNoSelection;

        
        /// <summary>
        /// Accessor for the selected index
        /// </summary>
        public List<int> SelectedIndex => selectedIndex;

        public delegate void OnSelectedDelegate(int[] selectedIndices);

        public OnSelectedDelegate OnSelected;

        private void Start()
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                var index = i;
                buttons[index].button.onClick.AddListener(() => Select(index));
            }

            if (!canHaveNoSelection && selectedIndex.Count == 0)
                selectedIndex.Add(0);
        }

        private void Update()
        {
            SelectButtons();
        }

        private void SelectButtons()
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                if (selectedIndex.Contains(i))
                {
                    buttons[i].HighlightNoSound();
                }
                else if (selectionChanged)
                {
                    buttons[i].Normal();
                }
            }

            selectionChanged = false;
        }

        public void Select(int index)
        {
            if (!multipleSelection)
            {
                selectedIndex.Clear();
            }
            
            if (selectedIndex.Contains(index))
            {
                if (selectedIndex.Count > 1)
                {
                    selectedIndex.Remove(index);
                }
            }
            else
            {
                selectedIndex.Add(index);
            }
            
            selectionChanged = true;
            OnSelected.Invoke(selectedIndex.ToArray());
        }
    }
}