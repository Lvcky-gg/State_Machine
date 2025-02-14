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


    public bool isBouncing;
    public int amountOffBounces;
    public float bounceSpeed;
    public List<Transform> enemyTargets;
    private int ammountOfBounces = 4;
    private int targetIndex = 0;

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
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
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
        if (isBouncing && enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < .5f)
            {
                targetIndex++;
                if (targetIndex >= enemyTargets.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTargets.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTargets.Add(hit.transform);
                    }
                }
            }
        }
        StuckInto(collision);
    }
    private void StuckInto(Collider2D collision)
    {

        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing)
            return;
        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
