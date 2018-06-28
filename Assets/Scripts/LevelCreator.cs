using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public GameObject LevelNode; //Nodo de nivel.
	public float NodeDistance = 60; //Distancia entre niveles.
	[Range(3,12)]
	public int TreeLenght = 4; //Cantidad de nodos a generar.

	public List<Node> Nodes = new List<Node>();
	void Start()
	{
        //1 - Genero el arbol de nodos, con un inicio y un fin.
        //2 - Con DFS obtengo un camino seguro de A hacia B.
        //3 - Excluyo el camino correcto del arbol y recorro los restantes 1 a 1:
            //3-A: De acuerdo al resultado de una ruleta Elimino (o no) X cantidad de conecciones.
            //3-B: Recorro el arbol y si encuentro nodos sin conecciones, los elimino.
        //4 - Recorro el arbol completo una vez mas y reemplazo los nodos, por el prefab que corresponda.

        GenerateNodeTree();
        FindSafeWayToB();
        ExcludeAndInspectConections();
        ReplaceNodesForLevels();
    }

    private void ReplaceNodesForLevels()
    {
        MonoBehaviour.print("ReplaceNodesForLevels se ha ejecutado");
    }

    private void ExcludeAndInspectConections()
    {
        MonoBehaviour.print("ExcludeAndInspectConections se ha ejecutado");
    }

    private void FindSafeWayToB()
    {
        MonoBehaviour.print("FindSafeWayToB se ha ejecutado");
    }

    private void GenerateNodeTree()
    {
        int xLengt = 1;
        int zLenght = 1;
        //Genero la grilla de nodos inicial.
        for (xLengt = 1; xLengt < TreeLenght + 1; xLengt++)
        {

            for (zLenght = 1; zLenght < TreeLenght + 1; zLenght++)
            {

                Vector3 pos = Vector3.zero;
                if (Nodes.Count == 0)
                {
                    var FirstNode = Instantiate(LevelNode, pos, Quaternion.identity);
                    Nodes.Add(FirstNode.GetComponent<Node>());
                    continue;
                }
                pos = new Vector3(pos.x + xLengt * NodeDistance, 0,pos.z + zLenght * NodeDistance);
                var NewNode = Instantiate(LevelNode, pos, Quaternion.identity);
                Nodes.Add(NewNode.GetComponent<Node>());
            }
        }
    }

    /// <summary>
    /// Dados un Nodo y un prefan, instancia el ultimo en la posicion del nodo, y luego destruye dicho nodo.
    /// </summary>
    public void ReplaceNode(Node nodo, GameObject prefab)
    {
        Instantiate(prefab, nodo.transform.position, Quaternion.identity);
        Destroy(nodo);
    }
}
