using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public string Name = "Node";
    public bool visited = false;
    public Node Parent = null;
    public bool CanDrawConections = false;

    public List<Node> Connections = new List<Node>();
    public List<bool> vDirections = new List<bool>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (CanDrawConections)
            for (int i = 0; i < Connections.Count; i++)
            {
                if (Connections[i] != null)
                    Gizmos.DrawLine(transform.position, Connections[i].transform.position);
            }
    }
}
