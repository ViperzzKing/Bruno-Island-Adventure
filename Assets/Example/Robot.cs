using UnityEngine;

// RPG is the main namespace for our game
namespace RPG.Example
{
    public class Robot : MonoBehaviour
    {
        /*
         Varibles
        ==========
        */
        public int numberr = 1;
        public float price = 20.99f;
        private string word = "Word";
        public bool isTurnedOn = false;

        public Robot()
        {
            // isTurnedOn = true;
            price = CalculatePrice(0.1f);
        }

        public float CalculatePrice(float discount)
        {
            return (price - (price * discount)) * 2;
        }
    }
}