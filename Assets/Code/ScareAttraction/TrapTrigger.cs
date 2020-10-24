using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private TrapScare trapScare;
    public TrapScare TrapScare { get => trapScare; set => trapScare = value; }

    [SerializeField] private Collider trapCollider;
    public Collider TrapCollider { get => trapCollider; set => trapCollider = value; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Entered Trap!");
        NpcCharacter npcCharacter = other.GetComponent<NpcCharacter>();
        if (npcCharacter != null)
        {
            Debug.Log(npcCharacter.name + " Was Scared!");
            trapScare.FireScare();
        }

    }
}
