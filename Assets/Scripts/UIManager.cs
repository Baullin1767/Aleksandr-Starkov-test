using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("Здоровье юнитов")]
    public Slider playerHealthBar;
    public Slider enemyHealthBar;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemyHealthText;

    [Header("Способности игрока")]
    public Transform abilitiesPanel;
    public GameObject abilityButtonPrefab;

    [Header("Статус-эффекты")]
    public Transform playerEffectsPanel;
    public Transform enemyEffectsPanel;
    public GameObject effectIconPrefab;

    [Header("Кнопки")]
    public Button restartButton;

    [Header("Логи")]
    public TextMeshProUGUI logsText;

    private Unit playerUnit;
    private Unit enemyUnit;
    private Dictionary<Ability, Button> abilityButtons = new Dictionary<Ability, Button>();

    private void Start()
    {
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }

    public void SetupUI(Unit player, Unit enemy)
    {
        playerUnit = player;
        enemyUnit = enemy;

        playerHealthBar.maxValue = player.Health;
        enemyHealthBar.maxValue = enemy.Health;

        CreateAbilityButtons();
        UpdateUI();
    }

    private void CreateAbilityButtons()
    {
        foreach (Transform child in abilitiesPanel) Destroy(child.gameObject);
        abilityButtons.Clear();

        foreach (var ability in playerUnit.Abilities)
        {
            GameObject buttonObj = Instantiate(abilityButtonPrefab, abilitiesPanel);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = $"{ability.Name} (КД: {ability.Cooldown})";
            button.onClick.AddListener(() => UseAbility(ability));

            abilityButtons[ability] = button;
        }
    }

    private void UseAbility(Ability ability)
    {
        if (!ability.IsOnCooldown)
        {
            ability.UseAbility(playerUnit, enemyUnit);
            GameManager.Instance.EndTurn();
        }
    }

    public void UpdateUI()
    {
        // Обновляем здоровье
        playerHealthBar.value = playerUnit.Health;
        enemyHealthBar.value = enemyUnit.Health;

        playerHealthText.text = $"Игрок: {playerUnit.Health} HP";
        enemyHealthText.text = $"Противник: {enemyUnit.Health} HP";

        // Обновляем статус-эффекты
        UpdateEffects(playerEffectsPanel, playerUnit.StatusEffects);
        UpdateEffects(enemyEffectsPanel, enemyUnit.StatusEffects);

        // Обновляем кнопки способностей
        foreach (var ability in playerUnit.Abilities)
        {
            if (abilityButtons.ContainsKey(ability))
            {
                Button button = abilityButtons[ability];
                button.interactable = !ability.IsOnCooldown;
                if (ability.IsOnCooldown)
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{ability.Name} (КД: {ability.currentCooldown})";
                else
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{ability.Name} (КД: {ability.Cooldown})";
            }
        }
        logsText.text = GameManager.Instance.logs;
    }

    private void UpdateEffects(Transform effectsPanel, List<StatusEffect> effects)
    {
        foreach (Transform child in effectsPanel) Destroy(child.gameObject);

        foreach (var effect in effects)
        {
            GameObject effectObj = Instantiate(effectIconPrefab, effectsPanel);
            TextMeshProUGUI effectText = effectObj.GetComponentInChildren<TextMeshProUGUI>();
            effectText.text = $"{effect.EffectType} ({effect.Duration})";
        }
    }
}
