using UnityEngine;
using TMPro; // Important pour manipuler tes objets TextMeshPro

public class GestionTexte : MonoBehaviour
{
    public static GestionTexte instance;

    public TextMeshProUGUI texteDefi;  // Ton enfant "Defis"
    public TextMeshProUGUI texteScore; // Ton enfant "Score"

    private int scoreActuel = 0;

    void Awake()
    {
        // Permet au script TriggerCandle de trouver le score facilement
        instance = this;
    }

    void Start()
    {
        // 1. On cache le texte Défi au lancement
        texteDefi.gameObject.SetActive(false);

        // 2. On affiche le score à zéro au début
        MettreAJourInterface();

        // 3. On lance l'apparition du défi après 3 secondes
        Invoke("AfficherDefi", 3f);
    }

    void AfficherDefi()
    {
        texteDefi.gameObject.SetActive(true);
    }

    // Cette fonction sera appelée par tes bougies
    public void AjouterPoint()
    {
        scoreActuel++;
        MettreAJourInterface();
    }

    void MettreAJourInterface()
    {
        texteScore.text = "Score : " + scoreActuel;
    }
}