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
        // Vérification importante : Si on touche le joueur, on ignore !
        if (collision.gameObject.CompareTag("Player")) return;

        // On plante la flèche
        rb.isKinematic = true;

        // On attache la flèche à l'objet touché (pour qu'elle bouge avec lui)
        transform.parent = collision.transform;
    }
}