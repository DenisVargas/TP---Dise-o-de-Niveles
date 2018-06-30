using UnityEngine;

public class LevelPrefabSettings : MonoBehaviour {
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
