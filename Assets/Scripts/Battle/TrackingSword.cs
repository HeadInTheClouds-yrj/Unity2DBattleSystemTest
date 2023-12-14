using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class TrackingSword : MonoBehaviour
{
    private Rigidbody2D flySword;
    private Vector2 direction;
    private Transform target;
    [SerializeField]private float flySpeed = 40f;
    [SerializeField]private float rotateSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        flySword = GetComponent<Rigidbody2D>();
        foreach (NpcAI item in NpcManager.Instance.GetNpcs())
        {
            if (target == null)
            {
                target = item.transform;
            }
            if ((target.position - transform.position).magnitude>(item.transform.position - transform.position).magnitude)
            {
                target = item.transform;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = (Vector2)target.position - flySword.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, -transform.up).z;
        flySword.angularVelocity = -rotateAmount * rotateSpeed;
        flySword.velocity = -transform.up * flySpeed;
    }
}
