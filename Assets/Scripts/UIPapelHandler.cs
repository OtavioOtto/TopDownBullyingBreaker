using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPapelHandler : MonoBehaviour
{
    public string qualTextoNome;
    [SerializeField] private TMP_Text texto;
    void Update()
    {
        if (qualTextoNome == "exemplo") {

            texto.text = "~historia~";

        }
        //colocar animacao e texto

    }

    public void AtivarDesativarUI() {

        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
