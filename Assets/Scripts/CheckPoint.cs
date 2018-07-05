using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CheckPoint : MonoBehaviour {
    public const float MinimunRange = 2;
    public float Range = 3f;
    public SphereCollider Coll;

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            c.GetComponent<PlayerInstance>().PosicionRespawn = transform.position;
    }
}
