using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor {
	public Node EditedNode;
	public bool Calculated = false;
	NodeConnections CalculatedConnections;
	private void OnEnable()
	{
		EditedNode = (Node)target;
	}

	public override void OnInspectorGUI()
	{
		//Debugg.
		//base.OnInspectorGUI();

		EditorGUILayout.PrefixLabel("Generador de Código.");
		EditedNode.GeneradorCodigo = (LevelCoder)EditorGUILayout.ObjectField(EditedNode.GeneradorCodigo, typeof(LevelCoder), true);

		GUI.backgroundColor = Color.green;
		if (GUILayout.Button("Calculate Connections"))
		{
			CalculatedConnections = EditedNode.GetConnectionSet();
			EditedNode.code = LevelCoder.GetCode(CalculatedConnections);
			Calculated = true;
		}

		EditorGUILayout.LabelField("Codigo generado: " +  EditedNode.code + ".");

		//--------------------------------------Validador-----------------------------------------------------
		//-------------------------------------------------------------------------------------
		string[] validados = new string[4] { "InValid", "InValid", "InValid", "InValid" };

		if (Calculated)
			 validados = valdidateConnections(EditedNode, CalculatedConnections);

		EditorGUILayout.BeginHorizontal();
		if (Calculated && CalculatedConnections.TopConnection)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.grey;

		GUILayout.Button("", new GUILayoutOption[] { GUILayout.MaxWidth(30) });
		EditorGUILayout.LabelField("Top Connection", new GUILayoutOption[] { GUILayout.MaxWidth(150) });

		GUI.backgroundColor = Color.white;
		EditorGUILayout.LabelField(validados[0]);

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		//-------------------------------------------------------------------------------------
		EditorGUILayout.BeginHorizontal();
		if (Calculated && CalculatedConnections.BottomConnection)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.grey;

		GUILayout.Button("", new GUILayoutOption[] { GUILayout.MaxWidth(30) });
		EditorGUILayout.LabelField("Bottom Connection", new GUILayoutOption[] { GUILayout.MaxWidth(150) });

		GUI.backgroundColor = Color.white;
		EditorGUILayout.LabelField(validados[1]);

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		//-------------------------------------------------------------------------------------
		EditorGUILayout.BeginHorizontal();
		if (Calculated && CalculatedConnections.RightConnection)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.grey;

		GUILayout.Button("", new GUILayoutOption[] { GUILayout.MaxWidth(30) });
		EditorGUILayout.LabelField("Right Connection", new GUILayoutOption[] { GUILayout.MaxWidth(150) });

		GUI.backgroundColor = Color.white;
		EditorGUILayout.LabelField(validados[2]);

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		//-------------------------------------------------------------------------------------
		EditorGUILayout.BeginHorizontal();
		if (Calculated && CalculatedConnections.LeftConnections)
			GUI.backgroundColor = Color.green;
		else
			GUI.backgroundColor = Color.grey;

		GUILayout.Button("", new GUILayoutOption[] { GUILayout.MaxWidth(30) });
		EditorGUILayout.LabelField("Left Connection", new GUILayoutOption[] { GUILayout.MaxWidth(150) });

		GUI.backgroundColor = Color.white;
		EditorGUILayout.LabelField(validados[3]);

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		//-------------------------------------------------------------------------------------
	}

	string[] valdidateConnections(Node editado, NodeConnections conecciones)
	{
		string[] ValidatedConnections = new string[4] { "InValid", "InValid" , "InValid" , "InValid" };

		List<Node> TrueConnections = new List<Node>();
		foreach (var item in editado.Connections)
		{
			if (item)
				TrueConnections.Add(item);
		}


		foreach (var coneccion in TrueConnections)
		{
			Vector3 dir = (coneccion.transform.position - editado.transform.position).normalized;
			Node AChequear = coneccion;

			if (dir == Vector3.forward)
			{
				//Chequeo la valides de la coneccion.
				foreach (var connec in AChequear.Connections)
				{
					if (connec == editado)
					{
						ValidatedConnections[0] = "Valid";
						break;
					}
				}
			}
			else
			if (dir == Vector3.back)
			{
				//Chequeo la valides de la coneccion.
				foreach (var connec in AChequear.Connections)
				{
					if (connec == editado)
					{
						ValidatedConnections[1] = "Valid";
						break;
					}
				}
			}
			else
			if (dir == Vector3.right)
			{
				//Chequeo la valides de la coneccion.
				foreach (var connec in AChequear.Connections)
				{
					if (connec == editado)
					{
						ValidatedConnections[2] = "Valid";
						break;
					}
				}
			}
			else
			if (dir == Vector3.left)
			{
				//Chequeo la valides de la coneccion.
				foreach (var connec in AChequear.Connections)
				{
					if (connec == editado)
					{
						ValidatedConnections[3] = "Valid";
						break;
					}
				}
			}
		}
		return ValidatedConnections;
	}

}
