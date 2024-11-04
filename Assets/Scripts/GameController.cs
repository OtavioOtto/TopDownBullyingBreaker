using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [Header("People")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyAura;

    [Header("Health Bar")]
    [SerializeField] private Slider playerHealth;
    [SerializeField] private Slider enemyHealth;

    [Header("Character Selection")]
    [SerializeField] private GameObject player1Selection;
    [SerializeField] private GameObject player2Selection;
    [SerializeField] private GameObject enemySelection;

    [Header("End Screens")]
    [SerializeField] private GameObject enemyDefeated;
    [SerializeField] private GameObject enemyConverted;
    [SerializeField] private GameObject enemyWon;

    [Header("Player Actions")]
    [SerializeField] private float player1AttackValue;
    [SerializeField] private float player1EmpathyValue;
    [SerializeField] private float player2AttackValue;
    [SerializeField] private float player2EmpathyValue;

    [Header("Action Buttons")]
    [SerializeField] private Button attackButton;
    [SerializeField] private Button empathyButton;
    [SerializeField] private Button itemButton;

    [Header("Action Buttons Text")]
    [SerializeField] private GameObject attackButtonTxt;
    [SerializeField] private GameObject empathyButtonTxt;
    [SerializeField] private GameObject itemButtonTxt;

    [Header("GameObject Buttons")]
    [SerializeField] private GameObject attackButtonGM;
    [SerializeField] private GameObject empathyButtonGM;
    [SerializeField] private GameObject itemButtonGM;
    [SerializeField] private GameObject nextSceneEnemyDefeatedGM;
    [SerializeField] private GameObject nextSceneEnemyConvertedGM;
    [SerializeField] private GameObject resetSceneGM;

    [Header("Buttons Attack")]
    [SerializeField] private Button firstAttackButton;
    [SerializeField] private GameObject firstAttackButtonTxt;
    [SerializeField] private Button secondAttackButton;
    [SerializeField] private GameObject secondAttackButtonTxt;

    [Header("Buttons Empathy")]
    [SerializeField] private Button firstEmpathyButton;
    [SerializeField] private GameObject firstEmpathyButtonTxt;
    [SerializeField] private Button secondEmpathyButton;
    [SerializeField] private GameObject secondEmpathyButtonTxt;

    [Header("Buttons Items")]
    [SerializeField] private Button firstitemButton;
    [SerializeField] private Button secondItemButton;
    [SerializeField] private Button thirdItemButton;

    [Header("Buttons Groups")]
    [SerializeField] private GameObject itemOptions;
    [SerializeField] private GameObject empathyOptions;
    [SerializeField] private GameObject attackOptions;
    [SerializeField] private GameObject defaultOptions;

    [Header("Enemy Stats")]
    [SerializeField] private float enemyAttackValue = 0.25f;
    [SerializeField] private float enemyHealthValue = 1.0f;

    [Header("Items")]
    [SerializeField] private float itemHealValue = 0.10f;

    private List<GameObject> turnOrder;
    private int currentTurnIndex;
    private float menu;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        InitializeGame();
    }
    private void Update()
    {
        menu = Input.GetAxis("MENU");
        Menu();
        SetButtonTexts();
        ReturnToDefaultButtons();

    }

    private void InitializeGame()
    {
        turnOrder = new List<GameObject> { player1, player2, enemy };
        currentTurnIndex = 0;
        enemyHealth.maxValue = enemyHealthValue;
        enemyHealth.value = enemyHealthValue;
        UpdateTurn();
    }

    private void UpdateTurn()
    {
        DeactivateSelections();
        DisableActionButtons();

        if (turnOrder[currentTurnIndex] == player1)
        {
            ActivatePlayer1Turn();
            EventSystem.current.SetSelectedGameObject(attackButtonGM);
        }
        else if (turnOrder[currentTurnIndex] == player2)
        {
            ActivatePlayer2Turn();
            EventSystem.current.SetSelectedGameObject(attackButtonGM);
        }
        else if (turnOrder[currentTurnIndex] == enemy)
        {
            ActivateEnemyTurn();
        }
    }

    private void DeactivateSelections()
    {
        player1Selection.SetActive(false);
        player2Selection.SetActive(false);
        enemySelection.SetActive(false);
    }

    private void DisableActionButtons()
    {
        attackButton.interactable = false;
        empathyButton.interactable = false;
        itemButton.interactable = false;
    }

    private void ActivatePlayer1Turn()
    {
        player1Selection.SetActive(true);
        EnableActionButtons();
    }

    private void ActivatePlayer2Turn()
    {
        player2Selection.SetActive(true);
        EnableActionButtons();
    }

    private void ActivateEnemyTurn()
    {
        enemySelection.SetActive(true);
        StartCoroutine(EnemyAttackRoutine());
    }

    private void EnableActionButtons()
    {
        attackButton.interactable = true;
        empathyButton.interactable = true;
        itemButton.interactable = true;
    }

    private IEnumerator EnemyAttackRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        playerHealth.value -= playerHealth.maxValue * enemyAttackValue;

        if (playerHealth.value <= 0)
        {
            enemyWon.SetActive(true);
            EventSystem.current.SetSelectedGameObject(resetSceneGM);
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            NextTurn();
        }
    }

    public void EmpathyButton()
    {
        AdjustEnemyAura();

        if (enemyAura.GetComponent<Image>().color.a == 0)
        {
            enemyConverted.SetActive(true);
            EventSystem.current.SetSelectedGameObject(nextSceneEnemyConvertedGM);
        }
        else
        {
            NextTurn();
        }
    }


    private void AdjustEnemyAura()
    {
        Color auraColor = enemyAura.GetComponent<Image>().color;

        string[] characters = { "Jimmy", "Vanessa"};
        if (turnOrder[currentTurnIndex] == player1)
            foreach (string character in characters)
            {
                if (player1.CompareTag(character))
                {
                    player1EmpathyValue = GetCharacterEmpathy(character);
                    auraColor.a = Mathf.Max(0, auraColor.a - player1EmpathyValue);
                    enemyAura.GetComponent<Image>().color = auraColor;
                }

            }
        if (turnOrder[currentTurnIndex] == player2)
            foreach (string character in characters)
            {
                if (player2.CompareTag(character))
                {
                    player2EmpathyValue = GetCharacterEmpathy(character);
                    auraColor.a = Mathf.Max(0, auraColor.a - player2EmpathyValue);
                    enemyAura.GetComponent<Image>().color = auraColor;
                }

            }
    }

    private float GetCharacterEmpathy(string character)
    {

        switch (character)
        {

            case "Jimmy":
                return 0.25f;

            case "Alice":
                return 0.25f;

            case "Jake":
                return 0.15f;

            case "Vanessa":
                return 0.45f;

            case "Gabriel":
                return 0.35f;

        }
        return 0f;

    }

    public void AttackButton()
    {
        //AdjustEnemyHealth();

        attackOptions.SetActive(true);
        defaultOptions.SetActive(false);
        firstAttackButton.Select();

        /*if (enemyHealth.value == 0)
        {
            enemyDefeated.SetActive(true);
            EventSystem.current.SetSelectedGameObject(nextSceneEnemyDefeatedGM);

        }
        else
        {
            NextTurn();
        }*/
    }
    private void ReturnToDefaultButtons() {

        if (attackOptions.activeSelf && Input.GetButtonDown("BRANCO0")) {

            attackOptions.SetActive(false);
            defaultOptions.SetActive(true);
            attackButton.Select();
        
        }

        else if (empathyOptions.activeSelf && Input.GetButtonDown("BRANCO0"))
        {

            empathyOptions.SetActive(false);
            defaultOptions.SetActive(true);
            empathyButton.Select();

        }

        else if (itemOptions.activeSelf && Input.GetButtonDown("BRANCO0"))
        {

            itemOptions.SetActive(false);
            defaultOptions.SetActive(true);
            itemButton.Select();

        }

    }

    private void AdjustEnemyHealth()
    {
        string[] characters = {"Jimmy", "Vanessa"};
        if (turnOrder[currentTurnIndex] == player1)
            foreach (string character in characters)
            {
                if (player1.CompareTag(character)) 
                {
                    player1AttackValue = GetCharacterDamage(character);
                    enemyHealth.value -= enemyHealth.maxValue * player1AttackValue;
                }

            }
        if (turnOrder[currentTurnIndex] == player2)
            foreach (string character in characters)
            {
                if (player2.CompareTag(character))
                {
                    player2AttackValue = GetCharacterDamage(character);
                    enemyHealth.value -= enemyHealth.maxValue * player2AttackValue;
                }

            }
    }

    public void ItemButton()
    {

        string itemName = "curativo"; //mudar
        AdjustPlayerHealth(itemName);

        if (enemyHealth.value == 0)
        {
            enemyDefeated.SetActive(true);
            EventSystem.current.SetSelectedGameObject(nextSceneEnemyDefeatedGM);

        }
        else
        {
            NextTurn();
        }
    }

    private void AdjustPlayerHealth(string item) {

        if (item == "curativo") {

            playerHealth.value += playerHealth.maxValue * (itemHealValue * 0.1f);

        }

        if (item == "maca")
        {

            playerHealth.value += playerHealth.maxValue * (itemHealValue * 0.15f);

        }

        if (item == "suco")
        {

            playerHealth.value += playerHealth.maxValue * (itemHealValue * 0.25f);

        }

    }

    private float GetCharacterDamage(string character) 
    {

        switch (character)
        {

            case "Jimmy":
                return 0.25f;

            case "Alice":
                return 0.25f;

            case "Jake":
                return 0.5f;

            case "Vanessa":
                return 0.10f;

            case "Gabriel":
                return 0.15f;

        }
        return 0f;

    }

    private void NextTurn()
    {
        currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
        UpdateTurn();
    }

    public void ResetSceneButton()
    {
        SceneManager.LoadScene("TopDownLuzDaEsperanca");
    }

    public void NextSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Menu()
    {

        if (menu > 0.0f)
            SceneManager.LoadScene(0);

    }
    private void SetButtonTexts() 
    {
        if (defaultOptions.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject == attackButtonGM)
            {
                attackButtonTxt.SetActive(true);
                empathyButtonTxt.SetActive(false);
                itemButtonTxt.SetActive(false);

            }

            if (EventSystem.current.currentSelectedGameObject == empathyButtonGM)
            {
                attackButtonTxt.SetActive(false);
                empathyButtonTxt.SetActive(true);
                itemButtonTxt.SetActive(false);

            }

            if (EventSystem.current.currentSelectedGameObject == itemButtonGM)
            {
                attackButtonTxt.SetActive(false);
                empathyButtonTxt.SetActive(false);
                itemButtonTxt.SetActive(true);

            }
        }

        if (attackOptions.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == firstAttackButton)
            {
                firstAttackButtonTxt.SetActive(true);
                secondAttackButtonTxt.SetActive(false);

            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == secondAttackButton)
            {
                firstAttackButtonTxt.SetActive(false);
                secondAttackButtonTxt.SetActive(true);

            }
        }

        if (empathyOptions.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == firstEmpathyButton)
            {
                firstEmpathyButtonTxt.SetActive(true);
                secondEmpathyButtonTxt.SetActive(false);

            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == secondEmpathyButton)
            {
                firstEmpathyButtonTxt.SetActive(false);
                secondEmpathyButtonTxt.SetActive(true);

            }
        }

    }
}
