using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UIMan : MonoBehaviourPun
{
    public GameObject[] vent;
    public GameObject player;
    public Button[] btn;

    void Start()
    {
        vent = new GameObject[]
        {
            GameObject.Find("MAP/Vent/Vent1"),
            GameObject.Find("MAP/Vent/Vent2"),
            GameObject.Find("MAP/Vent/Vent3"),
            GameObject.Find("MAP/Vent/Vent4"),
            GameObject.Find("MAP/Vent/Vent5"),
        };
    }

    void Update()
    {
        
    }

    public void OnClickVent(int idx)
    {
        photonView.RPC("MoveVentPos", RpcTarget.All, vent[idx].transform.position + new Vector3(0, 1.39f, 0));
        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Vent);
        Vent.instance.ventUI.SetActive(false);
    }

    public void OnClickEnergy(int idx)
    {
        EnergyMission.instance.energyBool[idx] = true;
        GameManager.instance.missionUi[1].SetActive(false);
        btn[idx].interactable = false;
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
