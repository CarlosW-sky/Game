using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CustomEditorWindow_001A;

namespace CustomEditorTool_001B
{
    public class CustomEditorTool : EditorWindow
    {
        
        [MenuItem("Ky Tools/Admin Manager")]
        public static void ShowCustomTool()
        {
            //Debug.Log("My Custom Tool is running!");
            // 在这里实现自定义工具的逻辑
        
        }
    
    }

    





}


      /*  代码讲解
         * 
         *  1. [MenuItem] 属性：
         *   MenuItem 是 Unity 的一个属性，用于将方法显示到菜单栏中。
         *   MenuItem("MyTools/Custom Tool") 会在菜单栏中创建一个新的菜单项：
         *   如果“MyTools”不存在，它会创建一个新菜单项
         *   Custom Tool 是该工具的具体名称，点击时会调用 ShowCustomTool 方法
         *
         *   2. 自定义工具逻辑：
         *   ShowCustomTool() 方法会在点击“MyTools > Custom Tool”菜单项时被调用。
         *   你可以在这里编写自定义工具的逻辑，比如显示窗口、修改游戏对象等。
         *
         *   
        */