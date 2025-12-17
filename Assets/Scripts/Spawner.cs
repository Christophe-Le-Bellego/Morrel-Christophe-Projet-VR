using UnityEngine;


public class Spawner : MonoBehaviour
{
    public Transform player;
    public Ennemy prefab;       // L�objet � faire appara�tre
    public float interval = 20f;     // Temps entre chaque apparition
    public Vector3 spawnPosition;   // Position de spawn
    public float delaiApresMort = 5f;


    void Start()
    {
        spawnPosition = transform.position;
        InvokeRepeating("TenterSpawn", 0f, interval);
    }


    void TenterSpawn()
    {
        bool delaiRespecte = (Time.time - Ennemy.tempsDerniereMort) > delaiApresMort;
        if (Ennemy.amount < 4 && delaiRespecte)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        //Quaternion rotation = Quaternion.LookRotation(player.position);
        Ennemy instance = Instantiate(prefab, spawnPosition, prefab.transform.rotation/*Quaternion.identity*/);
        Ennemy.amount++;
        instance.SetTarget(player);
        instance.tag = "Ennemy";
                
    }

}
