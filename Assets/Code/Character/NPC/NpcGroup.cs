using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGroup : MonoBehaviour {
    #region Declarations
    [SerializeField] public NpcGroupData _groupData;
    public NpcGroupData GroupData { get => _groupData; set => _groupData = value; }

    #endregion

    #region MonoBehavior Overrides
    protected void Awake() {

    }

    protected void Update() {

    }
    #endregion

    #region Methods
    public void SpawnGroup(Vector3 position) {

    }
    #endregion
}
