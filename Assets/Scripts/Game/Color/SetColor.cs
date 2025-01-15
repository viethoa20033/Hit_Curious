using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    private Renderer rend;
    
    public int typeColor;
    public int targetColor;

    public bool isCorrect;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetColorOnBulet(int _color, Material _mat)
    {
        typeColor = _color;
        rend.material = _mat;

        if (typeColor == targetColor)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }

    }
}
