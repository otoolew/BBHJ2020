using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScare : ScareAttraction
{
    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }
    [SerializeField] private float resetTime;
    public float ResetTime { get => resetTime; set => resetTime = value; }


    // Start is called before the first frame update
    void Start()
    {
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void FireScare()
    {
        //base.FireScare();
        Debug.Log("Ghost Scare");
        if (Timer.Finished)
        {
            animator.speed = 1.0f;
            Timer.ResetTimer(resetTime);
        }
        else
        {
            animator.speed = 0.0f;
        }
        //Debug.Log("Ghost Scare!");
    }
}
