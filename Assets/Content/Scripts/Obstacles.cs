using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {
    public int startHP=100;
    public int hp;
    float DMGtimer = 0.3f;

    private void Start()
    {
        hp = startHP;
    }
    private void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (hp < startHP)
        {
            if (DMGtimer > 0)
            {
                DMGtimer -= Time.deltaTime;
                animator.SetBool("dmg", true);
            }
            else
            {
                animator.SetBool("dmg", false);
                DMGtimer = 0.2f;
                startHP = hp;
            }
        }

       


        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        hp -= damage;
    }
}
