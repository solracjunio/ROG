using Obvious.Soap;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FloatVariable _currentHealth;
    [SerializeField] private FloatVariable _maxHealth;

    [SerializeField] private ScriptableEventInt _onPlayerDamaged;
    [SerializeField] private ScriptableEventInt _onPlayerHealed;
    [SerializeField] private ScriptableEventNoParam _onPlayerDeath;

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
            if (_currentHealth <= 0)
                _onPlayerDeath.Raise();
            else
                _onPlayerDamaged.Raise(Mathf.Abs(Mathf.RoundToInt(diff)));
        }
        else if (diff > 0)
        {
            _onPlayerHealed.Raise(Mathf.RoundToInt(diff));
        }
    }

    public void TakeDamage(int amount)
    {
        _currentHealth.Value -= amount;
    }
}