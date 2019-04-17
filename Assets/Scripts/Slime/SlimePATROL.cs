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
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (CKUtil.Detect(manager.sight, 1f, manager.playerCC))
        {

        }

        elapsedMoveTime += Time.deltaTime;

        CKUtil.CKMove(manager.cc, transform,
            destination, manager.moveSpeed,
            manager.rotateSpeed, manager.fallSpeed);    

        Vector3 diff = destination - transform.position;
        diff.y = 0;

        if (diff.sqrMagnitude < 0.1f * 0.1f || diff.sqrMagnitude / manager.moveSpeed + 1f < elapsedMoveTime)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }
    }
}
