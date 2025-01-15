using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : MonoBehaviour
{
    public CannonMan cannonMan;
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float force;

    public GameObject[] fx;
    public Transform fxPoint;
    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * force,ForceMode.Impulse);
        
        bullet.GetComponent<BulletColor>().SetTypeColor(cannonMan.typeColor);
        Destroy(bullet,5f);


        GameObject _fx = Instantiate(fx[Random.Range(0, fx.Length)], fxPoint.position, Quaternion.identity);
        Destroy(_fx,3f);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hammer"))
        {
            SpawnBullet();
        }
    }
    
    
}
