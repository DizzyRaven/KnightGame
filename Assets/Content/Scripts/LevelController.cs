using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

   
        public static LevelController current;
        void Awake()
        {
            current = this;
        }

    Vector3 startingPosition;
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onHeroDeath(HeroControl hero)
    {
        //При смерті кролика повертаємо на початкову позицію
        hero.transform.position = this.startingPosition;
    }
}
