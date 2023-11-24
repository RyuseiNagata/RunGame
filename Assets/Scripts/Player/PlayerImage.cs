using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImage : MonoBehaviour
{
    [SerializeField] Player player;
 
    float angle = 400.0f;

    // Update is called once per frame
    void Update()
    {
        if (player.GravityChange % 2 == 0)
        {
            angle = 400.0f;
        }
        else if (player.GravityChange % 2 == 1)
        {
            angle = -400.0f;
        }
        transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.back);

        if (player.IsDeath)
        {
            gameObject.SetActive(false);
        }
    }
}
