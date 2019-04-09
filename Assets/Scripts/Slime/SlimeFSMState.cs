using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSMState : MonoBehaviour {

    public SlimeFSMManager manager;

    private void Awake()
    {
        manager = GetComponent<SlimeFSMManager>();
    }

    public virtual void BeginState()
    {

    }
}
