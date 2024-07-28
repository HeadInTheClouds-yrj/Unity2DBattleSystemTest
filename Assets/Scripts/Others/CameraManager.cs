using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : NetworkBehaviour
{
    [SerializeField] private Camera main_Camera;
    [SerializeField] private Transform player_default;
    [Range(0f,50f)]
    [SerializeField] private float cameraMoveSpeed = 25f;
    [Range(0f,20f)]
    [SerializeField] private float cameraHeight = 10f;
    private Vector3 temp;
    private Transform otherUnit;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {   
    }
    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }
    public void SetCameraFollow(Transform target, float cameraMoveSpeed = 25f, float cameraHeight = 10f)
    {
        otherUnit = target;
        this.cameraMoveSpeed = cameraMoveSpeed;
        this.cameraHeight = cameraHeight;
    }
    public void ResetCameraFollow()
    {
        otherUnit = null;
        cameraMoveSpeed = 25f;
        cameraHeight = 10f;
    }
    public Transform CurrentFollowTransform()
    {
        return target;
    }
    private void CameraFollow()
    {
        if (PlayerContrl.localPlayer != null)
        {
            SetCameraFollow(PlayerContrl.localPlayer.transform);

        }
        if (otherUnit == null || otherUnit.IsDestroyed())
        {
            return;
        }
        else
        {
            target = otherUnit;
        }

        temp = target.position;
        temp.z = -cameraHeight;
        main_Camera.transform.position = Vector3.Lerp(main_Camera.transform.position, temp,cameraMoveSpeed);
    }
}
