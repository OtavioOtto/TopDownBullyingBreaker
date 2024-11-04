using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmarioCollider : MonoBehaviour
{
    [Header("Carteira Configs")]
    public string item;
    public GameObject armario;
    public SpriteRenderer armarioRenderer;
    [Header("Collider Verifier")]
    [SerializeField] private bool playerIsInside = false;
    [Header("UI Object")]
    [SerializeField] private GameObject uiPapel;
    private string tipoItem = "a";
    UIPapelHandler uiPapelHandler;
    void Start()
    {
        uiPapelHandler = uiPapel.GetComponent<UIPapelHandler>();
        armarioRenderer = armario.GetComponent<SpriteRenderer>();
        Transform child = transform.GetChild(0);
        item = child.gameObject.name;
    }
    private void Update()
    {
        if (playerIsInside)
        {

            Transform child = transform.GetChild(0);

            if (Input.GetButtonDown("VERDE0") && child.gameObject.activeSelf)
            {
                uiPapelHandler.AtivarDesativarUI();
                DeleteChild();
                armarioRenderer.color = Color.white;
            }

            if (Input.GetButtonDown("BRANCO0") && !child.gameObject.activeSelf)
            {
                uiPapelHandler.AtivarDesativarUI();
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