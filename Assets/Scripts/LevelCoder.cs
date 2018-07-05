using System.Collections.Generic;
using UnityEngine;

public class LevelCoder : MonoBehaviour
{
    public static int TopConnectionWeight = 2;
    public static int BottomConnectionWeight = 3;
    public static int RightConnectionWeight = 4;
    public static int LeftConnectionWeight = 6;

    //Muestra el codigo generado dado un objeto.
    public LevelPrefabSettings TestObjet;
    public int ExampleCode = 0;

//Muestra los codigos generados por cada objeto en una lista dentro del manager
    //Level Prefabs.
    public LevelCollectionManager CodeManager;
    public List<string[]> DebugResults = new List<string[]>();// Nombre del objeto [0],Codigo Generado[1],Si hay repetidos[2],Objetos que comparten codigo[3](x defecto None).

    public int GetCode(int conectionNumber, bool top, bool bottom, bool right, bool left)
    {
        int code = conectionNumber;
        if (top)
            code += TopConnectionWeight * conectionNumber;
        if (bottom)
            code += BottomConnectionWeight * conectionNumber;
        if (right)
            code += RightConnectionWeight * conectionNumber;
        if (left)
            code += LeftConnectionWeight * conectionNumber;
        return code;
    }
    public static int GetCode(NodeConnections ConfigSet)
    {
        int code = ConfigSet.ConnectionsNumber;
        if (ConfigSet.TopConnection)
            code += TopConnectionWeight * ConfigSet.ConnectionsNumber;
        if (ConfigSet.BottomConnection)
            code += BottomConnectionWeight * ConfigSet.ConnectionsNumber;
        if (ConfigSet.RightConnection)
            code += RightConnectionWeight * ConfigSet.ConnectionsNumber;
        if (ConfigSet.LeftConnections)
            code += LeftConnectionWeight * ConfigSet.ConnectionsNumber;
        return code;
    }

}

