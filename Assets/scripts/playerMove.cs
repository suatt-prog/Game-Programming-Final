using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public int sahne = 1;
    // Start is called before the first frame update
    Rigidbody2D rigidbody;
    CapsuleCollider2D capsuleCollider;
    Vector3 x_ekseninde_hareket_vektoru; // motion vector on the x-axis
    Vector3 y_ekseninde_hareket_vektoru;
    float hiz_buyuklugu_x = 4f; // speed amount
    float hiz_buyuklugu_y = 7f;
    public Animator animator;
    public TextMeshProUGUI playerScoreText;
    public GameObject kus , clone_kus , silah_tutma_pozisyonu;
    public AudioSource audioSource_jump , audioSource_steps;
    int sahne_kontrol;
    int enemy_count = 0;
    public AnaMenu_sc anamenu;
    Vector3 kus_hareketi;
    bool is_there_any_enemy;
    public int score;
    void Start()
    {
        anamenu = GameObject.Find("BackToMenu").GetComponent<AnaMenu_sc>();
        sahne_kontrol = 1;  // 0 : menu sahnesi  , 1 : 1. oyun sahnesi (sample scene)  2 : 2. oyun sahnesi
        Random.Range(0.0f, 1.0f);
        System.Random random = new System.Random();
        rigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        score = 0;
        kus_hareketi = new Vector3(-5.0f, 0f, 0f);
        is_there_any_enemy = false;
       
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {        }

        if (Input.GetAxis("Cancel") > 0.0f && sahne_kontrol == 1 && SceneManager.GetActiveScene().name == "SampleScene")
        {
            sahne_kontrol = 0;
            PlayerPrefs.SetInt("hold", 0);
            SceneManager.LoadScene("AnaMenu");
        }
        else if(Input.GetAxis("Cancel") > 0.0f && SceneManager.GetActiveScene().name == "bolum2")
        {
            sahne_kontrol = 1;
            PlayerPrefs.SetInt("hold", 1);
            SceneManager.LoadScene("AnaMenu");
        }


        createEnemy(is_there_any_enemy);
        if (transform.position.x > 50f && SceneManager.GetActiveScene().name=="SampleScene")
        {
            sahne = 2;
            print(sahne);
            PlayerPrefs.SetInt("sahne", sahne);
            SceneManager.LoadScene("bolum2");
        }
        if (transform.position.x>70f && SceneManager.GetActiveScene().name == "bolum2")
        {
            sahne = 1;
            PlayerPrefs.SetInt("sahne", sahne);
            SceneManager.LoadScene("StartMenu");
        }

        if (clone_kus != null)
        {
            if (clone_kus.transform.position.x < -75.0f)
            {
                is_there_any_enemy = false;
                enemy_count = 0;
            }
        }

        playerScoreText.text = "score: " + score.ToString();

        x_ekseninde_hareket_vektoru = new Vector3(Input.GetAxis("Horizontal") , 0f); // x pozisyonunu getaxis ile aldýk
                                                                    
        transform.position += x_ekseninde_hareket_vektoru * hiz_buyuklugu_x * Time.deltaTime;

         y_ekseninde_hareket_vektoru = new Vector3(0f, Input.GetAxisRaw("Vertical"));
        


        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetKey("w") && Mathf.Approximately(rigidbody.velocity.y , 0) )
        {
            audioSource_jump.Play();
            rigidbody.AddForce(y_ekseninde_hareket_vektoru * hiz_buyuklugu_y , ForceMode2D.Impulse);
            // devam etmeyen anlýk kuvvet uygulamak istediðimizde 2. parametre olarak ForceMode2D.Impulse veriyoruz
            animator.SetBool("isJumping", true);
            
        }

        if ( animator.GetBool("isJumping") && Mathf.Approximately(rigidbody.velocity.y, 0))
        {
            animator.SetBool("isJumping", false);
        }

        // GetAxisRaw methodu getAxis'ten farklý olarak ara deðerleri döndürmez "a" tuþu için -1 "d" tuþu için 1 
        //döndürür
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // karakterimizi y ekseni etrafýnda 180 derece 
            // dödürüyoruz bu sayede saða giderken saða bakýyormuþ gibi görünüyor
            
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        
        if(Input.GetAxis("Horizontal") != 0.0f && !audioSource_steps.isPlaying)
        {
                audioSource_steps.Play();  
        }

       
        if (Input.GetKey("s"))
        {
            transform.localScale = new Vector3(1.25517f, 0.9f, 0f);
            capsuleCollider.size = new Vector3(0.6778474f, 0.7f, 0f);
            capsuleCollider.offset = new Vector3(-0.04212213f, 0.1f , 0f);
        }
        else
        {
            transform.localScale = new Vector3(1.25517f, 1.25517f, 1.25517f);
            capsuleCollider.size = new Vector3(0.6778474f, 0.7f, 0f);
            capsuleCollider.offset = new Vector3(-0.04212213f, -0.01872075f , 0f);
        }

    }

    float enemy_create_position_y;
    
    public void createEnemy(bool dusman_var_mý)
    {
        enemy_create_position_y = Random.Range(2f, 3.0f);
        Vector3 enemy_create_position = new Vector3(70f, enemy_create_position_y, 0.0f);
        if (SceneManager.GetActiveScene().name == "bolum2")
        {
            enemy_create_position_y = Random.Range(1.5f, 2.0f);  
            enemy_create_position = new Vector3(70f, enemy_create_position_y, 0.0f);
        }
        if (!dusman_var_mý)
        {
            clone_kus = Instantiate(kus, enemy_create_position, Quaternion.identity);
            is_there_any_enemy = true;
            enemy_count++;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
