using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CKUtil
{
    public static bool Detect(Camera sight, float aspect, CharacterController cc)
    {
        sight.aspect = aspect;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }

    public static void CKMove(CharacterController cc,
        Transform self,
        Vector3 targetPos,
        float moveSpeed,
        float rotateSpeed,
        float fallSpeed)
    {
        Vector3 deltaMove = Vector3.MoveTowards(
            self.position,
            targetPos,
            Time.deltaTime * moveSpeed) - self.position;

        deltaMove.y = -fallSpeed * Time.deltaTime;

        cc.Move(deltaMove);

        Vector3 dir = targetPos - self.position;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            self.rotation = Quaternion.RotateTowards(
                self.rotation,
                Quaternion.LookRotation(dir),
                rotateSpeed * Time.deltaTime
                );
        }
    }
}
