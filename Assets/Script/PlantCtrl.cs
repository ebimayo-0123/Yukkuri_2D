using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCtrl : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.Find("Player");
        this.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pPos = player.transform.position;
        Vector2 myPos = this.transform.position;
        float distance = Vector2.Distance(pPos, myPos);

        if(distance<2&(pPos.y-myPos.y)<1)
        {
            anim.SetTrigger("TrgAtack");
        }

        if(isDead)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time*20));
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetTrigger("TrgDead");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine("Dead");
    }

    IEnumerator Dead()
    {
        isDead = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
