using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowDamageText : MonoBehaviour
{
    [SerializeField] public GameObject throwDamageTextPrefeb;
    public static ThrowDamageText instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ThrowReduceTextFactory(Transform parent, float value, float fontsize = 3f,float velocity =1f, float time = 1f)
    {
        if (parent != null)
        {
            GameObject obj = Instantiate(throwDamageTextPrefeb, parent);
            TMP_Text tmpTransform = obj.GetComponent<TMP_Text>();
            tmpTransform.fontSize = fontsize;
            tmpTransform.text = value.ToString();
            StartCoroutine(ThrowText(obj, tmpTransform, velocity, time));
        }

    }
    IEnumerator ThrowText(GameObject gameObject, TMP_Text tmpTransform, float velocity = 1f, float time = 1f)
    {
        float tmptime = 0f;
        while (tmptime<time*0.5)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,new Vector3(gameObject.transform.localPosition.x,0.3f, gameObject.transform.localPosition.z), Time.deltaTime * 1f);
            tmptime += Time.deltaTime;
            yield return null;
        }
        while (tmptime > time * 0.5&&tmptime<time)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, new Vector3(gameObject.transform.localPosition.x, 0.1f, gameObject.transform.localPosition.z), Time.deltaTime * 1f);
            tmptime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
