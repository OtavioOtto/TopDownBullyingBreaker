using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed;
    [Header("Player Properties")]
    public string[] inventarioAtaque = new string [9];
    public string[] inventarioDefesa = new string[9];
    public string[] inventarioCura = new string[9];
    public string[] buffs = new string[9];
    public Transform spawnPoint;
    public float rotacaoSpeed = 720;
    private Rigidbody2D rb;
    public bool canMove = true;
    GameObject instancia;
    public static PlayerController player;

    void Start()
    {
        //instancia = Instantiate(this.gameObject);
        player.gameObject.transform.position = spawnPoint.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        player = this;
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if(canMove)
        HandleMovement();
        VerifyScene();

    }

    private void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            Quaternion rotacaoDesejada = Quaternion.LookRotation(transform.forward, rb.velocity);
            Quaternion rotacao = Quaternion.RotateTowards(transform.rotation, rotacaoDesejada, rotacaoSpeed * Time.deltaTime);
            rb.MoveRotation(rotacao);
        }
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

    public void ManageInventory(string name, string tipoItem)
    {
        for (int i = 0; i < inventarioAtaque.Length; i++)
        {
            if (tipoItem == "ataque")
                inventarioAtaque[i] = name;

            if (tipoItem == "defesa")
                inventarioDefesa[i] = name;

            if (tipoItem == "cura")
                inventarioCura[i] = name;
        }

    }
    public void ChangeSpawnPoint(Transform newSpawn) {

        spawnPoint = newSpawn;

    }

    private void VerifyScene() 
    {
        if (SceneManager.GetSceneByName("TopDownLuzDaEsperanca").isLoaded)
        {
            this.gameObject.SetActive(true);
            canMove = true;
        }

        else
        {
            this.gameObject.SetActive(false);
            canMove = false;
        }

    }
}
