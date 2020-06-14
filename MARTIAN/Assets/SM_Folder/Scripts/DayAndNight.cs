using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DayAndNight : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float secondPerRealTimeSecond;

    #region ---===== 검은안개 =====-----
    //private bool isNight = false;

    //[SerializeField] private float fogDensityCalc;

    //[SerializeField] private float nightFogDensity;
    //private float dayFogDensity;
    //private float currentFogDensity;
    #endregion

    public static DayAndNight sun;

    public float myRotX;

    Quaternion rot;
    private void Awake()
    {
        if (sun == null) sun = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        //이것도 검은안개
        //dayFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
            myRotX = transform.eulerAngles.x;
        }
        else
        {
            transform.rotation = rot;
            myRotX = transform.eulerAngles.x;
        }
        

        #region ---======검은안개======----
        ////안개를 씌우고 걷히게 하는 부분
        //if (isNight)
        //{
        //    if(currentFogDensity <= nightFogDensity)
        //    {
        //        currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
        //        RenderSettings.fogDensity = currentFogDensity;
        //    }
        //}
        //else
        //{
        //    if (currentFogDensity >= dayFogDensity)
        //    {
        //        currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
        //        RenderSettings.fogDensity = currentFogDensity;
        //    }
        //}
        #endregion
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            rot = (Quaternion)stream.ReceiveNext();
        }
    }
}
