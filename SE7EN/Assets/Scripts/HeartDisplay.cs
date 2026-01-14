using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public HealthManager healthManager;
    public Sprite heartSprite;
    public Vector2 startOffset = new Vector2(20f, -20f);
    public float spacing = 36f;
    public Vector2 size = new Vector2(32f, 32f);

    RectTransform container;
    readonly List<Image> hearts = new List<Image>();
    int lastHealth = -1;

    void Awake()
    {
        if (healthManager == null)
        {
            healthManager = FindAnyObjectByType<HealthManager>();
        }

        EnsureContainer();
    }

    void Update()
    {
        if (healthManager == null || heartSprite == null || container == null)
        {
            return;
        }

        if (healthManager.health != lastHealth)
        {
            BuildHearts(healthManager.health);
        }
    }

    void EnsureContainer()
    {
        if (container != null)
        {
            return;
        }

        GameObject holder = new GameObject("HeartContainer", typeof(RectTransform));
        holder.transform.SetParent(transform, false);
        container = holder.GetComponent<RectTransform>();
        container.anchorMin = new Vector2(0f, 1f);
        container.anchorMax = new Vector2(0f, 1f);
        container.pivot = new Vector2(0f, 1f);
        container.anchoredPosition = startOffset;
        container.sizeDelta = Vector2.zero;
    }

    void BuildHearts(int count)
    {
        foreach (Image image in hearts)
        {
            if (image != null)
            {
                Destroy(image.gameObject);
            }
        }

        hearts.Clear();
        lastHealth = count;

        for (int i = 0; i < count; i++)
        {
            GameObject heart = new GameObject("Heart", typeof(RectTransform), typeof(Image));
            heart.transform.SetParent(container, false);

            RectTransform rect = heart.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.anchoredPosition = new Vector2(i * spacing, 0f);
            rect.sizeDelta = size;

            Image image = heart.GetComponent<Image>();
            image.sprite = heartSprite;
            image.preserveAspect = true;

            hearts.Add(image);
        }
    }
}
