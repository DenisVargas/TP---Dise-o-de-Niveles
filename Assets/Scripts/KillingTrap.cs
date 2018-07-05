using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillingTrap : MonoBehaviour {
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            c.gameObject.GetComponent<PlayerInstance>().DieAndRespawn();
    }
}
