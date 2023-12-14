using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lightning : MonoBehaviour
{
    [SerializeField] private VisualEffect effect;
    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetRightMouseDown += PlayVFXEffect_Onetwice;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void PlayVFXEffect_Onetwice(Transform parent)
    {
        
        transform.position = new Vector3(parent.position.x,parent.position.y+4,parent.position.z);
        effect.SendEvent("OnPlayLightning");
        
    }
}
