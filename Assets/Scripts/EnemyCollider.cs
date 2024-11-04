using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    [Header("Collider Verifier")]
    [SerializeField] private bool playerIsInside = false;
    private void Update()
    {
        if (playerIsInside && Input.GetButtonDown("VERDE0"))
        {
            SceneManager.LoadScene("NormalBattle");
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
}
