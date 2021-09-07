using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRPlayerM : MonoBehaviour
{
    public float speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(x, 0, 0);

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 8)
        {
            print("충돌");
            Destroy(gameObject);
            print("Mission Failed...");
        }
        if(collision.gameObject.layer == 9)
        {
            print("거기로 가면 안돼!");
            Destroy(gameObject);
        }
    }

}
