using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public Image hp_bar;
    public float curent_health, max_health;
    public float damage;
    public float attack_rate = 3;

    private float attack_elapsed;
    private float move_speed = 2;
    private float distance_to_player;
    private float max_distance_to_attack = 2;
    private bool is_moving;

    private void Start()
    {
        curent_health = max_health;
        hp_bar.transform.localScale = new Vector2(curent_health / max_health, 1);
    }

    private void Update()
    {
        distance_to_player = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance_to_player < max_distance_to_attack)
        {
            attack_elapsed += Time.deltaTime;
            if (attack_elapsed > attack_rate)
            {
                Player.instance.TakeHealth(damage);
                attack_elapsed = 0;
            }
        }
        else
        {
            GoToPlayer();
        }
    }

    public void TakeHP(float damage)
    {
        curent_health -= damage;
        if(curent_health < 0 )
        {
            Inventory.instance.AddGold(Random.Range(1, 20));
            Destroy(this.gameObject);
        }
        hp_bar.transform.localScale = new Vector2(curent_health / max_health, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player.instance.TakeHealth(damage);
        }
    }

    private void GoToPlayer()
    {
        if (!is_moving)
        {
            transform.DOMove(Player.instance.transform.position, Vector2.Distance(transform.position, Player.instance.transform.position) / move_speed).
                OnComplete(() => is_moving = false);
            is_moving = true;
        }
    }
}
