using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] trainEngines;
    public GameObject[] trainCars;
    public GameObject barrierPrefab;
    public Transform playerTransform;

    public float spawnDistanceAhead = 150f;
    public float distanceBetweenRows = 40f;
    public float laneDistance = 4f;
    public float despawnDistanceBehind = 20f;

    private float nextSpawnZ = 30f;
    private Queue<GameObject> activeObstacles = new Queue<GameObject>();

    void Update()
    {
        if (playerTransform.position.z + spawnDistanceAhead > nextSpawnZ)
        {
            spawnObstacleRow();
        }

        cleanUpObstacles();
    }

    void spawnObstacleRow()
    {
        int safeLane = Random.Range(0, 3);

        for (int currentLane = 0; currentLane < 3; currentLane++)
        {
            float xPos = (currentLane - 1) * laneDistance;
            Vector3 spawnPosition = new Vector3(xPos, 0, nextSpawnZ);

            if (currentLane == safeLane)
            {
                if (Random.value > 0.5f)
                {
                    GameObject barrier = Instantiate(barrierPrefab, spawnPosition, barrierPrefab.transform.rotation);
                    activeObstacles.Enqueue(barrier);
                }
            }
            else
            {
                float randomChoice = Random.value;

                if (randomChoice < 0.4f)
                {
                    GameObject engine = trainEngines[Random.Range(0, trainEngines.Length)];
                    GameObject spawnedEngine = Instantiate(engine, spawnPosition, engine.transform.rotation);
                    activeObstacles.Enqueue(spawnedEngine);
                }
                else if (randomChoice < 0.8f)
                {
                    GameObject car = trainCars[Random.Range(0, trainCars.Length)];
                    GameObject spawnedCar = Instantiate(car, spawnPosition, car.transform.rotation);
                    activeObstacles.Enqueue(spawnedCar);
                }
            }
        }

        nextSpawnZ += distanceBetweenRows;
    }

    void cleanUpObstacles()
    {
        while (activeObstacles.Count > 0)
        {
            GameObject oldestObstacle = activeObstacles.Peek();

            if (oldestObstacle == null)
            {
                activeObstacles.Dequeue();
                continue;
            }

            if (playerTransform.position.z - oldestObstacle.transform.position.z > despawnDistanceBehind)
            {
                Destroy(activeObstacles.Dequeue());
            }
            else
            {
                break;
            }
        }
    }
}