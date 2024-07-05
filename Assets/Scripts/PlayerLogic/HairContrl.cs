using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairContrl : MonoBehaviour
{
    private Transform[] hairs;
    private float mainHairRotation_Z;
    private void Awake()
    {
        hairs = GetComponentsInChildren<Transform>();
    }
    private void FixedUpdate()
    {
        
    }
}
