using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBehaviour : EnemyBehaviour
{
    private HarvestTile targetTile;

    [SerializeField] private float stopTime;
    [SerializeField] private float minBetweenStopsTime;
    [SerializeField] private float maxBetweenStopsTime;
    private float remainingStopTime;
    private float remainingTimeBetweenStops;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackRate;
        remainingStopTime = 0;
        remainingTimeBetweenStops = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;

        targetTile = enemyManager.GetTileAt(pos.x, pos.y);
        if (remainingStopTime > 0)
        {
            //stop for a bit
            spriteAnimator.SetTrigger("Idling");

            remainingStopTime -= Time.deltaTime;
            if (remainingStopTime < 0)
            {
                remainingStopTime = 0;
                remainingTimeBetweenStops = Random.Range(minBetweenStopsTime, maxBetweenStopsTime);
            }
        }        
        else if (targetTile != null && targetTile.GetGrowthStage() > 0)
        {
            spriteAnimator.SetTrigger("Idling");

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

            pos.x = pos.x + dir.x * speed * Time.deltaTime;
            pos.y = pos.y + dir.y * speed * Time.deltaTime;

            gameObject.transform.position = pos;

            remainingTimeBetweenStops -= Time.deltaTime;
            if (remainingTimeBetweenStops < 0)
            {
                //stop for a bit
                remainingTimeBetweenStops = 0;
                remainingStopTime = stopTime;
            }
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
