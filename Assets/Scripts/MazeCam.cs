using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCamera : MonoBehaviour
{
    public Transform player;
    public GameObject lightMask;  // Valguse/salap‰ra mask
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private float fixedZ;  // Fikseeritud Z-koordinaat

    // Start is called before the first frame update
    void Start()
    {
        fixedZ = transform.position.z;  // Saame algse Z-koordinaadi
        Camera.main.orthographicSize = 5;  // Kaamera l‰hemale (n‰idisv‰‰rtus)

        CreateLightMask();
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Fikseerime Y- ja Z-koordinaadi
        smoothedPosition.z = fixedZ;

        transform.position = smoothedPosition;

        // Liigutame valgusmaski kaameraga kaasa
        if (lightMask != null)
        {
            lightMask.transform.position = new Vector3(transform.position.x, transform.position.y, lightMask.transform.position.z);
        }
    }

    void CreateLightMask()
    {
        if (lightMask == null) return;

        int textureSize = 256;
        Texture2D tex = new Texture2D(textureSize, textureSize);

        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float distanceFromCenter = Vector2.Distance(new Vector2(x, y), new Vector2(textureSize / 2, textureSize / 2));
                float maxDistance = textureSize / 2;

                // Teeme enamus alast kottpimedaks
                float alpha = 1f; // alustame maksimaalse alpha v‰‰rtusega

                // Luua v‰ike ava keskel
                if (distanceFromCenter < maxDistance * 0.1f) // keskel on ava, mis on 10% maksimaalsest kaugusest
                {
                    alpha = 0f; // Ava on l‰bipaistev
                }

                tex.SetPixel(x, y, new Color(0f, 0f, 0f, alpha));
            }
        }

        tex.Apply();

        Rect rect = new Rect(0, 0, textureSize, textureSize);
        Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
        lightMask.GetComponent<SpriteRenderer>().sprite = sprite;
    }







}
