using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed; // karakterinizin sabit h�z�
    private bool isMoving; // karakterinizin hareket halinde olup olmad���n� belirten de�i�ken

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
