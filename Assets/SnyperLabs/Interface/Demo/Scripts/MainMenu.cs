using Interface.Elements.Scripts;
using UnityEngine;

namespace Interface.Demo.Scripts
{
    
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Exit notification ID. Used to hide the notification after response
        /// </summary>
        private int exitNotifID;

        [Tooltip("The game menu to set active when user is logged in")]
        [SerializeField] public GameObject gameMenu;

        public static Title Title;
        public static Login Login;
        public static Register Register;
        public static Reset Reset;

        public static MainMenu MainMenuT;

        private void Awake()
        {
            Title = FindObjectOfType<Title>();
            Login = FindObjectOfType<Login>();
            Register = FindObjectOfType<Register>();
            Reset = FindObjectOfType<Reset>();
            MainMenuT = FindObjectOfType<MainMenu>();
        }

        private void Start()
        {
            Title.Show();
            Login.OnLoginSuccess += ShowGameMenu;
            Login.OnLogout += HideGameMenu;
        }

        public void Logout()
        {
            AudioManager.Play(SoundEffects.Logout);
            Login.Logout();
        }

        public void ShowGameMenu()
        {
            Login.Hide(CanvasSide.Centre);
            gameMenu.SetActive(true);
        }

        private void HideGameMenu()
        {
            Login.Show(CanvasSide.Centre);
            gameMenu.SetActive(false);
        }

        public void Exit()
        {
            var title = "Exit Game".ToUpper();
            var description = "Are you sure you want to exit".ToUpper();
            // exitNotifID = Notification.Show(title, description, Icons.Get("exit"),
                // 20, NotifPosition.MidCenter,
                // NotifStyle.Style1, false, Color.clear, true,
                // () => ExitResponse(true), () => ExitResponse(false), "EXIT");
        }

        /// <summary>
        /// Response to exit notification
        /// </summary>
        /// <param name="response"></param>
        private void ExitResponse(bool response)
        {
            // Notification.BackgroundClicked();
            // Notification.Destroy(exitNotifID);
            if (response)
            {
                Application.Quit();
            }
        }
    }
    
    public struct Result
    {
        public bool Success;
        public string Message;
        public string[] Data;
    }
}