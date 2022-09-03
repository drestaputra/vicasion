using System;
using Interface.Elements.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Interface.Demo.Scripts
{
    public class Login : BasePanel
    {
        // Todo: remove the hardcoded values and connect it to a database
        private const string Username = "dresta";
        private const string Password = "abc123";
        
        private bool saveDetails;

        [SerializeField] private TextUI txtTitle;
        
        [Space]
        
        [SerializeField] private InputField inpUser;
        [SerializeField] private InputField inpPass;

        [Space]
        
        [SerializeField] private Toggle togSave;
        
        [Space]
        
        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnForgot;
        [SerializeField] private Button btnRegister;
        
        
        public delegate void SimpleDelegate();
        public static event SimpleDelegate OnLoginSuccess;
        public static event SimpleDelegate OnLogout;
        


        protected override void Start()
        {
            base.Start();
            
            togSave.onValueChanged.AddListener(Save);

            btnContinue.onClick.AddListener(Continue);
            btnForgot.onClick.AddListener(Forgot);
            btnRegister.onClick.AddListener(NoAccount);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (inpUser.isFocused)
                {
                    inpPass.ActivateInputField();
                }
                else if (inpPass.isFocused)
                {
                    btnContinue.Select();
                }
            }
        }

        private void Save(bool value) => saveDetails = value;
        
        private void ClearFields()
        {
            inpUser.text = "";
            inpPass.text = "";
        }

        private void Continue()
        {
            Launch(inpUser.text, inpPass.text);
        }

        private void Forgot()
        {
            Hide(CanvasSide.Right);
            MainMenu.Reset.Show(CanvasSide.Left);
        }

        private void NoAccount()
        {
            Hide(CanvasSide.Right);
            MainMenu.Register.Show(CanvasSide.Left);
        }

        public void Show()
        {
            cg.Show(1);
        }
        
        public override void Show(CanvasSide side)
        {
            base.Show(side);
            txtTitle.StartAnimation();
        }

        public void Launch(string username, string password)
        {
            if (inpUser.text == "" || inpPass.text == "")
            {
                var description = "Fill in all the details".ToUpper();
                Error(description);
                return;
            }
            
            // Todo: send details to server and wait for callback
            // Remove this hardcoded callback
            Result result;
            if (username == Username && password == Password)
            {
                result = new Result
                {
                    Success = true,
                    Message = "",
                    Data = new[] {Username, Password}
                };
            }
            else
            {
                result = new Result
                {
                    Success = false,
                    Message = "Wrong username or password",
                    Data = null
                };
            }
            
            
            if (result.Success)
            {
                Success(result.Data);
            }
            else
            {
                Error(result.Message);
            }
        }

        private void Success(string[] data)
        {
            ClearFields();
            var title = "Login successful".ToUpper();
            var description = ("Welcome <b>" + data[0] + "</b>").ToUpper();
            // Notification.Show(title, description);
            AudioManager.Play(SoundEffects.Success);
            SetSavedLogin(saveDetails, data[0], data[1]);
            OnLoginSuccess?.Invoke();
        }

        private void Error(string message)
        {
            var title = "Cannot login".ToUpper();
            var description = message.ToUpper();
            // Notification.Show(title, description);
            AudioManager.Play(SoundEffects.Error);
        }

        public void Logout()
        {
            SetSavedLogin(false);
            OnLogout?.Invoke();
        }
        
        /// <summary>
        /// Save Login credentials in the playerprefs
        /// </summary>
        /// <param name="save">Enable saving credentials</param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        private void SetSavedLogin(bool save, string user = "", string pass = "")
        {
            PlayerPrefs.SetString("SaveLogin", save.ToString());
            if (user == "" && pass == "")
                return;
            
            PlayerPrefs.SetString("SavedUsername", user);
            PlayerPrefs.SetString("SavedPassword", pass);
        }
        
        private bool GetSavedLogin(out string user, out string pass)
        {
            user = PlayerPrefs.GetString("SavedUsername");
            pass = PlayerPrefs.GetString("SavedPassword");
            return Convert.ToBoolean(PlayerPrefs.GetString("SaveLogin"));
        }
    }
}