using UnityEngine;

namespace AnimalScriptable
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "Scriptable Objects/AnimalData")]
    public class AnimalData : ScriptableObject
    {
        public float baseHunger;
        public float baseEntertainment;
        public float baseHealth;

        public int timeToDepleteHunger;
        public int timeToDepleteEntertainment;
        public int timeToDepleteHealth;

        public AnimationCurve hungerCurve;
        public AnimationCurve entertainmentCurve;
        public AnimationCurve healthCurve;

        public Item.ItemType favFood;
    }
}