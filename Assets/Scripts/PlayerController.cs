using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [Header("Player Properties")]
    public float inventario;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        HandleMovement();

    }
    public void HandleMovement()
    {

        if (Input.GetButton("HORIZONTAL0") || Input.GetButton("VERTICAL0"))
        {
            float moveInputX = Input.GetAxis("HORIZONTAL0");
            float moveInputY = Input.GetAxis("VERTICAL0");
            rb.velocity = new Vector2(moveInputX * walkSpeed, moveInputY * walkSpeed);
        }

    }

    public void ManageInventory(string name)
    {

        //EXEMPLOS, CODIGO VAI MUDAR QUANDO DECIDIDOS OS ITENS
        if (name.ToLower().Equals("curativo"))
        {

            inventario++;

        }

        if (name.ToLower().Equals("lapis"))
        {

            inventario++;

        }

        if (name.ToLower().Equals("caderno"))
        {

            inventario++;

        }

        if (name.ToLower().Equals("papel"))
        {

            //ativar uma parte da UI q mostre o papel encontrado com sua historia

        }


    }
}
