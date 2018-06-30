using System.Collections.Generic;
using UnityEngine;

public class LevelCollectionManager : MonoBehaviour {
    public List<GameObject> LevelPrefabs = new List<GameObject>();
    public Dictionary<int, List<GameObject>> prefabs = new Dictionary<int, List<GameObject>>();
    public static LevelCollectionManager instance;

    private void Awake()
    {
        instance = this;
        foreach (var item in LevelPrefabs)
        {
            NodeConnections A = item.GetComponent<LevelPrefabSettings>().getLevelSet();

            int code = LevelCode.GetCode(A);
            //print(code);

            if (prefabs.ContainsKey(code))
                prefabs[code].Add(item);
            else
                prefabs.Add(code, new List<GameObject> { item });
        }
        ////Debug.
        //var keylist = prefabs.Keys;
        //int iteracion = 0;
        //foreach (var key in keylist)
        //{
        //    iteracion++;
        //    print("Numero: " + iteracion + " y la key es: " + key);
        //}
    }

    public List<GameObject> getLevelPrefabs(NodeConnections ParameterNode)
    {
        int recievedCode = LevelCode.GetCode(ParameterNode);
        return prefabs[recievedCode];
    }
}
public static class LevelCode
{
    public static int GetCode(NodeConnections ConfigSet)
    {
        int code = ConfigSet.ConnectionsNumber;
        if (ConfigSet.TopConnection)
            code += 2 * ConfigSet.ConnectionsNumber;
        if (ConfigSet.BottomConnection)
            code += 3 * ConfigSet.ConnectionsNumber;
        if (ConfigSet.RightConnection)
            code += 4 * ConfigSet.ConnectionsNumber;
        if (ConfigSet.LeftConnections)
            code += 6 * ConfigSet.ConnectionsNumber;
        return code;
    }
}

