using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioHandler))]
public class PlayerAnimatorHandler : MonoBehaviour
{
    private static string MoveHash = "Move";

    private AudioHandler audioHandler;

    public CharacterController characterController;

    public Animator animator;
    private AnimatorState _currentState;
    public AnimatorState animatorState
    {
        private get { return _currentState; }
        set
        {
            ChangeState(value);
            _currentState = value;
        }
    }

    void Start()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    private void ChangeState(AnimatorState state)
    {
        switch (state)
        {
            case AnimatorState.idle:
                animator.SetBool(MoveHash, false);
                Debug.Log("Idle");
                break;
            case AnimatorState.run:
                animator.SetBool(MoveHash, true);
                Debug.Log(MoveHash);
                break;
            default:
                break;
        }
    }

    public void StepEvent()
    {
        audioHandler.PlaySound();
    }
}

public enum AnimatorState
{
    idle,
    run
}
