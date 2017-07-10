using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;

    public Image HeartUI;

    private HeroControl hero;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("hero").GetComponent<HeroControl>();

    }
    private void Update()
    {
        int num = hero.hp;
        if (hero.hp < 0)
            num = 0;
        if (hero.hp > 5)
            num = 5;
        HeartUI.sprite = HeartSprites[num];
    }

}
