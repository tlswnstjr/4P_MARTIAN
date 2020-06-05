using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1_Opening : MonoBehaviour
{
    Animator anim;

    public BoxCollider door_left, door_right;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("character_nearby", true);
        door_left.isTrigger = true;
        door_right.isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("character_nearby", false);
        door_left.isTrigger = false;
        door_right.isTrigger = false;
    }
}
