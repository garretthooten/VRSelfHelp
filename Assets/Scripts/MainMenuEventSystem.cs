using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuEventSystem : MonoBehaviour
{
    
    //  making do for lack of map in c# and no dictionary support in unity inspector
    //  make sure to match page and page names properly!
    //  same for quotes and authors
    public List<GameObject> pages;
    public List<string> pageNames;
    public TextAsset quoteData;
    public List<string> quotes;
    public List<string> quoteAuthors;

    //  main menu text preview objects
    public Text quoteText;
    public Text goalPreview0;
    public Text goalPreview1;

    public GameObject emotionContinueButton;

    //  in inspector, set currentPage to desired default page
    public GameObject currentPage;
    private int selectedEmotions = 0;
    public Text selectEmotionsText;

    [SerializeField] private GoalHandler goalHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        loadQuotes();
        if(goalHandler.goals.Count >= 1)
        {
            goalPreview0.text = goalHandler.goals[0].getGoalText();
        }
        else
        {
            goalPreview0.text = "";
        }
        if(goalHandler.goals.Count >= 2)
        {
            goalPreview1.text = goalHandler.goals[1].getGoalText();
        }
        else
        {
            goalPreview1.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPage(string pageName)
    {
        int index = pageNames.IndexOf(pageName);
        if(index != -1)
        {
            Debug.Log("Switching to page " + pageName);
            currentPage.SetActive(false);
            pages[index].SetActive(true);
            currentPage = pages[index];
        }
        else
        {
            Debug.LogWarning("Not able to find page " + pageName);
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void incrementEmotions(GameObject button)
    {
        selectedEmotions++;
        selectEmotionsText.text = "Select at least " + ( (3-selectedEmotions) > 0 ? (3-selectedEmotions) : 0 ) + " emotions";
        button.SetActive(false);
        if(selectedEmotions >= 3)
        {
            Debug.Log("3 or more emotions selected, enabling continue button");
            emotionContinueButton.SetActive(true);
        }
        else
        {
            Debug.Log("Not 3 emotions selected, disabling continue button");
            emotionContinueButton.SetActive(false);
        }
    }

    public void disableObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void loadQuotes()
    {
        //  load quotes from file
        string[] rawQuoteArray = quoteData.text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < rawQuoteArray.Length; i++)
        {
            if(i % 2 == 0)
            {
                quotes.Add(rawQuoteArray[i]);
            }
            else
            {
                quoteAuthors.Add(rawQuoteArray[i]);
            }
        }
        Debug.Log("Loaded Quotes: " + quotes.Count + "\nLoaded Quote Authors: " + quoteAuthors.Count);
        if(quotes.Count != quoteAuthors.Count)
        {
            Debug.LogWarning("Mismatch of Quotes and Authors!");
        }

        //  randomly select quote and display
        int num = UnityEngine.Random.Range(0, quotes.Count);
        string generatedQuote = "\"" + quotes[num] + "\" - " + quoteAuthors[num];
        quoteText.text = generatedQuote;
    }
}
