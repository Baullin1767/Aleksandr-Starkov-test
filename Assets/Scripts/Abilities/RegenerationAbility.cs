using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationAbility : Ability
{
    private int healPerTurn = 2;

    public RegenerationAbility() : base("Регенерация", cooldown: 5, duration: 3) { }

    public override void UseAbility(Unit user, Unit target)
    {
        user.ApplyEffect(new StatusEffect("Regeneration", Duration, healPerTurn));
        StartCooldown();
        GameManager.Instance.UpdateLogs($"{user} активировал Регенерацию, восстанавливая {healPerTurn} HP за ход на {Duration} хода.");
    }
}
