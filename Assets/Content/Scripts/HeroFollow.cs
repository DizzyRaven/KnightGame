using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    public double pluss = 0.3;
    public HeroControl hero;

    void Update()
    {
        //Отримуємо доступ до компонента Transform
        //це Скорочення до GetComponent<Transform>
        Transform hero_transform = hero.transform;

        //Отримуємо доступ до компонента Transform камери
        Transform camera_transform = this.transform;

        //Отримуємо доступ до координат кролика
        Vector3 hero_position = hero_transform.position;
        Vector3 camera_position = camera_transform.position;

        //Рухаємо камеру тільки по X,Y
        camera_position.x = hero_position.x;
        camera_position.y = hero_position.y+(1/2);

        //Встановлюємо координати камери
        camera_transform.position = camera_position;
    }
}