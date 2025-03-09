using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RespawnManager))]
public class RespawnManageEditor : Editor {

    RespawnManager respawnManagaer;
    private void Awake()
    {
        respawnManagaer = (RespawnManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Respawn"))
        {
            respawnManagaer.Respawn(null);
        }
    }
}
