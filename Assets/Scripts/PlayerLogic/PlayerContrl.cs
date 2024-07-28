using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerContrl : NetworkBehaviour
{
    public static PlayerContrl localPlayer;
    private Vector3 target;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private Animator animator;
    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetKey_W += GetKey_W;
        EventManager.Instance.InputEvent.OnGetKey_S += GetKey_S;
        EventManager.Instance.InputEvent.OnGetKey_D += GetKey_D;
        EventManager.Instance.InputEvent.OnGetKey_A += GetKey_A;

    }
    private void OnDisable()
    {
        EventManager.Instance.InputEvent.OnGetKey_W -= GetKey_W;
        EventManager.Instance.InputEvent.OnGetKey_S -= GetKey_S;
        EventManager.Instance.InputEvent.OnGetKey_D -= GetKey_D;
        EventManager.Instance.InputEvent.OnGetKey_A -= GetKey_A;
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
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            localPlayer = this;
            Debug.Log(localPlayer.OwnerClientId);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        test();


        EventManager.Instance.InputEvent.GetLeftMouse();
        EventManager.Instance.InputEvent.GetLeftMouseUp();
        EventManager.Instance.InputEvent.GetLeftMouseDown(this.transform);
        EventManager.Instance.InputEvent.GetRightMouseDown(this.transform);

    }
    private void test()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetKey_W();
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetKey_S();
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetKey_A();
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetKey_D();
        }
    }
}
