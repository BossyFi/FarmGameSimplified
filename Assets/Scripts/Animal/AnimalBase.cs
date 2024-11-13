using System;
using AnimalScriptable;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animal
{
    public class AnimalBase : MonoBehaviour
    {
        public AnimalData animalData;
        public float hunger;
        public float entertainment;
        public float health;

        public bool canGetHungry;
        public bool canGetBored;
        public bool canGetSick;

        public MMF_Player hungerFeedback;
        public MMF_Player boredFeedback;
        public MMF_Player sickFeedback;

        private float hungerTime;
        private float entertainmentTime;
        private float healthTime;

        private bool IsHungry => hunger <= 0;
        private bool IsBored => entertainment <= 0;
        private bool IsSick => health <= 0;

        private bool wasHungry;
        private bool wasBored;
        private bool wasSick;

        protected virtual void Start()
        {
            hunger = animalData.baseHunger;
            entertainment = animalData.baseEntertainment;
            health = animalData.baseHealth;
        }

        public virtual void Update()
        {
            UpdateAnimalStats();
            InvokeFeedbacks();
        }

        private void UpdateAnimalStats()
        {
            if (canGetHungry && !IsHungry)
            {
                hungerTime += Time.deltaTime;
                hunger = animalData.baseHunger *
                         animalData.hungerCurve.Evaluate(hungerTime / animalData.timeToDepleteHunger);
            }

            if (canGetBored && !IsBored)
            {
                entertainmentTime += Time.deltaTime;
                entertainment = animalData.baseEntertainment *
                                animalData.entertainmentCurve.Evaluate(entertainmentTime /
                                                                       animalData.timeToDepleteEntertainment);
            }

            if (canGetSick && !IsSick)
            {
                healthTime += Time.deltaTime;
                health = animalData.baseHealth *
                         animalData.healthCurve.Evaluate(healthTime / animalData.timeToDepleteHealth);
            }
        }

        private void InvokeFeedbacks()
        {
            // Verifica si el estado de hambre ha cambiado a true
            if (IsHungry && !wasHungry)
            {
                hungerFeedback?.PlayFeedbacks(); // Dispara el evento de hambre
                wasHungry = true; // Actualiza el estado previo
            }
            else if (!IsHungry)
            {
                wasHungry = false;
            }

            // Verifica si el estado de aburrimiento ha cambiado a true
            if (IsBored && !wasBored)
            {
                boredFeedback?.PlayFeedbacks(); // Dispara el evento de aburrimiento
                wasBored = true; // Actualiza el estado previo
            }
            else if (!IsBored)
            {
                wasBored = false;
            }

            // Verifica si el estado de enfermedad ha cambiado a true
            if (IsSick && !wasSick)
            {
                sickFeedback?.PlayFeedbacks(); // Dispara el evento de enfermedad
                wasSick = true; // Actualiza el estado previo
            }
            else if (!IsSick)
            {
                wasSick = false;
            }
        }
    }
}