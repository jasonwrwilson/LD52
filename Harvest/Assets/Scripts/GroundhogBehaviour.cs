using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundhogBehaviour : EnemyBehaviour
{
    private HarvestTile targetTile;
    [SerializeField] private float burrowTime;
    private float remainingBurrowTime;

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
            if (remainingBurrowTime > 0)
            {
                spriteAnimator.SetTrigger("Burrowing");
                remainingBurrowTime -= Time.deltaTime;
                if (remainingBurrowTime < 0)
                {
                    remainingBurrowTime = 0;
                }
            }
            else
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
        }
        else
        {
            spriteAnimator.SetTrigger("Burrowing");
            attackTimer = attackRate;

            Vector2 dir;

            HarvestTile closestCropTile = enemyManager.FindClosestCrop(gameObject.transform.position);
            if (closestCropTile != null)
            {
                //burrow at the closest crop
                Vector3 closestPos = closestCropTile.gameObject.transform.position;

                closestPos.x += Random.Range(-0.45f, 0.45f);
                closestPos.y += Random.Range(-0.45f, 0.45f);

                gameObject.transform.position = closestPos;

                remainingBurrowTime = burrowTime;
            }
            else
            {
                //stay where you are
            }
        }
    }
}
