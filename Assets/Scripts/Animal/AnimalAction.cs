using System;
using System.Collections;
using Animal;
using Unity.Behavior;
using UnityEngine;

public enum ActionType
{
    Feed,
    Entertain,
    Heal
}

public class AnimalAction : MonoBehaviour
{
    public ActionType type;
    public AnimalBase animal;
    public Transform target;
    public float actionDuration;

    private BehaviorGraphAgent _agent;
    private Clickable _clickable;

    private void Start()
    {
        _agent = animal.GetComponent<BehaviorGraphAgent>();
        _clickable = animal.GetComponentInParent<Clickable>();
    }

    public void Execute()
    {
        StopAgent();
        StartCoroutine(RotateAndMoveTowardsTarget());
    }

    private void StopAgent()
    {
        _agent.End();
    }

    private IEnumerator RotateAndMoveTowardsTarget()
    {
        if (target == null) yield break;
        _clickable.enabled = false;
        // **ROTACIÓN**
        // Calcula la rotación inicial y final
        Quaternion initialRotation = animal.transform.rotation;
        Vector3 flatTargetPosition = new Vector3(target.position.x, animal.transform.position.y, target.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(flatTargetPosition - animal.transform.position);

        // Duración del giro
        float rotationDuration = 2f;
        float rotationElapsed = 0f;

        // Interpola la rotación a lo largo del tiempo
        while (rotationElapsed < rotationDuration)
        {
            rotationElapsed += Time.deltaTime;
            float t = rotationElapsed / rotationDuration;
            // Interpolación esférica para rotar suavemente
            animal.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            yield return null; // Espera hasta el siguiente frame
        }

        // Asegura que termina exactamente mirando al objetivo
        animal.transform.rotation = targetRotation;
        float maxSpeed = 2f;
        float slowDownDistance = 3f;
        Animator animator = animal.GetComponent<Animator>();
        // **MOVIMIENTO**
        while (Vector3.Distance(animal.transform.position, target.position) > 1.5f)
        {
            float distanceToTarget = Vector3.Distance(animal.transform.position, target.position);

            // Calcula la velocidad basada en la distancia al objetivo
            float currentSpeed = Mathf.Lerp(0f, maxSpeed, Mathf.Clamp01(distanceToTarget / slowDownDistance));

            // Actualiza el parámetro de velocidad en el Animator
            if (animator != null)
            {
                animator.SetFloat("Speed", currentSpeed);
            }

            Vector3 targetPosition = new Vector3(target.position.x, animal.transform.position.y, target.position.z);
            // Mueve al objeto hacia el objetivo
            animal.transform.position = Vector3.MoveTowards(
                animal.transform.position,
                targetPosition,
                currentSpeed * Time.deltaTime
            );

            yield return null; // Espera al siguiente frame
        }

        // Asegura que el parámetro de velocidad se detiene cuando el movimiento termina
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            switch (type)
            {
                case ActionType.Feed:
                    animator.Play("Eat");
                    animator.Play("Eyes_Happy");
                    break;
                case ActionType.Entertain:
                    animator.Play("Spin");
                    animator.Play("Eyes_Spin");
                    break;
                case ActionType.Heal:
                    animator.Play("Swim");
                    animator.Play("Eyes_Happy");
                    break;
            }

            yield return new WaitForSeconds(actionDuration);
            animator.Play("Walk");
            animator.Play("Eyes_Blink");
            _agent.Start();
            _clickable.enabled = true;
        }
    }
}