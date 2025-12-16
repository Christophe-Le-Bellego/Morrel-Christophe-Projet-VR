using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("GroundCheck")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundCheckMask;

    public Transform target;   // R�f�rence � l'objet � suivre
    public float speed = 0.00000001f;
    public static int amount = 0;
    public int vie = 2;


    void Update()
    {


        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
        if (this.vie == 0) Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
