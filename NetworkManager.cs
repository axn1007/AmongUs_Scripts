using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.GameVersion = "0.1";

        int num = Random.Range(0, 100);
        PhotonNetwork.NickName = "Player" + num;
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 8 }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        print("∑Î ¿‘¿Â!");

        //Vector2 originPos = Random.insideUnitCircle * 2.0f;
        Vector2 originPos = new Vector2(54.6f, 5.6f);
        PhotonNetwork.Instantiate("Player", GameManager.instance.playerPos[0].transform.position + new Vector3(0, 1.39f, 0), Quaternion.identity);
        //PhotonNetwork.Instantiate("Player", new Vector3(originPos.x, 1.39f, originPos.y), Quaternion.identity);
    }


    void Update()
    {
        
    }
}
