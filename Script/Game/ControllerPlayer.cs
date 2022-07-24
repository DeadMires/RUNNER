using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove;

    Vector3 posStart;

    private void Update()
    {
        if (Input.touches.Length < 1) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            posStart = Input.GetTouch(0).position;
        else if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if(Input.GetTouch(0).position.x > posStart.x)
                playerMove.SwitchPosition(1);
            else
                playerMove.SwitchPosition(-1);
        }
    }
}
