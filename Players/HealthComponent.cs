using Godot;

public partial class HealthComponent : Node
{
    public int MaxHealth { get; set; } = 6;
    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            EmitSignal(nameof(HealthChanged), _currentHealth);
            if (_currentHealth <= 0)
                EmitSignal(nameof(Died));
        }
    }

    [Signal] public delegate void HealthChangedEventHandler(int currentHealth);
    [Signal] public delegate void DiedEventHandler();

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }
	 public void RestoreFullHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
