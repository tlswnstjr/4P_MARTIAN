using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Ore : J_Item
{
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (click == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //aaa();
                photonView.RPC("aaa", Photon.Pun.RpcTarget.AllBuffered);
                //J_GameManager.gm.StoneIns();
            }
        }
    }
}
