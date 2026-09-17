using UnityEditor;
using UnityEngine;

public class SceneMousePosTool : EditorWindow
{
    [MenuItem("Tools/拾取Scene视图坐标")]
    static void ShowPosToolWindow()
    {
        SceneMousePosTool win = GetWindow<SceneMousePosTool>();
        win.titleContent = new GUIContent("点位拾取器");
        win.Show();
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        
        //按住【Shift】 + **鼠标左键点击Scene里面网格**进行拾取！
        if ((Event.current.modifiers == EventModifiers.Shift)
            && Event.current.type == EventType.MouseDown
            && Event.current.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Plane plane2D = new Plane(Vector3.forward, Vector3.zero);
            if (plane2D.Raycast(ray, out float hitDistance))
            {
                Vector3 worldPoint = ray.GetPoint(hitDistance);
                string logText = $"【拾取点位】 X={worldPoint.x:F2} , Y={worldPoint.y:F2}";
                Debug.Log(logText);
                this.ShowNotification(new GUIContent(logText));
            }
            Event.current.Use();
        }
        Handles.EndGUI();
    }

    void OnGUI()
    {
        EditorGUILayout.HelpBox("⚠ 使用方法：保持本窗口打开！\n按住Shift，然后鼠标左键点击Scene视图网格！\n坐标打印在Console控制台！", MessageType.Info);
    }
}
