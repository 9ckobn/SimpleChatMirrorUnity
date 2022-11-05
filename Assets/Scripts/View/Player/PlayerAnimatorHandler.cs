using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    private static string MoveHash = "Move";

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

    // Update is called once per frame
    private void Update()
    {
        if (characterController.velocity.magnitude > 1)
            animatorState = AnimatorState.run;
        else
            animatorState = AnimatorState.idle;
    }

    private void ChangeState(AnimatorState state)
    {
        switch (state)
        {
            case AnimatorState.idle:
                animator.SetFloat(MoveHash, 0, .1f, Time.deltaTime);
                break;
            case AnimatorState.run:
                animator.SetFloat(MoveHash, characterController.velocity.magnitude, .1f, Time.deltaTime);
                break;
            default:
                break;
        }
    }
}

public enum AnimatorState
{
    idle,
    run
}
