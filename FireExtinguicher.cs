using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguicher : MonoBehaviour
{
    public Player myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "fire") return;

        Destroy(gameObject);
        Destroy(other.gameObject);
        SoundManager.instance.PlayEFT(SoundManager.EFT_TYPE.EFT_MCl);

        if (Player.instance.mission[5] != true) return;
        myPlayer.myScore += 1;
    }
}
