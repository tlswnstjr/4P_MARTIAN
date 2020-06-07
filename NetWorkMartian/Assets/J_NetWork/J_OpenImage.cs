using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class J_OpenImage : MonoBehaviourPunCallbacks, IPunObservable    
{
    public Image myImage;
    public Sprite[] playerPos;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(myImage);
        }
        else
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        myImage.enabled = true;
        myImage.sprite = playerPos[Random.Range(0, 4)];
        if (photonView.IsMine)
        {
            
        }

          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
