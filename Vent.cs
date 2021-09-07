using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Vent : MonoBehaviourPun
{
    public GameObject ventUI;
    public Player myPlayer;

    public static Vent instance;
    public bool sliderBool;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "vent")
        {
            print("this is vent!");

            if (Input.GetKeyDown(KeyCode.Alpha2) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                if (myPlayer.imposter != true) return;

                ventUI.SetActive(true);
            }
        }

        if (other.transform.tag == "lever")
        {
            print("this is lever!");

            if (Input.GetKeyDown(KeyCode.Alpha2) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                GameManager.instance.missionUi[0].SetActive(true);
                sliderBool = true;
            }
        }

        if (other.transform.tag == "energy")
        {
            print("this is energy");

            if (myPlayer.mission[2] != true) return;
            if (myPlayer.energyCount == 1) return;

            if(Input.GetKeyDown(KeyCode.Alpha2) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                GameManager.instance.missionUi[1].SetActive(true);
            }
        }

        if(other.transform.tag == "puzzle")
        {
            print("this is puzzle");

            if (Player.instance.mission[7] != true) return;

            if (Input.GetKeyDown(KeyCode.Alpha2) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                GameManager.instance.missionUi[2].SetActive(true);
            }
        }

        if (other.transform.tag == "call")
        {
            print("this is call");

            if (Input.GetKeyDown(KeyCode.Alpha6) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
            {
                for (int i = 0; i < GameManager.instance.players.Count; i++)
                {
                    GameManager.instance.players[i].photonView.RPC("BeCall", RpcTarget.All);
                }
            }
        }
    }
}
