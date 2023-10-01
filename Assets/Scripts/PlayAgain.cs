using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAigain : MonoBehaviour
{
    // Stseeni nimi, kuhu soovite liikuda
    public string sceneToLoad = "NewSceneName";

    // Update on called once per frame
    private void Update()
    {
        // Kontrollige, kas hiireklõps on toimunud
        if (Input.GetMouseButtonDown(0))
        {
            // Leia, kuhu kasutaja klikkis
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // Kui klikiti sellel objektil
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                // Lae uus stseen
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
