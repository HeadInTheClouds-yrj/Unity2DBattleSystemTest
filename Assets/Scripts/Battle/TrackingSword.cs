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
    [SerializeField] private LayerMask npcLayerMask;
    private float lastHitTime = 0;
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
        Collider2D collider2d = Physics2D.OverlapCircle(transform.position,.1f,npcLayerMask);
        if (lastHitTime<4f)
        {
            lastHitTime += Time.deltaTime;
        }
        if (collider2d != null)
        {
            if (collider2d.transform.TryGetComponent(out NpcAI npc)&& lastHitTime >2f)
            {
                Debug.Log(npc.name);
                npc.ReduceBlood(Random.Range(1, 101));
                lastHitTime = 0;
            }
        }
    }
}
