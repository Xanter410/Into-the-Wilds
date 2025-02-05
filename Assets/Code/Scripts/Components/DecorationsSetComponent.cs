using System.Collections.Generic;
using UnityEngine;

public class DecorationsSetComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite _sprite;

    [SerializeField] private bool _isUseRandomSprite;
    [SerializeField] private List<Sprite> _spriteSet;

    private void Start()
    {
        if (_isUseRandomSprite)
        {
            int indexSpriteSet = Random.Range(0, _spriteSet.Count);

            spriteRenderer.sprite = _spriteSet[indexSpriteSet];
        }
        else
        {
            spriteRenderer.sprite = _sprite;
        }
    }
}
