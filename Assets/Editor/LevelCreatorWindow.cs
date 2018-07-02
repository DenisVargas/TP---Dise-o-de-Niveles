using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelCreatorWindow : EditorWindow {
    public LevelReplacer Replacer;//Objeto que reemplaza los nodos x el nivel en concreto.
    public GameObject LevelNode; //Nodo de nivel.
    public float NodeDistance = 60; //Distancia entre niveles.
    [Range(3, 24)]
    public int TreeLenght = 4; //Cantidad de nodos a generar.
    public int LevelNodeLayer;

    public Material InitialNodeMaterial;
    public Material SafeWay;
    public Material PosibleFinalNode;
    public Material FinalNode;

    //-----------------------------Privados/Internos----------------------------------
    private List<Node> Nodes = new List<Node>();
    private List<Node> ExcludedNodes = new List<Node>();
    private List<Node> IncludedNodes = new List<Node>();

    Node StartingNode;
    Node FinalleNode;

    [MenuItem("Tools/LevelCreatorWindow")]
	public static void OpenWindow()
    {
        var MainWindow = GetWindow<LevelCreatorWindow>();
        MainWindow.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.PrefixLabel("Objeto Reemplazador");
        Replacer = (LevelReplacer)EditorGUILayout.ObjectField(Replacer, typeof(LevelReplacer), true);

        EditorGUILayout.PrefixLabel("Prefab de nodo");
        LevelNode = (GameObject)EditorGUILayout.ObjectField(LevelNode, typeof(GameObject), true);

        //---------------------------Materiales---------------------------------------
        EditorGUILayout.LabelField("Materiales:");
        InitialNodeMaterial = (Material)EditorGUILayout.ObjectField(InitialNodeMaterial, typeof(Material), true);
        EditorGUILayout.Space();
        SafeWay = (Material)EditorGUILayout.ObjectField(SafeWay, typeof(Material), true);
        EditorGUILayout.Space();
        PosibleFinalNode = (Material)EditorGUILayout.ObjectField(PosibleFinalNode, typeof(Material), true);
        EditorGUILayout.Space();
        FinalNode = (Material)EditorGUILayout.ObjectField(FinalNode, typeof(Material), true);
        EditorGUILayout.Space();


        //--------------------------Acciones------------------------------------------
        string Debug = "";
        if (!Replacer)
            Debug += "El Reemplazador no ha sido Asignado!.\n";
        if (!LevelNode)
            Debug += "El prefab de nodo no ha sido Asignado!.\n";

        EditorGUI.BeginDisabledGroup(Replacer && LevelNode ? false : true);
        //1 - Genero el arbol de nodos, con un inicio y un fin.
        if (GUILayout.Button("Generar Arbol de Nodos"))
        {
            GenerateNodeTree();
            //Con DFS obtengo un camino seguro de A hacia B.
            FindSafeWayToB();
            //Excluyo el camino correcto del arbol y destruyo conecciones al azar en los nodos restantes 1 a 1:
            ExcludeAndDestroyConections();
            Replacer.SetNodesToReplace(Nodes, true);
        }
        EditorGUI.EndDisabledGroup();
    }


    //--------------------------------------------------------------------------------
    private void ExcludeAndDestroyConections()
    {
        foreach (var item in IncludedNodes)
        {
            int conectionsAviable = item.Connections.Count;
            //Si no tiene conecciones--> Elimino el nodo.
            if (item == StartingNode || item == FinalleNode)
                continue;

            switch (conectionsAviable)
            {
                case 0://Posibilidad--> Ninguna, destuyo el objeto.
                    Nodes.Remove(item);
                    Destroy(item.gameObject);
                    break;
                case 1:
                    //Si tiene 1 sola coneccion.
                    //Posibilidades--> Elimino todas las conecciones. No hago nada.
                    List<float> A = new List<float>() { 38, 62 };
                    if (RoulleteSelection.RoulleteWheelSelection(A) == 1)
                    {
                        Nodes.Remove(item);
                        Destroy(item.gameObject);
                    }
                    break;
                case 2:
                    //Si tiene 2 conecciones.
                    //Posibilidades--> Elimino 1 coneccion. Elimino todas las conecciones. No hago nada.
                    List<float> B = new List<float>() { 53, 12, 35 };
                    int AccionT1 = RoulleteSelection.RoulleteWheelSelection(B);
                    if (AccionT1 == 1)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                    }
                    else
                    if (AccionT1 == 2)
                    {
                        Nodes.Remove(item);
                        Destroy(item.gameObject);
                    }
                    break;
                case 3:
                    //Si tiene 3 conecciones.
                    //Posibilidades--> Elimino 1 o 2 conecciones.Elimino todas las conecciones. No hago nada.
                    List<float> C = new List<float>() { 18.33f, 18.33f, 18.33f, 5, 35 };
                    int AccionT2 = RoulleteSelection.RoulleteWheelSelection(C);
                    if (AccionT2 == 1)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                    }
                    else
                    if (AccionT2 == 2)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                        int randomNumber2 = UnityEngine.Random.Range(0, item.Connections.Count);
                        item.Connections.RemoveAt(randomNumber2);
                    }
                    else
                    if (AccionT2 == 3)
                    {
                        Nodes.Remove(item);
                        Destroy(item.gameObject);
                    }
                    break;
                case 4:
                    //Si tiene 4 conecciones.
                    //Posibilidades--> Elmino 1 o 2 o 3 conecciones. Elimino todas las conecciones. No hago nada.
                    List<float> D = new List<float>() { 13.75f, 13.75f, 13.75f, 5, 35 };
                    int AccionT3 = RoulleteSelection.RoulleteWheelSelection(D);
                    if (AccionT3 == 1)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                    }
                    else
                    if (AccionT3 == 2)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                        int randomNumber2 = UnityEngine.Random.Range(0, item.Connections.Count);
                        item.Connections.RemoveAt(randomNumber2);
                    }
                    else
                    if (AccionT3 == 3)
                    {
                        int randomNumber = UnityEngine.Random.Range(0, conectionsAviable);
                        item.Connections.RemoveAt(randomNumber);
                        int randomNumber2 = UnityEngine.Random.Range(0, item.Connections.Count);
                        item.Connections.RemoveAt(randomNumber2);
                        int randomNumber3 = UnityEngine.Random.Range(0, item.Connections.Count);
                        item.Connections.RemoveAt(randomNumber3);
                    }
                    else
                    if (AccionT3 == 4)
                    {
                        Nodes.Remove(item);
                        Destroy(item.gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void FindSafeWayToB()
    {
        //Debemos encontrar el camino correcto.
        ExcludedNodes = DFS.DeepFirstSearch(StartingNode, FinalleNode, true);
        IncludedNodes = new List<Node>(Nodes);
        foreach (var item in ExcludedNodes)
        {
            if (IncludedNodes.Contains(item))
                IncludedNodes.Remove(item);
            item.GetComponent<MeshRenderer>().materials = new Material[] { SafeWay };
        }
        FinalleNode.GetComponent<MeshRenderer>().materials = new Material[] { FinalNode };
    }

    private void GenerateNodeTree()
    {
        //Genero la grilla de nodos inicial.
        int zValue = 0;
        int xValue = 0;
        int NodesGenerated = 0;
        for (int i = 0; i < TreeLenght; i++)
        {
            zValue = i * (int)NodeDistance;//--->Aumento el valor en z.
            for (int j = 0; j < TreeLenght; j++)
            {
                Vector3 pos = Vector3.zero;
                xValue = j * (int)NodeDistance;
                pos = new Vector3(xValue, 0, zValue);
                var NewNode = Instantiate(LevelNode, pos, Quaternion.identity);
                NodesGenerated += 1;
                NewNode.GetComponent<Node>().Name += NodesGenerated.ToString();
                Nodes.Add(NewNode.GetComponent<Node>());
            }
            xValue = 0;//--------------->Reseteo el valor de x.
        }

        //Relleno la lista de vecinos de cada nodo. 
        //Calculo los potenciales nodos de inicio y fin.
        List<Node> Included = new List<Node>();
        float maxDistance = Nodes[Nodes.Count - 1].gameObject.transform.position.x;
        float randomMaxDistance = UnityEngine.Random.Range(maxDistance * 0.6f, maxDistance);

        for (int i = 0; i < Nodes.Count; i++)
        {
            var currentNode = Nodes[i];
            List<Node> MisVecinos = new List<Node>();
            for (int j = 0; j < Nodes.Count; j++)
            {
                if (Nodes[j] == currentNode)
                    continue;

                var secondNode = Nodes[j];
                float distanceBetweenNodes = Vector3.Distance(currentNode.gameObject.transform.position, secondNode.gameObject.transform.position);
                if (distanceBetweenNodes < NodeDistance + (NodeDistance * 0.25))//Añadimos el vecino.
                    MisVecinos.Add(secondNode);
            }
            currentNode.GetComponent<Node>().Connections = MisVecinos;
        }

        //Selecciono el nodo inicial.
        StartingNode = Nodes[0];
        StartingNode.GetComponent<MeshRenderer>().materials = new Material[] { InitialNodeMaterial };//Asigno el material al nodo de inicio.

        for (int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i] == StartingNode)
                continue;
            if (Vector3.Distance(StartingNode.transform.position, Nodes[i].transform.position) > randomMaxDistance)
                Included.Add(Nodes[i]);
        }

        if (Included.Count > 0)
        {
            //For Debugg.
            foreach (var item in Included)
                item.GetComponent<MeshRenderer>().materials = new Material[] { PosibleFinalNode };

            int randomFinale = UnityEngine.Random.Range(0, Included.Count - 1);
            FinalleNode = Included[randomFinale];
        }
    }
}
