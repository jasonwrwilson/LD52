using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBehaviour : EnemyBehaviour
{
    private HarvestTile targetTile;
    
    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;

        targetTile = enemyManager.GetTileAt(pos.x, pos.y);
        if (targetTile != null && targetTile.GetGrowthStage() > 0)
        {
            //attack
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                targetTile.TakeDamage(damage);
                attackTimer = attackRate;
            }
        }
        else
        {
            attackTimer = attackRate;

            Vector2 dir = new Vector2(-gameObject.transform.position.x - 0.5f, -gameObject.transform.position.y + 0.5f);
            dir.Normalize();

            gameObject.GetComponent<SpriteRenderer>().flipX = dir.x > 0;
            gameObject.GetComponent<SpriteRenderer>().flipY = dir.y > 0;

            pos.x = pos.x + dir.x * speed * Time.deltaTime;
            pos.y = pos.y + dir.y * speed * Time.deltaTime;

            gameObject.transform.position = pos;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!!");
        HarvestTile harvestTile = other.gameObject.GetComponent<HarvestTile>();

        if (harvestTile != null && harvestTile.GetGrowthStage() > 0)
        {
            targetTile = harvestTile;
        }
    }
}
