using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierAbility : Ability
{
    private int shieldAmount = 5;

    public BarrierAbility() : base("Барьер", cooldown: 4, duration: 2) { }

    public override void UseAbility(Unit user, Unit target)
    {
        user.ApplyEffect(new StatusEffect("Barrier", Duration, shieldAmount));
        StartCooldown();
        GameManager.Instance.UpdateLogs($"{user} активировал Барьер, поглощающий {shieldAmount} урона на {Duration} хода.");
    }
}
