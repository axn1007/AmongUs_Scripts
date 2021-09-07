using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //EFT 종류
    public enum EFT_TYPE
    {
        EFT_1,
        EFT_ImOrCr,
        EFT_Ming,
        EFT_MCl,
        EFT_Vent,
        EFT_Kill,
        EFT_Emerg,
        EFT_Vote,
        EFT_VoteBye,
        EFT_ImWin,
        EFT_CrewWin,
        EFT_MOpen,
        EFT_Door,
        EFT_GameGo,
        EFT_Grab
    }

    //EFT 플레이하는 AudioSource
    public AudioSource eftAudio;

    //eft 음원 파일
    public AudioClip[] efts;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayEFT(EFT_TYPE type)
    {
        //eftAudio.clip = efts[(int)type];
        //eftAudio.Play();
        eftAudio.PlayOneShot(efts[(int)type]);
    }
}
