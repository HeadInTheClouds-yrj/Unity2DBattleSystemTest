using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MagicRayVFX : MonoBehaviour
{
    private VisualEffect visualEffect;
    // Start is called before the first frame update
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
        Vector3 endPosition = GetStartPosition(direction,transform.position,end);
        Debug.Log(endPosition);
        visualEffect.SetVector2("AttackDirection", direction.normalized);
        visualEffect.SetVector3("StartShootPosition", endPosition);
        visualEffect.Play();
    }
    private Vector3 GetStartPosition(Vector2 dir,Vector2 owner, Vector2 endPosition, float redius = 2f)
    {
        float b, k,x = owner.x,y = owner.y,r = redius ,n,g;
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
        Debug.Log("k=="+k+"\n" + "b=="+b);
        
        n = r * r - x * x - (b-y) * (b-y);
        g = (k * (b - y) - x)/Mathf.Sqrt(k*k + 1);
        target.x = (Mathf.Sqrt(Mathf.Abs(n + g))-g)/Mathf.Sqrt(k*k + 1);
        Debug.Log("n=="+n+"\n"+"g=="+g);
        //Multiply the final direction position
        target.y =Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
        return target;
    }
}
