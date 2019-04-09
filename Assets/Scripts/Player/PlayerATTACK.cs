using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATTACK : PlayerFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        //manager.anim.CrossFade("KK_Attack");
        manager.marker.gameObject.SetActive(false);
        manager.attackMarker.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = manager.target.position - transform.position;
        diff.y = 0;
        if (diff.sqrMagnitude > manager.attackRange * manager.attackRange)
        {
            manager.SetState(PlayerState.CHASE);
            return;
        }
    }
}
