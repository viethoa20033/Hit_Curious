using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotateBall : MonoBehaviour
{
    public float angle;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            other.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            
            rb.AddForce(other.transform.forward * force,ForceMode.Impulse);
        }
    }
}
