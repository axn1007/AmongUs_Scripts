using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    [SerializeField] [Range(10f, 200f)] float speed = 200f;
    public Rigidbody2D rb;
    float randomX, randomY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * speed);
    }

    void Update()
    {
        
    }
}
