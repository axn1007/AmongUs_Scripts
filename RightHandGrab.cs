using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RightHandGrab : MonoBehaviour
{
    public float throwPower = 3;
    
    //Transform catchedObj;

    public PhotonView photonView;
    public PlayerMove player;

    void Start()
    {

    }

    void Update()
    {
        if (photonView.IsMine == false) return;

        GrabObj();
        DropObj();
        CatchObj();
        ButtonOne();
    }


    void GrabObj()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            photonView.RPC("RpcGrabObj", RpcTarget.All);
        }
    }

    void DropObj()
    {
        if (player.catchedObj == null) return;

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            photonView.RPC("RpcDropObj", RpcTarget.All);
        }
    }

    void CatchObj()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            photonView.RPC("RpcCatchObj", RpcTarget.All);
        }
    }

    void ButtonOne()
    {
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            photonView.RPC("RpcButtonOne", RpcTarget.All);
        }
    }
}
