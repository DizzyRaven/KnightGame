using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour {
    float DMGtimer = 0.2f;
    public int startHP = 3;
    int hp;
    private float timer = time_to_wait;
    private static float time_to_wait = 3;
    float last_carrot = 0;
    public float radius_attack = 3.0f;
    bool isDead = false;


    public AudioClip DMGSound = null;
    AudioSource DMGSource = null;

    Rigidbody2D abody = null;

    private void Start()
    {
        DMGSource = gameObject.AddComponent<AudioSource>();
        DMGSource.clip = DMGSound;
        bool isDead = false;
        // bool isDead = false;
        abody = this.GetComponent<Rigidbody2D>();
        hp = startHP;
    }

  
    private void FixedUpdate()
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
            abody.simulated = false;
            animator.SetBool("die", true);
            isDead = true;
            timer -= Time.deltaTime;
           
            if (timer <= 0)
            {
                Destroy(gameObject);
                
            }
            //Destroy(gameObject);
        }
        
        if (isDead == false)
        {
            float value = this.getDirection();

            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
            if (value < 0)
            {
                sr.flipX = false;
            }
            else if (value > 0)
            {
                sr.flipX = true;
            }
        }
    }
    float getDirection()
    {
        Animator animator = GetComponent<Animator>();
        Vector3 my_pos = this.transform.position;
        Vector3 rabit_pos = HeroControl.lastHero.transform.position;
        if (Mathf.Abs(rabit_pos.x - my_pos.x) < radius_attack)
        {

            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
           
            if (my_pos.x < rabit_pos.x && launchTime() && Mathf.Abs(rabit_pos.x - my_pos.x) < radius_attack)
            {
                animator.SetBool("attack",true);
                launchCarrot(1);
                sr.flipX = true;
                last_carrot = Time.time;


                return 0;

            }
            else if (my_pos.x > rabit_pos.x && launchTime() && Mathf.Abs(rabit_pos.x - my_pos.x) < radius_attack)
            {
                animator.SetBool("attack",true);
                launchCarrot(-1);
                sr.flipX = false;
                last_carrot = Time.time;

                return 0;
            }
        }
        else
        {
            animator.SetBool("attack", false);
            animator.SetBool("die", false);
        }
        
       // animator.SetBool("attack", true);
        return 0;
    }
        bool launchTime()
    {
        return Time.time - last_carrot > 2.0f;
    }

    public GameObject prefabArrow;

    void launchCarrot(float direction)
    {
        //Створюємо копію Prefab
        GameObject obj = GameObject.Instantiate(this.prefabArrow);
        //Розміщуємо в просторі
        obj.transform.position = this.transform.position;
        Vector3 temp = new Vector3(0, -0.1f, 0);
        obj.transform.position += temp;
        //Запускаємо в рух
        Arrow arrow = obj.GetComponent<Arrow>();
        arrow.launch(direction);
    }


    public void Damage(int damage)
    {
        hp -= damage;
        if (hp<=0)
        {
            DMGSource.Play();
        }

        Animator animator = GetComponent<Animator>();
        if (DMGtimer > 0)
        {
            DMGtimer -= Time.deltaTime;
            animator.SetBool("dmg",true);
        }
        else
        {
            animator.SetBool("dmg", false);
        }
        //gameObject.GetComponent<Animation>().Play("ADMG");
    }
}