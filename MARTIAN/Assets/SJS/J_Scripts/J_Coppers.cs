using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
public class J_Coppers : MonoBehaviourPun
{
    //이 스크립트는 구리에 대한 스크립트입니다
    //플레이어 상호 작용으로 인해서 일정 시간 동안 채굴당하면
    //자신 보다 작은 분신을 2~5개정도를 분열해서 던져줍니다
    //채굴 시간은 플레이어가 장비하고 있는 장비에 따라 시간이 달라집니다

    bool mining;

    //채굴에 걸리는 시간을 계산해줍니다
    float currT;
    // Start is called before the first frame update
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public float cubeSize = 0.2f;
    public int cubeInRow = 5;


    float cubesPivotDistance;
    Vector3 cubesPivot;
    //재련된 광석의 프리팹을 저장해둡니다
    public GameObject reincarnation;

    //재련된 광석을 몇개 줄건지 랜덤하게 정해줍니다
    int a;


    //플레이어 스크립트의 정보를 할당 받을 변수입니다
    Test_PlayerMovement _PlayerMove;
    void Start()
    {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubeInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        _PlayerMove = GameObject.Find("Player_Body").GetComponent<Test_PlayerMovement>();
        a = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (mining)
        {
                if (Input.GetKey(KeyCode.E))
                    Ore();
        }

    }

    //모든 광석에 상속될 함수이기때문에 이름을 Ore로 정함
    public void Ore()
    {
        currT += Time.deltaTime;
        //나중에 하나의 조건을 더 만들어야하는데 그 조건은
        //플레이어가 무슨 채굴 장비를 가지고 있는지 확인하는 변수입니다
        //지금은 5초라는 시간으로 만들어지만 나중에는 채굴 장비의 종류에 따라
        // 저 시간이 달라집니다

        if (currT >= 1f)
        {
            //explode();
            photonView.RPC("explode", RpcTarget.All);
        }

    }

    [PunRPC]
    void explode()
    {
        gameObject.SetActive(false);

        for (int x = 0; x < cubeInRow; x++)
            for (int y = 0; y < cubeInRow; y++)
                for (int z = 0; z < cubeInRow; z++)
                {
                    createPiece(x, y, z);
                }
        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        int ignoreExp = 1 << 10;
        //~LayerMask.GetMask("Player");

        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius, ignoreExp);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
        PhotonView pr = J_GameManager.gm.GetComponent<PhotonView>();
        pr.RPC("Masters", RpcTarget.MasterClient , a, reincarnation.name, ReincarnationSize, transform.position);
    }

    public float ReincarnationSize;
    public void ReincarnationSummons()
    {
        print("확인합니다");
        for (int i = 0; i < a; i++)
        {
            GameObject sum = PhotonNetwork.Instantiate(Path.Combine("Stone", reincarnation.name), transform.position, Quaternion.identity);
                //Instantiate(reincarnation);
            //여기가 새로 생성해주는 재료의 크기를 정해주는 변수입니다
            sum.transform.localScale = new Vector3(ReincarnationSize, ReincarnationSize, ReincarnationSize);
        }

       /* Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        int ignoreExp = 1 << 10;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 2, ignoreExp);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(10, transform.position, explosionRadius, explosionUpward);
            }
        }*/
    }


    void createPiece(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.layer = LayerMask.NameToLayer("ttt");
        //새로 만들어준 큐브들의 위치를 이 스크립트 큐브의 위치로 정해준다
        piece.transform.position = transform.position + new Vector3(cubeSize * x,
            cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //만들어내는 큐브에 리지드바디를 넣어주고 그 무게를 0.2f 바꿔줍니다
        piece.AddComponent<Rigidbody>();
        //자기 자신을 지우는 스크립트를 할당해준다
        piece.AddComponent<J_SeilfDes>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            print("안녕 형 걍 포기해");
            //충돌 대상이 플레이어면 채굴이 가능합니다
            mining = true;
        }
    }


    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //충돌 대상이 플레이어면 채굴이 가능합니다
            mining = false;
        }
    }

}