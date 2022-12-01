using System.Collections;
using UnityEngine;

public class Dash : AbilityCast
{
    public override void Cast(CharacterController whoCast, AbilityConfig ability, Vector3 Direction)
    {
        var dashRoutine = DashRoutine(whoCast, ability, Direction);
        StartCoroutine(dashRoutine);

        StartCooldown(ability.CoolDown);
    }

    private IEnumerator DashRoutine(CharacterController whoCast, AbilityConfig ability, Vector3 Direction)
    {
        float startTime = Time.time;

        while (Time.time < startTime + ability.CastDuration)
        {
            whoCast.Move(Direction * (ability.dashDistance * 2) * Time.deltaTime);

            yield return null;
        }
    }
}
