using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomEditorWindow_001A
{
    public class CustomEditorWindow : EditorWindow //MonoBehaviour
    {
        [MenuItem("Ky Tools/  ")]
        public static void ShowWindow()
        {
            GetWindow<CustomEditorWindow>("Custom Tool Window");
        }       
    }
}
