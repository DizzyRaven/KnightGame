using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer2 : MonoBehaviour {

    public int startHP;
    public int currentHP;

    public float distance;
    public float shootInterval;
    public float arrrowSpeed = 100;
    public float arrowTimer;

    //booleans

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft;
    public Transform shootPointRight;
    


}
