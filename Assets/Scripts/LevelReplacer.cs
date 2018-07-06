using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelReplacer : MonoBehaviour {
    public static List<Node> Nodes = new List<Node>();

    public void SetNodesToReplace(List<Node> NodesToReplace, bool StartReplace)
    {
        Nodes = NodesToReplace;
        StartCoroutine(ReplaceNodesForLevels());
    }

	IEnumerator ReplaceNodesForLevels()
	{
		foreach (var node in Nodes)
		{
			NodeConnections NodeConfigSet = node.GetConnectionSet();
			int code = LevelCoder.GetCode(NodeConfigSet);
			//print("Config Code: " + code);
			if (code == 0)
				continue;

			List<GameObject> posibleLevel = LevelCollectionManager.getLevelPrefabs(NodeConfigSet);

			//print("Lista de Posibles niveles: " + posibleLevel.Count);
			int randomIndex = UnityEngine.Random.Range(0, posibleLevel.Count);
			//print("Index Random: " + randomIndex);

			Vector3 nodePos = node.transform.position;
			Quaternion rot = posibleLevel[randomIndex].transform.rotation;
			Instantiate(posibleLevel[randomIndex], nodePos, rot);
			yield return new WaitForEndOfFrame();
		}
        CleanNodes();
		print("Los nodos han sido reemplazados correctamente.");
        StopCoroutine(ReplaceNodesForLevels());
	}
    

    private void CleanNodes()
    {
        //print("Nodos a limpiar: " + Nodes.Count);
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].gameObject.SetActive(false);
            //DestroyImmediate(Nodes[i].gameObject);
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
