using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MagicRayVFX : MonoBehaviour
{
    private VisualEffect visualEffect;
    // Start is called before the first frame update
    [SerializeField] private float redius = 2f;
    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetRightMouseDown += Shoot;
    }
    private void OnDisable()
    {
        EventManager.Instance.InputEvent.OnGetRightMouseDown -= Shoot;
    }
    void Start()
    {
        visualEffect= GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Shoot(Transform transform)
    {
        
        Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = end - (Vector2)transform.position;
        Vector3 endPosition = GetStartPosition(direction,transform.position,end, redius);
        Debug.Log(endPosition);
        visualEffect.SetVector2("AttackDirection", direction.normalized);
        visualEffect.SetVector3("StartShootPosition", endPosition);
        visualEffect.Play();
    }
    private Vector3 GetStartPosition(Vector2 dir,Vector2 owner, Vector2 endPosition, float redius = 2f)
    {
        float b, k,x = owner.x,y = owner.y,r = redius ,m,n,p;
        Vector2 target = dir.normalized;
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
        m = k * k + 1;
        n = 2*(k * b - k * y - x);
        p = (b - y) * (b - y) - r * r + x * x;
        
        //Multiply the final direction position
        if (endPosition.y>owner.y&&endPosition.x > owner.x)
        {
            target.x = (-n - Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = -Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;

            Debug.Log(target.y);
            target.y = k * target.x + b;
            Debug.Log(target.y);
        }
        else if (endPosition.y<owner.y && endPosition.x < owner.x)
        {
            target.x = (-n + Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
            Debug.Log(target.y);
            target.y = k * target.x + b;
            Debug.Log(target.y);
        }
        else if (endPosition.y>owner.y&&endPosition.x<owner.x)
        {
            target.x = (-n + Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = -Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
        }
        else
        {
            target.x = (-n - Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
        }
        return target;
    }
}