using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BreathingExercise : MonoBehaviour
{

    public Text text;
    public GameObject breathingCircle;
    public Button resetButton;
    public Button homeButton;
    public List<string> introPhrases;
    public List<string> mindfulPhrases;
    public float exerciseDuration;
    private Animator textAnim;
    private Animator breathingCircleAnim;
    private Animator resetButtonAnim;
    private Animator homeButtonAnim;
    private bool timerBegin = false;
    private float timeLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = exerciseDuration;
        textAnim = text.GetComponent<Animator>();
        breathingCircleAnim = breathingCircle.GetComponent<Animator>();
        resetButtonAnim = resetButton.GetComponent<Animator>();
        homeButtonAnim = homeButton.GetComponent<Animator>();
        text.text = "";
        textAnim.Play("TextFadeOut");
        resetButtonAnim.Play("buttonFadeOut");
        homeButtonAnim.Play("buttonFadeOut");
        breathingCircleAnim.Play("breathingCircleFadeOut");
        StartCoroutine(TextProgression());
    }
    
    void Update()
    {
        if(timerBegin)
        {
            timeLeft -= Time.deltaTime;
        }
    }

    public IEnumerator TextProgression() 
    {
        foreach (string phrase in introPhrases)
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
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(BreatheTutorial());
    }

    public IEnumerator BreatheTutorial()
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
        yield return new WaitForSeconds(2);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Continue to breathe with the circle until the exercise is over.";
        textAnim.Play("textFadeIn");
        yield return new WaitForSeconds(2);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        StartCoroutine(BreatheExercise());
        StartCoroutine(PlayMindfulPhrases());
    }

    public IEnumerator BreatheExercise()
    {
        timerBegin = true;
        while(timeLeft > 0)
        {
            breathingCircleAnim.Play("breathingCircleGrow");
            yield return new WaitForSeconds(7);
            breathingCircleAnim.Play("breathingCircleShrink");
            
            Debug.Log("[BreatheExercise] Time left: " + exerciseDuration);
        }
        Debug.Log("Outside of while loop");
        timerBegin = false;
        breathingCircleAnim.Play("breathingCircleFadeOut");
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Great job completing the breathing exercise.";
        textAnim.Play("textFadeIn");
        yield return new WaitForSeconds(5);
        textAnim.Play("textFadeOut");
        yield return new WaitForSeconds(1);
        text.text = "Choose to restart the exercise again or return to home.";
        textAnim.Play("textFadeIn");
        resetButtonAnim.Play("buttonFadeIn");
        homeButtonAnim.Play("buttonFadeIn");
    }

    public IEnumerator PlayMindfulPhrases()
    {
        while(timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            text.text = mindfulPhrases[UnityEngine.Random.Range(0, mindfulPhrases.Count)];
            textAnim.Play("textFadeIn");
            yield return new WaitForSeconds(12);
            textAnim.Play("textFadeOut");
            yield return new WaitForSeconds(11);
            Debug.Log("[MindfulPhrases] Time left: " + exerciseDuration);
        }
    }

    public void restartExercise()
    {
        timeLeft = exerciseDuration;
        resetButtonAnim.Play("buttonFadeOut");
        homeButtonAnim.Play("buttonFadeOut");
        textAnim.Play("textFadeOut");
        breathingCircleAnim.Play("breathingCircleFadeIn");
        StartCoroutine(BreatheExercise());
        StartCoroutine(PlayMindfulPhrases());
    }

    public void returnToHome()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
