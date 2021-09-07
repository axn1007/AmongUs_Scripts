using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STManager : MonoBehaviour
{
    //생성시간
    public int createTime = 0;
    //현재시간
    float currTime;
    //SpaceTrash공장
    public GameObject STFactory;

    //게임시간
    float gameTime = 20;

    void Start()
    {
        createTime = Random.Range(3, 10);
        StartCoroutine(CreateSTProc());
    }

    void Update()
    {
        currTime += Time.deltaTime;
    }

    IEnumerator CreateSTProc()
    {
        while (currTime < gameTime)
        {
            yield return new WaitForSeconds(createTime);

            //공장에서 우주쓰레기 생성
            GameObject ST = Instantiate(STFactory); ;
            //우주쓰레기를 우주쓰레기매니저 위치에 놓는다
            ST.transform.position = transform.position;



        }
        yield return new WaitForSeconds(5);
        print("Mission Clear!!!!!");
    }

}
