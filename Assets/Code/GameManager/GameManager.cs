using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This will manage the game in the scene, not persistant data.
/// </summary>
public class GameManager : MonoBehaviour {
    #region Declarations
    [SerializeField] private NpcGroupWaveData _waveData = null;
    [SerializeField] private GroupSpawnPoint _groupSpawnPoint = null;
    [SerializeField] private NpcPathwayPoint[] _pathwayPoints = new NpcPathwayPoint[] { };
    [SerializeField] private int _maxActiveGroupCount = 4;

    public static GameManager Instance { 
        get {
            if (_instance == null) {
                _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
            return _instance;
        }
    }

    public NpcPathwayPoint[] PathwayPoints { get => _pathwayPoints; set => _pathwayPoints = value; }
    public int MaxActiveGroupCount { get => _maxActiveGroupCount; set => _maxActiveGroupCount = value; }

    public bool IsGameActive { get; private set; }
    public float CurrentFearAmount { get; set; }

    public int NextSpawnIndex { get; private set; }
    public float NextSpawnTimer { get; private set; }
    public int ActiveGroupCount { get { return _activeGroups.Count; } }
    public int SpawnGroupsRemaining { get { return _waveData.waves.Length - NextSpawnIndex; } }

    private static GameManager _instance;
    private List<NpcGroup> _activeGroups;
    #endregion

    #region Events
    public delegate void GameDelegate();
    public event GameDelegate GameOver;

    public delegate void GroupDelegate(NpcGroup group);
    public event GroupDelegate GroupSpawned;
    public event GroupDelegate GroupExited;
    #endregion

    #region MonoBehavior Overrides
    private void Awake() {
        _instance = this;

        if (_waveData == null || _waveData.waves.Length == 0) {
            Debug.LogError("Improper group data specified! Fix it, brah!");
            return;
        }

        _activeGroups = new List<NpcGroup>();

        IsGameActive = true;
        CurrentFearAmount = 0;

        NextSpawnIndex = 0;
        NextSpawnTimer = _waveData.waves[NextSpawnIndex].minWaveTime;
    }

    private void Update() {
        if (!IsGameActive) { return; }

        if (SpawnGroupsRemaining == 0) {
            // Check to see if the game is over
            EndGame();
            return; 
        }

        // Decrement our timer and check to see if we should spawn another group
        NextSpawnTimer -= Time.deltaTime;
        if (NextSpawnTimer < 0) { NextSpawnTimer = 0f; }
        if (NextSpawnTimer <= 0 && ActiveGroupCount < MaxActiveGroupCount) {
            SpawnNextGroup();
        }
    }

    private void OnDrawGizmosSelected() {
        if (_pathwayPoints != null && _pathwayPoints.Length > 1) {
            Color activeColor = Color.black;
            Gizmos.color = activeColor;

            for (int x=0; x < _pathwayPoints.Length; x++) {
                var point = _pathwayPoints[x];

                if (point != null && point.NextPoint != null) {
                    Gizmos.DrawSphere(point.transform.position, 0.2f);

                    float colorLevel = Mathf.Lerp(1f, 0f, (float)x / (float)_pathwayPoints.Length);
                    activeColor = new Color(colorLevel, colorLevel, 1.0f, 1f);
                    Gizmos.color = activeColor;

                    Gizmos.DrawLine(point.transform.position, point.NextPoint.transform.position);
                }
            }
        }
    }
    #endregion

    #region Game Loop Methods
    public void EndGame() {
        IsGameActive = false;
        GameOver?.Invoke();
    }
    #endregion

    #region Group Methods
    public NpcGroup[] GetActiveGroups() {
        return _activeGroups.ToArray();
    }

    public NpcGroupData GetNextGroupData() {
        if (_waveData.waves.Length <= NextSpawnIndex) {
            return null;
        }

        return _waveData.waves[NextSpawnIndex].group.GroupData;
    }

    public void DespawnGroup(NpcGroup group) {
        // Todo - get fear of group and add to the pool

        _activeGroups.Remove(group);
        GroupExited?.Invoke(group);
        group.DestroyGroup();
    }

    private NpcGroup SpawnNextGroup() {
        if (_waveData.waves.Length <= NextSpawnIndex) {
            return null;
        }

        NpcGroup nextGroup = _waveData.waves[NextSpawnIndex].group;

        if (nextGroup == null) { return null; }

        NextSpawnIndex += 1;
        if (_waveData.waves.Length > NextSpawnIndex) {
            NextSpawnTimer = _waveData.waves[NextSpawnIndex].minWaveTime;
        }
        else {
            NextSpawnTimer = 0f;
        }

        nextGroup = GameObject.Instantiate(nextGroup, _groupSpawnPoint.transform.position, Quaternion.identity);
        nextGroup.name = $"Group{NextSpawnIndex}";
        nextGroup.SpawnGroup(_groupSpawnPoint);
        _activeGroups.Add(nextGroup);

        GroupSpawned?.Invoke(nextGroup);
        return nextGroup;
    }
    #endregion

    #region Context Menu Methods
#if UNITY_EDITOR
    [ContextMenu("Auto Set Pathway Points")]
    public void AutoSetupPathwayPoints() {
        List<NpcPathwayPoint> availablePoints = new List<NpcPathwayPoint>(GameObject.FindObjectsOfType<NpcPathwayPoint>());
        List<NpcPathwayPoint> setPoints = new List<NpcPathwayPoint>();
        NpcPathwayPoint currentPoint = null;

        if (_pathwayPoints == null || _pathwayPoints.Length == 0) {
            Debug.LogError("First point must be set manually");
            return;
        }

        currentPoint = _pathwayPoints[0];
        currentPoint.PreviousPoint = null;
        currentPoint.NextPoint = null;

        while (currentPoint != null) {
            NpcPathwayPoint nextPoint = null;
            float nextPointDist = 0;

            foreach (var point in availablePoints) {
                float dist = Mathf.Abs(Vector3.Distance(currentPoint.transform.position, point.transform.position));
                if (nextPoint == null || dist < nextPointDist) {
                    if (Physics.Linecast(currentPoint.transform.position + new Vector3(0, 0.2f, 0), point.transform.position + new Vector3(0, 0.2f, 0))) {
                        continue;
                    }

                    nextPoint = point;
                    nextPointDist = dist;
                }
            }

            if (nextPoint != null) {
                availablePoints.Remove(nextPoint);
                currentPoint.NextPoint = nextPoint;
                nextPoint.PreviousPoint = currentPoint;

                EditorUtility.SetDirty(currentPoint);
                setPoints.Add(currentPoint);
            }
            currentPoint = nextPoint;
        }

        _pathwayPoints = setPoints.ToArray();
        EditorUtility.SetDirty(this);
    }
#endif
    #endregion
}
