using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Layer : MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    Color saveColor;

    Material[] giveMeMTR;
    

    //알파를 얼마나 줄일거야
    public float shrinkingDegree = 0.5f;

    [Range(0, 10)]
    public float speedDamp = 3f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;    
    }
    private void FixedUpdate()
    {
        Vector3 targetHeadfor = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetHeadfor, speedDamp * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    giveMeMTR = other.GetComponent<MeshRenderer>().material;
    //    //접촉한 오브젝트의 렌더링 모드를 Fade로 바꾸고 싶다.
    //    giveMeMTR.SetFloat("_Mode", 2);
    //    giveMeMTR.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
    //    giveMeMTR.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
    //    giveMeMTR.SetInt("_ZWrite", 0);
    //    giveMeMTR.DisableKeyword("_ALPHATEST_ON");
    //    giveMeMTR.EnableKeyword("_ALPHABLEND_ON");
    //    giveMeMTR.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //    giveMeMTR.renderQueue = 3000;

    //    if (other.tag == "Unnecessary")
    //    {
    //        //print(other.gameObject.name);
    //        saveColor = giveMeMTR.color;
    //        giveMeMTR.color = new Color(saveColor.r, saveColor.g, saveColor.b, shrinkingDegree);            
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Unnecessary")
        {
            DebugMsgManager.Instance.debugText.text = "OnTriggerStay \n";
            giveMeMTR = other.GetComponent<MeshRenderer>().materials;
            Alpha_Object ao = other.GetComponent<Alpha_Object>();
            if (ao == null) return;
            DebugMsgManager.Instance.debugText.text += "ag pass \n";
            ao.eixtAlpha = false;

            for (int i = 0; i < giveMeMTR.Length; i++)
            {

                saveColor = giveMeMTR[i].color;
                giveMeMTR[i].color = new Color(saveColor.r, saveColor.g, saveColor.b, shrinkingDegree);
                
                //렌더링 모드를 바꾸기 전에 자신의 렌더링 모드의 인덱스 넘버를 저장하고 싶다.

                //접촉한 오브젝트의 렌더링 모드를 Fade로 바꾸고 싶다.
                //float mode = giveMeMTR[i].GetFloat("_Mode");
                //if (mode != 0) continue;

                giveMeMTR[i].SetFloat("_Mode", 2);
                giveMeMTR[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                giveMeMTR[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                giveMeMTR[i].SetInt("_Zwrite", 0);
                giveMeMTR[i].DisableKeyword("_ALPHATEST_ON");
                giveMeMTR[i].EnableKeyword("_ALPHABLEND_ON");
                giveMeMTR[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                giveMeMTR[i].renderQueue = 3000;
                DebugMsgManager.Instance.debugText.text += "Alpha  적용 \n";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            return;
        }
        if (other.tag == "Unnecessary")
        {
            //print("---------------------------- : " + other.gameObject.name);
            Alpha_Object ao = other.GetComponent<Alpha_Object>();
            if (ao == null) return;

            ao.eixtAlpha = true;
            for (int i = 0; i < giveMeMTR.Length; i++)
            {
                //giveMeMTR[i].SetFloat("_Mode", 0);
                giveMeMTR[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                giveMeMTR[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                giveMeMTR[i].SetInt("_ZWrite", 1);
                giveMeMTR[i].DisableKeyword("_ALPHATEST_ON");
                giveMeMTR[i].DisableKeyword("_ALPHABLEND_ON");
                giveMeMTR[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                giveMeMTR[i].renderQueue = -1;
            }
        }
    }
}
