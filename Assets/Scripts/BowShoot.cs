using UnityEngine;

public class BowShoot : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float arrowSpeed = 50f;
    [SerializeField] float maxDistance = 100f; // utilise pour le raycast
    [SerializeField] LineRenderer laserLine;


void Update()
    {
        AfficherLaser();
    }

    void AfficherLaser()
    {
        // 1. Point de départ : Le bout de l'arc
        laserLine.SetPosition(0, firePoint.position);

        // 2. Calcul de la trajectoire
        Ray ray = new Ray(firePoint.position, firePoint.right);
        RaycastHit hit;

        // 3. Point d'arrivée
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Si on touche un mur/ennemi, le laser s'arrête dessus
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            // Si on ne touche rien, le laser va loin devant (maxDistance)
            laserLine.SetPosition(1, firePoint.position + (firePoint.right * maxDistance));
        }
    }





    public void Shoot()
    {
  
        //arrowPrefab.transform.Rotate(90, 0, 0);
        //firePoint.Rotate(0, 90, 0);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        arrow.transform.Rotate(90, 90, 0, Space.Self);
        //Debug.Log("Shoot instancie");
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.right * arrowSpeed;
        //Debug.Log("FL�CHE TIR�E");
    }
}