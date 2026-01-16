using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    public AudioSource baseMusicSource;
    public AudioSource bossMusicSource;
    public BossVisibilityController visibilityController;

    bool switched;

    void Awake()
    {
        if (bossMusicSource != null)
        {
            bossMusicSource.loop = true;
            bossMusicSource.playOnAwake = false;
        }
    }

    void Update()
    {
        if (switched)
        {
            return;
        }

        if (visibilityController == null)
        {
            visibilityController = FindAnyObjectByType<BossVisibilityController>();
        }

        if (visibilityController != null && visibilityController.IsRevealed)
        {
            SwitchToBossMusic();
        }
    }

    void SwitchToBossMusic()
    {
        switched = true;
        if (baseMusicSource != null)
        {
            baseMusicSource.Stop();
        }

        if (bossMusicSource != null)
        {
            bossMusicSource.Play();
        }
    }
}
