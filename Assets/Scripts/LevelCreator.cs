using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelReplacer : MonoBehaviour {
    public static bool ReplaceEnabled = false;
    public static List<Node> Nodes = new List<Node>();
	void Update()
	{
        if (ReplaceEnabled)
        {
            //4 - Recorro el arbol completo una vez mas y reemplazo los nodos, por el prefab que corresponda.
            StartCoroutine(ReplaceNodesForLevels());
        }
	}

    public void SetNodesToReplace(List<Node> NodesToReplace, bool StartReplace)
    {
        Nodes = NodesToReplace;
        ReplaceEnabled = StartReplace;
    }

	IEnumerator ReplaceNodesForLevels()
	{
		foreach (var node in Nodes)
		{
			NodeConnections NodeConfigSet = node.GetConnectionSet();
			int code = LevelCode.GetCode(NodeConfigSet);
			//print("Config Code: " + code);
			if (code == 0)
				continue;

			List<GameObject> posibleLevel = LevelCollectionManager.instance.getLevelPrefabs(NodeConfigSet);

			//print("Lista de Posibles niveles: " + posibleLevel.Count);
			int randomIndex = UnityEngine.Random.Range(0, posibleLevel.Count);
			//print("Index Random: " + randomIndex);

			Vector3 nodePos = node.transform.position;
			Quaternion rot = posibleLevel[randomIndex].transform.rotation;
			Instantiate(posibleLevel[randomIndex], nodePos, rot);
			yield return new WaitForEndOfFrame();
		}
		StopCoroutine(ReplaceNodesForLevels());
        ReplaceEnabled = false;
		CleanNodes();
		print("Los nodos han sido reemplazados correctamente.");
	}

    private void CleanNodes()
    {
        //print("Nodos a limpiar: " + Nodes.Count);
        foreach (var item in Nodes)
        {
            //print(item.Name + " sera destruido.");
            Destroy(item.gameObject);
        }
        Nodes.Clear();
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
