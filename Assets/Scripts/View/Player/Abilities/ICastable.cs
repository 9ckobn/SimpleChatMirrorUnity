using System.Collections;
using UnityEngine;
public interface ICastable
{
    public void Cast(CharacterController whoCast, AbilityConfig ability, Vector3 Direction);
    public void StartCooldown(float time);
}