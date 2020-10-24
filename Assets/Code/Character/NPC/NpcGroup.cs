using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcGroup : MonoBehaviour {
    #region Declarations
    [SerializeField] private NavMeshAgent _navAgent = null;
    [SerializeField] private NpcGroupData _groupData;
    public NpcGroupData GroupData { get => _groupData; set => _groupData = value; }

    public NpcCharacter[] ActiveCharacters { get; private set; }
    public NpcPathwayPoint CurrentPathPoint { get; private set; }
    public float GroupFearTotal { get; set; }
    #endregion

    #region MonoBehavior Overrides
    protected void Awake() {
        ActiveCharacters = new NpcCharacter[] { };
        CurrentPathPoint = null;
    }

    protected void Update() {
        if (CurrentPathPoint == null) {
            GameManager.Instance.DespawnGroup(this);
        }
        else if (_navAgent.isStopped || !_navAgent.hasPath) {
            MoveToNextPoint();
        }
    }
    #endregion

    #region Public Methods
    public void SpawnGroup(GroupSpawnPoint spawn) {
        List<NpcCharacter> characters = new List<NpcCharacter>();
        List<GameObject> availableSpawns = new List<GameObject>(spawn.spawnPoints);

        // Spawn each character
        for (int x=0; x < _groupData.characters.Length; x++) {
            int spawnIndex = Random.Range(0, availableSpawns.Count);
            Vector3 spawnPos = availableSpawns[spawnIndex].transform.position;
            availableSpawns.RemoveAt(spawnIndex);

            var spawnedChar = GameObject.Instantiate(_groupData.characters[x], spawnPos, transform.rotation);
            spawnedChar.gameObject.name = gameObject.name + $"_Character{x+1}";
            spawnedChar.SetupCharacterGroup(this);
            characters.Add(spawnedChar);
        }
        ActiveCharacters = characters.ToArray();

        // Set the initial pathway point
        MoveToNextPoint();
    }

    public void DestroyGroup() {
        GameManager.Instance.CurrentFearAmount += GroupFearTotal;

        for (int x=0; x < ActiveCharacters.Length; x++) {
            Destroy(ActiveCharacters[x].gameObject);
        }
        Destroy(gameObject);
    }
    #endregion

    #region Private Methods
    private void MoveToNextPoint() {
        if (CurrentPathPoint == null) {
            CurrentPathPoint = GameManager.Instance.PathwayPoints[0];
        }
        else {
            CurrentPathPoint = CurrentPathPoint.NextPoint;
        }

        if (CurrentPathPoint == null) {
            _navAgent.isStopped = true;
            return;
        }

        _navAgent.SetDestination(CurrentPathPoint.transform.position);
    }
    #endregion
}
