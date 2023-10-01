using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioClip walkingSoundClip;  // Kõndimise heli
    public AudioClip jumpingSoundClip;  // Hüppamise heli
    private AudioSource audioSource;  // AudioSource komponent
    private bool isWalking = false;  // Kas mängija liigub?
    private bool isGrounded = true;  // Eeldame, et mängija alguses maapinnal (vajab täiendavat loogikat)

    void Start()
    {
        // Leia AudioSource komponent ja seadista see
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Kontrolli, kas mängija liigub (Näiteks: kui "w" klahvi vajutatakse)
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            // Kui mängija liigub ja heli ei mängi, alusta heli
            if (!isWalking)
            {
                isWalking = true;
                if (audioSource && walkingSoundClip)
                {
                    audioSource.clip = walkingSoundClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            // Kui mängija ei liigu, peata heli
            if (isWalking)
            {
                isWalking = false;
                audioSource.Stop();
            }
        }

        // Hüppamise loogika
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Siin peaks olema teie hüppamise loogika, nt. Rigidbody'le jõu lisamine

            // Hüppamise heli esitamine
            if (audioSource && jumpingSoundClip)
            {
                audioSource.PlayOneShot(jumpingSoundClip);
            }
        }
    }
}
