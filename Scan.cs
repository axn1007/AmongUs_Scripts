using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float scanTime = 5.0f;
    float curTime;
    public Player myPlayer;

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);

            curTime += Time.deltaTime;

            if(curTime > scanTime)
            {
                if (Player.instance.mission[0] != true) return;

                print("Misson Complete!");
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_MCl);

                myPlayer.ScanMission(1);

                Transform mt = this.gameObject.transform.GetChild(1);
                mt.GetChild(0).gameObject.SetActive(false);
                mt.GetChild(1).gameObject.SetActive(true);

                curTime = 0;
            }
        }

        if(other.transform.name == "Bottle2")
        {
            curTime += Time.deltaTime;
            SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_Ming);

            if (curTime > scanTime)
            {
                SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_MCl);

                //if (myPlayer.mission[3] != true) return;

                print("Mission Complete!");
                myPlayer.myScore += 1;

                other.GetComponent<MeshRenderer>().material.color = Color.white;
                
                curTime = 0;
            }
        }
    }
}
