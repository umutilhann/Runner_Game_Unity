using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hýz Ayarlarý")]
    public float playerSpeed = 6;
    public float horizontalSpeed = 3;

    [Header("Sýnýrlar")]
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    [Header("Zýplama")]
    public float jumpForce = 7f;
    public float gravity = 20f;
    private bool isGrounded = true;
    private float verticalVelocity = 0f;
    private float groundY = 0f;

    [Header("Yetenek Durumlarý")]
    public bool hasDoubleJump = false;
    private bool canDoubleJump = false;

    void Start()
    {
        groundY = transform.position.y;

        LoadUpgrades();

        StartCoroutine(IncrementSpeedOverTime(10f, 0.5f));
    }

    void LoadUpgrades()
    {
        // 1. HIZ KONTROLÜ
        if (PlayerPrefs.GetInt("HasSpeed", 0) == 1)
        {
            // Sadece hýzý arttýrýyoruz, kameraya dokunmuyoruz.
            playerSpeed += 4.5f;
        }

        // 2. ÇÝFT ZIPLAMA
        hasDoubleJump = (PlayerPrefs.GetInt("HasDoubleJump", 0) == 1);
    }

    void Update()
    {
        // Ýleri Hareket
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Saða Sola Hareket
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
                transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed, Space.World);
        }

        // Zýplama
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                verticalVelocity = jumpForce;
                isGrounded = false;
                canDoubleJump = true;
            }
            else if (hasDoubleJump && canDoubleJump)
            {
                verticalVelocity = jumpForce;
                canDoubleJump = false;
            }
        }

        // Yerçekimi Uygulama
        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            transform.position += Vector3.up * verticalVelocity * Time.deltaTime;

            if (transform.position.y <= groundY)
            {
                Vector3 pos = transform.position;
                pos.y = groundY;
                transform.position = pos;
                verticalVelocity = 0f;
                isGrounded = true;
            }
        }
    }

    // Zamanla hýzlanma (Oyun zorlaþsýn diye)
    IEnumerator IncrementSpeedOverTime(float intervalSeconds, float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSeconds);
            playerSpeed += amount;
        }
    }
}