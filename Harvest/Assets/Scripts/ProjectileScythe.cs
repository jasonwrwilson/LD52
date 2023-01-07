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

        Vector3 pos = gameObject.transform.position;

        float degrees = rotationSpeed * Time.deltaTime;
        
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = direction.x;
        float ty = direction.y;

        direction.x = (cos * tx) - (sin * ty);
        direction.y = (sin * tx) + (cos * ty);

        pos.x = pos.x + direction.x * speed * Time.deltaTime;
        pos.y = pos.y + direction.y * speed * Time.deltaTime;

        gameObject.transform.position = pos;

    }

}
