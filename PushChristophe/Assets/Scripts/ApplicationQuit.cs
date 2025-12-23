using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    [SerializeField] InputActionReference quitAction;

    void OnEnable()
    {
        quitAction.action.Enable();
        quitAction.action.performed += Quit;
    }

    void OnDisable()
    {
        quitAction.action.performed -= Quit;
        quitAction.action.Disable();
    }

    void Quit(InputAction.CallbackContext ctx)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
