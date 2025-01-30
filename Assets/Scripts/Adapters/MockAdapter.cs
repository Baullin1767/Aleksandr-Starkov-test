using System.Collections.Generic;
using UnityEngine;

public class MockAdapter : IAdapter
{
    private Unit playerUnit;
    private Unit enemyUnit;
    private UIManager uiManager;

    public void InitializeGame()
    {
        playerUnit = new Unit(100);
        enemyUnit = new Unit(100);

        playerUnit.Abilities.Add(new AttackAbility());
        playerUnit.Abilities.Add(new BarrierAbility());
        playerUnit.Abilities.Add(new RegenerationAbility());
        playerUnit.Abilities.Add(new FireballAbility());
        playerUnit.Abilities.Add(new CleanseAbility());

        enemyUnit.Abilities.Add(new AttackAbility());
        enemyUnit.Abilities.Add(new BarrierAbility());
        enemyUnit.Abilities.Add(new RegenerationAbility());
        enemyUnit.Abilities.Add(new FireballAbility());
        enemyUnit.Abilities.Add(new CleanseAbility());

        uiManager = GameObject.FindObjectOfType<UIManager>();
        uiManager.SetupUI(playerUnit, enemyUnit);

        GameManager.Instance.UpdateLogs("Игра инициализирована.");
    }

    public void ProcessTurn()
    {
        playerUnit.ProcessEffects();
        enemyUnit.ProcessEffects();

        Ability enemyAbility = ChooseRandomAbility(enemyUnit);
        if (enemyAbility != null)
        {
            enemyAbility.UseAbility(enemyUnit, playerUnit);
        }

        ReduceCooldowns(playerUnit);
        ReduceCooldowns(enemyUnit);
        uiManager.UpdateUI();
    }

    private Ability ChooseRandomAbility(Unit unit)
    {
        List<Ability> availableAbilities = unit.Abilities.FindAll(a => !a.IsOnCooldown);
        if (availableAbilities.Count > 0)
        {
            return availableAbilities[Random.Range(0, availableAbilities.Count)];
        }
        return null;
    }

    private void ReduceCooldowns(Unit unit)
    {
        foreach (var ability in unit.Abilities)
        {
            ability.ReduceCooldown();
        }
    }

    public void RestartGame()
    {
        InitializeGame();
    }
}
