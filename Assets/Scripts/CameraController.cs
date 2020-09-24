using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > 0)
            transform.position = new Vector3(player.position.x, player.position.y, -10f);
        else
            transform.position = new Vector3(player.position.x, 0, -10f);
    }
}
