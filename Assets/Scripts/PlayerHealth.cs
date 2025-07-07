using Obvious.Soap;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;

    void Start()
    {
        _currentHealth.Value = _maxHealth.Value;
        _currentHealth.OnValueChanged += OnHealthChanged;
    }

    void OnDestroy()
    {
        _currentHealth.OnValueChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float newValue)
    {
        var diff = newValue - _currentHealth.PreviousValue;

        if (diff < 0)
        {
            Debug.Log($"Player took damage: {-diff}");
        }
        else if (diff > 0)
        {
            Debug.Log($"Player healed: {diff}");
        }
    }
}