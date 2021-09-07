using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDistroy : MonoBehaviour
{
    public GameObject[] st;
    public int count;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    Destroy(hit.collider.gameObject);
                    print("우주 쓰레기 제거!");
                    count++;
                    print("우주 쓰레기가 담겼어요");
                }
            }
            StCount();
        }
        
    }

    void StCount()
    {
        if(count == 6)
        {
            print("Mission Clear!!!!!");
        }
    }
}
