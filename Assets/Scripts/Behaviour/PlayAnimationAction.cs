using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Play Animation", story: "[Animator] plays [Animation]", category: "Action",
        id: "d87c7907e9922f4b7845e79d775466df")]
    public partial class PlayAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<Animator> Animator;
        [SerializeReference] public BlackboardVariable<string> Animation;

        protected override Status OnStart()
        {
            if (!Animator?.Value)
            {
                return Status.Failure;
            }

            if (string.IsNullOrEmpty(Animation.Value))
            {
                return Status.Success;
            }

            Animator.Value.Play(Animation.Value);
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
}