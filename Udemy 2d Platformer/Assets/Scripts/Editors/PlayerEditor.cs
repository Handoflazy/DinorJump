using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private string currentState = ""; // Khai báo bên ngoài OnInspectorGUI

    private void OnEnable()
    {
        EditorApplication.update += UpdateCurrentState; // Đăng ký callback
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateCurrentState; // Hủy đăng ký callback
    }

    private void UpdateCurrentState()
    {
        if (EditorApplication.isPlaying && target != null) // Kiểm tra Play Mode và target
        {
            Player sef = (Player)target;
            currentState = sef.playerStateMachine?.CurrentState.ToString() ?? ""; // Cập nhật liên tục
            Repaint(); // Vẽ lại Inspector
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Current State:", currentState); // Hiển thị giá trị cập nhật
        serializedObject.ApplyModifiedProperties();
    }
}