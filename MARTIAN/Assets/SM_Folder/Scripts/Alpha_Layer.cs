using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Layer : MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    Color saveColor;

    Material giveMeMTR;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Unnecessary")
        {
            //print(other.gameObject.name);
            saveColor = other.GetComponent<MeshRenderer>().material.color;
            other.GetComponent<MeshRenderer>().material.color = new Color(saveColor.r, saveColor.g, saveColor.b, shrinkingDegree);
            giveMeMTR = other.GetComponent<MeshRenderer>().material;
        }
        //접촉한 오브젝트의 렌더링 모드를 Fade로 바꾸고 싶다.
        giveMeMTR.SetFloat("_Mode", 2);
        giveMeMTR.SetInt("_SrcBland", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        giveMeMTR.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        giveMeMTR.SetInt("_Zwrite", 0);
        giveMeMTR.DisableKeyword("_ALPHATEST_ON");
        giveMeMTR.EnableKeyword("_ALPHABLEND_ON");
        giveMeMTR.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        giveMeMTR.renderQueue = 3000;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            return;
        }
        print("---------------------------- : " + other.gameObject.name);
        other.GetComponent<Alpha_Object>().eixtAlpha = true;
        giveMeMTR.SetFloat("_Mode", 0);
        giveMeMTR.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        giveMeMTR.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        giveMeMTR.SetInt("_ZWrite", 1);
        giveMeMTR.DisableKeyword("_ALPHATEST_ON");
        giveMeMTR.DisableKeyword("_ALPHABLEND_ON");
        giveMeMTR.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        giveMeMTR.renderQueue = -1;
    }
}
