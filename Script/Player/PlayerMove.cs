using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove inc;
    [SerializeField] Rigidbody rb;
    public float[] moves = new float[2];
    public float myRotate = 0;
    [SerializeField] CameraMove cameraMove;

    public Material MyMaterial;
    [SerializeField]
    int oldFixPosition = 1;
    public DateTime canSwitch = DateTime.Now;
    [SerializeField] GameObject ObjectSacale;
    [SerializeField] BoxCollider MyCollider;

    public float normalSpeed = 0.1f;

    private void Start()
    {
        inc = this;
    }


    private void Update()
    {
        Vector3 newMove = new (transform.position.x + moves[0], transform.position.y, transform.position.z + moves[1]);
        rb.MovePosition(newMove);
    }

    public void Flip(float value)
    {
        myRotate += value;
        transform.rotation =  Quaternion.Euler(0, myRotate, 0);
        moves[0] = 0;
        moves[1] = 0;
        if (myRotate == 0)
            moves[1] = normalSpeed;
        else if (myRotate > 0)
            moves[0] = normalSpeed;
        else if (myRotate < 0)
            moves[0] = -normalSpeed;
        if (value != 0)
            canSwitch = DateTime.Now.AddMilliseconds(500);
    }

    public void NewSace(float value)
    {
        ObjectSacale.transform.localScale = new (
            ObjectSacale.transform.localScale.x + value, 
            ObjectSacale.transform.localScale.y + value, 
            ObjectSacale.transform.localScale.z + value);
        MyCollider.size = new(MyCollider.size.x, MyCollider.size.y + (value * 1.35f), MyCollider.size.z);
    }

    public void EndGame()
    {
        ObjectSacale.transform.localScale = new(1f, 1f, 1f);
        MyCollider.size = new Vector3(0.5f, 1.3f, 0.5f);
        oldFixPosition = 1;
    }

    public void SwitchPosition(int fix)
    {
        if (canSwitch > DateTime.Now) return;
        if (oldFixPosition != fix) oldFixPosition = fix;
        else return;
        if (myRotate == 0)
            transform.position = new (rb.position.x + (1.5f * fix), transform.position.y, transform.position.z);
        else if (myRotate > 0)
            transform.position = new (rb.position.x, transform.position.y, transform.position.z - (1.5f * fix));
        else if (myRotate < 0)
            transform.position = new (rb.position.x, transform.position.y, transform.position.z + (1.5f * fix));
    }
}
