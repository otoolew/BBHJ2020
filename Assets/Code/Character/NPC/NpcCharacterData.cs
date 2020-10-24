using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcCharacterData", menuName = "Custom/NpcCharacterData", order = 1)]
public class NpcCharacterData : ScriptableObject {
    public float moveSpeed = 3f;
    public float rotationSpeed = 180f;
}
