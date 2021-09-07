using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MissionRand : MonoBehaviourPun
{
    public List<int> randmis = new List<int>();
    public Text[] texts;
    public Player myPlayer;

    void Start()
    {
        if (photonView.IsMine)
        {
            randMission();
            missionSetting();
        }
    }

    void Update()
    {
        
    }

    void randMission()
    {
        for (int i = 0; i < 10; i++)
        {
            randmis.Add(i);
        }

        for (int i = 0; i < 100; i++)
        {
            int tamp;
            int rand1 = Random.Range(0, randmis.Count);
            int rand2 = Random.Range(0, randmis.Count);

            tamp = randmis[rand1];
            randmis[rand1] = randmis[rand2];
            randmis[rand2] = tamp;
        }
    }

    public void missionSetting()
    {
        for(int i = 0; i < 4; i++)
        {
            SetMissionText(i + 1, randmis[i]);
        }
    }

    void SetMissionText(int missionOrder, int missionIdx)
    {
        //texts = (missionOrder + "번째 미션은?");
        print(missionOrder + "번째 미션은?");
        myPlayer.mission[missionIdx] = true;
        
        switch(missionIdx)
        {
            case 0:
                texts[missionOrder - 1].text = "의무실에 가서 스캔하기";
                print("의무실에 가서 스캔하기");
                break;
            case 1:
                texts[missionOrder - 1].text = "레버 올리기";
                print("레버 올리기");
                break;
            case 2:
                texts[missionOrder - 1].text = "에너지 전환하기";
                print("에너지 전환하기");
                break;
            case 3:
                texts[missionOrder - 1].text = "산소통 교체하기";
                print("산소통 교체하기");
                break;
            case 4:
                texts[missionOrder - 1].text = "연료 공급하기";
                print("연료 공급하기");
                break;
            case 5:
                texts[missionOrder - 1].text = "소화기 던져서 불 끄기";
                print("소화기 던져서 불 끄기");
                break;
            case 6:
                texts[missionOrder - 1].text = "우주쓰레기 제거하기";
                print("우주쓰레기 제거하기");
                break;
            case 7:
                texts[missionOrder - 1].text = "숫자퍼즐 누르기";
                print("숫자퍼즐 누르기");
                break;
            case 8:
                texts[missionOrder - 1].text = "문서 이동시키기";
                print("문서 이동시키기");
                break;
            case 9:
                texts[missionOrder - 1].text = "잡초 뽑아서 우주선을 깨끗하게 만들기";
                print("잡초 뽑아서 우주선을 깨끗하게 만들기");
                break;
            default:
                break;
        }
    }
}
