using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Object : MonoBehaviour
{
    public bool eixtAlpha;

    Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        myColor = GetComponent<MeshRenderer>().material.color;
        //print(myColor);
        //print(GetComponent<MeshRenderer>().material);
    }

    // Update is called once per frame
    void Update()
    {
        if (eixtAlpha == true)
        {
            GetComponent<MeshRenderer>().material.color = myColor;
            print(GetComponent<MeshRenderer>().material.color);
            print(GetComponent<MeshRenderer>().material);
            eixtAlpha = false;
        }
    }
}
