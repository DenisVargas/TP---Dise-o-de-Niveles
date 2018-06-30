using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
	public string Name = "Node";
	public bool visited = false;
	public Node Parent;

	public List<Node> Connections = new List<Node>();

	public NodeConnections GetConnectionSet()
	{
		List<Node> TrueConnections = new List<Node>();
		foreach (var item in Connections)
		{
			if (item)
				TrueConnections.Add(item);
		}
		Connections = TrueConnections;

		bool[] Con = new bool[4] {false, false, false, false };
		int ConnectionNumber = Connections.Count;
		foreach (var coneccion in Connections)
		{
			Vector3 dir = (coneccion.transform.position - transform.position).normalized;
			//MonoBehaviour.print("Nombre: " + Name + " Direccion del vecino: " + dir);
			if (dir == Vector3.forward)
				Con[0] = true;
			else
			if (dir == Vector3.back)
				Con[1] = true;
			else
			if (dir == Vector3.right)
				Con[2] = true;
			else
			if (dir == Vector3.left)
				Con[3] = true;
		}

		return new NodeConnections(ConnectionNumber,Con[0], Con[1], Con[2], Con[3]);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		if (UnityEditor.Selection.activeGameObject == this.gameObject)
		{
			for (int i = 0; i < Connections.Count; i++)
			{
				if (Connections[i] != null)
					Gizmos.DrawLine(transform.position, Connections[i].transform.position);
			}
		}
	}
}

public struct NodeConnections
{
	public int ConnectionsNumber;
	public bool TopConnection;
	public bool BottomConnection;
	public bool RightConnection;
	public bool LeftConnections;

	public NodeConnections(int connectionsNumber, bool top, bool bottom,bool right, bool left)
	{
		ConnectionsNumber = connectionsNumber;
		TopConnection = top;
		BottomConnection = bottom;
		RightConnection = right;
		LeftConnections = left;
	}
}
