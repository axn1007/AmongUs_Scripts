using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMission : MonoBehaviour
{
    public static EnergyMission instance;
    public GameObject [] energyButton;
    public bool [] energyBool;
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for(int i = 0; i< 5; i++)
        {
            energyButton = new GameObject[] {
            GameObject.Find("Mission/EnergyMission/Energy" + i) };
        }

    }

    void Update()
    {
        for(int i = 0; i < energyBool.Length; i++)
        {
            if(energyBool[i] != false)
            {
                energyButton[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
