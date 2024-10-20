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
    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Transform child = transform.GetChild(0);
        item = child.gameObject.name;
        carteiraRenderer = carteira.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (playerIsInside) {

            Transform child = transform.GetChild(0);
            item = child.gameObject.name;

            if (Input.GetButtonDown("VERDE0") && child.gameObject.activeSelf)
            {
                playerController.ManageInventory(item);
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