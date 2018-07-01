using UnityEngine;

public class LevelPrefabSettings : MonoBehaviour {
    public LevelCoder GeneradorCodigo;

    //Prendo y apago el prefab, Observo el codigo generado.
    public int code;
    public int ConnectionsNumber = 0;
    public bool TopConnection = false;
    public bool BottomConnection = false;
    public bool RightConnection = false;
    public bool LeftConnections = false;

    public NodeConnections getLevelSet()
    {
        return new NodeConnections(ConnectionsNumber, TopConnection, BottomConnection, RightConnection, LeftConnections);
    }
}
