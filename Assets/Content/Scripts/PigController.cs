using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public int hp = 1;
    public float runSpeed = 1f;
    public float walkSpeed = 0.3f;
    float speed;
    public float patrolDistance = 3;
    public float value = 0;
    bool heroDead = false;
    private float timer = time_to_wait;
    private static float time_to_wait = 2;

    public AudioClip attackSound = null;
    AudioSource attackSource = null;

    Vector3 pointA;
    Vector3 pointB;

    Mode mode;
    Mode oldMode;

    Rigidbody2D myBody = null;

    void Start()
    {
        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackSound;
        speed = walkSpeed;
        mode = Mode.GoToA;
        pointA = this.transform.position;
        pointB = pointA;

        pointA.x += patrolDistance;

        pointB.x -= patrolDistance;


       // GetComponent<Animator>().SetBool("dead", false);
        myBody = this.GetComponent<Rigidbody2D>();
       // LevelController.current.setStartPosition(transform.position);
    }
    public enum Mode
    {
        GoToA,
        GoToB,
        Attack,
        Dead

    }
    float getDirection()
    {
        if (mode != Mode.Attack)
            oldMode = mode;
        Vector3 hero_pos = HeroControl.lastHero.transform.position;
        Vector3 my_pos = this.transform.position;
        

        if (hero_pos.x > Mathf.Min(pointA.x, pointB.x)
&& hero_pos.x < Mathf.Max(pointA.x, pointB.x)&&heroDead==false)
        {
            mode = Mode.Attack;
        }
        else {
            mode = oldMode;
           
        }
        if (hero_pos.x > Mathf.Min(pointA.x, pointB.x)
&& hero_pos.x < Mathf.Max(pointA.x, pointB.x) && heroDead == true)
        {
            this.mode = Mode.GoToA;
        }
        else {
            heroDead = false;
        }

            if (mode == Mode.Attack && heroDead==false)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("run", true);
            speed = runSpeed;
            if (my_pos.x < hero_pos.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("run", false);
            speed = walkSpeed;
        }
        //======

        if (this.mode == Mode.GoToA)
        {
            if (my_pos.x > pointA.x)
            {
                this.mode = Mode.GoToB;
            }
            return 1;
        }
        else if (this.mode == Mode.GoToB)

        {
            if (my_pos.x < pointB.x)
            {
                this.mode = Mode.GoToA;
            }
            return -1;
        }

        return 0;
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
       
        Animator animator = GetComponent<Animator>();

        if (hp<=0)
        {
           
            myBody.simulated = false;
            animator.SetBool("die", true);
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }

        }
       

        value = this.getDirection();
  
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == "hero")
        {
            heroDead = true;
        }
        else{

        }

    }

    public void Damage(int damage)
    {
        attackSource.Play();
        hp -= damage;
    }

}