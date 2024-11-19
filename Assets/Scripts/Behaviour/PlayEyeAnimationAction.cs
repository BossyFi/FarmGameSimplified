using System;
using Animal;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Play Eye Animation", story: "[Animator] change Eyes", category: "Action",
    id: "b713e266992612743d07059ce11aba17")]
public partial class PlayEyeAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Animator;

    private AnimalBase _animalBase;
    private bool _isHappy;

    protected override Status OnStart()
    {
        if (!Animator?.Value)
        {
            return Status.Failure;
        }

        _animalBase = Animator.Value.GetComponent<AnimalBase>();
        _isHappy = _animalBase.GetBoolStats();
        Animator.Value.Play(_isHappy ? "Eyes_Happy" : "Eyes_Sad");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}