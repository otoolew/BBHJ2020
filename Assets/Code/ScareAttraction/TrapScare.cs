using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScare : ScareAttraction
{
    [SerializeField] private Timer timer;
    public override Timer Timer { get => timer; set => timer = value; }

    [SerializeField] private SphereCollider scareCollider;
    public override SphereCollider ScareCollider { get => scareCollider; set => scareCollider = value; }

    [SerializeField] private bool isActive;
    public override bool IsActive { get => isActive; set => isActive = value; }

    [SerializeField] private TrapTrigger trapTrigger;
    public TrapTrigger TrapTrigger { get => trapTrigger; set => trapTrigger = value; }

    private void Start()
    {
        scareCollider.enabled = false;
        trapTrigger.TrapCollider.enabled = true;
    }

    void Update()
    {
        if (timer.Finished)
        {
            scareCollider.enabled = false;
            isActive = false;
            trapTrigger.TrapCollider.enabled = true;
        }
        else
        {
            scareCollider.enabled = true;
            isActive = true;
            trapTrigger.TrapCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NpcCharacter npcCharacter = other.GetComponent<NpcCharacter>();
        if (npcCharacter != null)
        {
            Debug.Log(npcCharacter.name + " Was Scared!");
            FireScare();
        }
    }

    public override void FireScare()
    {

        scareCollider.enabled = true;
        isActive = true;
        timer.ResetTimer();
    }

}
