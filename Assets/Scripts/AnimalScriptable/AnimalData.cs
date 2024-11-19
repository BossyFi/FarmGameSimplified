using UI.Shop;
using Unity.Cinemachine;
using UnityEngine;

namespace AnimalScriptable
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "Scriptable Objects/AnimalData")]
    public class AnimalData : ScriptableObject
    {
        public float baseHunger = 100;
        public float baseEntertainment = 100;
        public float baseHealth = 100;
        public float baseHappiness = 50;

        public int timeToDepleteHunger = 100;
        public int timeToDepleteEntertainment = 100;
        public int timeToDepleteHealth = 100;
        public int timeToCheckHappiness = 20;
        public AnimationCurve hungerCurve = AnimationCurve.Linear(0, 1, 1, 0);
        public AnimationCurve entertainmentCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public AnimationCurve healthCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

        public ShopItem.ItemType favFood = ShopItem.ItemType.Item1;

        [Range(0, 1)] public float startCryingHunger = 0.7f;
        [Range(0, 1)] public float startCryingEntertainment = 0.5f;
        [Range(0, 1)] public float startCryingHealth = 0.3f;
    }
}