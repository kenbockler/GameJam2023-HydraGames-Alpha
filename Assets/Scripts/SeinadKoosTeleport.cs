using UnityEngine;
using UnityEngine.SceneManagement;

public class SeinadKoosTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kontrollime, kas üks objektidest on "sein" ja teine "sein2"
        if (gameObject.tag == "sein" && other.gameObject.tag == "sein2" || 
            gameObject.tag == "sein2" && other.gameObject.tag == "sein")
        {
            // Laeme uue stseeni (asendage "NewSceneName" teie soovitud stseeni nimega)
            SceneManager.LoadScene("end");
        }
    }
}
