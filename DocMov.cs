using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocMov : MonoBehaviour
{
    public Player myPlayer;
    bool clear;
    void Start()
    {
        clear = true;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(clear == true && other.transform.name == "Pos")
        {
            if (myPlayer.mission[8] != true) return;

            SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_MCl);
            print("Mission Clear!!!!!!!");
            myPlayer.myScore += 1;
            clear = false;
        }
    }
}
