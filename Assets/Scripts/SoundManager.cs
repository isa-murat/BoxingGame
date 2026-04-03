using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Saldiri Sesleri")]
    public AudioClip[] punchSounds;

    [Header("Bloklama Sesi")]
    public AudioClip blockSound;

    [Header("Raund Zili")]
    public AudioClip bellSound;

    [Header("Kazanma Sesi")]
    public AudioClip crowdCheer;

    [Header("Knockout Sesi")]
    public AudioClip knockoutSound;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayPunch()
    {
        if (punchSounds.Length > 0)
        {
            AudioClip clip = punchSounds[Random.Range(0, punchSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayBlock()
    {
        if (blockSound != null)
            audioSource.PlayOneShot(blockSound);
    }

    public void PlayBell()
    {
        if (bellSound != null)
            audioSource.PlayOneShot(bellSound);
    }

    public void PlayCrowdCheer()
    {
        if (crowdCheer != null)
            audioSource.PlayOneShot(crowdCheer);
    }

    public void PlayKnockout()
    {
        if (knockoutSound != null)
            audioSource.PlayOneShot(knockoutSound);
    }
}
