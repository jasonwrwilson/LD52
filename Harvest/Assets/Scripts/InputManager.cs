using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private ProjectileManager projectileManager;
    private ScarecrowBehaviour scarecrow;
    
    // Start is called before the first frame update
    void Start()
    {
        projectileManager = gameObject.GetComponent<ProjectileManager>();
        scarecrow = gameObject.GetComponent<ScarecrowBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fire Bullets
        if(Input.GetButtonDown("Fire1"))
        {
           projectileManager.FireBullet();
        }

        //Movement
        if (!scarecrow.IsMoving())
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            if (horizontalMovement > 0)
            {
                scarecrow.MoveLeft();
            }
            else if (horizontalMovement < 0)
            {
                scarecrow.MoveRight();
            }
            else if (verticalMovement > 0)
            {
                scarecrow.MoveUp();
            }
            else if (verticalMovement < 0)
            {
                scarecrow.MoveDown();
            }
        }

    }
}
