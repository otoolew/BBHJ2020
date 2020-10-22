using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcGroupData", menuName = "Custom/NpcGroupData", order = 1)]
public class NpcGroupData : ScriptableObject {
    public float maxScarePoints = 5f;
    public float targetScareLevel = 1f;

    public NpcCharacter[] characters;
}
