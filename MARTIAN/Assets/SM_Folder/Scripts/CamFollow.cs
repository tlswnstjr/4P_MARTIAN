using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CamFollow : MonoBehaviourPun
{
    //뭘 따라갈것인가
    public Transform target;
    //얼마나 부드럽게 움직일것인가.
    public float smoothing = 5f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        print(offset);
    }

    private void FixedUpdate()
    {      
            Vector3 targetCamPos = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);      
    }
}
