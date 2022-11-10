using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController), typeof(Animator), typeof(PlayerAnimatorHandler))]
public class PlayerBehaviour : NetworkBehaviour
{
    public float _speed;
    protected IInputSevice _inputSrevice;

    protected CharacterController _characterController;
    private Animator animator;

    protected PlayerAnimatorHandler AnimatorHandler;

    [SerializeField] private Cinemachine.CinemachineFreeLook Camera;

    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log("Entered");

        _inputSrevice = Game.InputService;
        _characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();
        InitializeAnimator();

        if(!isLocalPlayer)
            Destroy(Camera.gameObject);
    }


    public void InitializeAnimator()
    {
        AnimatorHandler = GetComponent<PlayerAnimatorHandler>();

        AnimatorHandler.animator = animator;
        AnimatorHandler.characterController = _characterController;
    }
}
