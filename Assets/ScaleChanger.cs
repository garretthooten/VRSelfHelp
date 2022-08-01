using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{

    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] bool repeat;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float duration = 3.0f;

    IEnumerator Start()
    {
        minScale = transform.localScale;
        while (repeat)
        {
            yield return RepeatLerp(minScale, maxScale, duration);
            yield return RepeatLerp(maxScale, minScale, duration);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

}
