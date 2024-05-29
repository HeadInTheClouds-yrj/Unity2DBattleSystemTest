using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class MagicRay : MonoBehaviour
{
    public static MagicRay instance;
    [SerializeField] private Transform startParticle;
    [SerializeField] private Transform endParticle;     //mousebutton
    [SerializeField] private ParticleSystem _particleSystemStart;
    [SerializeField] private ParticleSystem _particleSystemEnd;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _material_LineRender;
    [SerializeField] private Transform start; // player
    [SerializeField] private float lineRendererWidthDefault = 0.4f;
    private Vector3 end;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetLeftMouse += MagicRayPreWarm;
        EventManager.Instance.InputEvent.OnGetLeftMouseUp += FireMagicRay;
    }
    private void OnDisable()
    {
        EventManager.Instance.InputEvent.OnGetLeftMouse -= MagicRayPreWarm;
        EventManager.Instance.InputEvent.OnGetLeftMouseUp -= FireMagicRay;
    }

    private void MagicRayPreWarm()
    {
        startParticle.position = start.position;
        Vector3.MoveTowards(startParticle.position,start.position,Time.deltaTime *100);
        _lineRenderer.SetPosition(0, start.position);
        _lineRenderer.SetPosition(1, start.position);
        _material_LineRender.SetFloat("_Alpha",1);
        if (!_particleSystemStart.isPlaying)
        {
            _particleSystemStart.Play();
        }
    }

    private void FireMagicRay()
    {
        _lineRenderer.widthMultiplier = lineRendererWidthDefault;
        _material_LineRender.SetFloat("_AlphaCenter", 0.01f);
        _material_LineRender.SetFloat("_Alpha", 1f);
        end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lineRenderer.SetPosition(0,start.position);
        _lineRenderer.SetPosition(1,GetTarget(start.position,end));
        RaycastHit2D[] hit = Physics2D.RaycastAll(start.position,(Vector2)(end - start.position).normalized,(end - start.position).magnitude*10);
        if (hit.Length>0)
        {
            foreach (var item in hit)
            {
                //Debug.Log(item.transform.name);
            }
            //StartCoroutine(EndParticlePlay());
        }
        _particleSystemStart.Stop();
        StartCoroutine(RayDestroy());

    }
    public Vector3 GetTarget(Vector3 startPositon, Vector3 endPosition,float multiply = 5000)
    {
        float b, k;
        Vector3 target = endPosition - startPositon;
        //The player is the zero point of the coordinate system.
        //The relative position of the mouse click is in the first or third quadrant, close to the y coordinate axis.
        if (target.y / target.x > float.MaxValue)
        {
            k = 999f;
        }
        //The player is the zero point of the coordinate system.
        //The relative position of the mouse click is in the second or fourth quadrant, close to the y coordinate axis.
        else if (target.y / target.x < float.MinValue)
        {
            k = -999f;
        }
        else
        {
            k = target.y / target.x;
        }
        //b= y - k*x
        if (endPosition.y - endPosition.x * k > float.MaxValue)
        {
            b = float.MaxValue;
        }
        else if (endPosition.y - endPosition.x * k < float.MinValue)
        {
            b = float.MinValue;
        }
        else
        {
            b = endPosition.y - endPosition.x * k;
        }

        //Multiply the final direction position
        target.x *= multiply;

        if (k*target.x + b>float.MaxValue)
        {
            target.y = float.MaxValue;
        }
        else if (k*target.x + b<float.MinValue)
        {
            target.y = float.MinValue;
        }
        else
        {
            target.y = k * target.x +b;
        }
        
        return target;
    }
    IEnumerator EndParticlePlay()
    {
        _particleSystemEnd.Play();
        float a = 0.2f;
        while (a>0)
        {
            a-=Time.deltaTime;
            yield return null;
        }
        _particleSystemEnd.Stop();
    }
    IEnumerator RayDestroy()
    {
        float a = 0.01f; //center
        float w = _lineRenderer.widthMultiplier;
        while (w>0.01f)
        {
            a+= Time.deltaTime*5;
            w -= Time.deltaTime;
            _lineRenderer.widthMultiplier = w;
            _material_LineRender.SetFloat("_AlphaCenter", a);
            yield return null;
        }
        _material_LineRender.SetFloat("_Alpha",0);
    }
    IEnumerator RayDestroy(Material _material_LineRender, LineRenderer _lineRenderer)
    {
        float a = 0.01f; //center
        float w = _lineRenderer.widthMultiplier;
        while (w > 0.01f)
        {
            a += Time.deltaTime * 8;
            w -= Time.deltaTime*2;
            _lineRenderer.widthMultiplier = w;
            _material_LineRender.SetFloat("_AlphaCenter", a);
            yield return null;
        }
        _material_LineRender.SetFloat("_Alpha", 0);
    }
    IEnumerator EndParticlePlay(ParticleSystem _particleSystemEnd,bool flip,float angle,Vector3 start,Vector3 end)
    {
        if (start.y > end.y)
        {
            _particleSystemEnd.transform.localRotation = Quaternion.Euler(new Vector3(-angle, 90, 0));
        }
        else
        {
            _particleSystemEnd.transform.localRotation = Quaternion.Euler(new Vector3(angle, 90, 0));
        }
        _particleSystemEnd.Play();
        float a = 0.2f;
        while (a > 0)
        {
            a -= Time.deltaTime;
            yield return null;
        }
        _particleSystemEnd.Stop();
    }
    IEnumerator PullLingQi(ParticleSystem _particleSystemStart)
    {
        float randomizePosition = _particleSystemStart.shape.randomPositionAmount;
        ParticleSystem.ShapeModule shape = _particleSystemStart.shape;
        while (randomizePosition >0)
        {
            randomizePosition-= Time.deltaTime*10*2;
            shape.randomPositionAmount = randomizePosition;
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayVFXPower(VisualEffect startParticle)
    {
        startParticle.Play();
    }
    public void NPCMagicRayPreWarm(VisualEffect startParticle,Transform start,LineRenderer _lineRenderer,Material _material_LineRender,ParticleSystem _particleSystemStart)
    {
        //startParticle.position = start.position;
        //Vector3.MoveTowards(startParticle.position, start.position, Time.deltaTime * 100);
        _lineRenderer.SetPosition(0, start.position);
        _lineRenderer.SetPosition(1, start.position);
        _material_LineRender.SetFloat("_Alpha", 1);
        //if (!_particleSystemStart.isPlaying)
        //{
        //    _particleSystemStart.Play();
        //    StartCoroutine(PullLingQi(_particleSystemStart));
        //}
    }

    public void NPCFireMagicRay(Transform endParticle,LineRenderer _lineRenderer, Material _material_LineRender,Vector3 end,Transform start,Transform player, ParticleSystem _particleSystemStart, ParticleSystem _particleSystemEnd,bool flip)
    {
        _lineRenderer.widthMultiplier = 0.5f;
        _material_LineRender.SetFloat("_AlphaCenter", 0.01f);
        _material_LineRender.SetFloat("_Alpha", 1f);
        _lineRenderer.SetPosition(0, start.position);
        _lineRenderer.SetPosition(1, GetTarget(start.position, end));
        RaycastHit2D[] hit = Physics2D.RaycastAll(start.position, (Vector2)(end - start.position).normalized, (end - start.position).magnitude * 10);
        if (hit.Length > 0)
        {
            foreach (var item in hit)
            {
                if (item.transform.TryGetComponent<NpcAI>(out NpcAI npcAI))
                {

                }
                else
                {
                    endParticle.position = item.transform.position;
                    Vector3 self_target = item.transform.position - start.position;
                    Vector3 horizon = (new Vector3(item.transform.position.x - 1, item.transform.position.y, item.transform.position.z) - item.transform.position).normalized;
                    float angle = Mathf.Acos(Vector3.Dot(self_target, horizon) / self_target.magnitude * horizon.magnitude) * Mathf.Rad2Deg;
                    StartCoroutine(EndParticlePlay(_particleSystemEnd, flip, angle, start.position, item.transform.position));
                }
            }
        }
        ParticleSystem.ShapeModule shape = _particleSystemStart.shape;
        _particleSystemStart.Stop();
        shape.randomPositionAmount = 10f;
        StartCoroutine(RayDestroy(_material_LineRender,_lineRenderer));
    }
}
