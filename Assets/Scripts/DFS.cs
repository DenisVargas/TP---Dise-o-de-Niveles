using System.Collections.Generic;

public static class DFS
{
/// <summary>
	/// Dados dos nodos, recorre un arbol de nodos, hasta encontrar el segundo y devuelve el recorrido.
	/// </summary>
	/// <param name="start">Nodo en donde se inicia la busqueda.</param>
	/// <param name="end">Nodo que se desea encontrar.</param>
	/// <param name="returnCorrectPath">Si es verdadero, devuelve el camino mas corto. de lo contrario, devuelve todo el recorrido.</param>
	/// <returns>Lista de nodos, que han sido recorridos para encontrar el 2do nodo dado por parámetro</returns>
	public static List<Node> DeepFirstSearch (Node start, Node end,bool returnCorrectPath = false)
	{
		List<Node> Recorrido = new List<Node>();

		Stack<Node> S = new Stack<Node>();
		S.Push(start);
		start.visited = true;
		
		while (S.Count > 0)
		{
			Node CurrentNode = S.Pop();
			Recorrido.Add(CurrentNode);
			CurrentNode.visited = true;
			
			if (CurrentNode == end)
				return Recorrido;
			foreach (var CurrentConnection in CurrentNode.Connections)
				if (!CurrentConnection.visited)
				{
					S.Push(CurrentConnection);
					CurrentConnection.visited = true;
					CurrentConnection.Parent = CurrentNode;
				}
		}

		//Retorno el recorrido correcto.
		if (returnCorrectPath)
		{
			var CorrectWayList = CorrectPath(end);
			Recorrido = CorrectWayList;
		}

		return Recorrido;
	}

    /// <summary>
    /// Dado un nodo, retorna el camino formado por sus "Padres".
    /// </summary>
    /// <param name="EndNode">El nodo final de un arbol ya procesado.</param>
    /// <returns>Camino correcto.</returns>
	private static List<Node> CorrectPath(Node EndNode)
	{
		List<Node> CorrectWay = new List<Node>();
		Node CurrentNode = EndNode;

		if (CurrentNode.Parent == null)
		{
			CorrectWay.Add(CurrentNode);
			return CorrectWay;
		}

		CorrectWay = CorrectPath(CurrentNode.Parent);
		CorrectWay.Add(CurrentNode);
		return CorrectWay;
	}
}
