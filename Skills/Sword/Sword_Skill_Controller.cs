using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{

    private float returnspeed = 12f;
    [Header("Sword Info")]
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private bool canRotate = true;
    private bool isReturning;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        //player = PlayerManager.instance.player.GetComponent<Player>();
    }
    public void SetupSword(Vector2 _launchDir, float _gravityScale, Player _player)
    {
        player = _player;
        rb.velocity = _launchDir;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotation", true);
    }
    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;



    }
    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity.normalized;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnspeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1f)
                player.ClearSword();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Rotation", false);
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
