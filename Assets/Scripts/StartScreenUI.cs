using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    public List<GameObject> screens;
    public Button nextButton;
    public Button prevButton;
    public Button startButton;
    private int index=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (index==0)
        {
            startButton.gameObject.SetActive(false);
            prevButton.gameObject.SetActive(false);
        }
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        screens[index].SetActive(true);
        
    }

    public void PrevButton()
    {
        screens[index].SetActive(false);
        index--;
        screens[index].SetActive(true);

        startButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        if (index == 0)
        {
            prevButton.gameObject.SetActive(false);
        }
    }

public void NextButton()
    {
        screens[index].SetActive(false);
        index++;
        screens[index].SetActive(true);

        prevButton.gameObject.SetActive(true);

        if (index == screens.Count - 1)
        {
            nextButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }
}