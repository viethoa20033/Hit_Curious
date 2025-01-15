using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMan : MonoBehaviour
{
    private Animator anim;

    [Header("Man Attack")]
    public bool isAttacking;
    public float timeDeload;

    [Header("Set Material Cannon And Man")] 
    public int typeColor;
    public SkinnedMeshRenderer rendMan;
    public Renderer rendCannon;
    public Material[] materials;

    private AudioSource audioSource;

   private void Awake()
   {
       audioSource = GetComponent<AudioSource>();
       anim = GetComponent<Animator>();
   }
    private void Start()
    {
        Material[] sharedMaterials = rendMan.sharedMaterials;
        sharedMaterials[3] = materials[typeColor];
        rendMan.sharedMaterials = sharedMaterials;
        
        
        rendMan.materials[3] = materials[typeColor];
        rendCannon.material = materials[typeColor];
    }
    
    public void ClickToAttack()
    {
        if (isAttacking) return;

        anim.SetBool("attack", true);
        StartCoroutine(FasleAttack());
        
    }

    IEnumerator FasleAttack()
    {
        isAttacking = true;
        
        yield return new WaitForSeconds(.5f);
        audioSource.Play();
        
        yield return new WaitForSeconds(timeDeload);

        anim.SetBool("attack", false);
        isAttacking = false;
    }
}