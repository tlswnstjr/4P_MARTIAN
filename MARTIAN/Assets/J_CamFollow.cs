using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class J_CamFollow : MonoBehaviourPun, IPunObservable
{
    //월래 카메라 스크립트의 복사본입니다
    //뭘 따라갈것인가
    public Transform target;

    //무슨 카메라가 움직일 거?
    public Transform cam;

    //얼마나 부드럽게 움직일것인가.
    public float smoothing = 5f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        else
        {
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(target);
            stream.SendNext(transform.position);
        }
        else
        {
            target = (Transform)stream.ReceiveNext();
            transform.position = (Vector3)stream.ReceiveNext();

        }
    }
}
