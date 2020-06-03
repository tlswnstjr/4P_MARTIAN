using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsGrowUP : MonoBehaviour
{
    //작물이 자라는 쿨타임을 저장할 변수
    public float growCool = 5f;

    //몇번이나 자랄거야?
    public int amount = 2;
    //한번에 얼마나 크게 자랄거야?
    public float point = 0.2f;

    public ParticleSystem glitter;

    SphereCollider sc;

    //작물공장 주소
    public GameObject cropFactory;
    //작물을 스폰시킬 위치
    public Transform spawnPos;

    private void OnEnable()
    {
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;

        //내 태그에 따라 수확시 스폰될 아이템을 정하고 싶다.
        //cropFactory = Resources.Load("Test_Potato") as GameObject;
        if (gameObject.transform.CompareTag("POTATO")) cropFactory = Resources.Load<GameObject>("Test_Potato");
        if (gameObject.transform.CompareTag("CARROT")) cropFactory = Resources.Load<GameObject>("Carrot_Fruit");
        if (gameObject.transform.CompareTag("RADISH")) cropFactory = Resources.Load<GameObject>("Turnip_Fruit");
    }

    public void Growup()
    {
        StartCoroutine(GrowCrops());
    }
    public IEnumerator GrowCrops()
    {
        for (int i = 0; i <= amount; i++)
        {
            yield return new WaitForSeconds(growCool);
            transform.localScale += new Vector3(point, point, point);
            if (i == amount)
            {
                glitter.Play();
                //콜라이더를 켜자
                sc.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SpawnCrops(cropFactory);
            }
        }
    }

    private void SpawnCrops(GameObject cropFactory)
    {
        int a = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < a; i++)
        {
            GameObject crops = Instantiate(cropFactory);
            crops.transform.position = spawnPos.position;
            if (i == a)
            {
                Debug.Log("~~~~~~~~~~");
                //내 부모를 죽이겠다.
                Destroy(gameObject.transform.parent);
            }
        }
    }
}
