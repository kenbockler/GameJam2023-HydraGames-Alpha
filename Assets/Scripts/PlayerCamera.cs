using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private float fixedY;  // Fikseeritud Y-koordinaat
    private float fixedZ;  // Fikseeritud Z-koordinaat

    // Start is called before the first frame update
    void Start()
    {
        fixedY = transform.position.y;  // Saame algse Y-koordinaadi
        fixedZ = transform.position.z;  // Saame algse Z-koordinaadi
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Fikseerime Y- ja Z-koordinaadi
        smoothedPosition.y = fixedY;
        smoothedPosition.z = fixedZ;

        transform.position = smoothedPosition;
    }
}
