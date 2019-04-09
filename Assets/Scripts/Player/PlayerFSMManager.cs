using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK,
    DEAD
}

public class PlayerFSMManager : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerState startState;
    public Transform marker;
    public CharacterController cc;
    public Animation anim;
    public float moveSpeed;
    public float rotateSpeed;
    public float fallSpeed;

    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        cc = GetComponent<CharacterController>();

        states.Add(PlayerState.IDLE, GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());

        anim = GetComponentInChildren<Animation>();
        //moveSpeed = 3;
    }

    private void Start()
    {
        SetState(startState);
    }

    public void SetState(PlayerState newState)
    {
        foreach (PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
        states[newState].BeginState();
        currentState = newState;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000))
            {
                marker.position = hit.point;
                SetState(PlayerState.RUN);
            }
        }
        
    }

}
