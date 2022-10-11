using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public Animator ftbAnim;
    public MeshRenderer mRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        ftbAnim = GetComponent<Animator>();
        mRenderer = GetComponent<MeshRenderer>();
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public IEnumerator FadeInCoroutine()
    {
        ftbAnim.Play("FTBFadeIn");
        yield return new WaitForSeconds(2);
        mRenderer.enabled = false;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator FadeOutCoroutine()
    {
        mRenderer.enabled = true;
        ftbAnim.Play("FTBFadeOut");
        yield return new WaitForSeconds(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
