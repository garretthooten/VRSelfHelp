using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketReparent : MonoBehaviour
{

    public GameObject vase;
    [SerializeField] private XRSocketInteractor socket;
    public GoalHandler goalHandler;

    private string storedGoal;
    private GameObject storedFlower;

    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        goalHandler = GameObject.Find("GoalHandler").GetComponent<GoalHandler>();
        storedGoal = "";
    }

    public void Reparent()
    {
        Debug.Log("Inside of the socket!");
        Vector3 scale = socket.selectTarget.transform.localScale;
        storedFlower = socket.selectTarget.gameObject;
        storedFlower.transform.SetParent(vase.transform, true);
        storedFlower.transform.localScale = scale;

        //  get goal from Flower(selectTarget) and set goal status to complete
        Goal tempGoal = goalHandler.goals.Find(g => g.getGoalText() == storedFlower.GetComponentInChildren<Text>().text);
        tempGoal.setGoalStatus(true);
        Debug.Log("tempGoal: " + tempGoal.toString());
        storedGoal = storedFlower.GetComponentInChildren<Text>().text;
        storedFlower.GetComponentInChildren<Text>().text = "";
    }

    public void Unparent()
    {
        Debug.Log("Outside of the socket!");
        
        //  get goal from Flower(selectTarget) and set goal status to incomplete
        Goal tempGoal = goalHandler.goals.Find(g => g.getGoalText() == storedGoal);
        tempGoal.setGoalStatus(false);
        Debug.Log("tempGoal: " + tempGoal.toString());
        storedFlower.GetComponentInChildren<Text>().text = storedGoal;
        storedGoal = "";

        storedFlower.transform.parent = null;
    }

}
