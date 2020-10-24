﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActiveScare : ScareAttraction
{
    [SerializeField] private SphereCollider scareCollider;
    public SphereCollider ScareCollider { get => scareCollider; set => scareCollider = value; }

    [SerializeField] private bool isActive;
    public bool IsActive { get => isActive; set => isActive = value; }

    [SerializeField] private GameObject scareObj;
    public GameObject ScareObj { get => scareObj; set => scareObj = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.Finished)
        {
            scareCollider.enabled = false;
            isActive = false;
            scareObj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character npcCharacter = other.GetComponent<Character>();
        if (npcCharacter != null)
        {
            Debug.Log(npcCharacter.name + " Was Scared!");
        }
    }

    public override void FireScare()
    {
        scareCollider.enabled = true;
        isActive = true;
        scareObj.SetActive(true);
        Timer.ResetTimer();
    }
}
