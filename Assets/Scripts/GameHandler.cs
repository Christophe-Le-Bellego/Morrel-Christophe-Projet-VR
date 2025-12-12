using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerController PlayerControl;
    public UIDocument UIDoc;

    private Label m_HealthLabel;
    private VisualElement m_HealthBarMask;

    private void Start()
    {
        PlayerControl.OnHealthChange += HealthChanged;
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("Vie");
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("Masque");

        HealthChanged();
    }


    void HealthChanged()
    {
        m_HealthLabel.text = $"{PlayerControl.vie}/{PlayerControl.MaxVie}";
        float healthRatio = (float)PlayerControl.vie / PlayerControl.MaxVie;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}// Il faudra utiliser  Length.Percent pour definir une valeur (entre 0 et 100)