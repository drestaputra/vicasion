using UnityEditor;
using UnityEngine;

namespace Interface.Elements.Scripts
{
    public static class EditorMenu
    {
        private const string Title = "Text/Title";
        private const string Common = "Text/Common";

        private const string Button1 = "Buttons/Button 1";
        private const string Button2 = "Buttons/Button 2";
        private const string Button3 = "Buttons/Button 3";
        private const string Button4 = "Buttons/Button 4";
        private const string Button5 = "Buttons/Button 5";
        private const string Button6 = "Buttons/Button 6";
        private const string Button7 = "Buttons/Button 7";
        private const string Button8 = "Buttons/Button 8";
        
        private const string InputField1 = "Input Fields/Input 1";
        private const string InputField2 = "Input Fields/Input 2";
        private const string InputField3 = "Input Fields/Input 3";

        private const string Grid = "List/Grid";
        private const string Horizontal = "List/Horizontal";
        private const string Vertical = "List/Vertical";

        private const string NotifManager = "Notifications/Notification";
        
        private const string Toggle1 = "Toggle/Toggle 1";
        private const string Toggle2 = "Toggle/Toggle 2";

        private static void SpawnObject(string path)
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (!canvas)
            {
                Debug.LogWarning("Could not find canvas");
                return;
            }
            
            var go = Resources.Load<GameObject>(path);
            var newGo = GameObject.Instantiate(go, canvas.transform, false);
            newGo.name = newGo.name.Replace("(Clone)", "");
            
            // Undo.RegisterCreatedObjectUndo(newGo, newGo.name);
        }

        #region Text

        // [MenuItem("GameObject/UI/Interface/Text/Title")]
        public static void AddTitle()
        {
            SpawnObject(Title);
        }
        
        // [MenuItem("GameObject/UI/Interface/Text/Common")]
        public static void AddCommon()
        {
            SpawnObject(Common);
        }

        #endregion

        #region Button

        // [MenuItem("GameObject/UI/Interface/Button/Button 1")]
        public static void AddButton1()
        {
            SpawnObject(Button1);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 2")]
        public static void AddButton2()
        {
            SpawnObject(Button2);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 3")]
        public static void AddButton3()
        {
            SpawnObject(Button3);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 4")]
        public static void AddButton4()
        {
            SpawnObject(Button4);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 5")]
        public static void AddButton5()
        {
            SpawnObject(Button5);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 6")]
        public static void AddButton6()
        {
            SpawnObject(Button6);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 7")]
        public static void AddButton7()
        {
            SpawnObject(Button7);
        }
        
        // [MenuItem("GameObject/UI/Interface/Button/Button 8")]
        public static void AddButton8()
        {
            SpawnObject(Button8);
        }

        #endregion

        #region Input Field

        // [MenuItem("GameObject/UI/Interface/Input Field/Input 1")]
        public static void AddInput1()
        {
            SpawnObject(InputField1);
        }
        
        // [MenuItem("GameObject/UI/Interface/Input Field/Input 2")]
        public static void AddInput2()
        {
            SpawnObject(InputField2);
        }
        
        // [MenuItem("GameObject/UI/Interface/Input Field/Input 3")]
        public static void AddInput3()
        {
            SpawnObject(InputField3);
        }

        #endregion

        #region Toggle

        // [MenuItem("GameObject/UI/Interface/Toggle/Toggle 1")]
        public static void AddToggle1()
        {
            SpawnObject(Toggle1);
        }
        
        // [MenuItem("GameObject/UI/Interface/Toggle/Toggle 2")]
        public static void AddToggle2()
        {
            SpawnObject(Toggle2);
        }

        #endregion

        #region List

        // [MenuItem("GameObject/UI/Interface/List/Vertical")]
        public static void AddVertical()
        {
            SpawnObject(Vertical);
        }

        // [MenuItem("GameObject/UI/Interface/List/Horizontal")]
        public static void AddHorizontal()
        {
            SpawnObject(Horizontal);
        }
        
        // [MenuItem("GameObject/UI/Interface/List/Grid")]
        public static void AddGrid()
        {
            SpawnObject(Grid);
        }
        

        #endregion

        #region Notification

        // [MenuItem("GameObject/UI/Interface/Notification")]
        public static void AddNotification()
        {
            var go = Resources.Load<GameObject>(NotifManager);
            var newGo = GameObject.Instantiate(go);
            newGo.name = newGo.name.Replace("(Clone)", "");
            
            // Undo.RegisterCreatedObjectUndo(newGo, newGo.name);
        }

        #endregion
    }
}