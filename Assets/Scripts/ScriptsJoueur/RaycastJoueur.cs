using UnityEngine;

public class RaycastJoueur : MonoBehaviour
{


    private void Start()
    {
        
    }
    void Update()
    {
        Ray shootRay = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(shootRay, out hit, 10f))
        {
            Debug.Log("J'ai touché " + hit.collider.name);
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);
        }
    }
}
