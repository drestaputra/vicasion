using UnityEngine;
using UnityEngine.UI;

namespace Interface.Demo.Scripts
{
    public class Multiplayer : MonoBehaviour
    {
        [SerializeField] private Text textTeam;
        [SerializeField] private Image imageTeam;
        [SerializeField] private Text textMap;
        [SerializeField] private Image imageMap;
        [SerializeField] private ButtonGroup teamSelection;
        [SerializeField] private ButtonGroup mapSelection;

        private void Start()
        {
            teamSelection.OnSelected += OnTeamSelected;
            mapSelection.OnSelected += OnMapSelected;
            
            OnTeamSelected(new []{0});
            OnMapSelected(new []{0});
        }

        private void OnTeamSelected(int[] selectedIndices)
        {
            textTeam.text = teamSelection.buttons[selectedIndices[0]].mainText.text;
            imageTeam.sprite = teamSelection.buttons[selectedIndices[0]].images[1].sprite;
        }

        private void OnMapSelected(int[] selectedIndices)
        {
            imageMap.sprite = mapSelection.buttons[selectedIndices[0]].images[1].sprite;
            
            var maps = "";
            foreach (var i in selectedIndices)
            {
                maps += mapSelection.buttons[i].mainText.text + "\n";
            }

            textMap.text = maps;
        }
    }
}