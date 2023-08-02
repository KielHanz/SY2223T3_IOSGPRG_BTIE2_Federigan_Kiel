using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofVisibility : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
    }
}
