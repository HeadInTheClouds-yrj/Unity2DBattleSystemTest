using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class NpcAI : MonoBehaviour
{
    [SerializeField] private GameObject flySword;
    [SerializeField] private Transform player;
    private Vector3 target;
    [SerializeField] private float moveSpeed = 10f;
    private bool direction = false;
    [Range(0f, 30f)]
    [SerializeField] private float radius = 10f;
    private bool temp = false;
    private float tiemfloat = 0;
    private SpriteRenderer spriteRenderer;

    private bool flag2 = false;

    [Header("Debug")]
    [SerializeField] private VisualEffect startParticle;
    [SerializeField] private Transform endParticle;     //mousebutton
    [SerializeField] private ParticleSystem _particleSystemStart;
    [SerializeField] private ParticleSystem _particleSystemEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _material_LineRender;
    [Header("自己")]
    [SerializeField] private Transform start; // 
    private Vector3 end;
    private bool flag3 = true;
    [SerializeField]
    private MagicRayVFX magicRayVFX;
    //Y = sqrt(r'2 - (X-x)'2) + y
    //(y-tansform.position.y)'2 + (x-transform.position.x)'2 == r'2;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        NpcManager.Instance.AddNpc(this);
    }
    void Start()
    {
        //GameObject sword = Object.Instantiate<GameObject>(flySword, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        //if (sword.transform.TryGetComponent<TrackingSword>(out TrackingSword trackingSword))
        //{
        //    trackingSword.InitializedSword(transform,player, LayerMask.GetMask("Player"),100f,2000f);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();


    }
    private void Shoot()
    {
        if (temp)
        {
            magicRayVFX.Shoot(transform,player.transform.position);
            //MagicRay.instance.NPCFireMagicRay(endParticle, _lineRenderer, _material_LineRender, end, start, player, _particleSystemStart, _particleSystemEnd, spriteRenderer.flipX);
            direction = Random.Range(0, 3) < 1f ? true : false;
            temp = !temp;
            flag2 = !flag2;
            flag3 = !flag3;
        }
        if (!temp)
        {
            tiemfloat += Time.deltaTime;
            if (tiemfloat > 1)
            {
                if (tiemfloat > 1 && flag3)
                {
                    flag3 = !flag3;
                }
                NpcRadiuMove(0f);
                if (tiemfloat > 1.75f && !flag2)
                {
                    end = player.position;
                    Debug.DrawLine(transform.position, end, _material_LineRender.GetColor("_Color"), 0.2f, false);
                    flag2 = !flag2;
                }
            }
            else
            {
                NpcRadiuMove(moveSpeed);
            }
            if (tiemfloat > 2)
            {
                temp = true;
                tiemfloat = 0;
            }
        }
    }
    public void ReduceBlood(float value)
    {
        ThrowDamageText.instance.ThrowReduceTextFactory(transform, value,15f);
        if (value >=80)
        {
            Destroy(gameObject);
        }
    }
    private void ShootSwort()
    {

    }
    private void NpcRadiuMove(float moveSpeed)
    {
        //npc 在 player 的半径之外
        if (transform.position.x > radius + player.position.x || transform.position.x < -radius + player.position.x)
        {
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }
        // npc 在 player 左方
        else if (Mathf.Abs(transform.position.y - player.position.y) < Mathf.Sqrt(2) * radius * 0.5f && transform.position.x < player.position.x)
        {
            target.y = transform.position.y;
            target.x = -Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((target.y - player.position.y), 2)) + player.position.x;
            if (direction)
            {
                target.y -= Time.deltaTime * moveSpeed;
            }
            else
            {
                target.y += Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
        // npc 在 player 右方
        else if (Mathf.Abs(transform.position.y - player.position.y) < Mathf.Sqrt(2) * radius * 0.5f && transform.position.x > player.position.x)
        {
            target.y = transform.position.y;
            target.x = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((target.y - player.position.y), 2)) + player.position.x;
            if (direction)
            {
                target.y += Time.deltaTime * moveSpeed;
            }
            else
            {
                target.y -= Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
                
            }
            else
            {
                spriteRenderer.flipX = true;
                
            }
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
        // npc 在 player 上方
        else if (Mathf.Abs(transform.position.y - player.position.y) > Mathf.Sqrt(2) * radius * 0.5f && transform.position.y > player.position.y)
        {
            target.x = transform.position.x;
            target.y = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((target.x - player.position.x), 2)) + player.position.y;
            if (direction)
            {
                target.x -= Time.deltaTime * moveSpeed;
            }
            else
            {
                target.x += Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
        // npc 在 player 下方
        else if (Mathf.Abs(transform.position.y - player.position.y) > Mathf.Sqrt(2) * radius * 0.5f && transform.position.y < player.position.y)
        {
            target.x = transform.position.x;
            target.y = -Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((target.x - player.position.x), 2)) + player.position.y;
            if (direction)
            {
                target.x += Time.deltaTime * moveSpeed;
            }
            else
            {
                target.x -= Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        }
    }
}
