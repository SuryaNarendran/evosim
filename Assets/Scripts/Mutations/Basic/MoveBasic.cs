using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveBasic")]
public class MoveBasic : Mutation
{
    [SerializeField] float speed;
    [SerializeField] float movementCost;
    [SerializeField] float motionChangeRate;

    public Vector2 direction = Vector2.zero;
    public  Vector2 directionTarget = Vector2.zero;

    protected override void Setup()
    {
        myOrganism.moveDefault = MoveRandom;
        myOrganism.movementSpeed += speed;
        myOrganism.baseMovementCost = movementCost;
        myOrganism.StartCoroutine(ChangeDirection());
       
    }

    private void MoveRandom()
    {
        direction = Vector2.Lerp(direction, directionTarget, motionChangeRate * Time.fixedDeltaTime);
        myOrganism.debug_direction = direction;
        myOrganism.debug_direction_target = directionTarget;
        myOrganism.SetDirection(direction);

        float cost = -myOrganism.baseMovementCost * myOrganism.size / Organism.baseScale * myOrganism.movementSpeed;
        myOrganism.AddSize(cost * Time.fixedDeltaTime);
    }

    private IEnumerator ChangeDirection()
    {
        while (myOrganism.gameObject.activeInHierarchy)
        {
            //causes crash if no valid direction can be achieved; bad code
            //do
            //{
            //    directionTarget = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //}
            //while (Boundaries.Contains(myOrganism.position + directionTarget) == false);

            directionTarget = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            if(Boundaries.Contains(myOrganism.position + directionTarget) == false)
            {
                //move towards the centre of the map if blocked
                directionTarget = new Vector2(-(myOrganism.position.x), -(myOrganism.position.y));
                myOrganism.isBlocked = true;
            }
            else
            {
                myOrganism.isBlocked = false;
            }

            yield return new WaitForSeconds(1f/motionChangeRate);
        }
    }
}
