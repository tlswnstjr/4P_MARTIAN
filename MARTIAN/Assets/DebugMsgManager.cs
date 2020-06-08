using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMsgManager : MonoBehaviour
{
    public static DebugMsgManager Instance;
    public Text debugText;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

       
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
