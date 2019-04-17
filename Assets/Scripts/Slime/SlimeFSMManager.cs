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
    public Animator anim;
    public Transform target;
    public Camera sight;
    public CharacterController playerCC;

    public float attackRange;
    public float moveSpeed;
    public float rotateSpeed;
    public float fallSpeed;


    Dictionary<SlimeState, SlimeFSMState> states = new Dictionary<SlimeState, SlimeFSMState>();
    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        states.Add(SlimeState.IDLE, GetComponent<SlimeIDLE>());
        states.Add(SlimeState.PATROL, GetComponent<SlimePATROL>());
        states.Add(SlimeState.CHASE, GetComponent<SlimeCHASE>());
        states.Add(SlimeState.ATTACK, GetComponent<SlimeATTACK>());

        anim = GetComponentInChildren<Animator>();
        sight = GetComponentInChildren<Camera>();
        playerCC = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

    }


    //에디터에서도 돌아가는 로직
    private void OnDrawGizmos()
    {
        if(sight != null)
        {
            Gizmos.color = Color.red;
            Matrix4x4 temp = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(
                sight.transform.position,
                sight.transform.rotation,
                Vector3.one
                );
            Gizmos.DrawFrustum(Vector3.zero,
                sight.fieldOfView, // 시야각
                sight.farClipPlane, // far(원) 평면 거리
                sight.nearClipPlane, // near(근) 평면 거리
                16/9f); // 종횡비(view port 비율)  (넓이를 높이로 나눈 값)(넓이 / 높이)


            Gizmos.matrix = temp;
        }
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
