using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float lives = PlayerPrefs.GetFloat("Lives");
        gameObject.GetComponent<Text>().text = " Lives: " + lives.ToString();
    }
}
