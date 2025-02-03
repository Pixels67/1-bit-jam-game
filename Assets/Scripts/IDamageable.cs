public interface IDamageable  {
    public int CurrentHealth { get; }
    public void TakeDamage(uint damage);
}
