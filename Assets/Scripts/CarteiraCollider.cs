using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteiraCollider : MonoBehaviour
{
    [Header("Carteira Configs")]
    public string item;
    public GameObject carteira;
    public SpriteRenderer carteiraRenderer;
    [Header("Collider Verifier")]
    [SerializeField] private bool playerIsInside = false;
    private string[] listaItens = new string[9]; //colocar o limite com os itens q vao ter
    private string[] listaItensAtaque = {"a", "b", "c"}; //colocar os itens
    private string[] listaItensDefesa = { "a", "b", "c" }; //colocar os itens
    private string[] listaItensCura = { "a", "b", "c" }; //colocar os itens
    private string tipoItem;
    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Transform child = transform.GetChild(0);
        for (int i = 0; i < listaItens.Length; i++) {
            if (listaItensAtaque[i] != null)
            {
                if (child.gameObject.name == listaItensAtaque[i])
                {
                    item = child.gameObject.name;
                    tipoItem = "ataque";
                }
            }

            if (listaItensDefesa[i] != null)
            {
                if (child.gameObject.name == listaItensDefesa[i])
                {
                    item = child.gameObject.name;
                    tipoItem = "defesa";
                }
            }

            if (listaItensCura[i] != null)
            {
                if (child.gameObject.name == listaItensCura[i])
                {
                    item = child.gameObject.name;
                    tipoItem = "cura";
                }
            }
        }
        carteiraRenderer = carteira.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (playerIsInside) {

            Transform child = transform.GetChild(0);

            if (Input.GetButtonDown("VERDE0") && child.gameObject.activeSelf)
            {
                playerController.ManageInventory(item, tipoItem);
                DeleteChild();
                //dps a gente muda como isso funciona, eu so fiz isso para ser um indicador visual q vc pegou o item
                carteiraRenderer.color = Color.white;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }
    public void DeleteChild()
    {
        transform.GetChild(0).gameObject.SetActive(false);

    }
}