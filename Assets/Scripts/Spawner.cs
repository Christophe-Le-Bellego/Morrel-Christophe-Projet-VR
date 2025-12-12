using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public Ennemy prefab;       // L’objet à faire apparaître
    public float interval = 200f;     // Temps entre chaque apparition
    public Vector3 spawnPosition;   // Position de spawn

    void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating("SpawnObject", 0f, interval);
    }

    void SpawnObject()
    {
        Ennemy instance = Instantiate(prefab, spawnPosition, Quaternion.identity);
        instance.SetTarget(player);
    }
}
