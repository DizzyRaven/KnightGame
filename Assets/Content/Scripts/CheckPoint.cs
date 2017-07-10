using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public AudioClip attackSound = null;
    AudioSource attackSource = null;

    private int once = 1;

    void Start()
    {
        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackSound;
    }
  

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        HeroControl hero = collider.GetComponent<HeroControl>();
        //Впасти міг не тільки кролик
        if (hero != null)
        {
            if (once == 1)
            {

                attackSource.Play();
                Vector3 temp = new Vector3(0, 0, -1f);
                LevelController.current.setStartPosition(transform.position + temp);
                Animator animator = GetComponent<Animator>();
                animator.SetBool("fire", true);
                once = 0;
            }
        }
    }
   
        //
    
}
