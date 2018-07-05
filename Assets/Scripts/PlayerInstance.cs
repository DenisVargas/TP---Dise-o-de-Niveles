using UnityEngine;

public class PlayerInstance : MonoBehaviour {
	public GameObject PuntoInicial;
	public float PosicionMinima = 0;

	public Vector3 PosicionRespawn;
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

    public void DieAndRespawn()
    {
        transform.position = PosicionRespawn;
        transform.rotation = Quaternion.identity;
    }
}
