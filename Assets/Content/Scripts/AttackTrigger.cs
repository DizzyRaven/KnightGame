using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int dmg = 1;

    private void Update()
    {
        // dmg = GetComponent<HeroControl>().DMG;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("enemy"))
        {
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
