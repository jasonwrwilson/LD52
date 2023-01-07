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
        if (targetTile != null && targetTile.HasCrop())
        {
            spriteAnimator.SetTrigger("Idling");
            gameObject.GetComponent<SpriteRenderer>().flipY = false;

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
            spriteAnimator.SetTrigger("Moving");
            attackTimer = attackRate;

            Vector2 dir;

            HarvestTile closestCropTile = enemyManager.FindClosestCrop(gameObject.transform.position);
            if (closestCropTile != null)
            {
                //fly toward closest crop
                dir = new Vector2(closestCropTile.gameObject.transform.position.x - gameObject.transform.position.x, closestCropTile.gameObject.transform.position.y - gameObject.transform.position.y);
            }
            else
            {
                //fly toward centre
                dir = new Vector2(-gameObject.transform.position.x, -gameObject.transform.position.y);
            }

            dir.Normalize();

            gameObject.GetComponent<SpriteRenderer>().flipX = dir.x > 0;
            gameObject.GetComponent<SpriteRenderer>().flipY = dir.y > 0;

            pos.x = pos.x + dir.x * speed * Time.deltaTime;
            pos.y = pos.y + dir.y * speed * Time.deltaTime;

            gameObject.transform.position = pos;
        }
    }
}
