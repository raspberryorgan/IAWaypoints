using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Movement(Vector3 dir)
    {
        rb.MovePosition(this.transform.position + dir * speed * Time.deltaTime);

    }
}
