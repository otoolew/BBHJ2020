﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcGroup : MonoBehaviour {
    #region Declarations
    [SerializeField] private NavMeshAgent _navAgent = null;
    [SerializeField] private NpcGroupData _groupData;
    [SerializeField] private float _fearDelayTime = 2f;
    public NpcGroupData GroupData { get => _groupData; set => _groupData = value; }

    public NpcCharacter[] ActiveCharacters { get; private set; }
    public NpcPathwayPoint CurrentPathPoint { get; private set; }
    public float GroupFearTotal { get; private set; }

    private bool _fearDelay;
    private float _fearDelayTimer;
    #endregion

    #region MonoBehavior Overrides
    protected void Awake() {
        ActiveCharacters = new NpcCharacter[] { };
        CurrentPathPoint = null;
    }

    protected void Update() {
        if (CurrentPathPoint != null && Vector3.Distance(transform.position, CurrentPathPoint.transform.position) < 0.5f) {
            MoveToNextPoint();
        }
        if (CurrentPathPoint == null) {
            GameManager.Instance.DespawnGroup(this);
        }

        if (_fearDelay) {
            _fearDelayTimer += Time.deltaTime;
            if (_fearDelayTimer >= _fearDelayTime) {
                _fearDelay = false;
                _fearDelayTimer = 0;

                _navAgent.isStopped = false;
                _navAgent.SetDestination(CurrentPathPoint.transform.position);
            }
        }
    }
    #endregion

    #region Public Methods
    public void SpawnGroup(GroupSpawnPoint spawn, int characterCount) {
        List<NpcCharacter> characters = new List<NpcCharacter>();
        List<GameObject> availableSpawns = new List<GameObject>(spawn.spawnPoints);

        // Spawn each character
        for (int x=0; x < characterCount; x++) {
            int spawnIndex = Random.Range(0, availableSpawns.Count);
            Vector3 spawnPos = availableSpawns[spawnIndex].transform.position;
            availableSpawns.RemoveAt(spawnIndex);

            var spawnedChar = GameObject.Instantiate(_groupData.characters[Random.Range(0, _groupData.characters.Length)], spawnPos, transform.rotation);
            spawnedChar.gameObject.name = gameObject.name + $"_Character{x+1}";
            spawnedChar.SetupCharacterGroup(this);
            characters.Add(spawnedChar);
        }
        ActiveCharacters = characters.ToArray();

        // Set the initial pathway point
        CurrentPathPoint = GameManager.Instance.PathwayPoints[0];
        _navAgent.SetDestination(CurrentPathPoint.transform.position);
    }

    public void DestroyGroup() {
        GameManager.Instance.CurrentFearAmount += GroupFearTotal;

        for (int x=0; x < ActiveCharacters.Length; x++) {
            Destroy(ActiveCharacters[x].gameObject);
        }
        Destroy(gameObject);
    }

    public void AddFearValue(float fearAmount) {
        GroupFearTotal += fearAmount;

        if (!_fearDelay) {
            _navAgent.isStopped = true;
            _fearDelay = true;
        }
        else {
            _fearDelayTimer = 0f;
        }
    }
    #endregion

    #region Private Methods
    private void MoveToNextPoint() {
        CurrentPathPoint = CurrentPathPoint.NextPoint;
        if (CurrentPathPoint == null) {
            _navAgent.isStopped = true;
            return;
        }

        _navAgent.SetDestination(CurrentPathPoint.transform.position);
    }
    #endregion
}
