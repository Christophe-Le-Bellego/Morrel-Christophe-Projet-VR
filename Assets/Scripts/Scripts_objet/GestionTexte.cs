using UnityEngine;
using TMPro;

public class GestionTexte : MonoBehaviour
{
    public static GestionTexte instance;

    [Header("UI Textes")]
    public TextMeshProUGUI texteBougies;
    public TextMeshProUGUI texteZombie;
    public TextMeshProUGUI texteScore;
    public TextMeshProUGUI texteVictoire;

    private int scoreBougies = 0;
    private int scoreZombies = 0;
    private int etapeJeu = 1;

    void Awake() { instance = this; }

    void Start()
    {
        // On cache tout au début sauf le score
        texteBougies.gameObject.SetActive(false);
        texteZombie.gameObject.SetActive(false);
        texteVictoire.gameObject.SetActive(false);

        ActualiserInterface();
        Invoke("AfficherPremierDefi", 2f);
    }

    void AfficherPremierDefi()
    {
        texteBougies.gameObject.SetActive(true);
    }

    public void AjouterBougie()
    {
        if (etapeJeu == 1)
        {
            scoreBougies++;
            ActualiserInterface();
            if (scoreBougies >= 5) PasserAuDefiSuivant();
        }
    }

    public void AjouterMortZombie()
    {
        if (etapeJeu == 2)
        {
            scoreZombies++;
            ActualiserInterface();
            if (scoreZombies >= 5) VictoireFinale();
        }
    }

    void PasserAuDefiSuivant()
    {
        etapeJeu = 2; // On passe officiellement à l'étape 2

        // 1. Gestion des textes de Mission
        texteBougies.gameObject.SetActive(false); // Disparition texte "Bougies"
        texteZombie.gameObject.SetActive(true);    // Apparition texte "Zombies"

        // 2. Mise à jour immédiate du score à l'écran
        // On appelle ActualiserInterface() pour que "Bougies : 5/5" 
        // soit remplacé par "Zombies tués : 0/5"
        ActualiserInterface();

        // 3. Message de transition
        texteVictoire.gameObject.SetActive(true);
        texteVictoire.text = "Bravo ! Maintenant, survivez...";

        Invoke("CacherMessageVictoire", 3f);
    }

    void CacherMessageVictoire()
    {
        texteVictoire.gameObject.SetActive(false);
    }

    void ActualiserInterface()
    {
        // Cette fonction gère quel score afficher selon l'étape
        if (etapeJeu == 1)
        {
            texteScore.text = "Bougies : " + scoreBougies + " / 5";
        }
        else
        {
            texteScore.text = "Zombies tués : " + scoreZombies + " / 5";
        }
    }

    void VictoireFinale()
    {
        texteZombie.gameObject.SetActive(false);
        texteScore.gameObject.SetActive(false); // Optionnel : on cache le score à la fin
        texteVictoire.gameObject.SetActive(true);
        texteVictoire.text = "CIMETIÈRE PURIFIÉ !";
    }
}