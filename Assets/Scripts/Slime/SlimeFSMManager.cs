using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    IDLE = 0,
    PATROL,
    CHASE,
    ATTACK,
    DEAD
}

public class SlimeFSMManager : MonoBehaviour {

    public SlimeState currentState;
    public SlimeState startState;
    public CharacterController cc;
    public Animation anim;
    public float moveSpeed;
    public float rotateSpeed;
    public float fallSpeed;


    Dictionary<SlimeState, SlimeFSMState> states = new Dictionary<SlimeState, SlimeFSMState>();
    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        states.Add(SlimeState.IDLE, GetComponent<SlimeIDLE>());
        states.Add(SlimeState.PATROL, GetComponent<SlimePATROL>());

        anim = GetComponentInChildren<Animation>();
    }


    private void Start()
    {
        SetState(startState);
    }

    public void SetState(SlimeState newState)
    {
        foreach (SlimeFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
        states[newState].BeginState();
        currentState = newState;
    }

}
