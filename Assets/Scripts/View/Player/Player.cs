using Mirror;
using UnityEngine;

public class Player : PlayerAbilities
{
    [SerializeField] private AbilityConfig Dash;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = false;
    }

    public void Update()
    {
        if (isLocalPlayer == false)
            return;

        Vector3 movementVector = Vector3.zero;

        if (_inputSrevice.Axis.sqrMagnitude > float.Epsilon)
        {
            movementVector = cam.transform.TransformDirection(_inputSrevice.Axis);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
        }

        movementVector += Physics.gravity;

        Move(movementVector);

        if (_inputSrevice.buttonDown)
            CastDash(Dash, movementVector);

        if (AnimatorHandler.characterController.velocity.magnitude > 1)
            AnimatorHandler.animatorState = AnimatorState.run;
        else
            AnimatorHandler.animatorState = AnimatorState.idle;
    }

    public void Move(Vector3 movementVector)
    {
        movementVector += Physics.gravity;

        _characterController.Move(_speed * movementVector * Time.deltaTime);
    }
}
