using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    // Start is called before the first frame update
    public FPSPlayer player;
    FSM<PlayerStates> _fsm;
    void Start()
    {
        SetFSM();
    }
    void SetFSM()
    {
        var idle = new BaseState<PlayerStates>();
        var walk = new BaseState<PlayerStates>();

        //behaviourz
        walk.Execute = owo;

        idle.AddTransition(PlayerStates.walk, walk);
        walk.AddTransition(PlayerStates.idle, idle);

        _fsm = new FSM<PlayerStates>(idle);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (_fsm.CanTransicion(PlayerStates.walk))
            {
                _fsm.Transition(PlayerStates.walk);
            }
        }
        else
        {
            if (_fsm.CanTransicion(PlayerStates.idle))
            {
                _fsm.Transition(PlayerStates.idle);
            }
        }
        _fsm.OnUpdate();
        //Independientemente de la fsm la camara se tiene que mover
        //camContr.MoveCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    void owo()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        player.Movement(dir);
    }
}

