using UnityEngine;
using UnityEditor;

public class LevelCollectionsWindow : EditorWindow {
    public LevelPrefabSettings ItemToAdd;

    [MenuItem("Tools/LevelCollections")]
    public static void OpenWindow()
    {
        var MainWindow = GetWindow<LevelCollectionsWindow>();
        //Me fijo si existe un archivo de configuracion.
        LevelCollectionManager.LoadPrefabs();
        MainWindow.Show();
    }
    private void OnGUI()
    {
        //Informo sobre cuantos objetos hay en prefabs.
        EditorGUILayout.LabelField(string.Format("La coleccion contiene {0} objetos.", LevelCollectionManager.LevelPrefabs.Count));
        //Muestro una lista de prefabs.

        //Permito Añadir y expandir la lista de prefabs.
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Nuevo Prefab de nivel: ",new GUILayoutOption[] { GUILayout.MaxWidth(150)});
        ItemToAdd = (LevelPrefabSettings)EditorGUILayout.ObjectField(ItemToAdd, typeof(LevelPrefabSettings), true);
        EditorGUI.BeginDisabledGroup(!ItemToAdd);
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add"))
        {
            LevelCollectionManager.LevelPrefabs.Add(ItemToAdd.gameObject);
            ItemToAdd = null;
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
    }

}
