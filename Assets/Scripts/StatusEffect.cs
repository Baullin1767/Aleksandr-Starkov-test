public class StatusEffect
{
    public string EffectType { get; private set; }
    public int Duration { get; private set; }
    public int Value { get; private set; } // Урон от горения или лечение от регенерации

    public StatusEffect(string effectType, int duration, int value = 0)
    {
        EffectType = effectType;
        Duration = duration;
        Value = value;
    }

    public void Tick()
    {
        Duration--;
    }

    public bool IsExpired()
    {
        return Duration <= 0;
    }
}
