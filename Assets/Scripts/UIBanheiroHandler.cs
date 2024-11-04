using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class UIBanheiroHandler : MonoBehaviour
{
    [Header("Tela Recrutados")]
    [SerializeField] private GameObject recrutados;
    [SerializeField] private Button bully1Select;
    [SerializeField] private Button bully2Select;
    [Header("Tela Party")]
    [SerializeField] private GameObject party;
    [SerializeField] private Button jimmySelect;
    [SerializeField] private Button aliceSelect;
    [SerializeField] private Button itensSelect;
    [Header("Checks")]
    [SerializeField] private bool canChangeScreen = true;
    [SerializeField] private bool canCloseUI = true;
    [Header("Select")]
    [SerializeField] private GameObject pencilJimmy;
    [SerializeField] private GameObject pencilAlice;
    [SerializeField] private GameObject pencilItens;
    [SerializeField] private GameObject pencilBully1;
    [SerializeField] private GameObject pencilBully2;
    [Header("Tela Dados")]
    [SerializeField] private GameObject dadosJimmy;
    [SerializeField] private GameObject dadosAlice;
    [SerializeField] private GameObject dadosItens;
    [Header("Tela Recrutados")]
    [SerializeField] private GameObject dadosBully1;
    [SerializeField] private GameObject dadosBully2;
    [Header("Itens")]
    [SerializeField] private TMP_Text item1;
    [SerializeField] private TMP_Text item2;
    [SerializeField] private TMP_Text item3;
    [Header("Itens Game Object")]
    [SerializeField] private Button item1BTTN;
    [SerializeField] private Button item2BTTN;
    [SerializeField] private Button item3BTTN;
    private string[] listaItensAtaque;
    private string[] listaItensCura;
    private string[] listaItensDefesa;
    private string[] listaBuffs;
    [Header("Itens Select")]
    [SerializeField] private GameObject item1Select;
    [SerializeField] private GameObject item2Select;
    [SerializeField] private GameObject item3Select;
    [Header("Dados Alice")]
    [SerializeField] private GameObject ataqueAlicePencil;
    [SerializeField] private Button ataqueAliceSelect;
    [SerializeField] private GameObject defesaAlicePencil;
    [SerializeField] private Button defesaAliceSelect;
    [SerializeField] private GameObject buffAlicePencil;
    [SerializeField] private Button buffAliceSelect;
    [SerializeField] private TMP_Text itemAliceAtaque;
    [SerializeField] private TMP_Text itemAliceDefesa;
    [SerializeField] private TMP_Text aliceBuff;
    [Header("DadosJimmy")]
    [SerializeField] private GameObject ataqueJimmyPencil;
    [SerializeField] private Button ataqueJimmySelect;
    [SerializeField] private GameObject defesaJimmyPencil;
    [SerializeField] private Button defesaJimmySelect;
    [SerializeField] private GameObject buffJimmyPencil;
    [SerializeField] private Button buffJimmySelect;
    [SerializeField] private TMP_Text itemJimmyAtaque;
    [SerializeField] private TMP_Text itemJimmyDefesa;
    [SerializeField] private TMP_Text jimmyBuff;

    PlayerController playerController;
    BanheiroCollider banheiroCollider;
    public GameObject banheiroOBJ;
    float x0;

    void Start()
    {
        x0 = Input.GetAxis("HORIZONTAL0");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        banheiroCollider = banheiroOBJ.GetComponent<BanheiroCollider>();
        listaItensCura = playerController.inventarioCura;
        listaItensAtaque = playerController.inventarioAtaque;
        listaItensDefesa = playerController.inventarioDefesa;
        listaBuffs = playerController.buffs;

        for (int i = 0; i < listaItensCura.Length; i++) {

            if (listaItensCura[i] != null) {

                if (item1.text != null)
                    item1.text = listaItensCura[i];
                else if (item2.text != null)
                    item2.text = listaItensCura[i];
                else if (item3.text != null)
                    item3.text = listaItensCura[i];

            }

        }
    }
    void Update()
    {
        ChangeScreens();
        BullyAttributes();
        CharacterAttributes();
        CharacterAttributesScreen();
        ItemChange();
        HealItens();
        HealItensScreen();

    }
    private void ChangeScreens() {
        if (canChangeScreen)
        {
            if (Input.GetButtonDown("HORIZONTAL0"))
            {
                if (recrutados.activeSelf)
                {
                    recrutados.SetActive(false);
                    party.SetActive(true);
                    jimmySelect.Select();
                    pencilJimmy.SetActive(true);
                    pencilBully1.SetActive(false);
                }
                else if (party.activeSelf)
                {
                    party.SetActive(false);
                    recrutados.SetActive(true);
                    bully1Select.Select();
                    pencilBully1.SetActive(true);
                    pencilJimmy.SetActive(false);
                }
            }
        }
    }
    private void BullyAttributes() {

        if (recrutados.activeSelf && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == bully1Select)
        {
            pencilBully1.SetActive(true);
            dadosBully1.SetActive(true);
            pencilBully2.SetActive(false);
            dadosBully2.SetActive(false);
        }

        if (recrutados.activeSelf && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == bully2Select)
        {
            pencilBully2.SetActive(true);
            dadosBully2.SetActive(true);
            pencilBully1.SetActive(false);
            dadosBully1.SetActive(false);
        }

    }
    private void CharacterAttributes()
    {
        if (party.activeSelf && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == jimmySelect && !dadosJimmy.activeSelf)
        {
            pencilJimmy.SetActive(true);
            pencilAlice.SetActive(false);
            pencilItens.SetActive(false);
            if (Input.GetButtonDown("VERDE0")) { 
            
                dadosJimmy.SetActive(true);
                pencilJimmy.SetActive(false);
                defesaJimmyPencil.SetActive(true);
                defesaJimmySelect.Select();
                canChangeScreen = false;
                canCloseUI = false;

            }
        }

        if (party.activeSelf && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == aliceSelect && !dadosAlice.activeSelf)
        {
            pencilAlice.SetActive(true);
            pencilJimmy.SetActive(false);
            pencilItens.SetActive(false);
            if (Input.GetButtonDown("VERDE0"))
            {
                pencilAlice.SetActive(false);
                dadosAlice.SetActive(true);
                defesaAlicePencil.SetActive(true);
                defesaAliceSelect.Select();
                canChangeScreen = false;
                canCloseUI = false;

            }
        }

        if (dadosAlice.activeSelf == true && Input.GetButtonDown("BRANCO0"))
        {

            dadosAlice.SetActive(false);
            pencilAlice.SetActive(true);
            aliceSelect.Select();
            canChangeScreen = true;
            canCloseUI = true;

        }

        if (dadosJimmy.activeSelf == true && Input.GetButtonDown("BRANCO0"))
        {

            dadosJimmy.SetActive(false);
            pencilJimmy.SetActive(true);
            jimmySelect.Select();
            canChangeScreen = true;
            canCloseUI = true;

        }
    }
    private void HealItens()
    {

        if (party.activeSelf && EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == itensSelect && !dadosAlice.activeSelf)
        {
            pencilItens.SetActive(true);
            pencilAlice.SetActive(false);
            pencilJimmy.SetActive(false);
            if (Input.GetButtonDown("VERDE0"))
            {
                pencilItens.SetActive(false);
                dadosItens.SetActive(true);
                item1Select.SetActive(true);
                item1BTTN.Select();
                canChangeScreen = false;
                canCloseUI = false;

            }
        }

        if (dadosItens.activeSelf == true && Input.GetButtonDown("BRANCO0"))
        {

            dadosItens.SetActive(false);
            pencilItens.SetActive(true);
            itensSelect.Select();
            canChangeScreen = true;
            canCloseUI = true;

        }
    }
    private void CharacterAttributesScreen()
    { 

        if (dadosJimmy.activeSelf == true)
        {

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == ataqueJimmySelect)
            {
                ataqueJimmyPencil.SetActive(true);
                defesaJimmyPencil.SetActive(false);
                buffJimmyPencil.SetActive(false);
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == defesaJimmySelect)
            {
                ataqueJimmyPencil.SetActive(false);
                defesaJimmyPencil.SetActive(true);
                buffJimmyPencil.SetActive(false);
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == buffJimmySelect)
            {
                ataqueJimmyPencil.SetActive(false);
                defesaJimmyPencil.SetActive(false);
                buffJimmyPencil.SetActive(true);
            }

        }

        if (dadosAlice.activeSelf == true)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == ataqueAliceSelect)
            {
                ataqueAlicePencil.SetActive(true);
                defesaAlicePencil.SetActive(false);
                buffAlicePencil.SetActive(false);
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == defesaAliceSelect)
            {
                ataqueAlicePencil.SetActive(false);
                defesaAlicePencil.SetActive(true);
                buffAlicePencil.SetActive(false);            
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == buffAliceSelect)
            {
                ataqueAlicePencil.SetActive(false);
                defesaAlicePencil.SetActive(false);
                buffAlicePencil.SetActive(true);
            }

        }
    }
    private void HealItensScreen()
    {

        if (dadosItens.activeSelf == true)
        {
                        
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == item1BTTN)
            {
                item1Select.SetActive(true);
                item2Select.SetActive(false);
                item3Select.SetActive(false);
            }

            else if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == item2BTTN)
            {
                item1Select.SetActive(false);
                item2Select.SetActive(true);
                item3Select.SetActive(false);
            }

            else if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == item3BTTN)
            {
                item1Select.SetActive(false);
                item2Select.SetActive(false);
                item3Select.SetActive(true);
            }
        }
    }
    private void ItemChange() {
        if (dadosJimmy.activeSelf == true)
        {

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == ataqueJimmySelect)
            {

                int position = Array.FindIndex(listaItensAtaque, item => item == itemJimmyAtaque.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    itemJimmyAtaque.text = listaItensAtaque[position] + "\n+" + 4 + " Ataque"; //deixar o valor variavel

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    itemJimmyAtaque.text = listaItensAtaque[position] + "\n+" + 4 + " Ataque"; //deixar o valor variavel

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosJimmy.SetActive(false);
                    pencilJimmy.SetActive(true);
                    jimmySelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == defesaJimmySelect)
            {

                int position = Array.FindIndex(listaItensDefesa, item => item == itemJimmyDefesa.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    itemJimmyDefesa.text = listaItensDefesa[position] + "\n+" + 4 + " Defesa"; //deixar o valor variavel

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    itemJimmyDefesa.text = listaItensDefesa[position] + "\n+" + 4 + " Defesa"; //deixar o valor variavel

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosJimmy.SetActive(false);
                    pencilJimmy.SetActive(true);
                    jimmySelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == buffJimmySelect)
            {

                int position = Array.FindIndex(listaBuffs, item => item == jimmyBuff.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    jimmyBuff.text = listaBuffs[position];

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    jimmyBuff.text = listaBuffs[position];

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosJimmy.SetActive(false);
                    pencilJimmy.SetActive(true);
                    jimmySelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

        }

        if (dadosAlice.activeSelf == true)
        {

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == ataqueAliceSelect)
            {

                int position = Array.FindIndex(listaItensAtaque, item => item == itemAliceAtaque.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    itemAliceAtaque.text = listaItensAtaque[position] + "\n+" + 4 + " Ataque"; //deixar o valor variavel

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    itemAliceAtaque.text = listaItensAtaque[position] + "\n+" + 4 + " Ataque"; //deixar o valor variavel

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosAlice.SetActive(false);
                    pencilAlice.SetActive(true);
                    aliceSelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == defesaAliceSelect)
            {

                int position = Array.FindIndex(listaItensDefesa, item => item == itemAliceDefesa.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    itemAliceDefesa.text = listaItensDefesa[position] + "\n+" + 4 + " Defesa"; //deixar o valor variavel

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    itemAliceDefesa.text = listaItensDefesa[position] + "\n+" + 4 + " Defesa"; //deixar o valor variavel

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosAlice.SetActive(false);
                    pencilAlice.SetActive(true);
                    aliceSelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == buffAliceSelect)
            {

                int position = Array.FindIndex(listaBuffs, item => item == aliceBuff.text);

                if (Input.GetButtonDown("HORIZONTAL0") && x0 > 0 && !canChangeScreen)
                {

                    position++;
                    aliceBuff.text = listaBuffs[position];

                }

                if (Input.GetButtonDown("HORIZONTAL0") && x0 < 0 && !canChangeScreen)
                {

                    position--;
                    aliceBuff.text = listaBuffs[position];

                }
                if (Input.GetButtonDown("VERDE0"))
                {

                    //setar o item no inventario dps e como inutilizavel por outros, e dessetar o anterior
                    dadosAlice.SetActive(false);
                    pencilAlice.SetActive(true);
                    aliceSelect.Select();
                    canChangeScreen = true;
                    canCloseUI = true;
                }
            }

        }
    }
    public void UIActiveOrDeactivate() {

        if (canCloseUI) {

            banheiroCollider.canChangeUIState = true;

        }

        if (!canCloseUI)
        {

            banheiroCollider.canChangeUIState = false;

        }


    }
}
