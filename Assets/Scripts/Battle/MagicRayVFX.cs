using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MagicRayVFX : MonoBehaviour
{
    private string BEGING_TOTAL_TIME = "BegingTotalTime";
    private string ATTACK_DIRECTION = "AttackDirection";
    private string START_SHOOT_POSITION = "StartShootPosition";
    private string PER_POWERFULL_START_POSITION = "PerPowerfullStartPosition";
    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask mask;
    private VisualEffect visualEffect;
    // Start is called before the first frame update
    [SerializeField] 
    private float redius = 2f;
    private void OnEnable()
    {
        //EventManager.Instance.InputEvent.OnGetRightMouseDown += Shoot;
    }
    private void OnDisable()
    {
        //EventManager.Instance.InputEvent.OnGetRightMouseDown -= Shoot;
    }
    void Start()
    {
        visualEffect= GetComponentInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (counttime<.5f && tempflag == true)
        //{
        //    counttime += Time.deltaTime;
        //}
        //else if (tempHit && counttime>=.25f)
        //{
        //    RaycastHit2D[] all = Physics2D.RaycastAll(startPosition,endPosition1 - startPosition,100000f,LayerMask.GetMask("Npc"));
        //    foreach (RaycastHit2D item in all)
        //    {
        //        Debug.Log(item.transform.name);
        //    }
        //    tempHit = false;
        //}
        //else if(counttime>.5f&&tempflag == true)
        //{
        //    visualEffect.SetFloat("BegingTotalTime",9999999f);
        //    counttime= 0;
        //    tempflag = false;
        //}
    }
    float counttime = 0;
    bool tempflag = false;
    bool tempHit = false;
    Vector3 startPosition;
    Vector3 endPosition1;
    public void Shoot(Transform transform,Vector3 targetPosition)
    {
        //startPosition = transform.position;
        //Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //endPosition1 = end;
        //Vector2 direction = end - (Vector2)transform.position;
        //Vector3 endPosition = GetStartPosition(direction,transform.position,end, redius);
        //visualEffect.SetFloat("BegingTotalTime", 0f);
        //visualEffect.SetVector2("AttackDirection", direction.normalized);
        //visualEffect.SetVector3("StartShootPosition", endPosition);
        //visualEffect.Play();
        //tempHit= true;
        //tempflag = true;
        //counttime = 0;
        StartCoroutine(ShootRayCountTime(transform,visualEffect,targetPosition,mask));
    }
    IEnumerator ShootRayCountTime(Transform transform,VisualEffect visualEffect,Vector3 targetPosition,LayerMask mask)
    {
        float counttime = 0;
        bool raycastHitFlag = true;
        //Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        InitializePropety(transform, visualEffect, targetPosition);
        while (true)
        {
            counttime += Time.deltaTime;
            if (counttime>.23f && raycastHitFlag)
            {
                RaycastHit2D[] all = Physics2D.RaycastAll(transform.position, (Vector3)targetPosition - transform.position,((Vector3)targetPosition - transform.position).magnitude * 100f , mask);
                foreach (RaycastHit2D item in all)
                {
                    Debug.Log(item.transform.name);
                }
                raycastHitFlag = !raycastHitFlag;
            }
            if (counttime>.4f)
            {
                visualEffect.SetFloat("BegingTotalTime", 9999999f);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    private void InitializePropety(Transform transform, VisualEffect visualEffect, Vector3 targetPosition)
    {
        Vector2 end = targetPosition;
        Vector2 direction = end - (Vector2)transform.position;
        Vector3 endPosition = GetStartPosition(direction, transform.position, end, redius);
        visualEffect.SetFloat(BEGING_TOTAL_TIME, 0f);
        visualEffect.SetVector2(ATTACK_DIRECTION, direction.normalized);
        visualEffect.SetVector3(START_SHOOT_POSITION, endPosition);
        visualEffect.SetVector3(PER_POWERFULL_START_POSITION, new Vector3(transform.position.x - player.position.x, transform.position.y - player.position.y, 0));
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

            target.y = k * target.x + b;
        }
        else if (endPosition.y<owner.y && endPosition.x < owner.x)
        {
            target.x = (-n + Mathf.Sqrt(n * n - 4 * m * p)) / (2 * m);
            target.y = Mathf.Sqrt(Mathf.Pow(r, 2) - Mathf.Pow((target.x - x), 2)) + y;
            target.y = k * target.x + b;
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
