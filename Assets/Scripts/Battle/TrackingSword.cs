using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class TrackingSword : MonoBehaviour
{
    private Rigidbody2D flySword;
    private Vector2 direction;
    private Transform target;
    [SerializeField]
    private float attackCoolingTime = 4f;
    [SerializeField]private float flySpeed = 100f;
    [SerializeField]private float rotateSpeed = 500f;
    [SerializeField]private LayerMask npcLayerMask;
    private float lastHitTime = 0;
    private Transform owner;
    // Start is called before the first frame update
    void Start()
    {
        flySword = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (owner.IsDestroyed())
        {
            Destroy(gameObject);
        }
        else if (!target.IsDestroyed() && target != owner)
        {
            Vector2 randomTarget = new Vector2(target.position.x + Random.Range(-2.5f, 2.5f), target.position.y + Random.Range(-2.5f, 2.5f));
            direction = randomTarget - flySword.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, -transform.up).z;
            flySword.angularVelocity = -rotateAmount * rotateSpeed;
            flySword.velocity = -transform.up * flySpeed;
            Collider2D collider2d = Physics2D.OverlapCircle(transform.position, .1f, npcLayerMask);
            if (lastHitTime < attackCoolingTime)
            {
                lastHitTime += Time.deltaTime;
            }
            if (collider2d != null)
            {
                if (collider2d.transform.TryGetComponent(out NpcAI npc) && lastHitTime > 2f)
                {
                    Debug.Log(npc.name);
                    npc.ReduceBlood(Random.Range(1, 101));
                    lastHitTime = 0;
                }
            }
        }
        else
        {
            target = owner;
            direction = (Vector2)target.position - flySword.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, -transform.up).z;
            flySword.angularVelocity = -rotateAmount * rotateSpeed;
            flySword.velocity = -transform.up * flySpeed;
        }
    }
    public void InitializedSword(Transform owner ,Transform target, LayerMask npcLayerMask, float flySpeed=100f, float rotateSpeed=500f)
    {
        this.owner = owner;
        this.target = target;
        this.flySpeed = flySpeed;
        this.rotateSpeed = rotateSpeed;
        this.npcLayerMask = npcLayerMask;
    }
    public void SetNewTarget(Transform target)
    {
        this.target = target;
    }
}
