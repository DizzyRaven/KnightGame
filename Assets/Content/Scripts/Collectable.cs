using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    protected virtual void OnHeroHit(HeroControl hero)
    {
        // CollectedHide();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        // if (!this.hideAnimation)
        //{
        HeroControl hero = collider.GetComponent<HeroControl>();
        if (hero != null)
        {
            this.OnHeroHit(hero);
        }
        //  }
    }
    public void CollectedHide()
    {
        Destroy(this.gameObject);
    }

    //=====================Collactables===============

}