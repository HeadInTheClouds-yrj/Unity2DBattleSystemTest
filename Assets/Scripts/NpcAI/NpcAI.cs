using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.VFX;
using static Cyan.Blit;
using static UnityEngine.GraphicsBuffer;

public class NpcAI : MonoBehaviour
{
    [SerializeField] private GameObject flySword;
    private Transform player;
    private Vector3 finalTargetPosition;
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
    [Header("�Լ�")]
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
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log(player == null);
            Debug.Log(1111);
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Shoot();

        }

    }
    private void Shoot()
    {
        if (temp)
        {
            magicRayVFX.Shoot(transform,player.position);
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
                NpcRadiuMove(0f, radius,player.position,direction, spriteRenderer);
                if (tiemfloat > 1.75f && !flag2)
                {
                    end = player.position;
                    Debug.DrawLine(transform.position, end, _material_LineRender.GetColor("_Color"), 0.2f, false);
                    flag2 = !flag2;
                }
            }
            else
            {
                NpcRadiuMove(moveSpeed,radius, player.position,direction, spriteRenderer);
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
    /**
     * ժҪ:��Ҫ����transformΧ��һ��Ŀ����ת
     * ����:
     * moveSpeed:�ƶ��ٶ�
     * originTarget:ԭʼĿ��
     * direction:�����Ƿ�ı�,true:˳ʱ��.false:��ʱ��;
     * spriteRenderer:��ʾ���
     */
    private void NpcRadiuMove(float moveSpeed,float radius,Vector3 originTarget,bool direction, SpriteRenderer spriteRenderer)
    {
        float edge = Mathf.Sqrt(2) * radius * 0.5f;
        Vector3 finalTargetPosition = Vector3.zero;
        //npc �� player �İ뾶֮��
        if (transform.position.x > radius + originTarget.x || transform.position.x < -radius + originTarget.x)
        {
            if (transform.position.x < originTarget.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, originTarget, Time.deltaTime * moveSpeed);
        }
        // npc �� player ��(x������45��+x������45��)
        else if (Mathf.Abs(transform.position.y - originTarget.y) < edge && transform.position.x < originTarget.x)
        {
            finalTargetPosition.y = transform.position.y;
            finalTargetPosition.x = -Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((finalTargetPosition.y - originTarget.y), 2)) + originTarget.x;
            if (direction)
            {
                finalTargetPosition.y -= Time.deltaTime * moveSpeed;
            }
            else
            {
                finalTargetPosition.y += Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < originTarget.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, Time.deltaTime * moveSpeed);
        }
        // npc �� player �ҷ�
        else if (Mathf.Abs(transform.position.y - originTarget.y) < edge && transform.position.x > originTarget.x)
        {
            finalTargetPosition.y = transform.position.y;
            finalTargetPosition.x = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((finalTargetPosition.y - originTarget.y), 2)) + originTarget.x;
            if (direction)
            {
                finalTargetPosition.y += Time.deltaTime * moveSpeed;
            }
            else
            {
                finalTargetPosition.y -= Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < originTarget.x)
            {
                spriteRenderer.flipX = false;
                
            }
            else
            {
                spriteRenderer.flipX = true;
                
            }
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, Time.deltaTime * moveSpeed);
        }
        // npc �� player �Ϸ�
        else if (Mathf.Abs(transform.position.y - originTarget.y) > edge && transform.position.y > originTarget.y)
        {
            finalTargetPosition.x = transform.position.x;
            finalTargetPosition.y = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((finalTargetPosition.x - originTarget.x), 2)) + originTarget.y;
            if (direction)
            {
                finalTargetPosition.x -= Time.deltaTime * moveSpeed;
            }
            else
            {
                finalTargetPosition.x += Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < originTarget.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, Time.deltaTime * moveSpeed);
        }
        // npc �� player �·�
        else if (Mathf.Abs(transform.position.y - originTarget.y) > edge && transform.position.y < originTarget.y)
        {
            finalTargetPosition.x = transform.position.x;
            finalTargetPosition.y = -Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((finalTargetPosition.x - originTarget.x), 2)) + originTarget.y;
            if (direction)
            {
                finalTargetPosition.x += Time.deltaTime * moveSpeed;
            }
            else
            {
                finalTargetPosition.x -= Time.deltaTime * moveSpeed;
            }
            if (transform.position.x < originTarget.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, finalTargetPosition, Time.deltaTime * moveSpeed);
        }
    }
}
