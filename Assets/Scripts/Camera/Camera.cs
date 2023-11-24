using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 6.5f, 0.0f, -10.0f);
    }
}
