using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalListItem : MonoBehaviour
{
    public Goal myGoal;
    public static int completed = 0;
    public string goalString;
    static public Text completedDisplay;

    private Text displayText;
    private Text buttonText;
    
    // Start is called before the first frame update
    void Start()
    {
        displayText.text = goalString;
        completedDisplay.text = "Completed: 0";
    }

    void CompleteGoal()
    {
        completed++;
        completedDisplay.text = "Completed: " + completed;
        buttonText.text = "Done!";
    }
}
