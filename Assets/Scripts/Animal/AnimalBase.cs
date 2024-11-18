using System;
using System.Collections;
using AnimalScriptable;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animal
{
    public class AnimalBase : MonoBehaviour
    {
        [Header("SCRIPTABLE OBJECT")] public AnimalData animalData;
        [Space(1)] [Header("STATS")] public float hunger;
        public float entertainment;
        public float health;
        public float happiness;

        [Space(1)] [Header("FLAGS")] public bool canGetHungry;
        public bool canGetBored;
        public bool canGetSick;

        [Space(1)] [Header("FEEDBACKS")] public MMF_Player hungerFeedback;
        public MMF_Player boredFeedback;
        public MMF_Player sickFeedback;

        #region Private Variables

        private float _hungerTime;
        private float _entertainmentTime;
        private float _healthTime;

        private bool IsHungry => hunger <= animalData.startCryingHunger * animalData.baseHunger;
        private bool IsBored => entertainment <= animalData.startCryingEntertainment * animalData.baseEntertainment;
        private bool IsSick => health <= animalData.startCryingHealth * animalData.baseHealth;

        private bool _wasHungry;
        private bool _wasBored;
        private bool _wasSick;

        private float _maxHunger;
        private float _maxEntertainment;
        private float _maxHealth;

        private float _prevHunger;
        private float _prevEntertainment;
        private float _prevHealth;

        private float _maxHungerTime;
        private float _maxEntertainmentTime;
        private float _maxHealthTime;

        private WaitForSeconds _waitTime;
        private bool _canCheckHappiness;

        public bool CanCheckHappiness
        {
            get => _canCheckHappiness;
            set
            {
                if (_canCheckHappiness != value)
                {
                    _canCheckHappiness = value;
                    if (_canCheckHappiness)
                    {
                        StartCoroutine(nameof(CheckHappiness));
                    }
                    else
                    {
                        StopCoroutine(nameof(CheckHappiness));
                    }
                }
            }
        }

        #endregion


        protected virtual void Start()
        {
            InitializeData();
        }

        public virtual void Update()
        {
            UpdateAnimalStats();
            InvokeFeedbacks();
        }

        private void UpdateAnimalStats()
        {
            if (canGetHungry && hunger > 0)
            {
                if (!Mathf.Approximately(_prevHunger, hunger))
                {
                    _hungerTime = 0;
                    _maxHunger = hunger;
                    _maxHungerTime = _maxHunger * animalData.timeToDepleteHunger / animalData.baseHunger;
                }

                _hungerTime += Time.deltaTime;
                hunger = _maxHunger *
                         animalData.hungerCurve.Evaluate(_hungerTime / _maxHungerTime);
                _prevHunger = hunger;
            }

            if (canGetBored && entertainment > 0)
            {
                if (!Mathf.Approximately(_prevEntertainment, entertainment))
                {
                    _entertainmentTime = 0;
                    _maxEntertainment = entertainment;
                    _maxEntertainmentTime = _maxEntertainment * animalData.timeToDepleteEntertainment /
                                            animalData.baseEntertainment;
                }

                _entertainmentTime += Time.deltaTime;
                entertainment = _maxEntertainment *
                                animalData.entertainmentCurve.Evaluate(_entertainmentTime / _maxEntertainmentTime);
                _prevEntertainment = entertainment;
            }

            if (canGetSick && health > 0)
            {
                if (!Mathf.Approximately(_prevHealth, health))
                {
                    _healthTime = 0;
                    _maxHealth = health;
                    _maxHealthTime = _maxHealth * animalData.timeToDepleteHealth / animalData.baseHealth;
                }

                _healthTime += Time.deltaTime;
                health = _maxHealth *
                         animalData.healthCurve.Evaluate(_healthTime / _maxHealthTime);
                _prevHealth = health;
            }
        }

        private void InvokeFeedbacks()
        {
            // Verifica si el estado de hambre ha cambiado a true
            if (IsHungry && !_wasHungry)
            {
                hungerFeedback?.PlayFeedbacks(); // Dispara el evento de hambre
                _wasHungry = true; // Actualiza el estado previo
            }
            else if (!IsHungry)
            {
                _wasHungry = false;
            }

            // Verifica si el estado de aburrimiento ha cambiado a true
            if (IsBored && !_wasBored)
            {
                boredFeedback?.PlayFeedbacks(); // Dispara el evento de aburrimiento
                _wasBored = true; // Actualiza el estado previo
            }
            else if (!IsBored)
            {
                _wasBored = false;
            }

            // Verifica si el estado de enfermedad ha cambiado a true
            if (IsSick && !_wasSick)
            {
                sickFeedback?.PlayFeedbacks(); // Dispara el evento de enfermedad
                _wasSick = true; // Actualiza el estado previo
            }
            else if (!IsSick)
            {
                _wasSick = false;
            }
        }

        private void InitializeData()
        {
            hunger = animalData.baseHunger;
            entertainment = animalData.baseEntertainment;
            health = animalData.baseHealth;
            happiness = 0;
            _maxHunger = hunger;
            _maxEntertainment = entertainment;
            _maxHealth = health;
            _maxHungerTime = animalData.timeToDepleteHunger;
            _maxEntertainmentTime = animalData.timeToDepleteEntertainment;
            _maxHealthTime = animalData.timeToDepleteHealth;
            _prevHunger = hunger;
            _prevEntertainment = entertainment;
            _prevHealth = health;
            _waitTime = new WaitForSeconds(animalData.timeToCheckHappiness);
        }

        private IEnumerator CheckHappiness()
        {
            int hungerValue = 0;
            int entertainmentValue = 0;
            int healthValue = 0;
            while (CanCheckHappiness)
            {
                hungerValue = IsHungry ? -2 : 1;
                entertainmentValue = IsBored ? -1 : 1;
                healthValue = IsSick ? -3 : 0;
                happiness = Mathf.Clamp(happiness + hungerValue + entertainmentValue + healthValue, 0, 100);

                yield return _waitTime;
            }
        }

        [ContextMenu("Switch Happiness Flag")]
        public void SwitchHappinessFlag()
        {
            CanCheckHappiness = !CanCheckHappiness;
        }
    }
}