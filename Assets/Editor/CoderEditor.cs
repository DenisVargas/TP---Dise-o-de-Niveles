using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCoder))]
public class CoderEditor : Editor{
    LevelCoder Edit;
    private void OnEnable()
    {
        Edit = (LevelCoder)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Top & Bottom Connection Weight: ");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("TOP: ", new GUILayoutOption[] { GUILayout.MaxWidth(60) });
        LevelCoder.TopConnectionWeight = EditorGUILayout.IntField(LevelCoder.TopConnectionWeight);
        EditorGUILayout.LabelField("BOTTOM: ", new GUILayoutOption[] { GUILayout.MaxWidth(60) });
        LevelCoder.BottomConnectionWeight = EditorGUILayout.IntField(LevelCoder.BottomConnectionWeight);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Right & Left Connection Weight: ");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Right: ", new GUILayoutOption[] { GUILayout.MaxWidth(60) });
        LevelCoder.RightConnectionWeight = EditorGUILayout.IntField(LevelCoder.RightConnectionWeight);
        EditorGUILayout.LabelField("Left: ", new GUILayoutOption[] { GUILayout.MaxWidth(60) });
        LevelCoder.LeftConnectionWeight = EditorGUILayout.IntField(LevelCoder.LeftConnectionWeight);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.PrefixLabel("Objeto de prueba:");
        Edit.TestObjet = (LevelPrefabSettings)EditorGUILayout.ObjectField(Edit.TestObjet,typeof(LevelPrefabSettings),true);

        EditorGUI.BeginDisabledGroup(Edit.TestObjet ? false : true);
        if (GUILayout.Button("¡Test Code Generated!"))
        {
            Edit.ExampleCode = LevelCoder.GetCode(Edit.TestObjet.getLevelSet());
        }
        EditorGUILayout.LabelField("Codigo Generado: " + Edit.ExampleCode.ToString());
        EditorGUILayout.LabelField(string.Format("Los Pesos usados son:\n{0} para TOP.\n{1} para Bottom.\n{2} para Right.\n{3} para Left",LevelCoder.TopConnectionWeight,LevelCoder.BottomConnectionWeight,LevelCoder.RightConnectionWeight,LevelCoder.LeftConnectionWeight),new GUILayoutOption[] { GUILayout.MinHeight(150)});
        EditorGUI.EndDisabledGroup();
    }
}
