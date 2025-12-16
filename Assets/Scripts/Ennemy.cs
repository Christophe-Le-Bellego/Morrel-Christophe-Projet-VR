using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("GroundCheck")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundCheckMask;

    public Transform target;   // Référence à l'objet à suivre
    public float speed = 0.00000001f;
    public static int amount = 0;


    void Update()
    {


        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
