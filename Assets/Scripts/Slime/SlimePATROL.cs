using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePATROL : SlimeFSMState
{
    Vector3 destination;
    float elapsedMoveTime = 0f;

    public override void BeginState()
    {
        base.BeginState();
        destination = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        elapsedMoveTime = 0f;
        manager.anim.CrossFade("SL_Run");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        elapsedMoveTime += Time.deltaTime;

        Vector3 deltaMove = Vector3.MoveTowards(
            transform.position,
            destination,
            Time.deltaTime * manager.moveSpeed) - transform.position;

        deltaMove.y = -manager.fallSpeed * Time.deltaTime;

        manager.cc.Move(deltaMove);

        Vector3 dir = (destination - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(dir),
                manager.rotateSpeed * Time.deltaTime
                );
        }

        Vector3 diff = destination - transform.position;
        diff.y = 0;

        if (diff.sqrMagnitude < 0.1f * 0.1f || diff.sqrMagnitude / manager.moveSpeed + 1f < elapsedMoveTime)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }
    }
}
