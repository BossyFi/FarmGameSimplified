using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Behaviour
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Rotate To Location", story: "[Agent] rotates to [Location] for [duration] seconds",
        category: "Action", id: "529a822474a255ca6ad3447aa4a31750")]
    public partial class RotateToLocationAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<Vector3> Location;
        [SerializeReference] public BlackboardVariable<int> Duration;
        private Quaternion _targetRotation;
        private Quaternion _initialRotation;
        [CreateProperty] private float _progress;

        protected override Status OnStart()
        {
            if (Agent.Value == null)
            {
                LogFailure("No Target set.");
                return Status.Failure;
            }

            // Ajustar la posición del objetivo en el mismo plano Y que el agente
            Vector3 flatTargetPosition = new Vector3(Location.Value.x, Agent.Value.transform.position.y, Location.Value.z);
            _targetRotation = Quaternion.LookRotation(flatTargetPosition - Agent.Value.transform.position);

            if (Duration.Value <= 0.0f)
            {
                Agent.Value.transform.rotation = _targetRotation;
                return Status.Success;
            }

            _initialRotation = Agent.Value.transform.rotation;

            _progress = 0;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            _progress += Time.deltaTime / Duration.Value;
            float normalizedProgress = Mathf.Clamp01(_progress);

            Agent.Value.transform.rotation = Quaternion.Slerp(_initialRotation, _targetRotation, normalizedProgress);

            return normalizedProgress >= 1.0f ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
        }
    }
}