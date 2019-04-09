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
    public Transform attackMarker;
    public Transform target;
    public CharacterController cc;
    public Animator anim;
    public float moveSpeed;
    public float rotateSpeed;
    public float fallSpeed;
    public float attackRange;

    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    public int layerMask;

    private void Awake()
    {
        layerMask = (1 << 9) | (1 << 10);
        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        attackMarker = GameObject.FindGameObjectWithTag("AttackMarker").transform;
        cc = GetComponent<CharacterController>();

        states.Add(PlayerState.IDLE, GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());

        anim = GetComponentInChildren<Animator>();
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
            if (Physics.Raycast(r, out hit, 1000, layerMask))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    marker.position = hit.point;
                    SetState(PlayerState.RUN);
                }
                else if (hit.transform.gameObject.layer == 10)
                {
                    target = hit.transform;
                    attackMarker.parent = hit.transform;
                    attackMarker.transform.localPosition = Vector3.zero;
                    SetState(PlayerState.CHASE);
                }
            }
        }
        
    }

}
