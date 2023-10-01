using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    public AudioClip touchSoundClip;  // Puudutamise heli
    private AudioSource audioSource;  // AudioSource komponent

    void Start()
    {
        // Leia AudioSource komponent ja seadista see
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kontrollime, kas üks objektidest on "Player" ja teine "TriggerObject"
        if (gameObject.tag == "Player" && other.gameObject.tag == "TriggerObject" || 
            gameObject.tag == "TriggerObject" && other.gameObject.tag == "Player")
        {
            // Mängime heli
            if (audioSource && touchSoundClip)
            {
                audioSource.PlayOneShot(touchSoundClip);
            }
        }
    }
}
