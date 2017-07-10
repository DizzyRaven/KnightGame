using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CC : MonoBehaviour {

    public int coins;

    public Text coinsText;
    public Text press;

    private void Update()
    {
        coinsText.text = (""+coins/2);
    }
}
