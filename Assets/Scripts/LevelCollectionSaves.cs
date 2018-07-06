using System.Collections.Generic;
using UnityEngine;

public class LevelCollectionSaves : ScriptableObject {
    public List<GameObject> LevelPrefabs = new List<GameObject>();
    public List<int> LevelPrefabCodes = new List<int>();
}
