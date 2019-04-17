using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public PlayerFSMManager manager;

    private void Awake()
    {
        manager = transform.root.GetComponent<PlayerFSMManager>();
    }

    void AttackHitCheck()
    {
        manager.AttackCheck();
    }
}
