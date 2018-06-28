using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public string Name = "Node";
    public bool visited = false;
    public Node Parent = null;

    public List<Node> Connections = new List<Node>();
    public List<bool> vDirections = new List<bool>();
}
