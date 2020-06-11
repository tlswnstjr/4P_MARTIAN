using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChlidAnimator : MonoBehaviour
{
    //자식오브젝트에 있는 애니메이터 가져오기
    public Animator animator;

    //현제 위치에 있는 애니메이터 가자오기
    Animator animator2;
    // Start is called before the first frame update
    void Start()
    {
        animator2 = GetComponent<Animator>();
        animator2 = animator;
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
