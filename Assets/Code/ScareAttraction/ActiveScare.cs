using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActiveScare : ScareAttraction
{
    [SerializeField] private Timer timer;
    public override Timer Timer { get => timer; set => timer = value; }

    [SerializeField] private SphereCollider scareCollider;
    public override SphereCollider ScareCollider { get => scareCollider; set => scareCollider = value; }

    [SerializeField] private bool isActive;
    public override bool IsActive { get => isActive; set => isActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            scareCollider.enabled = false;
            isActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NpcCharacter npcCharacter = other.GetComponent<NpcCharacter>();
        if (npcCharacter != null)
        {
            Debug.Log(npcCharacter.name + " Was Scared!");
        }
    }

    public override void FireScare()
    {
        scareCollider.enabled = true;
        isActive = true;
        timer.ResetTimer();
    }
}
