using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6;
    public float horizontalSpeed = 3;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;

    // H�z art�rma/geri alma i�in alanlar
    public KeyCode boostKey = KeyCode.J;
    public KeyCode resetKey = KeyCode.K;
    public float boostAmount = 4f; // J ile art�� miktar� (�r: +4)
    private float originalSpeed;
    private bool isBoosted = false;

    public float jumpForce = 7f;
    public float gravity = 20f;
    private bool isGrounded = true;
    private float verticalVelocity = 0f;
    private float groundY = 0f; // Zemin y�ksekli�i

    void Start()
    {
        groundY = transform.position.y;
        originalSpeed = playerSpeed; // Orijinal h�z� sakla
    }

    void Update()
    {
        // H�z art�rma / s�f�rlama kontrolleri
        if (Input.GetKeyDown(boostKey) && !isBoosted)
        {
            playerSpeed = originalSpeed + boostAmount;
            isBoosted = true;
        }
        if (Input.GetKeyDown(resetKey) && isBoosted)
        {
            playerSpeed = originalSpeed;
            isBoosted = false;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x <= leftLimit) return;
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x >= rightLimit) return;
            transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
        }
        

        // Z�plama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            {
                verticalVelocity = jumpForce;
                isGrounded = false;
            }

            // Yer�ekimi ve dikey hareket
            if (!isGrounded)
            {
                verticalVelocity -= gravity * Time.deltaTime;
                transform.position += Vector3.up * verticalVelocity * Time.deltaTime;

                // Yere inme kontrol�
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
    }
}
