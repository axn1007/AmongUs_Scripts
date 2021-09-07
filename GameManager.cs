using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class GameManager : MonoBehaviourPun
{
    public static GameManager instance;

    public Transform[] playerPos;
    public bool[] isEmpty;
    public GameObject wall;
    public Player myPlayer;
    public GameObject[] missionUi;
    public bool result;
    public bool startGame;

    public Text text;
    int crews;

    //Player 객체들이 저장될 변수
    public List<Player> players = new List<Player>();

    public int[,] arrayCount = new int[8, 2];
    public int infoIdx;
    public int sum;
    public GameObject voteUi;
    public GameObject photonVoice;

    public void AddPlayer(Player player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;

        isEmpty = new bool[playerPos.Length];

        StartCoroutine(ImposterRand());
        //투표
        StartCoroutine(SortVoting());

        StartCoroutine(CheckResult());
    }

    void Update()
    {
        
    }

    public Vector3 GetEmptyPos()
    {
        for (int i = 0; i < isEmpty.Length; i++)
        {
            if (isEmpty[i] == false)
            {
                isEmpty[i] = true;
                return playerPos[i].position;
            }
        }

        return Vector3.zero;
    }

    //담아둔 Players배열을 랜덤으로 돌려서 첫번째를 임포스터로 선정
    IEnumerator ImposterRand()
    {
        while (players.Count != 4)
        {
            yield return null;
        }

        if (PhotonNetwork.IsMasterClient)
        {

            if (players.Count == 4)
            {
                startGame = true;

                print("랜덤!");
                for (int i = 0; i < 100; i++)
                {
                    Player tamp;
                    int rand1 = Random.Range(0, players.Count);
                    int rand2 = Random.Range(0, players.Count);

                    tamp = players[rand1];
                    players[rand1] = players[rand2];
                    players[rand2] = tamp;
                }

                players[0].imposter = true;
                players[0].crew = false;

                for (int i = 1; i < players.Count; i++)
                {
                    players[i].imposter = false;
                    players[i].crew = true;
                }

                for(int i = 0; i < players.Count; i++)
                {
                    players[i].photonView.RPC("SetImposter", RpcTarget.All, players[i].imposter);
                }
            }
        }
    }

    public void CountDown()
    {
        StartCoroutine(CoCountDown());
    }

    IEnumerator CoCountDown()
    {
        print("플레이어가 모두 입장하였습니다.");
        Player.instance.countDown.text = "플레이어가 모두 입장하였습니다.";
        yield return new WaitForSeconds(5.0f);
        print("5초 뒤에 게임이 시작됩니다.");
        Player.instance.countDown.text = "5초 뒤에 게임이 시작됩니다";
        yield return new WaitForSeconds(5.0f);
        print("5");
        Player.instance.countDown.text = "5";
        yield return new WaitForSeconds(1.0f);
        print("4");
        Player.instance.countDown.text = "4";
        yield return new WaitForSeconds(1.0f);
        print("3");
        Player.instance.countDown.text = "3";
        yield return new WaitForSeconds(1.0f);
        print("2");
        Player.instance.countDown.text = "2";
        yield return new WaitForSeconds(1.0f);
        print("1");
        Player.instance.countDown.text = "1";
        yield return new WaitForSeconds(1.0f);
        print("임포스터 선정 중....");
        Player.instance.intro[0].SetActive(false);
        Player.instance.intro[1].SetActive(true);
        yield return new WaitForSeconds(10.0f);
        Destroy(wall);
        photonVoice.SetActive(false);
    }

    //투표시스템
    public void OnClickVoteBtn1()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼1이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 0);

        print("버튼1은" + arrayCount[0, 1]);
        
        Player.instance.vote = false;
    }
    public void OnClickVoteBtn2()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼2이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 1);

        print("버튼1은" + arrayCount[1, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn3()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼3이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 2);

        print("버튼1은" + arrayCount[2, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn4()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼4이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 3);

        print("버튼1은" + arrayCount[3, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn5()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼5이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 4);

        print("버튼1은" + arrayCount[4, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn6()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼6이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 5);

        print("버튼1은" + arrayCount[5, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn7()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼7이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 6);

        print("버튼1은" + arrayCount[6, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn8()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("버튼8이 눌렸습니다");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 7);

        print("버튼1은" + arrayCount[7, 1]);

        Player.instance.vote = false;
    }

    IEnumerator SortVoting()
    {
        while (sum != 4)
        {
            yield return null;
        }

        if (sum == 4)
        {
            print("투표");

            List<int> bestIdx = new List<int>();
            int score = -1;
            for (int i = 0; i < 8; i++)
            {
                if (arrayCount[i, 1] > score)
                {
                    score = arrayCount[i, 1];
                    bestIdx.Clear();
                    bestIdx.Add(i);
                }
                else if (arrayCount[i, 1] == score)
                {
                    bestIdx.Add(i);
                }
            }
            print(bestIdx[0]);
            infoIdx = bestIdx[0] + 1;

            myPlayer.intro[1].SetActive(true);
            yield return new WaitForSeconds(3.0f);
            myPlayer.intro[1].SetActive(false);

            if (bestIdx.Count > 1)
            {
                print("부결!");
                myPlayer.intro[8].SetActive(true);
            }

            else
            {
                for (int i = 0; i < players.Count; i++)
                {
                    print("비교 시작");

                    if (players[i].infoNum == (bestIdx[0] + 1))
                    {
                        players[i].transform.GetChild(0).gameObject.SetActive(false);
                        print(players[i].infoNum);
                        print("비교 끝!");
                        players[i].transform.GetChild(2).gameObject.SetActive(false);
                        players[i].die = true;

                        //버튼 비활성화
                        voteUi.transform.GetChild(bestIdx[0] + 3).gameObject.SetActive(false);

                        if(players[i].imposter == true)
                        {
                            myPlayer.uiText.text = "축하합니다! 임포스터 입니다.";
                        }

                        if(players[i].crew == true)
                        {
                            myPlayer.uiText.text = "아쉽습니다. 임포스터가 아니었습니다.";
                        }

                        myPlayer.intro[9].SetActive(true);

                        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_VoteBye);
                    }
                }
            }

            yield return new WaitForSeconds(10.0f);
            myPlayer.intro[8].SetActive(false);
            myPlayer.intro[9].SetActive(false);
            photonVoice.SetActive(false);
            voteUi.SetActive(false);
            result = true;            
        }
    }

    IEnumerator CheckResult()
    {
        while (result == false)
        {
            yield return null;
        }

        if(result == true)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].crew == true)
                {
                    if (players[i].die == false)
                    {
                        crews++;
                    }
                }

                if (players[i].imposter == true)
                {
                    if (players[i].die == true)
                    {
                        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_CrewWin);
                        myPlayer.intro[6].SetActive(true);
                        print("크루 승리!");
                        result = false;
                    }
                }
            }

            if (crews < 2)
            {
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_ImWin);
                myPlayer.intro[4].SetActive(true);
                print("임포스터 승리!");
                result = false;
            }

            if (crews > 2)
            {
                crews = 0;
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_GameGo);
                print("게임 계속 진행");
                result = false;
            }
        }
    }

    public void AddCount(int i)
    {
        arrayCount[i, 1] += 1;
        sum += 1;
    }
}
