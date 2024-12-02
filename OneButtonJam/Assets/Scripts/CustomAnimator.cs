using UnityEngine;
using System.Collections.Generic;

public class CustomAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
    private AnimationData currentAnimationData;
    private string currentAnimation = "idle";
    private int animationFrame;
    private bool isAnimating = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        CancelInvoke(nameof(NextFrame));
    }

    private void Start()
    {
        if (currentAnimationData != null)
        {
            Debug.Log($"Starting animation: {currentAnimation}");
            InvokeRepeating(nameof(NextFrame), currentAnimationData.timeBetweenFrames, currentAnimationData.timeBetweenFrames);
        }
    }

    private void NextFrame()
    {
        if (isAnimating) return;

        isAnimating = true;
        animationFrame++;

        if (currentAnimationData.loop && animationFrame >= currentAnimationData.frames.Count)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < currentAnimationData.frames.Count)
        {
            var frameSprite = currentAnimationData.frames[animationFrame];
            if (frameSprite != null)
            {
                Debug.Log($"Setting sprite for frame {animationFrame}: {frameSprite.name}");
                spriteRenderer.sprite = frameSprite;
            }
            else
            {
                Debug.LogWarning($"Frame {animationFrame} is null.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid frame index detected.");
        }

        isAnimating = false;
    }

    public void SetAnimation(string name)
    {
        if (animations.ContainsKey(name))
        {
            Debug.Log($"Setting animation to: {name}");
            currentAnimation = name;
            currentAnimationData = animations[name];
            animationFrame = 0;
            CancelInvoke(nameof(NextFrame));
            InvokeRepeating(nameof(NextFrame), currentAnimationData.timeBetweenFrames, currentAnimationData.timeBetweenFrames);
        }
        else
        {
            Debug.LogWarning($"Animation {name} not found in animations dictionary.");
        }
    }

    public void AddAnimation(string name, AnimationData animationData)
    {
        if (!animations.ContainsKey(name))
        {
            Debug.Log($"Adding animation: {name} with {animationData.frames.Count} frames");
            animations.Add(name, animationData);
        }
    }
}
