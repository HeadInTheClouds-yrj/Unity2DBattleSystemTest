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
        Debug.Log("--");
    }
    private void Shoot(Transform transform)
    {
        
        Vector2 end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(end);
        Vector2 direction = end - (Vector2)transform.position;
        visualEffect.SetVector2("AttackDirection", direction.normalized);
        visualEffect.SetVector3("StartShootPosition",transform.position);
        visualEffect.Play();
    }
}
