using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public LayerMask cannonLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isPlaying)
        {
            Ray rayHit = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayHit, out RaycastHit hit, Mathf.Infinity, cannonLayer))
            {
                if (hit.transform.GetComponent<CannonMan>() != null)
                {
                    hit.transform.GetComponent<CannonMan>().ClickToAttack();
                }
            }
        }
    }
}
