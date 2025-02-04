using IntoTheWilds;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class HealthBarHud : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private UIDocument _uiDocument;

    private Label _healthLabel;
    private VisualElement _healthBarMask;

    [Inject]
    public void Constuct(HealthComponent healthComponent)
    {
        _healthComponent = healthComponent;
    }

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        _healthLabel = _uiDocument.rootVisualElement.Q<Label>("HealthLabel");
        _healthBarMask = _uiDocument.rootVisualElement.Q<VisualElement>("HealthBarMask");

        _healthComponent.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _healthComponent.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        OnHealthChanged(_healthComponent.HealthPoints);
    }

    private void OnHealthChanged(int healthPoints)
    {
        float healthPercent = (float)healthPoints / _healthComponent.MaxHealthPoints * 100;
        _healthLabel.text = $"{healthPercent}%";

        float healthMaskPercent = Mathf.Lerp(8, 92, (float)healthPoints / _healthComponent.MaxHealthPoints);
        _healthBarMask.style.width = Length.Percent(healthMaskPercent);
    }
}
