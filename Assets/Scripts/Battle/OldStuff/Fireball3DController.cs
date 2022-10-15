using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball3DController : MonoBehaviour
{
    // Components
    private Rigidbody fireballBody;

    // Physics
    public float aimAngle;

    // Constants
    private float speed = 1;
    private float destroyTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        fireballBody = GetComponent<Rigidbody>();
        fireballBody.AddForce(new Vector3(-Mathf.Sin(Mathf.Deg2Rad * aimAngle), Mathf.Cos(Mathf.Deg2Rad * aimAngle) * speed, 0), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
