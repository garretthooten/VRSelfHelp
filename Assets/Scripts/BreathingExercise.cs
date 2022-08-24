using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingExercise : MonoBehaviour
{

    public Text text;
    public GameObject breathingCircle;
    private Animator textAnim;
    private Animator breathingCircleAnim;
    [SerializeField] private List<string> phrases;
    
    // Start is called before the first frame update
    void Start()
    {
        textAnim = text.GetComponent<Animator>();
        breathingCircleAnim = breathingCircle.GetComponent<Animator>();
        StartCoroutine(TextProgression());
    }

    public IEnumerator TextProgression() 
    {
        //  safety to make sure that text is gone at start
        textAnim.Play("TextFadeOut");
        breathingCircleAnim.Play("breathingCircleFadeOut");
        foreach (string phrase in phrases)
        {
            textAnim.Play("textFadeOut");
            yield return new WaitForSeconds(3);
            text.text = phrase;
            textAnim.Play("textFadeIn");
            if (phrase.Contains("circle"))
            {
                breathingCircleAnim.Play("breathingCircleFadeIn");
            }
            yield return new WaitForSeconds(6);
        }
        textAnim.Play("textFadeOut");
        yield return StartCoroutine(Breathe());
    }

    public IEnumerator Breathe()
    {
        text.text = "Breathe in...";
        textAnim.Play("textFadeIn");
        breathingCircleAnim.Play("breathingCircleGrow");
        yield return new WaitForSeconds(4);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Hold...";
        textAnim.Play("textFadeIn");
        yield return new WaitForSeconds(2);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Release...";
        textAnim.Play("textFadeIn");
        breathingCircleAnim.Play("breathingCircleShrink");
        yield return new WaitForSeconds(4);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Great!";
        textAnim.Play("textFadeIn");

    }
}
