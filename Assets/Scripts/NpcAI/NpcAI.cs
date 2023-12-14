using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NpcAI : MonoBehaviour
{
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
    [SerializeField] private Transform startParticle;
    [SerializeField] private Transform endParticle;     //mousebutton
    [SerializeField] private ParticleSystem _particleSystemStart;
    [SerializeField] private ParticleSystem _particleSystemEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _material_LineRender;
    [Header("�Լ�")]
    [SerializeField] private Transform start; // 
    private Vector3 end;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (temp)
        {
            
            MagicRay.instance.NPCFireMagicRay(endParticle,_lineRenderer,_material_LineRender,end,start, player,_particleSystemStart,_particleSystemEnd, spriteRenderer.flipX);
            direction = Random.Range(0, 3)<1f ? true : false;
            temp = !temp;
            flag2 = !flag2;
        }
        if (!temp)
        {
            tiemfloat+=Time.deltaTime;
            if (tiemfloat > 1)
            {
                MagicRay.instance.NPCMagicRayPreWarm(startParticle, start, _lineRenderer, _material_LineRender, _particleSystemStart);
                NpcRadiuMove(moveSpeed*0.5f);
                if (tiemfloat > 1.75f && !flag2)
                {
                    end = player.position;
                    Debug.DrawLine(transform.position,end, _material_LineRender.GetColor("_Color"),0.25f,false);
                    flag2 = !flag2;
                }
            }
            else
            {
                NpcRadiuMove(moveSpeed);
            }
            if (tiemfloat>2)
            {
                temp = true;
                tiemfloat = 0;
            }
        }
        
    }
    private void NpcRadiuMove(float moveSpeed)
    {
        //npc �� player �İ뾶֮��
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
        // npc �� player ��
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
        // npc �� player �ҷ�
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
        // npc �� player �Ϸ�
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
        // npc �� player �·�
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
