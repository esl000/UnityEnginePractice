using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIDLE : SlimeFSMState {

    public float patrolTime;
    float elapsedTime = 0f;

    public override void BeginState()
    {
        base.BeginState();
        elapsedTime = 0f;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(CKUtil.Detect(manager.sight, 1f, manager.playerCC))
        {

        }

        elapsedTime += Time.deltaTime;

        if(elapsedTime > patrolTime)
        {
            manager.SetState(SlimeState.PATROL);
            return;
        }

    }
}
