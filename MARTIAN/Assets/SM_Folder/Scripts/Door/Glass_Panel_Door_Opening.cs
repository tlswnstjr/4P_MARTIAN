using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Panel_Door_Opening : MonoBehaviour
{
    Animator anim;
    public BoxCollider glassDoor;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("character_nearby", true);
        glassDoor.isTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("character_nearby", false);
        glassDoor.isTrigger = false;
    }
}
