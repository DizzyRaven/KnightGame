using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour {

    float DMGtimer = 0.2f;
    public int startHP = 5;
    public int hp = 5;
    public float speed = 3f;
    public float damage = 3f;
    bool isGrounded = false;
    bool JumpActive = false;
    public bool isDead = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 0.2f;
    public float JumpSpeed = 0.2f;
    public static HeroControl lastHero = null;
    private float timer = time_to_wait;
    private static float time_to_wait = 2;
    public int DMG = 30;
  

    private CC cc;

    public AudioClip DMGSound = null;
    AudioSource DMGSource = null;

    Rigidbody2D myBody = null;
    Transform heroParent = null;
    // Use this for initialization
    void Awake()
    {
        startHP = 5;
        hp = startHP;
        isDead = false;
        lastHero = this;
    }
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("lc").GetComponent<CC>();
       //lc = gameObject.findGetComponent<CoinCounter>();
        DMGSource = gameObject.AddComponent<AudioSource>();
        DMGSource.clip =DMGSound;

        isDead = false;
        hp = startHP;
        LevelController.current.setStartPosition(transform.position);
        myBody = this.GetComponent<Rigidbody2D>();
        this.heroParent = this.transform.parent;
    }

    // Update is called once per frame
    void Update () {
        if (hp < startHP && hp >0)
        {

            if (DMGtimer > 0)
            {
               
                DMGtimer -= Time.deltaTime;
                GetComponent<SpriteRenderer>().color = Color.red;
                
            }
            else
            {
                
                GetComponent<SpriteRenderer>().color = Color.white;
                DMGtimer = 0.2f;
                startHP = hp;
            }
        }

        if (hp <= 0)
        {
            isDead = true;
            //DMGSource.Play();
            
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
    void FixedUpdate()
    {
        
        Animator animator = GetComponent<Animator>();
        if (isDead == true)
        {

           // myBody.simulated=false;
              HeroControl hero = GetComponent<HeroControl>();
            animator.SetBool("dead", true);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LevelController.current.onHeroDeath(hero);
                timer = time_to_wait;
                isDead = false;
                startHP = 5;
                hp = startHP;
            }

        }
        else
        {
            myBody.simulated = true;
            animator.SetBool("dead", false);
        }
        if (isDead == true)
        {
            return;
        }

        Vector3 from = transform.position + Vector3.up * 0.1f;
        Vector3 to = transform.position + Vector3.down * 0.2f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");



        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }

        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        //[-1, 1]
        float value = Input.GetAxis("Horizontal");
       
       
       
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag != "MovingPlatform")
        {
            transform.parent = null;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "bottle")
        {
            if(hp<=5)
            hp += 1;
        }
        if (collider.tag == "coin")
        {
            //Destroy(collider.gameObject);
            cc.coins += 1;
        }
       
    }
    public void BottleUp()
    {
        //int dmg = GetComponent

    }
    public void Damage(int damage)
    {
        hp -= damage;
        DMGSource.Play();
    }

}
