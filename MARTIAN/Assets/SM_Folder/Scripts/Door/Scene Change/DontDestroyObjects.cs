using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObjects : MonoBehaviour
{
    public Transform player, Camera, Inv;
    void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(Camera);
        DontDestroyOnLoad(Inv);
        DontDestroyOnLoad(gameObject);
    }
}
