using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Collectable
{
    protected override void OnHeroHit(HeroControl hero)
    {

        this.CollectedHide();
    }
}