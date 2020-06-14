using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3_Opening : MonoBehaviour
{
    Animator anim;
    AudioSource audioPlayer;
    public AudioClip doorSound;

    public BoxCollider bottom_A, bottom_B;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("character_nearby", true);
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.clip = doorSound;
            audioPlayer.Play();

        }
        bottom_A.isTrigger = true;
        bottom_B.isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("character_nearby", false);
        bottom_A.isTrigger = false;
        bottom_B.isTrigger = false;
    }
}
