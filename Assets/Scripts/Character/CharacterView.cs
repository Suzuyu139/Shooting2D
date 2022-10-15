using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _renderers = null;

    public void SetSpriteRenderersAlpha(float alpha)
    {
        for(int i = 0; i < _renderers.Length; i++)
        {
            var color = _renderers[i].color;
            color.a = alpha;
            _renderers[i].color = color;
        }
    }
}
