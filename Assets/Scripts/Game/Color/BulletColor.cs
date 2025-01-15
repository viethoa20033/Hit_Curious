using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColor : MonoBehaviour
{
    private Renderer rend;
    private int typeColor;

    public Material[] materials;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetTypeColor(int color)
    {
        typeColor = color;
        rend.material = materials[typeColor];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("setColor") && !GameManager.Instance.isWin)
        {
            other.GetComponent<SetColor>().SetColorOnBulet(typeColor, materials[typeColor]);
        }
    }
}
