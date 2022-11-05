using UnityEngine;

public abstract class InputService : IInputSevice
{
    protected const string _verticalAxis = "Vertical";
    protected const string _horizontalAxis = "Horizontal";

    public abstract Vector2 Axis { get; }

    protected static Vector2 DesktopInput() =>
            new Vector2(UnityEngine.Input.GetAxis(_horizontalAxis), UnityEngine.Input.GetAxis(_verticalAxis));
}
