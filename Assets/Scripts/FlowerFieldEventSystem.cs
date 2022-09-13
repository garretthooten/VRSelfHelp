using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlowerFieldEventSystem : MonoBehaviour
{
    public Text completedTextHeader;
    public GameObject defaultPage;
    public GameObject goalsPage;

    [SerializeField] private int completedCount = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        completedTextHeader.text = "Completed: " + completedCount;
        defaultPage.SetActive(true);
        goalsPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchPage(string name)
    {
        if(name == "defaultPage")
        {
            defaultPage.SetActive(true);
            goalsPage.SetActive(false);
        }
        else if(name == "goalsPage")
        {
            goalsPage.SetActive(true);
            defaultPage.SetActive(false);
        }
    }
    
    public void incrementCompleted(GameObject goal)
    {
        completedCount++;
        completedTextHeader.text = "Completed: " + completedCount;
        goal.SetActive(false);
    }
    
    public void returnToHome()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
