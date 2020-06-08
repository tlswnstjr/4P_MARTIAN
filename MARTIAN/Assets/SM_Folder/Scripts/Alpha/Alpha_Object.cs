using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Object : MonoBehaviour
{
    public bool eixtAlpha;

    Material[] myMat;
    Color[] myColor;
    float[] myRenderingMode;

    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().materials;
        myRenderingMode = new float[myMat.Length];
        myColor = new Color[myMat.Length];
        for (int i = 0; i < myMat.Length; i++)
        {
            //print(myMat[i].GetFloat("_Mode"));
            myRenderingMode[i] = myMat[i].GetFloat("_Mode");
            //print(myRenderingMode[i]);
            myColor[i] = myMat[i].color;
        }

        //print(myColor);
        //print(GetComponent<MeshRenderer>().material);
    }

    // Update is called once per frame
    void Update()
    {
        if (eixtAlpha == true)
        {
            for (int i = 0; i < myMat.Length; i++)
            {
                myMat[i].color = myColor[i];
                myMat[i].SetFloat("_Mode", myRenderingMode[i]);
                //GetComponent<MeshRenderer>().material.color = myColor[];
                //print(GetComponent<MeshRenderer>().material.color);
                //print(GetComponent<MeshRenderer>().material);
            }
            eixtAlpha = false;
        }
    }
}
