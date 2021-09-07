using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentText : MonoBehaviour
{
    public GameObject vent;

    //float createTime = 0.1f;
    //float currTime;

    void Start()
    {
        iTween.MoveBy(vent, iTween.Hash("y", 0.5f,
                                            "time", 1.0f,
                                            "easetype", iTween.EaseType.easeInOutCubic,
                                            "looptype", iTween.LoopType.pingPong));
    }

    void Update()
    {
        
    }

}
