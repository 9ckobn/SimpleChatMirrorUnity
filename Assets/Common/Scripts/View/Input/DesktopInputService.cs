using UnityEngine;

public class DesktopInputService : InputService
{
    public override Vector2 Axis
    {
        get
        {
            Vector2 Axis = DesktopInput();

            if (Axis == Vector2.zero)
                Axis = UnityAxis();

            return Axis;
        }
    }

    private static Vector2 UnityAxis() =>
        new Vector2(UnityEngine.Input.GetAxis(_horizontalAxis), UnityEngine.Input.GetAxis(_verticalAxis));
}
