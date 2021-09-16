////////////////////////////////////////////////////////////////////////////////
//
//	MG_Random.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace MG_Liquidator
{
    public static class MG_Random
    {
        private static readonly Random rnd = new Random();

        #region Public Methods     

        public static int Random(int Min, int Max)
        {
     
            return rnd.Next(Min, Max);
        }

        public static int Random(int Max)
        {
            return rnd.Next(Max);
        }

        public static int Random()
        {
            return rnd.Next(100);
        }

        ///// <summary>
        ///// Перемешать лист
        ///// </summary>
        ///// <typeparam name="E"></typeparam>
        ///// <param name="inputList"></param>
        ///// <returns></returns>
        //public static List<E> ShuffleList<E>(List<E> inputList)
        //{
        //    List<E> randomList = new List<E>();

        //    Random r = new Random();
        //    int randomIndex = 0;
        //    while (inputList.Count > 0)
        //    {
        //        randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
        //        randomList.Add(inputList[randomIndex]); //add it to the new, random list
        //        inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        //    }

        //    return randomList; //return the new random list
        //}

        public static E RandomElement<E>(List<E> inputList)
        {
            if (inputList.Count == 1)
            {
                return inputList[0];
            }


            E result = inputList[Random(0, inputList.Count)];
            return result;
        }

        public static E RandomElement<E>(E[] inputArray)
        {
            int lenght = inputArray.Length;

            if (lenght == 1)
            {
                return inputArray[0];
            }

            E result = inputArray[Random(0, lenght)];
            return result;
        }

        public static bool RandomB()
        {
            return (Random() > 50);
        }

        #endregion Public Methods
    }
}
