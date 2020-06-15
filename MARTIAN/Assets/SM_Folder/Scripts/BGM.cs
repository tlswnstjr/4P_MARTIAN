using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource bgmPlayer;
    // Start is called before the first frame update
    void Start()
    {
        bgmPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bgmPlayer.isPlaying)
        {
            bgmPlayer.Play();
        }
    }
}
