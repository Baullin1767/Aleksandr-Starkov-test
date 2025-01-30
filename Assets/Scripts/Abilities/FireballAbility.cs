using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : Ability
{
    private int initialDamage = 5;
    private int burnDamage = 1;

    public FireballAbility() : base("Огненный шар", cooldown: 6, duration: 5) { }

    public override void UseAbility(Unit user, Unit target)
    {
        target.ApplyDamage(initialDamage);
        target.ApplyEffect(new StatusEffect("Burning", Duration, burnDamage));
        StartCooldown();
        GameManager.Instance.UpdateLogs($"{user} запустил Огненный шар в {target}, нанося {initialDamage} урона + горение {burnDamage} на {Duration} ходов.");
    }
}
