using UnityEngine;

public class PlayerAbilities : PlayerHitHandler
{
    public void CastDash(AbilityConfig Dash, Vector3 Direction)
    {
        var _dash = gameObject.AddComponent<Dash>();

        _dash.Cast(_characterController, Dash, Direction);

        HitCapsule.enabled = true;
    }
}
