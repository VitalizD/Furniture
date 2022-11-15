using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
public class SpriteManager : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer { get; set; }
    private Image Image { get; set; }
    private Animation Animation { get; set; }

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Image = GetComponent<Image>();
        Animation = GetComponent<Animation>();
    }

    public void Play(string animationName)
    {
        Animation.Play(animationName);
    }

    public void SetNextSprite(Sprite sprite)
    {
        if (SpriteRenderer != null)
            SpriteRenderer.sprite = sprite;
    }

    public void SetNextImage(Sprite sprite)
    {
        if (Image != null)
            Image.sprite = sprite;
    }
}