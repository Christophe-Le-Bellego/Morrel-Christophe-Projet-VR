using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; 
    private Transform playerTransform;         
    public float spawnRange = 10f;   // Rayon max du spawn
    public float safeDistance = 5f;  // Distance minimum du joueur
    public int numberOfObjects = 5; // Nombre d'objets à créer

    void Start()
    {
        // 1. On cherche l'objet qui a le tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("my_player");

        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            // 2. Une fois le joueur trouvé, on lance le spawn
            SpawnObjects();
        }
        else
        {
            // Petit message d'alerte si tu as oublié de mettre le tag sur le joueur
            Debug.LogError("Attention : Aucun objet avec le tag 'Player' n'a été trouvé !");
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPos;
        float distance;

        do
        {
            // Génère une position aléatoire sur le sol (X et Z)
            float x = Random.Range(-spawnRange, spawnRange);
            float z = Random.Range(-spawnRange, spawnRange);
            randomPos = new Vector3(x, 0.5f, z); // 0.5f pour que la sphère ne soit pas à moitié enterrée

            // Calcule la distance entre cette position et le joueur
            distance = Vector3.Distance(randomPos, playerTransform.position);

        } while (distance < safeDistance); // Recommence si c'est trop proche

        return randomPos;
    }
}