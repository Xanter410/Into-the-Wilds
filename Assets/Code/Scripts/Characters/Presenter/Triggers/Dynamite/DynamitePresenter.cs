using UnityEngine;

public class DynamitePresenter : MonoBehaviour
{
    [SerializeField] private float _timeBeforeExplosion = 5f;

    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _minFlashFrequency = 0.5f;
    [SerializeField] private float _maxFlashFrequency = 10f;

    [SerializeField] GameObject _explosionTrigger;

    private Animator _animator;
    private Material _material;

    private float _initialExplosionTime;
    private float _flashTimer;
    private bool _isPlaying;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _isPlaying = true;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        _material = spriteRenderer.material;
        _material.SetColor("_FlashColor", _flashColor);

        _initialExplosionTime = _timeBeforeExplosion;
    }
    private void Update()
    {

        if (_timeBeforeExplosion > 0)
        {
            UpdateFlashEffect();
            _timeBeforeExplosion -= Time.deltaTime;
        }
        else if (_isPlaying == true)
        {
            Explode();
        }
    }
    private void UpdateFlashEffect()
    {
        float explosionProgress = 1f - (_timeBeforeExplosion / _initialExplosionTime);

        float currentFrequency = Mathf.Lerp(_minFlashFrequency, _maxFlashFrequency, explosionProgress);

        _flashTimer += Time.deltaTime * currentFrequency;

        float flashAmount = (Mathf.Sin(_flashTimer * Mathf.PI * 2f) + 1f) * 0.5f;

        _material.SetFloat("_FlashAmount", flashAmount);
    }
    private void Explode()
    {
        _material.SetFloat("_FlashAmount", 0);

        _isPlaying = false;
        _explosionTrigger.SetActive(true);
        _animator.SetTrigger("boom");
    }
    public void DestroyDynamite()
    {
        Destroy(gameObject);
    }
}
