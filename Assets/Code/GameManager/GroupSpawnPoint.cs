using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GroupSpawnPoint : MonoBehaviour {
    public GameObject[] spawnPoints;
    public float characterSize = 1f;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (spawnPoints != null && spawnPoints.Length > 0) {
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            for (int x = 0; x < spawnPoints.Length; x++) {
                Gizmos.DrawSphere(spawnPoints[x].transform.position, characterSize * 0.5f);
            }
        }
    }

    [ContextMenu("Auto Setup")]
    private void SetupSpawnPoints() {
        List<GameObject> newPoints = new List<GameObject>();
        Collider collider = GetComponent<Collider>();

        if (collider == null) {
            Debug.LogError("No collider on object!");
            return;
        }

        if (spawnPoints != null && spawnPoints.Length > 0) {
            for (int x=0; x < spawnPoints.Length; x++) {
                if (spawnPoints[x] != null) { DestroyImmediate(spawnPoints[x]); }
            }
            spawnPoints = new GameObject[] { };
        }

        int xSize = Mathf.RoundToInt(collider.bounds.size.x / characterSize);
        int ySize = Mathf.RoundToInt(collider.bounds.size.z / characterSize);

        float xOffset = (-collider.bounds.size.x / 2f) + (characterSize * 0.5f);
        float yOffset = (-collider.bounds.size.x / 2f) + (characterSize * 0.5f);

        for (int y = 0; y < ySize; y++) {
            for (int x = 0; x < xSize; x++) {
                GameObject point = new GameObject($"Point {x}-{y}");
                point.transform.SetParent(transform);
                point.transform.position = transform.position + new Vector3(characterSize * x + xOffset, 0f, characterSize * y + yOffset);
                newPoints.Add(point);
            }
        }

        spawnPoints = newPoints.ToArray();
        EditorUtility.SetDirty(this);
    }
#endif
}
