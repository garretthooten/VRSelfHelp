using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{

    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] bool repeat;
    [SerializeField] float duration = 4.0f;
    [SerializeField] int holdBreathDuration = 4;

    public bool fadedIn = false;
    [SerializeField] float fadeDuration = 1.0f;
    private Color alphaColor;

    IEnumerator Start()
    {
        alphaColor = GetComponent<MeshRenderer>().material.color;
        minScale = transform.localScale;
        while (repeat)
        {
            yield return RepeatLerp(maxScale, minScale, duration);
            yield return new WaitForSeconds(holdBreathDuration);
            yield return RepeatLerp(minScale, maxScale, duration);
            yield return new WaitForSeconds(holdBreathDuration);
        }
    }

    public void Fade() 
    {
        Debug.Log("[Before] Alpha is: " + alphaColor.a);
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, fadeDuration * Time.deltaTime);
        StartCoroutine(FadeIn());
        Debug.Log("[After] Alpha is: " + alphaColor.a);
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = this.GetComponent<MeshRenderer>().material.color;
            c.a = f;
            this.GetComponent<MeshRenderer>().material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time)/* * speed*/;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }


}
