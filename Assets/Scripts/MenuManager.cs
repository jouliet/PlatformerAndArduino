using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject controls;
    [SerializeField] Button returnButton;
    [SerializeField] Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ControlButton()
    {
        controls.SetActive(true);
        returnButton.Select();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ReturnButton()
    {
        controls.SetActive(false);
        playButton.Select();
    }
}
