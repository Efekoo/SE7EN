using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossVisibilityController : MonoBehaviour
{
    public SpriteRenderer[] renderers;
    public Light2D[] lights;
    public bool startHidden = true;

    bool revealed;
    public bool IsRevealed => revealed;

    void Awake()
    {
        if (renderers == null || renderers.Length == 0)
        {
            renderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        if (lights == null || lights.Length == 0)
        {
            lights = GetComponentsInChildren<Light2D>(true);
        }

        if (startHidden)
        {
            SetVisible(false);
        }
    }

    public void Reveal()
    {
        if (revealed)
        {
            return;
        }

        revealed = true;
        SetVisible(true);
    }

    void SetVisible(bool visible)
    {
        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] != null)
                {
                    renderers[i].enabled = visible;
                }
            }
        }

        if (lights != null)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null)
                {
                    lights[i].enabled = visible;
                }
            }
        }
    }
}
