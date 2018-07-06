using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class LevelCollectionManager{
	public static List<GameObject> LevelPrefabs = new List<GameObject>();
	public static Dictionary<int, List<GameObject>> prefabs = new Dictionary<int, List<GameObject>>();
	public static bool Registered = false;
	public static int registeredPrefabs = 0;

	private static LevelCollectionSaves SaveFile;
	
	//---------------------Save and Load------------------------------
	/// <summary>
	/// Carga las configuraciones guardadas.
	/// </summary>
	public static void LoadPrefabs()
	{
		if (File.Exists(Application.dataPath + "/Editor/Saves/LevelCollectionSaves.asset"))
		{
			//Si existe lo cargo --->igualo el contenido a lo que tiene LevelCollectionManager.
			SaveFile = (LevelCollectionSaves)AssetDatabase.LoadAssetAtPath("Assets/Editor/Saves/LevelCollectionSaves.asset", typeof(LevelCollectionSaves));

			if (SaveFile.LevelPrefabs.Count > 0)
			{
				LevelPrefabs = SaveFile.LevelPrefabs;
				Register();
				CheckRegisteredList();
				MonoBehaviour.print("Se ha cargado correctamente la configuracion .");
			}
		}
		else
			ScriptableObjectUtility.CreateAsset("Assets/Editor/Saves",out SaveFile);
	}
	/// <summary>
	/// Permite Sobreescribir el archivo de guardado y actualizar el estado.
	/// </summary>
	/// <param name="ListToSave">Lista de prefabs externa.</param>
	public static void SavePrefabs(List<GameObject> ListToSave)
	{
		if (SaveFile)
		{
			LevelPrefabs = ListToSave;
			SaveFile.LevelPrefabs = LevelPrefabs;
			MonoBehaviour.print("Se ha sobreescrito la configuracion correctamente.");
		}
		else
		{
			LoadPrefabs();
			SavePrefabs(ListToSave);
		}
	}
	public static void Register()
	{
		registeredPrefabs = 0;
		foreach (var item in LevelPrefabs)
		{
			LevelPrefabSettings A = item.GetComponent<LevelPrefabSettings>();
			int code = LevelCoder.GetCode(A.getLevelSet());
			if (prefabs.ContainsKey(code))
			{
				if (!prefabs[code].Contains(A.gameObject))
					prefabs[code].Add(A.gameObject);
			}
			else
				prefabs.Add(code, new List<GameObject>() { A.gameObject });
		}
		MonoBehaviour.print("Registrados!");
	}
	public static void CheckRegisteredList()
	{
		registeredPrefabs = 0;
		foreach (var item in LevelPrefabs)
			foreach (var registrados in prefabs)
				if (registrados.Value.Contains(item))
				{
					registeredPrefabs++;
					break;
				}
		MonoBehaviour.print("Chequeados!");
	}
	//----------------------------------------------------------------
	public static List<GameObject> getLevelPrefabs(NodeConnections ParameterNode)
	{
		//Relleno la lista de prefabs
		if (LevelPrefabs.Count > 0)
		{
			//obtengo el codigo del nodo recibido.
			int recievedCode = LevelCoder.GetCode(ParameterNode);
			//devuelvo los prefabs existentes.
			NodeConnections A = ParameterNode;

			int code = LevelCoder.GetCode(A);

			if (prefabs.ContainsKey(code))
					return prefabs[recievedCode];
			else
			{
				MonoBehaviour.print(string.Format("No hay objetos con codigo {0} en la coleccion!.",code));
				return new List<GameObject>();
			}
		}
		return null;
	}
}

