using UnityEngine;

public class TriggerCandle : MonoBehaviour
{
    [Header("Paramètres de la Bougie")]
    public bool destroyOnPickUp = true;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie bien que le tag de ton joueur est "my_player" dans Unity !
        if (other.CompareTag("my_player"))
        {
            OnPlayerCollect();
        }
    }

    private void OnPlayerCollect()
    {
        Debug.Log("Bougie récupérée !");

        if (destroyOnPickUp)
        {
            // 1. On prévient le gestionnaire central d'ajouter un point
            if (GestionTexte.instance != null)
            {
                GestionTexte.instance.AjouterPoint();
            }

            // 2. On détruit la bougie
            Destroy(gameObject);
        }
        else
        {
            // Si on ne détruit pas, on coupe au moins le collider
            GetComponent<Collider>().enabled = false;
        }
    }
}