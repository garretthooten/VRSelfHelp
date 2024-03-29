using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using VRKeys;

public class VRRenderScale : MonoBehaviour {
    void Start () {
        if(Application.platform == RuntimePlatform.Android)
            XRSettings.eyeTextureResolutionScale = 2.5f;
    }
}

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

    public FadeScreen fadeScreen;

    public Keyboard keyboard;

    private bool hasBeenBooted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hasBeenBooted = GetBootedStatus();
        if(hasBeenBooted)
        {
            Debug.Log("Application has been booted, skipping mood survey");
            SwitchPage("mainmenu");
        }
        else
        {
            Debug.Log("Application has not been booted, performing mood survey");
        }

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

            if(!keyboard.disabled)
            {
                keyboard.SetText("");
                keyboard.Disable();
            }
        }
        else
        {
            Debug.LogWarning("Not able to find page " + pageName);
        }
    }

    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene(levelName);
        StartCoroutine(LoadLevelCoroutine(levelName));
    }

    public IEnumerator LoadLevelCoroutine(string levelName)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(2);
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

    private bool GetBootedStatus()
    {
        string bootStatusFilePath = Path.Combine(Application.persistentDataPath, "bootStatus.txt");
        if(File.Exists(bootStatusFilePath))
        {
            Debug.Log("Boot status file found!");
            return true;
        }
        else
        {
            Debug.LogWarning("Boot status file not present! Creating file...");
            StreamWriter sw = new StreamWriter(bootStatusFilePath);
            sw.WriteLine("application has been booted");
            sw.Close();
            return false;
        }
    }

    public void ResetApplication()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "bootStatus.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "goals.txt"));
        goalHandler.mainMenuResetSwitch = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetApplicationAndQuit()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "bootStatus.txt"));
        File.Delete(Path.Combine(Application.persistentDataPath, "goals.txt"));
        goalHandler.mainMenuResetSwitch = true;
        Application.Quit();
    }
}
