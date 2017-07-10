using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

    public int lvlToLoad;

    private CC cc;

    private void Start()
    {
        cc = GameObject.FindGameObjectWithTag("lc").GetComponent<CC>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("hero"))
        {
            cc.press.text = ("Press E");
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("hero"))
        {
            if (Input.GetKeyDown("e")){
                Application.LoadLevel(lvlToLoad);
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("hero"))
        {
            cc.press.text = ("");
        }
    }
}
