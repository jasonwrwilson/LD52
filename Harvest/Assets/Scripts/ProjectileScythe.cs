using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScythe : ProjectileBehaviour
{
    [SerializeField] float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isDead)
        {
            return;
        }

        Vector3 pos = gameObject.transform.position;

        float degrees = rotationSpeed * Time.deltaTime * GetSpeedBonus();
        
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = direction.x;
        float ty = direction.y;

        direction.x = (cos * tx) - (sin * ty);
        direction.y = (sin * tx) + (cos * ty);

        pos.x = pos.x + direction.x * speed * Time.deltaTime * GetSpeedBonus();
        pos.y = pos.y + direction.y * speed * Time.deltaTime * GetSpeedBonus();

        gameObject.transform.position = pos;

    }

    protected override float GetDamageBonus()
    {
        return inventoryManager.GetScytheDamageBonus();
    }

    protected override float GetSpeedBonus()
    {
        return inventoryManager.GetScytheSpeedBonus(); ;
    }

}
