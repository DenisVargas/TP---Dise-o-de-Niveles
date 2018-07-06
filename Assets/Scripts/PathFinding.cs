using System.Collections.Generic;
using UnityEngine;

public static class PathFinding
{
	private static List<Node> _openSet = new List<Node>();
	private static List<Node> _closedSet = new List<Node>();

	public static List<Node> AStar(Node Start,Node End)
	{
		_openSet.Clear();
		_closedSet.Clear();

		_openSet.Add(Start);//2
		Start.Parent = Start;
		Node CurrentNode = Start;

		while (_openSet.Count > 0)//4
		{
			CurrentNode = LowerNode();//5
			_openSet.Remove(CurrentNode);
			_closedSet.Add(CurrentNode);

			if (CurrentNode == End)//6
				return rebuildPath(Start,End);//7

			foreach (var link in CurrentNode.Connections)//8
			{

				if (!link || _closedSet.Contains(link) || link.isBlocked)//9
					continue;//10

				if (!_openSet.Contains(link))
				{
					_openSet.Add(link);
					link.Parent = CurrentNode;

					//Calculamos posicion Manhattan.
					link.H = ((CurrentNode.transform.position.x - link.transform.position.x) + (CurrentNode.transform.position.z - link.transform.position.z));
					link.G = Vector3.Distance(CurrentNode.transform.position, link.transform.position);
					link.F = link.Parent.F + link.G + link.H;
				}
				else
				{
					float G_old = link.G; //--> Distancia entre el nodo y su padre actual.
					float G_new = Vector3.Distance(CurrentNode.transform.position, link.transform.position);
					if (G_new < G_old)
					{
						link.Parent = CurrentNode;
						link.G = G_new;
						link.F = link.Parent.F + link.G + link.H;
					}
				}
			}
		}
		return null;
	}

    public static List<Node> ThetaStar(Node Start, Node End)
    {
        _openSet.Clear();
        _closedSet.Clear();

        _openSet.Add(Start);//2
        Start.Parent = Start;
        Node CurrentNode = Start;

        while (_openSet.Count > 0)//4
        {
            CurrentNode = LowerNode();//5
            _openSet.Remove(CurrentNode);
            _closedSet.Add(CurrentNode);

            if (CurrentNode == End)//6
                return rebuildPath(Start, End);//7

            foreach (var link in CurrentNode.Connections)//8
            {
                if (!link || _closedSet.Contains(link) || link.isBlocked)//9
                    continue;//10

                if (!_openSet.Contains(link))
                {
                    _openSet.Add(link);
                    link.Parent = CurrentNode;

                    //Calculamos posicion Manhattan.
                    link.H = ((CurrentNode.transform.position.x - link.transform.position.x) + (CurrentNode.transform.position.z - link.transform.position.z));
                    link.G = Vector3.Distance(CurrentNode.transform.position, link.transform.position);
                    link.index = Vector3.Distance(link.transform.position, End.transform.position);
                    link.F = link.Parent.F + link.G + link.H + link.index;
                }
                else
                {
                    float G_old = link.G; //--> Distancia entre el nodo y su padre actual.
                    float G_new = Vector3.Distance(CurrentNode.transform.position, link.transform.position);
                    if (G_new < G_old)
                    {
                        link.Parent = CurrentNode;
                        link.G = G_new;
                        link.F = link.Parent.F + link.G + link.H + link.index;
                    }
                }
            }
        }
        return null;
    }

    private static Node LowerNode()
	{
		Node Selected = _openSet[0];
		float fm = Selected.F;

		foreach (var item in _openSet)
		{
			if (item.F < fm)
			{
				fm = item.F;
				Selected = item;
			}
			else
				continue;
		}
		return Selected;
	}
	private static List<Node> rebuildPath(Node start, Node end)
	{
		Node C = end;
		List<Node> rebuildedPath = new List<Node>();

		while (C != start)
		{
			C = C.Parent;
			rebuildedPath.Add(C);
		}
		rebuildedPath.Reverse();
		return rebuildedPath;
	}
}