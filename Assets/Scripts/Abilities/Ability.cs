using UnityEngine;
public abstract class Ability
{
    public string Name { get; protected set; }
    public int Cooldown { get; protected set; }
    public int Duration { get; protected set; }
    public bool IsOnCooldown => currentCooldown > 0;

    public int currentCooldown; // Таймер перезарядки

    protected Ability(string name, int cooldown, int duration)
    {
        Name = name;
        Cooldown = cooldown;
        Duration = duration;
        currentCooldown = 0;
    }

    public abstract void UseAbility(Unit user, Unit target);

    public void StartCooldown()
    {
        currentCooldown = Cooldown;
    }

    public void ReduceCooldown()
    {
        if (currentCooldown > 0)
        {
            currentCooldown--;
        }
    }
}
