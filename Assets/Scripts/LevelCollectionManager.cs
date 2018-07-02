using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class LevelCollectionManager{
    public static List<GameObject> LevelPrefabs = new List<GameObject>();
    public static Dictionary<int, List<GameObject>> prefabs = new Dictionary<int, List<GameObject>>();
    public static List<int> Codes = new List<int>();
    static LevelCollectionSaves SaveFile;

    public static void LoadPrefabs()
    {
        if (File.Exists(Application.dataPath + "/Editor/Saves/LevelCollectionSaves.asset"))
        {
            //Si existe lo cargo --->igualo el contenido a lo que tiene LevelCollectionManager.
            SaveFile = (LevelCollectionSaves)AssetDatabase.LoadAssetAtPath("Assets/Editor/Saves/LevelCollectionSaves.asset", typeof(LevelCollectionSaves));

            if (SaveFile.LevelPrefabs.Count > 0)
            {
                LevelPrefabs = SaveFile.LevelPrefabs;
                Codes = SaveFile.LevelPrefabCodes;
            }
            MonoBehaviour.print("Se ha cargado la configuracion.");
        }
        else
            ScriptableObjectUtility.CreateAsset("Assets/Editor/Saves",out SaveFile);
        LevelPrefabs.Clear();
    }
    public static void SavePrefabs()
    {
        if (SaveFile)
        {
            SaveFile.LevelPrefabs = LevelPrefabs;
            SaveFile.LevelPrefabCodes = Codes;
        }
        else
        {
            LoadPrefabs();
            SavePrefabs();
        }
    }
    public static List<GameObject> getLevelPrefabs(NodeConnections ParameterNode)
    {
        //Relleno la lista de prefabs
        if (LevelPrefabs.Count > 0)
        {
            foreach (var item in LevelPrefabs)
            {
                NodeConnections A = item.GetComponent<LevelPrefabSettings>().getLevelSet();

                int code = LevelCoder.GetCode(A);
                //print(code);

                if (prefabs.ContainsKey(code))
                    prefabs[code].Add(item);
                else
                    prefabs.Add(code, new List<GameObject> { item });
            }
        }
        //obtengo el codigo del nodo recibido.
        int recievedCode = LevelCoder.GetCode(ParameterNode);
        //devuelvo los prefabs existentes.
        return prefabs[recievedCode];
    }
}

