using UnityEditor;
using UnityEngine;

namespace Scripts.GamePlay.Editor
{
    public class Tools
    {
        [MenuItem("Tools/ClearSaves")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}