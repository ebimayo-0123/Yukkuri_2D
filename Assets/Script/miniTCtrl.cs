using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniTCtrl : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spRenderer;
    private Rigidbody2D rb2d;
    private GameObject player;
    public float speed = 25;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 1;

        if(this.transform.rotation.y==180)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }

        rb2d.AddForce(Vector2.right * x * speed);
    }
}
