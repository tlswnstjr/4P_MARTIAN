using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    Material giveMeMTR;
    // Start is called before the first frame update
    void Start()
    {
        giveMeMTR = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ChangeAlpha();
        }
    }

    void ChangeAlpha()
    {
        //렌더링 모드를 바꾸기 전에 자신의 렌더링 모드의 인덱스 넘버를 저장하고 싶다.

        //접촉한 오브젝트의 렌더링 모드를 Fade로 바꾸고 싶다.
        //float mode = giveMeMTR[i].GetFloat("_Mode");
        //if (mode != 0) continue;
        
        print("11111111111111111111111111");
        giveMeMTR.SetFloat("_Mode", 3);
        giveMeMTR.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        giveMeMTR.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        giveMeMTR.SetInt("_Zwrite", 0);
        giveMeMTR.DisableKeyword("_ALPHATEST_ON");
        giveMeMTR.EnableKeyword("_ALPHABLEND_ON");
        giveMeMTR.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        giveMeMTR.renderQueue = 3000;

        giveMeMTR.color = new Color(giveMeMTR.color.r, giveMeMTR.color.g, giveMeMTR.color.b, 0.3f);
        print("2222222222222222222222222222222222222");
    }
}
