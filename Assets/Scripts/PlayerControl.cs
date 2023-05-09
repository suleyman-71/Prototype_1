using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed; // karakterinizin sabit hýzý
    private bool isMoving; // karakterinizin hareket halinde olup olmadýðýný belirten deðiþken

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.touchCount == 0)
        {
            isMoving = false;
        }
    }
}
