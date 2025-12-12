using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public Transform target;   // Référence à l'objet à suivre
    public float speed = 10f;
    public static int amount = 0;

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
