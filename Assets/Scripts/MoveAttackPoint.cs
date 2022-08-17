using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackPoint : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
