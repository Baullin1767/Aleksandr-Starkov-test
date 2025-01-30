using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseAbility : Ability
{
    public CleanseAbility() : base("Очищение", cooldown: 5, duration: 0) { }

    public override void UseAbility(Unit user, Unit target)
    {
        user.RemoveNegativeEffects();
        StartCooldown();
        GameManager.Instance.UpdateLogs($"{user} использовал Очищение и снял все негативные эффекты.");
    }
}
