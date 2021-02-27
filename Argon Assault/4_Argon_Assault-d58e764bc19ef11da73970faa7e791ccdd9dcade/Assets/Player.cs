using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float xSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float xRawPos = transform.localPosition.x + xOffset;

        transform.localPosition = new Vector3(xRawPos, transform.localPosition.y, transform.localPosition.z);

    }
}