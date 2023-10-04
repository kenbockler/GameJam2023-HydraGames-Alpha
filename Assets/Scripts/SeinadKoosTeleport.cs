using UnityEngine;
using UnityEngine.SceneManagement;

public class SeinadKoosTeleport : MonoBehaviour
{
    public float xThreshold = -26.9168f;  // Määrake x-koordinaadi lävendväärtus

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Logime objekti, mis puudutati
        Debug.Log("Puudutatud objekt: " + collision.gameObject.tag);

        // Kontrollime, kas objekti x-koordinaat on suurem või võrdne kui lävend
        if (this.transform.position.x >= xThreshold)
        {
            // Logime sündmuse
            Debug.Log("X-koordinaat on suurem või võrdne kui lävend, laadin steeni 'end'");

            // Laen stseeni nimega "end"
            SceneManager.LoadScene("end");
        }
        else
        {
            // Kui objekti x-koordinaat ei ole suurem või võrdne kui lävend,
            // siis võite siin teha midagi muud.
        }
    }
}
