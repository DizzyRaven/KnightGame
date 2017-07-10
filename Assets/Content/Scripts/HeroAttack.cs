using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{

    private bool attacking = false;

    private float attackTimer = 0;
    private float attackCD = 0.4f;

    public Collider2D attackTrigger;

    private Animator anim;

    public AudioClip attackSound = null;

    AudioSource attackSource = null;
    
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
    }
    void Start()
    {
        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackSound;
       
       // attackFlashSource.Play();

    }

    private void Update()
    {
        if (Input.GetKeyDown("f") && !attacking)
        {
            attackSource.Play();
            attacking = true;
            attackTimer = attackCD;
            attackTrigger.enabled = true;

        }
        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }
        anim.SetBool("attack", attacking);
    }
}
