using System.Collections;
using UnityEngine;

public abstract class AbilityCast : MonoBehaviour, ICastable
{
    public AbilityType currentAbility { get; set; }

    public virtual void Cast(CharacterController whoCast, AbilityConfig ability, Vector3 Direction)
    {
        StartCooldown(ability.CoolDown);
    }

    public void StartCooldown(float time)
    {
        var routine = CoolDown(time);
        StartCoroutine(routine);
    }

    public IEnumerator CoolDown(float time)
    {
        GameSettings.activeKeyCode[GameSettings.LeftMouseButton] = false;
        yield return new WaitForSeconds(time);
        GameSettings.activeKeyCode[GameSettings.LeftMouseButton] = true;

        Destroy(this);
    }
}

public enum AbilityType
{
    dash
}

