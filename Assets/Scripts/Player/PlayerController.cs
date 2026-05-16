using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Pengaturan Pergerakan")]
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 4.0f;
    public float gravity = -9.81f;

    [Header("Pengaturan Kamera")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    [Header("Efek Headbob")]
    public float bobSpeed = 12f;
    public float bobAmount = 0.05f;
    private float defaultPosY = 0;
    private float timer = 0;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
        
        // Menyimpan posisi Y awal kamera untuk titik tengah headbob
        if (playerCamera != null)
        {
            defaultPosY = playerCamera.localPosition.y;
        }
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Panggil fungsi Headbob jika pemain menyentuh tanah
        if (controller.isGrounded)
        {
            HandleHeadBob(x, z, isSprinting);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleHeadBob(float x, float z, bool isSprinting)
    {
        // Cek apakah pemain menekan tombol gerak
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            // Kecepatan ayunan bertambah saat menekan Shift
            timer += Time.deltaTime * (isSprinting ? bobSpeed * 1.3f : bobSpeed);
            playerCamera.localPosition = new Vector3(
                playerCamera.localPosition.x, 
                defaultPosY + Mathf.Sin(timer) * bobAmount, 
                playerCamera.localPosition.z
            );
        }
        else
        {
            // Mengembalikan posisi kamera perlahan ke tengah saat berhenti
            timer = 0;
            playerCamera.localPosition = new Vector3(
                playerCamera.localPosition.x, 
                Mathf.Lerp(playerCamera.localPosition.y, defaultPosY, Time.deltaTime * bobSpeed), 
                playerCamera.localPosition.z
            );
        }
    }
}