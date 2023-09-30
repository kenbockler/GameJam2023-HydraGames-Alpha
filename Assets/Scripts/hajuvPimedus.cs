using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hajuvPimedus : MonoBehaviour
{
    public int textureSize = 256;
    public Image image;

    void Start()
    {
        Texture2D tex = new Texture2D(textureSize, textureSize);

        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                float alpha = Mathf.Max(0f, 1f - Vector2.Distance(new Vector2(x, y), new Vector2(textureSize / 2, textureSize / 2)) / (textureSize / 2));
                tex.SetPixel(x, y, new Color(0f, 0f, 0f, alpha));
            }
        }

        tex.Apply();

        Rect rect = new Rect(0, 0, textureSize, textureSize);
        image.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
    }
}
