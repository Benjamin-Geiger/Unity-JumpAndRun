using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MovingPlatform))]
public class MovPlatEditor : Editor
{
    private SerializedProperty start;
    private SerializedProperty end;

    private void OnEnable()
    {
        this.start = this.serializedObject.FindProperty("start");
        this.end = this.serializedObject.FindProperty("end");
    }

    private void OnSceneGUI()
    {
        this.start.vector3Value = Handles.PositionHandle(start.vector3Value, Quaternion.identity);
        this.end.vector3Value = Handles.PositionHandle(end.vector3Value, Quaternion.identity);
        this.serializedObject.ApplyModifiedProperties();
        
        Handles.color = Color.green;
        Handles.DrawLine(start.vector3Value, end.vector3Value);
    }
    
    
}
