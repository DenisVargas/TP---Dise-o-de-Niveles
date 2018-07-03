using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCollectionsWindow : EditorWindow {
    public LevelPrefabSettings ItemToAdd;

    private List<GameObject> LocalPrefabs = new List<GameObject>();
    private Vector2 _scroolPos;
    private bool _hasChanges = false;
    private bool _Saved = false;
    private bool _Checked = false;

    [MenuItem("Tools/LevelCollections")]
    public static void OpenWindow()
    {
        var MainWindow = GetWindow<LevelCollectionsWindow>();
        //Me fijo si existe un archivo de configuracion.
        LevelCollectionManager.LoadPrefabs();
        MainWindow.LocalPrefabs = new List<GameObject>(LevelCollectionManager.LevelPrefabs);
        LevelCollectionManager.Register();
        LevelCollectionManager.CheckRegisteredList();
        MainWindow._Checked = true;
        MainWindow.Show();
    }
    private void OnGUI()
    {
        //Informo sobre cuantos objetos hay en prefabs.
        string Info = "";
        Info += string.Format("La coleccion contiene {0} objetos.\n", LevelCollectionManager.LevelPrefabs.Count);
        if (LevelCollectionManager.registeredPrefabs != LocalPrefabs.Count)
            Info += string.Format("Solo {0} objetos estan registrados!.\n", LevelCollectionManager.registeredPrefabs);
        if (LevelCollectionManager.registeredPrefabs == LocalPrefabs.Count)
            if (LevelCollectionManager.registeredPrefabs != 0)
                Info += "Todos los objetos estan registrados";
        if (!_Checked)
            Info += "No se ha chequeado que todos los objetos esten registrados!\n";

        EditorGUILayout.HelpBox(Info, MessageType.Info);

        //Muestro una lista de prefabs.
        _scroolPos = EditorGUILayout.BeginScrollView(_scroolPos,false,false,new GUILayoutOption[] { GUILayout.MinHeight(220)});
        if (LocalPrefabs.Count > 0)
        {
            List<GameObject> LevelsToRemove = new List<GameObject>();
            foreach (var level in LocalPrefabs)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(string.Format("Codigo del Nivel: {0}.",LevelCoder.GetCode(level.GetComponent<LevelPrefabSettings>().getLevelSet())),new GUILayoutOption[] { GUILayout.MaxWidth(120)});
                EditorGUILayout.ObjectField(level, typeof(GameObject), false);
                GUI.color = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    LevelsToRemove.Add(level);
                    _Saved = false;
                    _hasChanges = true;
                }
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
            }
            if (LevelsToRemove.Count > 0)
                foreach (var item in LevelsToRemove)
                    LocalPrefabs.Remove(item);
        }

        EditorGUILayout.EndScrollView();

        //Permito Añadir y expandir la lista de prefabs.
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Nuevo Prefab de nivel: ",new GUILayoutOption[] { GUILayout.MaxWidth(150)});
        ItemToAdd = (LevelPrefabSettings)EditorGUILayout.ObjectField(ItemToAdd, typeof(LevelPrefabSettings), true);
        EditorGUI.BeginDisabledGroup(!ItemToAdd);
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add"))
        {
            LocalPrefabs.Add(ItemToAdd.gameObject);
            ItemToAdd = null;
            _Saved = false;
            _Checked = false;
            _hasChanges = true;
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        //Permito que se guarden todos los cambios de manera segura en el Scriptable Object.
        EditorGUI.BeginDisabledGroup(!_hasChanges);
        if (GUILayout.Button("Save Changes"))
        {
            LevelCollectionManager.SavePrefabs(LocalPrefabs);
            _hasChanges = false;
            _Checked = false;
            _Saved = true;
        }
        EditorGUI.EndDisabledGroup();


        GUI.backgroundColor = Color.white;
        if (_hasChanges && !_Saved)
            EditorGUILayout.HelpBox("Hay cambios que no han sido guardados!", MessageType.Warning);


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Force Check All objects!"))
        {
            LevelCollectionManager.CheckRegisteredList();
            
            _Checked = true;
        }
        EditorGUI.BeginDisabledGroup(!_Checked);
        if (GUILayout.Button("Force Register All objects!"))
        {
            LevelCollectionManager.Register();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();


    }

}
