using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentSpawner : MonoBehaviour
{
   [Header("House")]
   [SerializeField] private Transform leftHousePosition;
   [SerializeField] private Transform rightHousePosition;
   [SerializeField] private GameObject [] houses;
   private int _houseIndex;
   private GameObject _spawnedHouse;

   [Header("Lamps")] 
   [SerializeField] private Transform leftLampPosition;
   [SerializeField] private Transform rightLampPosition;
   [SerializeField] private GameObject leftLamp;
   [SerializeField] private GameObject rightLamp;
   private GameObject _spawnedLamp;

   [SerializeField] private bool isNormalRoad;

   [Header("Obstacles")] 
   [SerializeField] private float percentForObstacleSpawn = 50;
   [SerializeField] private GameObject[] obstacles;
   private int _indexOfObstacle;
   [SerializeField] private Transform[] pointsForSpawnObstacle;
   private int _indexToSpawnPosition;
   private GameObject _obstacle;

   [Header("Coins")] 
   [SerializeField] private GameObject[] coins;
   private int _coinIndex;
   [SerializeField] private Transform[] pointsToSpawnCoins;
   private int _pointsIndex;
   private GameObject _coin;
   

   private void Start()
   {
      SpawnHouses();
      SpawnLamps();

      if (isNormalRoad)
      {
          if (Random.Range(0,100)>=percentForObstacleSpawn)
          {
              if (Random.Range(0,100)<50)
              {
                  SpawnObstacle();
              }
              else
              {
                  SpawnCoin();
              }
          }
      }
   }

   private void SpawnHouses()
   {
      _houseIndex = Random.Range(0, houses.Length);
     _spawnedHouse = Instantiate(houses[_houseIndex], leftHousePosition.position, Quaternion.identity);
     _spawnedHouse.transform.SetParent(transform);
      
      _houseIndex = Random.Range(0, houses.Length);
     _spawnedHouse = Instantiate(houses[_houseIndex], rightHousePosition.position, Quaternion.identity);
     _spawnedHouse.transform.SetParent(transform);
   }

   private void SpawnLamps()
   {
      _spawnedLamp = Instantiate(leftLamp, leftLampPosition.position, Quaternion.identity);
      _spawnedLamp.transform.SetParent(transform);
      
     _spawnedLamp = Instantiate(rightLamp, rightLampPosition.position, Quaternion.identity);
     _spawnedLamp.transform.SetParent(transform);
   }

   private void SpawnObstacle()
   {
       _indexOfObstacle = Random.Range(0, obstacles.Length);
       _indexToSpawnPosition = Random.Range(0, pointsForSpawnObstacle.Length);

      _obstacle = Instantiate(obstacles[_indexOfObstacle], pointsForSpawnObstacle[_indexToSpawnPosition].position,
           transform.rotation);
      
      _obstacle.transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles.x,180f,transform.rotation.eulerAngles.z);

      _obstacle.transform.SetParent(transform);
   }

   private void SpawnCoin()
   {
       _coinIndex = Random.Range(0, coins.Length);
       _pointsIndex = Random.Range(0, pointsToSpawnCoins.Length);

       _coin = Instantiate(coins[_coinIndex], pointsToSpawnCoins[_pointsIndex].position, Quaternion.identity);
       _coin.transform.SetParent(transform);
   }
}
