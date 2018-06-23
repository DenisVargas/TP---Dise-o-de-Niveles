using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorido : MonoBehaviour {
    public GameObject PuntoInicial;
    public Vector3 PosicionRespawn;


	// Use this for initialization
	void Start () {
        PosicionRespawn = PuntoInicial.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < -21)
        {
            transform.position = PosicionRespawn;
        }
	}
}
