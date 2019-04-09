using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRUN : PlayerFSMState
{
    public override void BeginState()
    {
        base.BeginState();
        manager.anim.CrossFade("KK_Run");
    }

    void Update () {

        Vector3 deltaMove = Vector3.MoveTowards(
            transform.position, 
            manager.marker.position,
            Time.deltaTime * manager.moveSpeed) - transform.position;

        deltaMove.y = -manager.fallSpeed * Time.deltaTime;

        manager.cc.Move(deltaMove);

        Vector3 dir = manager.marker.position - transform.position;
        dir.y = 0;
        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(dir),
                manager.rotateSpeed * Time.deltaTime
                );
        }

        Vector3 diff = manager.marker.position - transform.position;
        diff.y = 0;
        if(diff.sqrMagnitude < 0.1f * 0.1f)
        {
            manager.SetState(PlayerState.IDLE);
            return;
        }

    }
}
