using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private GameObject welcomePanel;
    public GameObject WelcomePanel { get => welcomePanel; set => welcomePanel = value; }

    // Start is called before the first frame update
    void Start()
    {
        OpenPanel(WelcomePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
