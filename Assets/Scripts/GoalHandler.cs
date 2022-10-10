using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private string goalText = "";
    private bool completeStatus = false;

    public Goal(string text, bool status)
    {
        setGoalText(text);
        setGoalStatus(status);
    }

    public void setGoalText(string text)
    {
        goalText = text;
    }

    public void setGoalStatus(bool status)
    {
        completeStatus = status;
    }

    public string getGoalText()
    {
        return goalText;
    }

    public bool getGoalStatus()
    {
        return completeStatus;
    }

    public string toString()
    {
        return goalText + ": " + completeStatus;
    }

    public static bool operator== (Goal obj1, Goal obj2)
    {
        return (obj1.getGoalText() == obj2.getGoalText());
    }
    public static bool operator!= (Goal obj1, Goal obj2)
    {
        return (obj1.getGoalText() != obj2.getGoalText());
    }
}

public class GoalHandler : MonoBehaviour
{
    string fileName = "testfile.txt";
    string goalFilePath = "";
    public List<Goal> goals;
    
    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        goalFilePath = Path.Combine(Application.persistentDataPath, "goals.txt");
        loadGoalsToList(goalFilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        writeGoalsToFile(goalFilePath);
        Debug.Log("Destroying GoalHandler");
    }

    //  Loads goals from file into list of goals
    //  params: takes filepath (including desired file) to load
    //  return: true if all goals successfully loaded, false otherwise
    private bool loadGoalsToList(string filePath)
    {
        if(File.Exists(filePath))
        {
            Debug.Log("Goals file exists! Reading file...");
            StreamReader sr = File.OpenText(filePath);
            string line = "";
            bool evenEntry = true;  //  Keeps track of if the current line is an "even" entry or not because a goal is make of pairs of 2 lines.
            string goalText = "";
            bool goalStatus = false;
            while((line = sr.ReadLine()) != null)
            {
                if(evenEntry)   //  If even entry, then it is a string of goalText...
                {
                    goalText = line;
                    //  Flip even counter
                    evenEntry = !evenEntry;
                }
                else    //  Else, it's an odd entry, or the status of the previous line...
                {
                    if(line == "True")
                    {
                        goalStatus = true;
                    }
                    else if(line == "False")
                    {
                        goalStatus = false;
                    }
                    else
                    {
                        Debug.LogWarning("Line " + line + "is not a bool! Abandoning read!");
                        return false;
                    }
                    //  Now we have goalText and goalStatus, create new goal object and put it in the list...
                    goals.Add(new Goal(goalText, goalStatus));
                    //  Reset temp variables
                    goalText = "";
                    goalStatus = false;
                    //  Flip even counter
                    evenEntry = !evenEntry;
                }
            }
            sr.Close();
            Debug.Log("Completed loop through goal file. Goals length: " + goals.Count);
            return true;
        }
        else
        {
            Debug.LogWarning("Goal file not present! Creating file...");
            //  Since there is no file, there should be no goals... Make a goal and write to file.
            writeGoalToList("Enter some goals to track", false);
            writeGoalsToFile(filePath);
            return true;
        }
    }

    //  Manually add a goal to the list
    public void writeGoalToList(string goalText, bool goalStatus)
    {
        goals.Add(new Goal(goalText, goalStatus));
    }

    //  overwrites file with current goal list
    public void writeGoalsToFile(string filePath)
    {
        Debug.Log("Writing goals to file: " + printGoals());
        StreamWriter sw = new StreamWriter(filePath);
        foreach(Goal g in goals)
        {
            string goalText = g.getGoalText();
            bool goalStatus = g.getGoalStatus();
            sw.WriteLine(goalText);
            sw.WriteLine(goalStatus);
        }
        sw.Close();
    }

    public string printGoals()
    {
        string result = "";
        foreach(Goal g in goals)
        {
            result = result + g.getGoalText() + ", ";
        }
        return result;
    }

}
