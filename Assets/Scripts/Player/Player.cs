using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float move_speed = 3f;
    public bool ui_open = false;
    public Collider2D weapon_collider;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    private float current_hp = 100, max_hp = 100;
    private float damage = 20;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HidePlayer();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameplayUI.instance.UpdateHP(current_hp / max_hp);
    }

    private void Update()
    {
        if (!ui_open)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            //GetComponent<PlayerBody>().Turn(movement.x);
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                weapon_collider.enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Store.instance.ShowInventory();
            }
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * move_speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Store")
        {
            Store.instance.OpenStore();
            weapon_collider.enabled = false;
        }
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.gameObject.GetComponent<Enemy>().TakeHP(damage);
        }
    }

    public void ChangeArmor(InventoryItem item)
    {
        print("Change item " + item.name);
        if (item.body == InventoryItem.BodyPart.torso)
        {
            GetComponent<PlayerBody>().ChangeTorsoArmor(item);
        }
        if (item.body == InventoryItem.BodyPart.head)
        {
            GetComponent<PlayerBody>().ChangeHelmet(item);
        }
    }

    public void TakeHealth(float damage)
    {
        current_hp -= damage;
        if (current_hp < 0)
        {
            current_hp = 0;
            HidePlayer();
            Inventory.instance.PlayerIsDead();
            GetComponent<PlayerBody>().SetUpBasicArmor();
            GameplayUI.instance.OpenStartMenu();
            EnemyManager.instance.StopGame();
        }
        GameplayUI.instance.UpdateHP(current_hp / max_hp);
    }

    private void HidePlayer()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        ui_open = true;
    }

    public void ShowPlayer()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
        current_hp = max_hp;
        GameplayUI.instance.UpdateHP(current_hp / max_hp);
        ui_open = false;
    }
}
