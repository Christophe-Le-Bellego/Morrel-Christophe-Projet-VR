using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //transform.Rotate(0, 90, 0);

        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rification importante : Si on touche le joueur, on ignore !
        if (collision.gameObject.CompareTag("Player")) return;

        // On plante la fl�che
        rb.isKinematic = true;

    if (collision.gameObject.CompareTag("Ennemy"))
        {
            // 1. On cherche le script "Ennemy" sur l'objet qu'on a touché
            Ennemy scriptEnnemy = collision.gameObject.GetComponent<Ennemy>();
            
            // 2. Si le script existe bien, on baisse sa vie
            if (scriptEnnemy != null)
            {
                scriptEnnemy.vie--; // Ou scriptEnnemy.TakeDamage(1); c'est encore mieux
            }
        }

        // On attache la fl�che � l'objet touch� (pour qu'elle bouge avec lui)
        transform.parent = collision.transform;
    }
}