using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelPrefabSettings))]
public class LevelPrefabSettingsEditor : Editor
{
    LevelPrefabSettings Settings;
    LevelCoder coder;
    private void OnEnable()
    {
        Settings = (LevelPrefabSettings)target;
        coder = Settings.GeneradorCodigo;
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.PrefixLabel("Codificador:");
        Settings.GeneradorCodigo = (LevelCoder)EditorGUILayout.ObjectField(Settings.GeneradorCodigo, typeof(LevelCoder),true);

        if (Settings.GeneradorCodigo)
        {
            Settings.code = coder.GetCode(Settings.ConnectionsNumber, Settings.TopConnection, Settings.BottomConnection, Settings.RightConnection, Settings.LeftConnections);
        }

        Settings.ConnectionsNumber = ConnectionsAviable(Settings);
        EditorGUILayout.LabelField("Cantidad de conecciones activadas: " + Settings.ConnectionsNumber);

        EditorGUILayout.LabelField("Codigo de prefab: " + Settings.code);

        //----------------------------------------------------------------------------
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();

        if (Settings.TopConnection)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.grey;
        
        if (GUILayout.Button("Top"))
            Settings.TopConnection = !Settings.TopConnection;

        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //----------------------------------------------------------------------------

        EditorGUILayout.BeginHorizontal();
        if (Settings.LeftConnections)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.grey;

        if (GUILayout.Button("Left"))
            Settings.LeftConnections = !Settings.LeftConnections;
        EditorGUILayout.Space();
        //----------------------------------------------------------------------------
        if (Settings.RightConnection)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.grey;

        if (GUILayout.Button("Right"))
            Settings.RightConnection = !Settings.RightConnection;
        EditorGUILayout.EndHorizontal();

        //----------------------------------------------------------------------------
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (Settings.BottomConnection)
            GUI.backgroundColor = Color.green;
        else
            GUI.backgroundColor = Color.grey;

        if (GUILayout.Button("Bottom"))
            Settings.BottomConnection = !Settings.BottomConnection;
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    private int ConnectionsAviable(LevelPrefabSettings A)
    {
        int connections = 0;
        if (A.TopConnection)
            connections++;
        if (A.BottomConnection)
            connections++;
        if (A.RightConnection)
            connections++;
        if (A.LeftConnections)
            connections++;
        return connections;
    }
}
