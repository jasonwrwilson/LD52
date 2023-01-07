using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : ProjectileBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        Vector3 pos = gameObject.transform.position;
        pos.x = pos.x + direction.x * speed * Time.deltaTime;
        pos.y = pos.y + direction.y * speed * Time.deltaTime;

        gameObject.transform.position = pos;
    }

}
