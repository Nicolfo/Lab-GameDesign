using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        //rb = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float amount) => speed = amount;

    private void FixedUpdate()
    {
        rb.velocity = speed * transform.forward;
    }
}
