using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public int dmg = 20;
    public float speed = 7f;

    void Start()
    {
        StartCoroutine(destroyLater());
    }
    public void launch(float direction)
    {
        Rigidbody2D myBody = this.GetComponent<Rigidbody2D>();
        float value = direction;

        Vector2 vel = myBody.velocity;
        vel.x = value * speed;
        myBody.velocity = vel;
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }
    }


    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("hero"))
        {
            col.SendMessageUpwards("Damage", dmg);
            Destroy(this.gameObject);
        }

    }
}
