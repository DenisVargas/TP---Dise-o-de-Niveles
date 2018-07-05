using UnityEngine;

public class PlayerMorido : MonoBehaviour {
	public GameObject PuntoInicial;
	public float PosicionMinima = 0;

	private Vector3 PosicionRespawn;
	// Use this for initialization
	void Start () {
		PosicionRespawn = PuntoInicial.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < PosicionMinima)
		{
			transform.position = PosicionRespawn;
		}
	}
}
