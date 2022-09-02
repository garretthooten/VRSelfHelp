using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerFieldEventSystem : MonoBehaviour
{
    [SerializeField] public int myWeirdNum = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void returnToHome()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public int five()
    {
        return 5;
    }
}
