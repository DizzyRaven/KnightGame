using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public int dmg = 100;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("hero"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
