using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BanheiroCollider : MonoBehaviour
{
    public GameObject banheiroUI;
    public GameObject banheiroUITxt;
    public GameObject salvandoTxtGM;
    public TMP_Text salvandoTxt;
    public Transform spawnPoint;
    private bool isPlayerInside;
    public bool canChangeUIState = true;
    private bool cancChangeVerificationState = true;
    private GameObject instancia;
    PlayerController playerController;
    UIBanheiroHandler uiBanheiroHandler;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        uiBanheiroHandler = banheiroUI.GetComponent<UIBanheiroHandler>();
    }
    private void Update()
    {
        if (cancChangeVerificationState) {
            if (uiBanheiroHandler != null)
                uiBanheiroHandler.UIActiveOrDeactivate();

            else
                Debug.Log("n ta certo");

        }
        if (Input.GetButtonDown("BRANCO0") && isPlayerInside && canChangeUIState && !instancia.activeSelf)
        {
            banheiroUI.SetActive(true);
            playerController.canMove = false;
            banheiroUITxt.SetActive(false);
            canChangeUIState = false;
            StartCoroutine(ChangeUIStateCooldown());
        }
        if (Input.GetButtonDown("BRANCO0") && isPlayerInside && canChangeUIState && instancia.activeSelf)
        {
            Destroy(instancia);
            playerController.canMove = true;
            banheiroUITxt.SetActive(true);
            canChangeUIState = false;
            StartCoroutine(ChangeUIStateCooldown());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            banheiroUITxt.SetActive(true);
            salvandoTxtGM.SetActive(false);
            playerController.ChangeSpawnPoint(spawnPoint);
            StopCoroutine(SalvandoTxt());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInside = false;
        if (collision.CompareTag("Player"))
        {
            banheiroUITxt.SetActive(false);
            salvandoTxtGM.SetActive(true);
            StartCoroutine(SalvandoTxt());
        }
    }

    IEnumerator SalvandoTxt() {

        for (int i = 0; i < 3; i++) {

            salvandoTxt.SetText("Salvando.");
            yield return new WaitForSeconds(0.5f);
            salvandoTxt.SetText("Salvando..");
            yield return new WaitForSeconds(0.5f);
            salvandoTxt.SetText("Salvando...");
            yield return new WaitForSeconds(0.5f);


        }
        salvandoTxtGM.SetActive(false);
    }

    IEnumerator ChangeUIStateCooldown() {
        yield return new WaitForSeconds(0.25f);
        canChangeUIState = true;
    }
}
