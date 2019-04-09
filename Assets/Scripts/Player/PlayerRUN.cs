using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRUN : PlayerFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        //manager.anim.CrossFade("KK_Run");
        manager.attackMarker.gameObject.SetActive(false);
    }

    void Update () {

        CKUtil.CKMove(manager.cc, transform,
            manager.marker.position, manager.moveSpeed,
            manager.rotateSpeed, manager.fallSpeed);

        Vector3 diff = manager.marker.position - transform.position;
        diff.y = 0;
        if(diff.sqrMagnitude < 0.1f * 0.1f)
        {
            manager.SetState(PlayerState.IDLE);
            return;
        }

    }
}
