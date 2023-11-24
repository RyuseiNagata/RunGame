using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImageTutorial : MonoBehaviour
{
    [SerializeField] PlayerTutorial playerTutorial;

    float angle = 400.0f;

    // Update is called once per frame
    void Update()
    {
        if (playerTutorial.GravityChange % 2 == 0)
        {
            angle = 400.0f;
        }
        else if (playerTutorial.GravityChange % 2 == 1)
        {
            angle = -400.0f;
        }
        transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.back);
    }
}
