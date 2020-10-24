using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActiveScare : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    [SerializeField] private float scareRadius;
    public float ScareRadius { get => scareRadius; set => scareRadius = value; }

    [SerializeField] private float scareTravelDistance;
    public float ScareTravelDistance { get => scareTravelDistance; set => scareTravelDistance = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireScare()
    {
        RaycastHit[] allHit = Physics.SphereCastAll(firePoint.position, ScareRadius, transform.forward, ScareTravelDistance);

        if (allHit.Length>0)
        {
            for (int i = 0; i < allHit.Length; i++)
            {

            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, scareRadius);

        Gizmos.DrawSphere(transform.position + new Vector3(0.0f, 0.0f, scareTravelDistance), scareRadius);
    }
}
