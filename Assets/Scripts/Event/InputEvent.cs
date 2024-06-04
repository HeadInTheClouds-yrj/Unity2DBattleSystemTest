using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    public event Action OnGetKey_W;
    public void GetKey_W()
    {
        if (OnGetKey_W != null&&Input.GetKey(KeyCode.W))
        {
            OnGetKey_W();
        }
    }
    public event Action OnGetKey_S;
    public void GetKey_S()
    {
        if (OnGetKey_S != null && Input.GetKey(KeyCode.S))
        {
            OnGetKey_S();
        }
    }
    public event Action OnGetKey_A;
    public void GetKey_A()
    {
        if (OnGetKey_A != null && Input.GetKey(KeyCode.A))
        {
            OnGetKey_A();
        }
    }
    public event Action OnGetKey_D;
    public void GetKey_D()
    {
        if (OnGetKey_D != null && Input.GetKey(KeyCode.D))
        {
            OnGetKey_D();
        }
    }
    public event Action OnGetLeftMouse;
    public void GetLeftMouse()
    {
        if (OnGetLeftMouse != null && Input.GetMouseButton(0))
        {
            OnGetLeftMouse();
        }
    }
    public event Action<Transform> OnGetLeftMouseDown;
    public void GetLeftMouseDown(Transform parent) 
    {
        if (OnGetLeftMouseDown != null && Input.GetMouseButtonDown(0))
        {
            OnGetLeftMouseDown(parent);
        }
    }
    public event Action OnGetLeftMouseUp;
    public void GetLeftMouseUp()
    {
        if (OnGetLeftMouseUp != null && Input.GetMouseButtonUp(0))
        {
            OnGetLeftMouseUp();
        }
    }
    public event Action<Transform> OnGetRightMouseDown;
    public void GetRightMouseDown(Transform parent)
    {
        if (OnGetRightMouseDown != null && Input.GetMouseButtonDown(2))
        {
            OnGetRightMouseDown(parent);
        }
    }
}
