using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObjects : MonoBehaviour
{
    public Transform player, Camera, InvCanvas, invManager, ItemManager ;
    void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(Camera);
        DontDestroyOnLoad(InvCanvas);
        DontDestroyOnLoad(invManager);
        DontDestroyOnLoad(ItemManager);
        DontDestroyOnLoad(gameObject);
    }
}
