using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playtipes.Creature {
    public class SpawnItem : MonoBehaviour {
        [SerializeField] private Transform spawnPointTransform;

        [Header("Prefab to Spawn")]
        [SerializeField] private GameObject itemToSpawnPrefab;

        private int spawnedCount;

        private float spawnTime;
        private float spawnTimer;



        private void Awake() {
            spawnTime = Random.Range(3f, 10f);
        }



        private void Update() {
            HandleSpawnTimer();
        }



        private void HandleSpawnTimer() {
            if (spawnedCount >= 5) return;

            if (spawnTimer >= spawnTime) {
                Spawn();
                spawnTimer = 0f;
                spawnTime = Random.Range(3f, 10f);
            } else {
                spawnTimer += Time.deltaTime;
            }
        }



        private void Spawn() {
            GameObject spawnedItem = Instantiate(itemToSpawnPrefab, spawnPointTransform.position, Quaternion.identity);
            spawnedCount++;
        }
    }
}
