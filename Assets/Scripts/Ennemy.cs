using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("Settings")]
    public Transform target;
    [SerializeField] private float speed = 7f;
    public int vie = 2;
    
    // Distance à laquelle l'ennemi peut taper le joueur
    [SerializeField] private float attackRange = 1.5f; 

    // Référence directe au script du joueur pour ne pas le chercher en boucle
    private PlayerController playerScript;

    public static int amount = 0;
    public static float tempsDerniereMort = -1f;

    void Start()
    {
        amount++;

        // 1. Si pas de cible, on cherche le joueur
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;       
            }
        }

        // 2. IMPORTANT : On récupère le script du joueur DÈS LE DÉBUT
        // Comme ça, on sait déjà qui attaquer sans faire de collision
        if (target != null)
        {
            playerScript = target.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (target == null) return;

        // Calcul de la distance entre l'ennemi et le joueur
        float distance = Vector3.Distance(transform.position, target.position);

        // --- MOUVEMENT ---
        // Si on est loin, on avance
        if (distance > attackRange)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        // --- ATTAQUE ---
        // Si on est assez proche (<= attackRange)
        else 
        {
            // On s'assure de regarder le joueur même en attaquant
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            // On attaque !
            if (playerScript != null)
            {
                playerScript.RecevoirDegats();
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        vie -= damageAmount;
        if (vie <= 0) Mourir();
    }

    public void SetTarget(Transform newTarget)
    {
        this.target = newTarget;
        // Si le spawner définit la cible, on récupère le script du joueur ici aussi
        if (newTarget != null)
        {
            playerScript = newTarget.GetComponent<PlayerController>();
        }
    }

    void Mourir()
    {
        tempsDerniereMort = Time.time;
        amount--;
        Destroy(gameObject);
    }
    
    // On peut supprimer OnCollisionEnter car on utilise la distance maintenant !
}