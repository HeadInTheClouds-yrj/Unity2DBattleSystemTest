using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float moveSpeed = 10;

    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetKey_W += GetKey_W;
        EventManager.Instance.InputEvent.OnGetKey_S += GetKey_S;
        EventManager.Instance.InputEvent.OnGetKey_A += GetKey_A;
        EventManager.Instance.InputEvent.OnGetKey_D += GetKey_D;
    }
    private void OnDisable()
    {
        EventManager.Instance.InputEvent.OnGetKey_W -= GetKey_W;
        EventManager.Instance.InputEvent.OnGetKey_S -= GetKey_S;
        EventManager.Instance.InputEvent.OnGetKey_A -= GetKey_A;
        EventManager.Instance.InputEvent.OnGetKey_D -= GetKey_D;
    }

    private void GetKey_W()
    {
        target = transform.position;
        target.y += 1;
        transform.position = Vector3.MoveTowards(transform.position,target,Time.deltaTime * moveSpeed);
    }

    private void GetKey_S()
    {
        target = transform.position;
        target.y -= 1;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
    }

    private void GetKey_A()
    {
        target = transform.position;
        target.x -= 1;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
    }

    private void GetKey_D()
    {
        target = transform.position;
        target.x += 1;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        EventManager.Instance.InputEvent.GetKey_W();
        EventManager.Instance.InputEvent.GetKey_S();
        EventManager.Instance.InputEvent.GetKey_A();
        EventManager.Instance.InputEvent.GetKey_D();
        EventManager.Instance.InputEvent.GetLeftMouse();
        EventManager.Instance.InputEvent.GetLeftMouseUp();
        EventManager.Instance.InputEvent.GetLeftMouseDown(this.transform);
        EventManager.Instance.InputEvent.GetRightMouseDown(this.transform);
    }
}
