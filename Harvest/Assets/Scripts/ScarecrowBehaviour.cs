using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowBehaviour : MonoBehaviour
{
    [SerializeField] private float moveTime;
    private float remainingMoveTime;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    [SerializeField] private float hopHeight;
    [SerializeField] private FieldManager fieldManager;
    
    // Start is called before the first frame update
    void Start()
    {
        remainingMoveTime = 0;
        fieldManager.RegisterScarecrowLocation(gameObject.transform.position.x, gameObject.transform.position.y - 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingMoveTime > 0)
        {
            remainingMoveTime -= Time.deltaTime;
            bool landed = false;
            if (remainingMoveTime < 0)
            {
                remainingMoveTime = 0;
                landed = true;
            }
            Vector3 pos = Vector3.Lerp(targetPosition, startingPosition, remainingMoveTime / moveTime);
            pos.y += hopHeight * Mathf.Sin((remainingMoveTime / moveTime) * Mathf.PI);
            gameObject.transform.position = pos;

            if (landed)
            {
                fieldManager.RegisterScarecrowLocation(pos.x, pos.y - 1);
            }
        }
    }

    public void MoveRight()
    {
        if (fieldManager.CanMoveRight())
        {
            startingPosition = gameObject.transform.position;
            targetPosition = startingPosition;
            targetPosition.x -= 1;
            remainingMoveTime = moveTime;
        }
    }

    public void MoveLeft()
    {
        if (fieldManager.CanMoveLeft())
        {
            startingPosition = gameObject.transform.position;
            targetPosition = startingPosition;
            targetPosition.x += 1;
            remainingMoveTime = moveTime;
        }
    }

    public void MoveUp()
    {
        if (fieldManager.CanMoveUp())
        {
            startingPosition = gameObject.transform.position;
            targetPosition = startingPosition;
            targetPosition.y += 1;
            remainingMoveTime = moveTime;
        }
    }

    public void MoveDown()
    {
        if (fieldManager.CanMoveDown())
        {
            startingPosition = gameObject.transform.position;
            targetPosition = startingPosition;
            targetPosition.y -= 1;
            remainingMoveTime = moveTime;
        }
    }

    public bool IsMoving()
    {
        return remainingMoveTime > 0;
    }
}
