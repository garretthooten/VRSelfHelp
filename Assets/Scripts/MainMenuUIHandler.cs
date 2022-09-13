using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
    //quick and dirty but implement list-based dynamic page system later
    public GameObject page1;
    public GameObject page2;
    
    // Start is called before the first frame update
    void Start()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }
}
