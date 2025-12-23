using UnityEngine;

public class BowShoot : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float arrowSpeed = 50f;
    [SerializeField] float maxDistance = 100f; // utilise pour le raycast
    
    [SerializeField] LayerMask layersToIgnore;



    private LineRenderer laserLine;

    void Start()
    {
        // 1. CRÉATION AUTOMATIQUE DU COMPOSANT
        // Si l'objet n'a pas de LineRenderer, on l'ajoute par code
        laserLine = GetComponent<LineRenderer>();
        if (laserLine == null)
        {
            laserLine = gameObject.AddComponent<LineRenderer>();
        }

        // 2. CRÉATION AUTOMATIQUE DU MATÉRIAU (Le truc qui te manquait)
        // On cherche le shader de base "Sprites/Default" qui est toujours présent dans Unity
        Shader shader = Shader.Find("Sprites/Default");
        Material laserMat = new Material(shader);
        
        // On assigne ce matériau au LineRenderer
        laserLine.material = laserMat;

        Color rougeTransparent = new Color(1f, 0f, 0f, 0.3f);

        // 3. RÉGLAGES VISUELS (Couleur et Épaisseur)
        laserLine.startColor = rougeTransparent;
        laserLine.endColor = rougeTransparent;
        
        laserLine.startWidth = 0.01f; // Très fin
        laserLine.endWidth = 0.01f;

        laserLine.positionCount = 2; // Départ et Arrivée
        laserLine.useWorldSpace = true; // Important !
    }

    void Update()
    {
        // Sécurité : si tu as oublié de mettre le FirePoint, on prend la position de l'objet lui-même
        Vector3 depart = (firePoint != null) ? firePoint.position + firePoint.right : transform.position;
        Vector3 direction = (firePoint != null) ? firePoint.right : transform.right; 
        // NOTE : Si le laser part vers le haut ou le bas, change "right" par "forward" ou "up"

        // On place le début du laser
        laserLine.SetPosition(0, depart);

        Ray ray = new Ray(depart, direction);
        RaycastHit hit;

        // On lance le rayon physique
        // Le "~layersToIgnore" inverse le masque pour dire "Touche tout SAUF ça"
        if (Physics.Raycast(ray, out hit, maxDistance, ~layersToIgnore))
        {
            // On touche un mur
            laserLine.SetPosition(1, hit.point);
        }
        else
        {
            // On ne touche rien, on tire loin
            laserLine.SetPosition(1, depart + (direction * maxDistance));
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