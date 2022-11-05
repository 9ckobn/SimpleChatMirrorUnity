using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour : MonoBehaviour
{
    private IInputSevice _inputSrevice;

    private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private Animator animator;

    public PlayerAnimatorHandler AnimatorHandler;

    private Camera camera;

    void Awake()
    {
        _inputSrevice = Game.InputService;
        _characterController = GetComponent<CharacterController>();
        InitializeAnimator();

        camera = Camera.main;
    }

    public void InitializeAnimator()
    {
         AnimatorHandler = gameObject.AddComponent<PlayerAnimatorHandler>();

         AnimatorHandler.animator = animator;
         AnimatorHandler.characterController = _characterController;
    }

    void FixedUpdate()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputSrevice.Axis.sqrMagnitude > float.Epsilon)
        {
            movementVector = camera.transform.TransformDirection(_inputSrevice.Axis);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;

        }

        movementVector += Physics.gravity;

        _characterController.Move(_speed * movementVector * Time.deltaTime);
    }
}
