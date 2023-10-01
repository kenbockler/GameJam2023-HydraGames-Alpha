using UnityEngine;

public class klikkimine : MonoBehaviour
{
    public GameObject objectToMove; // Objekt, mida liigutada
    public Vector3 directionToMove; // Suund, kuhu objekti liigutada
    public float moveDistance = 1.0f; // Kaugus, mille võrra objekti liigutada

    // Update is called once per frame
    void Update()
    {
        // Kontrollime, kas vasak hiirenupp on vajutatud
        if (Input.GetMouseButtonDown(0))
        {
            // Kontrollime, kas objekt on määratud
            if (objectToMove != null)
            {
                // Liigutame objekti etteantud suunas ja kauguse võrra
                objectToMove.transform.position += directionToMove.normalized * moveDistance;
            }
        }
    }
}
