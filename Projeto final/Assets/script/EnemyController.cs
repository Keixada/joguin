using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform WaypointA;
    public Transform WaypointB;
    public float movementSpeed;
    private Animator anime;
    
    private Rigidbody2D rb;
    private Vector3 scale;

    private Transform currentTarget;

    public int enemyHealth = 50;
    public float attackInterval = 3f;

    private Coroutine attackCoroutine;

    void Start()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentTarget = WaypointA;
        scale = transform.localScale;
        Debug.Log("Life do Enemy: " + enemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZoneAttack"))
        {
            Debug.Log("Inimigo entrou na zona de ataque");
        }

        Player player = collision.GetComponent<Player>();
        if (player == null)
        {
            player = collision.GetComponentInParent<Player>();
        }

        if (player != null)
        {
            if(attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackPlayer(player));
            }
            else
            {
                Debug.Log("Player não encontrado no objeto com a tag ZoneAttack");
            }
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ZoneAttack"))
        {
            Debug.Log("Inimigo saiu da zona de ataque.");

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine= null;
            }
        }
    }

    private IEnumerator AttackPlayer(Player player)
    {
        while (true)
        {
            player.TakeDamage(10);//Valor pode ser alterado conforme sua necessidade.
            anime.SetTrigger("Attack");
            Debug.Log("Inimigo atacando...");

            yield return new WaitForSeconds(attackInterval);
        }
    }


    private void MoveTowardsTarget()
    {
        Vector3 curTargetHorizontal = new Vector2(currentTarget.position.x, transform.position.y);
        Vector2 direction = (curTargetHorizontal +- transform.position).normalized;

        transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;

        if (Vector2.Distance(curTargetHorizontal, transform.position) <= 0.2f)
        {
            SwitchTarget();
        }

        
    }
    private void SwitchTarget()
    {
        if (currentTarget == WaypointA)
        {
            currentTarget = WaypointB;
            Flip();
        }
        else
        {
            currentTarget = WaypointA;
            transform.localScale = scale;
        }
    }


   

    private void Flip()
    {
        Vector3 flippedScale = scale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

}
