using UnityEngine;

namespace Interface.Elements.Scripts
{
    public static class Icons
    {
        /// <summary>
        /// Get an icon from resources folder
        /// </summary>
        /// <param name="key">The file name (without extension)</param>
        /// <returns></returns>
        public static Sprite Get(string key)
        {
            return Resources.Load<Sprite>("Icons/" + key);
        }
    }
}