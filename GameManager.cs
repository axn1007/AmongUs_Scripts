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

    //Player ��ü���� ����� ����
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
        //��ǥ
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

    //��Ƶ� Players�迭�� �������� ������ ù��°�� �������ͷ� ����
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

                print("����!");
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
        print("�÷��̾ ��� �����Ͽ����ϴ�.");
        Player.instance.countDown.text = "�÷��̾ ��� �����Ͽ����ϴ�.";
        yield return new WaitForSeconds(5.0f);
        print("5�� �ڿ� ������ ���۵˴ϴ�.");
        Player.instance.countDown.text = "5�� �ڿ� ������ ���۵˴ϴ�";
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
        print("�������� ���� ��....");
        Player.instance.intro[0].SetActive(false);
        Player.instance.intro[1].SetActive(true);
        yield return new WaitForSeconds(10.0f);
        Destroy(wall);
        photonVoice.SetActive(false);
    }

    //��ǥ�ý���
    public void OnClickVoteBtn1()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư1�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 0);

        print("��ư1��" + arrayCount[0, 1]);
        
        Player.instance.vote = false;
    }
    public void OnClickVoteBtn2()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư2�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 1);

        print("��ư1��" + arrayCount[1, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn3()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư3�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 2);

        print("��ư1��" + arrayCount[2, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn4()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư4�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 3);

        print("��ư1��" + arrayCount[3, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn5()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư5�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 4);

        print("��ư1��" + arrayCount[4, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn6()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư6�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 5);

        print("��ư1��" + arrayCount[5, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn7()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư7�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 6);

        print("��ư1��" + arrayCount[6, 1]);

        Player.instance.vote = false;
    }
    public void OnClickVoteBtn8()
    {
        if (Player.instance.vote != true) return;

        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vote);

        print("��ư8�� ���Ƚ��ϴ�");

        myPlayer.photonView.RPC("AddCount", RpcTarget.All, 7);

        print("��ư1��" + arrayCount[7, 1]);

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
            print("��ǥ");

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
                print("�ΰ�!");
                myPlayer.intro[8].SetActive(true);
            }

            else
            {
                for (int i = 0; i < players.Count; i++)
                {
                    print("�� ����");

                    if (players[i].infoNum == (bestIdx[0] + 1))
                    {
                        players[i].transform.GetChild(0).gameObject.SetActive(false);
                        print(players[i].infoNum);
                        print("�� ��!");
                        players[i].transform.GetChild(2).gameObject.SetActive(false);
                        players[i].die = true;

                        //��ư ��Ȱ��ȭ
                        voteUi.transform.GetChild(bestIdx[0] + 3).gameObject.SetActive(false);

                        if(players[i].imposter == true)
                        {
                            myPlayer.uiText.text = "�����մϴ�! �������� �Դϴ�.";
                        }

                        if(players[i].crew == true)
                        {
                            myPlayer.uiText.text = "�ƽ����ϴ�. �������Ͱ� �ƴϾ����ϴ�.";
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
                        print("ũ�� �¸�!");
                        result = false;
                    }
                }
            }

            if (crews < 2)
            {
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_ImWin);
                myPlayer.intro[4].SetActive(true);
                print("�������� �¸�!");
                result = false;
            }

            if (crews > 2)
            {
                crews = 0;
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_GameGo);
                print("���� ��� ����");
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