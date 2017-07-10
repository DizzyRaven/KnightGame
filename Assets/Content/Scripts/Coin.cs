using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable

    
{
    public float Timer = 0.2f;
    private bool up = false;
    public AudioClip coinSound = null;

    AudioSource attackSource = null;
    void Start()
    {
        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = coinSound;
    }
    private void Update()
    {
        if (up == true)
        {

            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                this.CollectedHide();
            }
        }
    }

    protected override void OnHeroHit(HeroControl hero)
    {
        attackSource.Play();
        up = true;
    }
}