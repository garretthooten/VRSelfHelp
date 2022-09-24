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
    public GameObject friendsPage;
    public List<Transform> spawnPoints;
    public GameObject flower;
    public GameObject vase;
    public Animator vaseAnim;

    [SerializeField] private int completedCount = 0;
    [SerializeField] private int flowersGrown = 0;
    private List<bool> flowerSpawned = new List<bool> {false, false, false, false};
    
    
    // Start is called before the first frame update
    void Start()
    {
        completedTextHeader.text = "Completed: " + completedCount;
        defaultPage.SetActive(true);
        goalsPage.SetActive(false);
        friendsPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  Handles switching UI pages.
    public void switchPage(string name)
    {
        Debug.Log("Switching Page");
        if(name == "defaultPage")
        {
            if(vase.activeSelf)
            {
                Debug.Log("Vase is active");
                vaseAnim.Play("vaseHolderShrink");
            }
            defaultPage.SetActive(true);
            goalsPage.SetActive(false);
            friendsPage.SetActive(false);
        }
        else if(name == "goalsPage")
        {
            goalsPage.SetActive(true);
            defaultPage.SetActive(false);
            friendsPage.SetActive(false);
        }
        else if(name == "friendsPage")
        {
            friendsPage.SetActive(true);
            defaultPage.SetActive(false);
            goalsPage.SetActive(false);
        }
    }
    
    //  Called whenever a goal "completed" button is clicked. Increments private completed
    //  count and updates header in UI.
    public void incrementCompleted(GameObject goal)
    {
        completedCount++;
        completedTextHeader.text = "Completed: " + completedCount;
        goal.SetActive(false);
    }

    //  Spawns 1 flower for each of the completed goals at the predetermined spawn locations.
    public void spawnFlowers()
    {
        if(!vase.activeSelf)
        {
            Debug.Log("Vase not active, activating");
            vase.SetActive(true);
        }
        vaseAnim.Play("vaseHolderGrow");
        //  TODO: add random flower color generation!
        int flowersToGrow = completedCount - flowersGrown;
        Debug.Log("Flowers to grow: " + flowersToGrow);
        for(int i = 0; i < flowersToGrow; i++)
        {
            Instantiate(flower, spawnPoints[i].position, new Quaternion(0,0,0,1));
            flowersGrown++;
        }
    }
    
    //  Takes the user back to the main menu.
    public void returnToHome()
    {
        SceneManager.LoadScene("Main Menu");
    }

    
}
