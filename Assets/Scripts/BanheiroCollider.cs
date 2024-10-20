using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanheiroCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ativar a UI de party/itens/npcs recrutados
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //desativar a UI de party/itens/npcs recrutados
    }
}
