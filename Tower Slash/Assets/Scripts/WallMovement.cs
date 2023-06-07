using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float scrollSpeed;
    private SpriteRenderer spriteRenderer;
    private float offset;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        spriteRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
