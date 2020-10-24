using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcGroupWaveData", menuName = "Custom/NpcGroupWaveData", order = 1)]
public class NpcGroupWaveData : ScriptableObject {
    [System.Serializable]
    public class GroupWave {
        public NpcGroup group;
        public int characterCount = 4;
        public int minWaveTime = 30;
    }

    public GroupWave[] waves;
}
