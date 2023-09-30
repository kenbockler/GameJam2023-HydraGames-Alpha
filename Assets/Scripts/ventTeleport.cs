using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ventTeleport : MonoBehaviour
{
    public string sceneToLoad; // Nimi stseenist, mida laadida

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kontrolli, kas kokkupuude toimus mängija objektiga
        if (other.tag == "Player")
        {
            // Lae uus stseen
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
