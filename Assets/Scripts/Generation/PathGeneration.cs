using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGeneration : MonoBehaviour
{
    [SerializeField] private GameObject normalRoadPrefabs;
    [SerializeField] private GameObject brokenRoadPrefab;

    [SerializeField] private Transform tresholdPoint;

    [SerializeField] private float offsetZ = 6;

    [SerializeField] private Transform parent;
    private bool _firstSpawn;
    private int _platformSpawned;
    [SerializeField] private int firstNormalPlatforms = 5;

    private void Start()
    {
        _firstSpawn = true;
    }

    private void Update()
    {
        ControlGeneration();
    }

    private void ControlGeneration()
    {
        if (transform.position.z < tresholdPoint.position.z)
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        if (_firstSpawn)
        {
            GameObject firstPlatform = Instantiate(normalRoadPrefabs, transform.position, transform.rotation);

            _platformSpawned++;
            firstPlatform.GetComponent<EnvironmentSpawner>().enabled = false;

            if (_platformSpawned >= firstNormalPlatforms)
            {
                _firstSpawn = false;
            }
        }
        else
        {
            if (Random.Range(0, 100) < 70)
            {
                Instantiate(normalRoadPrefabs, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(brokenRoadPrefab, transform.position, transform.rotation);
            }


            transform.position += new Vector3(0, 0, offsetZ);
        }
    }
}
