using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Camera")] [SerializeField] public Camera cam;
    [Header("Movement")]
    [SerializeField] private float camSensitivity;
    [SerializeField] private float moveSensitivity;
    [SerializeField] private float gravity = -9.81f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference zqsd;
    [SerializeField] private InputActionReference mouseMovement;
    [SerializeField] private InputActionReference fire;
    [SerializeField] private InputActionReference jump;
    
    [Header("GroundCheck")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundCheckMask;



    [Header("Weapon System")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private GameObject weaponPrefab;
  
    
   

    private GameManager gameManager;
    private GameObject equippedWeapon;
    

    public CharacterController controller;
    private float rotationX = 0.0f;
    private bool isGrounded = false;
    private Vector3 velocity = Vector3.zero;
    private int vie = 100;
    public int MaxVie = 100;
    public event Action OnHealthChange;
    public float lastDamageTime = 0f;
    public float damageCooldown = 1.0f; // 1 seconde d'attente entre chaque dégât
    public float Jump = 5f;

    //---------------------------------- faire un OnHealthChange?.Invoke() ----------------------------------
    // On perd 8 de vie à chaque collision //

    public void RecevoirDegats()
    {
        if (Time.time > lastDamageTime + damageCooldown)
        {
            this.vie -= 8;

            // Le debug pour être SÛR que ça marche
            Debug.Log("Aïe ! Vie restante : " + vie);

            // Mise à jour de l'UI
            OnHealthChange.Invoke();
            lastDamageTime = Time.time;

            if (vie <= 0)
            {
                Mourir();
            }
        }
    }


    void Mourir()
{
    Debug.Log("Le joueur est mort !");
    if (gameManager != null)
    {
        gameManager.GameOver();
    }
}

    public int GetVie()
    {
        return this.vie;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (zqsd)
        {
            zqsd.action.Enable();
        }

        if (mouseMovement)
        {
            mouseMovement.action.Enable();
        }

        if (jump)
        {
            jump.action.Enable();
        }

        if (fire)
        {
            fire.action.performed += FirePressed;
            fire.action.Enable();
        }

        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;


        if (weaponPrefab && weaponHolder)
        {
            EquipWeapon();
        }

        gameManager = FindFirstObjectByType<GameManager>();

    }


    private void EquipWeapon()
    {
        if (equippedWeapon != null)
            Destroy(equippedWeapon);

        equippedWeapon = Instantiate(weaponPrefab, weaponHolder);

        // S�assurer que l�arme est bien align�e dans la main
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;
    }


    private void FirePressed(InputAction.CallbackContext ctx)
    {
        if (equippedWeapon == null)
        {
            //Debug.LogWarning("Pas d'arme �quip�e !");
            return;
        }

        BowShoot bow = equippedWeapon.GetComponent<BowShoot>();
        if (bow == null)
        {
            //Debug.LogWarning("L'arme �quip�e n'a pas de script BowShoot !");
            return;
        }

        bow.Shoot();
    }


    /*
    private void FirePressed(InputAction.CallbackContext obj)
    {   
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
        }
    }
    */

    // Update is called once per frame

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundCheckMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float mouseX = mouseMovement.action.ReadValue<Vector2>().x * camSensitivity * Time.deltaTime;
        float mouseY = mouseMovement.action.ReadValue<Vector2>().y * camSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
        
        Vector2 zqsdValue = zqsd.action.ReadValue<Vector2>();


        controller.Move(transform.TransformDirection(new Vector3(zqsdValue.x, 0, zqsdValue.y)).normalized * moveSensitivity * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }


}