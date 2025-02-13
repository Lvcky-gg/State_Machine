using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{

    [Header("Sword Info")]
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        //player = PlayerManager.instance.player.GetComponent<Player>();
    }
    public void SetupSword(Vector2 _launchDir, float _gravityScale)
    {
        rb.velocity = _launchDir;
        rb.gravityScale = _gravityScale;
    }
}
