using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : Ability
{
    private int damage = 8;

    public AttackAbility() : base("Атака", cooldown: 0, duration: 0) { }

    public override void UseAbility(Unit user, Unit target)
    {
        target.ApplyDamage(damage);
        GameManager.Instance.UpdateLogs($"{user} атаковал {target}, нанеся {damage} урона.");
    }
}
