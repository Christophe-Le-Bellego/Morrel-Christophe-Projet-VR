using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("GroundCheck")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundCheckMask;

    public Transform target;   // R�f�rence � l'objet � suivre
    public float speed = 0;
    public static int amount = 0;
    public int vie = 2;
    public float mort;
    public static float tempsDerniereMort = -1f; // Initialisé bas pour pouvoir spawner dès le début


    void Update()
    {

        Vector3 direction = target.position - transform.position;
        //transform.position += direction.normalized * speed * Time.deltaTime;
        if (this.vie <= 0) {
            Mourir();
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Mourir()
    {
        tempsDerniereMort = Time.time;
        amount--;
        Destroy(gameObject);
    }

}
