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
        manager.anim.CrossFade("SL_Idle");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > patrolTime)
        {
            manager.SetState(SlimeState.PATROL);
            return;
        }

    }
}
