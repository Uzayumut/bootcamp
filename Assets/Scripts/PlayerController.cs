using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float jump=8f;
    private float moveSpeed=5;
    private GameObject cam;
    private Joystick joystick;
    private Animator animator;
    private AudioSource walkSound;
    private int health =3;
    public Image[] heart;
    private SpriteRenderer spriteRenderer;
    
    
    Quaternion quad;

    //fire
    public GameObject fireSpawnPos;
    public GameObject fire;
    private float fireSpeed = 6;
    public AudioClip fireSfx;

    //TMP
    public TMP_Text crystalText;
    public int crystal1=0;

    //chest
    private Animator chestAnimator;
    private bool chestOpening = true;
    public ParticleSystem chestEfect;
    public AudioClip chestOpen;
    public AudioClip chestClose;

    //coin
    public AudioClip crystalSfx;

    //nextlevel
    public GameObject nextLevel;

    //restart
    public GameObject restartPanel;

    //enemy
    public AudioClip hitDamageSfx;


    
    void Start()
    {
        //crystal1 = PlayerPrefs.GetInt("crystal");
        
        transform.position = new Vector3(-0.1f, -0.1f, 0);
        nextLevel.SetActive(false);
        restartPanel.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        walkSound = GetComponent<AudioSource>();
        walkSound.enabled = false;
        
        crystalText.text = crystal1.ToString();
        quad = Quaternion.Euler(0, 0, 0);
        cam = GameObject.Find("Main Camera");
        joystick = (Joystick)FindObjectOfType(typeof(Joystick));
        if (joystick)
        {
            Debug.Log("Bulundu "+joystick.name);
        }
        else
        {
            Debug.Log("bULUNAMADI");
        }
        
        animator = GetComponent<Animator>();

        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        spriteRenderer.color = Color.white;
        PlayerMovement();
        CameraMovement();
        
        
    }
    
    public void CameraMovement()
    {
        Vector3 playerPosition = new Vector3(transform.position.x, transform.position.y+2, transform.position.z-10);
        cam.transform.position = playerPosition;
    }
    public void TouchPosition()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPosition.z = 0f;
        //    joystickRect.anchoredPosition3D = touchPosition;
        //}
        //else
        //{
        //    joystick.transform.position = Vector3.zero;
        //}
    }
    public void PlayerMovement()
    {
        if (joystick.Horizontal >= .2f)
        {
            horizontal = joystick.Horizontal * moveSpeed * Time.fixedDeltaTime;

            walkSound.enabled = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isWalking",true);
            //koşma animasyonları eklenecek
        }
        else if (joystick.Horizontal <= -.2f)
        {
            horizontal = -joystick.Horizontal * moveSpeed * Time.fixedDeltaTime;
            walkSound.enabled = true;

            transform.rotation = Quaternion.Euler(0, 180, 0);

            animator.SetBool("isWalking",true);
            //koşma animasyonları eklenecek

        }
        else
        {
            horizontal = 0f;
            walkSound.enabled = false;

            animator.SetBool("isWalking", false);
        }
        vertical = joystick.Vertical;
        if (joystick.Vertical >= .5f)
        {
            animator.SetTrigger("isJump");
            vertical = joystick.Vertical*jump*Time.fixedDeltaTime;
            
            
            //zıplama animasyonu ettkinleştirilecek
        }
        else if(joystick.Vertical <= -.5f)
        {
            vertical = joystick.Vertical * jump *Time.fixedDeltaTime;
           
            animator.SetTrigger("isJump");
            //çökme animasyonu aktifleşecek
        }
        else
        {
            vertical = 0f;
        }
        
        Vector2 move = new Vector2(horizontal, vertical);
        transform.Translate(move);
    }
    
    public void LeftFire()
    {
        AudioSource.PlayClipAtPoint(fireSfx, transform.position);
        fire.transform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetTrigger("isShoot");
        Instantiate(fire, fireSpawnPos.transform.position,Quaternion.identity);
        fire.transform.Translate(Vector2.left * Time.deltaTime * fireSpeed);
    }
    public void RightFire()
    {
        AudioSource.PlayClipAtPoint(fireSfx, transform.position);
        fire.transform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetTrigger("isShoot");
        Instantiate(fire, fireSpawnPos.transform.position, Quaternion.Euler(0,180,0));
        fire.transform.Translate(Vector2.right * Time.deltaTime * fireSpeed);
    }
    public void RotateFire()
    {
        if (transform.rotation == quad)
        {
            LeftFire();
            Debug.Log("LeftFire");
        }
        else
        {
            RightFire();
            Debug.Log("RightFire");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WoodenChest"))
        {
            //sandık sesi
            
            AudioSource.PlayClipAtPoint(chestOpen, transform.position);
            chestEfect.Play();
            Destroy(chestEfect, 3f);
            chestAnimator =collision.gameObject.GetComponent<Animator>();
            chestAnimator.SetBool("isOpen", true);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitDamageSfx, transform.position);
            spriteRenderer.color = Color.red;
            HealthDamage();
        }
        if (collision.gameObject.CompareTag("Sailor"))
        {

        }
        

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            spriteRenderer.color = Color.red;
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WoodenChest"))
        {
            //sandık sesi
            if (!chestOpening)
            {
                collision.gameObject.GetComponent<TextMeshPro>().text = "Chest already opening";
            }
            else
            {
                AudioSource.PlayClipAtPoint(chestClose, transform.position);

                crystal1 = crystal1 + 5;
                crystalText.text = crystal1.ToString();
                
                chestOpening = false;
            }
            
            chestAnimator = collision.gameObject.GetComponent<Animator>();
            chestAnimator.SetBool("isOpen", false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crystal"))
        {
            Debug.Log("Playerprefs ="+PlayerPrefs.GetInt("crystal"));
            AudioSource.PlayClipAtPoint(crystalSfx, transform.position);
            crystal1++;
            PlayerPrefs.SetInt("crystal", crystal1);
            crystalText.text = crystal1.ToString();
            Destroy(collision.gameObject);
        }
        //if (collision.gameObject.CompareTag("Enemy"))
        //{

        //    HealthDamage();



        //    //hasar alma efekti görüntü bulanıklaştırma

        //}
        if (collision.gameObject.CompareTag("Spike"))
        {
            Vector3 move = new Vector3(0, 2, 0);
            transform.Translate(move*Time.deltaTime*2);
            spriteRenderer.color = Color.red;
            AudioSource.PlayClipAtPoint(hitDamageSfx, transform.position);
            HealthDamage();
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finish1"))
        {
            Debug.Log("finish ile çarepıştı");
            GameObject finObject = collision.gameObject;
            Vector3 finishPosition = new Vector3(finObject.transform.position.x, finObject.transform.position.y+2, finObject.transform.position.z - 10);
            cam.transform.position = finishPosition;
            Vector2 move = Vector2.right * moveSpeed * Time.deltaTime;
            transform.Translate(move);
            animator.SetBool("isWalking", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("finish1"))
        {
            GameObject finObject = collision.gameObject;
            Vector3 finishPosition = new Vector3(finObject.transform.position.x, finObject.transform.position.y + 2, finObject.transform.position.z - 10);
            cam.transform.position = finishPosition;
            nextLevel.SetActive(true);
            
            Debug.Log("Yeni Sahne");
        }
    }
    public void HealthDamage()
    {
        
        health--;
        if (health == 2)
        {
            heart[2].GetComponent<Image>().enabled = false;
           
        }
        else if (health == 1)
        {
            heart[1].GetComponent<Image>().enabled = false;
            
        }
        else if (health == 0)
        {
            heart[0].GetComponent<Image>().enabled = false;
            
            animator.SetTrigger("isDead");

            restartPanel.SetActive(true);
        }
    }
    public void HealthCorrect()
    {
        health++;
        
        if (health == 3)
        {
            Debug.Log("Canın full");
        }
        else if (health == 2)
        {
            
            heart[2].GetComponent<Image>().enabled = true;
        }
        else if (health == 1)
        {
            
            heart[1].GetComponent<Image>().enabled = true;
        }
        
    }
    //public void PlayerLoad()
    //{
        
        
    //    PlayerPrefs.GetInt("health",health);
    //    PlayerPrefs.GetInt("crystal",crystal1);
    //}
    
   
}


//for (int i = 0; i < Input.touchCount; i++)
//{
//    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
//    Debug.DrawLine(Vector3.zero,touchPosition,Color.red);

//}
