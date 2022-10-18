using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRKeys;
using TMPro;

public class GoalEditingList : MonoBehaviour
{
    [SerializeField] private GoalHandler goalHandler;
    public TMP_InputField goalInputText;

    public GameObject itemTemplate;
    public GameObject savedTemplate;

    public Keyboard kboard;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject itemTemplate = transform.GetChild(0).gameObject;
        CreateGoalListItems();
        kboard.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  Creates items in canvas to display goals and remove them
    void CreateGoalListItems()
    {
        //  Find sample goal display in scene and store it to create other objects from
        //GameObject itemTemplate = transform.GetChild(0).gameObject;
        GameObject listItem;
        //  For each goal in the goalhandler...
        foreach(Goal g in goalHandler.goals)
        {
            Debug.Log("Creating list item for: " + g.getGoalText());
            //  Create a new instance of the goal display we found above and set the text to the proper goal...
            listItem = Instantiate(itemTemplate, transform);
            listItem.transform.GetComponentInChildren<Text>().text = g.getGoalText();
            //  Store display item and goal as temp variables for the onClick listener...
            GameObject tempItem = listItem;
            Goal tempGoal = g;
            //  Create new listener for delete button onClick to remove the item display and the goal from the goalhandler... 
            listItem.transform.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveGoalFromList(tempItem, tempGoal));
            savedTemplate = listItem;
        }
        Destroy(itemTemplate);
    }

    //  Destroys the listItem display passed in and removes parameter goal from the list in goalhandler
    void RemoveGoalFromList(GameObject listItem, Goal goal)
    {
        Debug.Log("Goals list before removing " + goal.getGoalText() + ": " + goalHandler.printGoals());
        int index = goalHandler.goals.FindIndex(g => g.getGoalText() == goal.getGoalText());
        Debug.Log("Index of goal in list: " + index);
        goalHandler.goals.RemoveAt(index);
        Debug.Log("Goals list after removing " + goal.getGoalText() + ": " + goalHandler.printGoals());
        Destroy(listItem);
    }

    public void GoalAddButtonPressed()
    {
        if(kboard.disabled)
            kboard.Enable();
    }

    public void GoalEnterButtonPressed()
    {
        string newGoalText = kboard.text;
        Goal tempGoal = new Goal(newGoalText, false);
        goalHandler.goals.Add(tempGoal);
        
        GameObject listItem = Instantiate(savedTemplate, transform);
        listItem.transform.GetComponentInChildren<Text>().text = newGoalText;
        //  Store display item and goal as temp variables for the onClick listener...
        GameObject tempItem = listItem;
        //  Create new listener for delete button onClick to remove the item display and the goal from the goalhandler... 
        listItem.transform.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveGoalFromList(tempItem, tempGoal));
        DisableKeyboard();
    }

    public void DisableKeyboard()
    {
        kboard.SetText("");
        kboard.Disable();
    }
}
