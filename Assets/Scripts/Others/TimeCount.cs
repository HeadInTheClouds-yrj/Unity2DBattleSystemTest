using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCount : MonoBehaviour
{
    public static TimeCount instance;
    private bool isFinish = false;
    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsFinished()
    {
        return isFinish;
    }
    public void SetCountTime(float time)
    {
        StartCoroutine(DeltaTimeCount(time));
    }
    IEnumerator DeltaTimeCount(float maxTime)
    {
        isFinish = false;
        while (maxTime>0)
        {
            maxTime-= Time.deltaTime;
            yield return null;
        }
        isFinish = true;
        Debug.Log("IE");
    }
}
