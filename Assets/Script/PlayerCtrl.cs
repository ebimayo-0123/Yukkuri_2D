using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 400f;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spRenderer;
    private bool isGround;
    private bool isSloped;
    private bool isDead = false;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.spRenderer= GetComponent<SpriteRenderer>();

        Sound.LoadSe("dead", "dead");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");//��-1�E�Ȃɂ����Ȃ�0�E�E1

        //�X�v���C�g�̌�����ς���
        if (x < 0)
        {
            spRenderer.flipX = true;
        }else if (x > 0)
        {
            spRenderer.flipX = false;
        }

        if (!isDead)
        {
            rb2d.AddForce(Vector2.right * x * speed);//�������ɗ�
        }
        anim.SetFloat("Speed", Mathf.Abs(x * speed));//�����A�j���[�V����

        if(isSloped)//�⓹
        {
            this.gameObject.transform.Translate(0.05f * x, 0.0f, 0.0f);
        }

        if(Input.GetButtonDown("Jump")&isGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            anim.SetBool("isJump", true);
        }
        if(isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;

        if(velY>0.5f)
        {
            anim.SetBool("isJump", true);
        }
        if (velY < -0.1f)
        {
            anim.SetBool("isFall", true);
        }

        if (Mathf.Abs(velX)>5)
        {
            if(velX>5.0f)
            {
                rb2d.velocity = new Vector2(4.0f, velY);
            }
            if (velX < -5.0f)
            {
                rb2d.velocity = new Vector2(-4.0f, velY);
            }
        }
    }

    private void FixedUpdate()
    {
        isGround = false;

        float x = Input.GetAxisRaw("Horizontal");

        //�����̗����Ă���ꏊ
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y);
        //�n�ʔ���G���A
        Vector2 groundArea = new Vector2(0.5f, 0.5f);

        Vector2 wallArea1 = new Vector2(x*0.8f, 1.5f);
        Vector2 wallArea2 = new Vector2(x*0.3f, 1.0f);

        Vector2 wallArea3 = new Vector2(x * 1.5f, 0.6f);
        Vector2 wallArea4 = new Vector2(x * 1.0f, 0.1f);

        //Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);
        //Debug.DrawLine(groundPos + wallArea1, groundPos + wallArea2, Color.red);
        //Debug.DrawLine(groundPos + wallArea3, groundPos + wallArea4, Color.red);

        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);

        bool area1 = false;
        bool area2 = false;

        area1 = Physics2D.OverlapArea(groundPos + wallArea1, groundPos + wallArea2, groundLayer);
        area2 = Physics2D.OverlapArea(groundPos + wallArea3, groundPos + wallArea4, groundLayer);

        if(!area1&area2)
        {
            isSloped = true;
        }
        else
        {
            isSloped = false;
        }

        //Debug.Log(isSloped);
    }

    IEnumerator Dead()
    {
        anim.SetBool("isDamage", true);

        yield return new WaitForSeconds(0.3f);

        rb2d.AddForce(Vector2.up * jumpForce);
        GetComponent<CircleCollider2D>().enabled = false;
        Sound.PlaySe("dead", 0);
    }

    void OnTriggerEnter2D(Collider2D col)//�ʂ蔲�������ǂ���
    {
        if (col.gameObject.tag == "Enemy")
        {
            isDead = true;
            StartCoroutine("Dead");
        }
    }

    void OnCollisionEnter2D(Collision2D col)//������Ƃ�
    {
        if(col.gameObject.tag=="Enemy")
        {
            anim.SetBool("isJump", true);
            rb2d.AddForce(Vector2.up * jumpForce);
        }

        if (col.gameObject.tag == "Damage")
        {
            isDead = true;
            StartCoroutine("Dead");
        }
    }
}
