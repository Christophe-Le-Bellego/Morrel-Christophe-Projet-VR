using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage; // Glisse l'image "Fill" ici
    private Transform cam;

    void Start()
    {
        // On trouve la caméra principale automatiquement
        cam = Camera.main.transform;
    }

    // LateUpdate est mieux pour suivre la caméra sans tremblements
    void LateUpdate()
    {
        // BILLBOARD : La barre regarde toujours vers la caméra
        transform.LookAt(transform.position + cam.forward);
    }

    public void UpdateHealthBar(float vie, float maxHealth)
    {
        // Le calcul de pourcentage (entre 0 et 1)
        fillImage.fillAmount = vie / maxHealth;
    }
}