using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareAttraction : MonoBehaviour
{
    [SerializeField] private Timer timer;
    public Timer Timer { get => timer; set => timer = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void FireScare()
    {
        Debug.Log("Fire Scare");
    }
    //public void OnTimerFinished(bool value)
    //{
    //    isReady = value;
    //}
}
