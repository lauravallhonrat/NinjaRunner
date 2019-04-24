using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    [SerializeField]
    private float speed = 5;
    void Update()
    {
        gameObject.transform.Rotate(0, speed*Time.deltaTime, 0);
    }
}
