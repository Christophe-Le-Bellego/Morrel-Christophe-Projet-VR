using UnityEngine;

public class BowShoot : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float arrowSpeed = 50f;

    public void Shoot()
    {
        //arrowPrefab.transform.Rotate(90, 0, 0);
        //firePoint.Rotate(0, 90, 0);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        arrow.transform.Rotate(90, 90, 0, Space.Self);
        Debug.Log("Shoot instancie");
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.right * arrowSpeed;
        Debug.Log("FLÈCHE TIRÉE");
    }
}