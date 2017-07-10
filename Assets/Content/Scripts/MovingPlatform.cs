using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject patform;

    public float speed;
    public float time_to_wait;
    private float timer;


    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;


    void Start()
    {
        timer = time_to_wait;
        currentPoint = points[pointSelection];
    }

    void Update()
    {

        patform.transform.position = Vector3.MoveTowards(patform.transform.position, currentPoint.position, Time.deltaTime * speed);

        if (patform.transform.position == currentPoint.position)
        {
            //timer
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                pointSelection++;
                timer = time_to_wait;
            }

            if (pointSelection == points.Length)
            {
                pointSelection = 0;
            }

            currentPoint = points[pointSelection];
        }
    }


}