using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static bool StartLook = false;
    [SerializeField] Transform target;
    [SerializeField] PlayerMove playerMove;
    int fixX;

    void Update()
    {
        if (StartLook)
        {
            if (playerMove.myRotate == 0) fixX = 0;
            else if (playerMove.myRotate > 0) fixX = 10;
            else fixX = -10;
            transform.position = Vector3.MoveTowards(transform.position,
                   new Vector3(target.position.x - fixX, target.position.y + 3.5f, target.position.z - (playerMove.myRotate == 0 ? 10f : 0)),
                   Time.deltaTime * 20f);
            transform.LookAt(target);
        }
    }
}
