using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Et laadida uus stseen

public class PlayerMazeTeleport : MonoBehaviour
{
    // OnTriggerExit2D aktiveerub, kui objekt lahkub triggerist
    private void OnTriggerExit2D(Collider2D other)
    {
        // Kontrollime, kas lahkunud objekt on taust
        if (other.tag == "Background")
        {
            // Lae uus stseen
            SceneManager.LoadScene("Palat");
        }
    }
}
