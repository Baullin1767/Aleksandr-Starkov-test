using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Unit
{
    public int Health { get; private set; }
    public List<Ability> Abilities { get; private set; }
    public List<StatusEffect> StatusEffects { get; private set; }
    private int shield = 0; // Для Барьера

    public Unit(int health)
    {
        Health = health;
        Abilities = new List<Ability>();
        StatusEffects = new List<StatusEffect>();
    }

    public void ApplyDamage(int damage)
    {
        if (shield > 0)
        {
            int absorbed = Mathf.Min(shield, damage);
            shield -= absorbed;
            damage -= absorbed;
            GameManager.Instance.UpdateLogs($"Щит поглотил {absorbed} урона. Осталось {shield} щита.");
        }
        if (damage > 0)
        {
            Health = Mathf.Max(0, Health - damage);
            GameManager.Instance.UpdateLogs($"Нанесено {damage} урона. Осталось {Health} HP.");
            if(Health <= 0)
            {
                GameManager.Instance.RestartGame();
                GameManager.Instance.UpdateLogs($"Нанесено {damage} урона. Unit погиб");
            }
        }
    }

    public void ApplyEffect(StatusEffect effect)
    {
        if (effect.EffectType == "Barrier")
        {
            shield = effect.Value;
        }
        else
        {
            StatusEffects.Add(effect);
        }
    }

    public void RemoveNegativeEffects()
    {
        StatusEffects.RemoveAll(effect => effect.EffectType == "Burning");
        GameManager.Instance.UpdateLogs("Негативные эффекты удалены.");
    }

    public void ProcessEffects()
    {
        for (int i = StatusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = StatusEffects[i];

            if (effect.EffectType == "Burning")
            {
                ApplyDamage(effect.Value);
            }
            else if (effect.EffectType == "Regeneration")
            {
                Health += effect.Value;
                GameManager.Instance.UpdateLogs($"Восстановлено {effect.Value} HP.");
            }

            effect.Tick();
            if (effect.IsExpired())
            {
                StatusEffects.RemoveAt(i);
            }
        }
    }
}
