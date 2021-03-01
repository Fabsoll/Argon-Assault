using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xSpeed = 4f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float ySpeed = 3f;
    [SerializeField] float yRange = 2f;
    [SerializeField] float positionPitchFacor = -5f;
    [SerializeField] float controlPitchFacor = -15f;
    [SerializeField] float positionYawFacor = 5f;
    [SerializeField] float controlRollFacor = 15f;

    [SerializeField] GameObject[] guns;

    bool isControlEnabled = true;

    float xThrow, yThrow;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFire();
        }
        
    }

    private void ProcessFire()
    {
        if (Input.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFacor + yThrow * controlPitchFacor;
        float yaw = transform.localPosition.x * positionYawFacor;
        float roll = xThrow * controlRollFacor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float xRawPos = transform.localPosition.x + xOffset;
        float xClampedPos = Mathf.Clamp(xRawPos, -xRange, xRange);

        yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float yRawPos = transform.localPosition.y + yOffset;
        float yClampedPos = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3(xClampedPos, yClampedPos, transform.localPosition.z);
    }

    private void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
}
