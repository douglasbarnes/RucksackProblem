using System;
using System.Collections.Generic;

namespace RucksackProblem
{
    public struct Item
    {
        public float Ratio;
        public int Index;
        public Item(float ratio, int index)
        {
            Ratio = ratio;
            Index = index;
        }
        public override string ToString()
        {
            return $"R:{Ratio} I:{Index}";
        }
    }
    class Program
    {
        public const int MaxMass = 15;
        public static byte[] Masses = new byte[] { 1, 12,2,1,4};
        public static byte[] Prices = new byte[] { 2,4,2,1,10};
        static void Main(string[] args)
        {            
            // Sorted list of parsed items
            List<Item> Items = FindRatios(Masses, Prices);

            // All ratios:index pairs
            Console.WriteLine(string.Join(' ', Items));

            // The optimal solutions; best items to pack.

            // 0/1
            Console.WriteLine("\n" + OneOrZero(Items));

            // Unbounded
            Console.WriteLine("\n" + Unbounded(Items));
        }

        public static string Unbounded(List<Item> Items)
        {
            string Order = "Unbounded: ";
            int MassLeft = MaxMass;
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                // Use as many as the best ratio that fits before moving onto the second best ratio. If the
                // second best ratio no longer fits, move on to the third etc.
                while (MassLeft >= Masses[Items[i].Index])
                {
                    Order += $" {Items[i].Index} ";
                    MassLeft -= Masses[Items[i].Index];
                }
            }
            return Order;
        }

        public static string OneOrZero(List<Item> Items)
        {
            string Order = "0/1: ";
            int MassLeft = MaxMass;
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if(MassLeft >= Masses[Items[i].Index])
                {
                    // Each item can only be added once in 0/1, so move on to the next best after the best has been added.
                    Order += $" {Items[i].Index} ";
                    MassLeft -= Masses[Items[i].Index];
                }                
            }
            return Order;
        }


        public static List<Item> FindRatios(byte[] masses, byte[] prices)
        {
            List<Item> Output = new List<Item>(masses.Length);
            for (int i = 0; i < masses.Length; i++)
            {               
                Item CurrentItem = new Item((float)prices[i] / (float)masses[i], i);                

                if (i == 0)
                {
                    Output.Add(CurrentItem);
                }
                else
                {
                    for (int j = 0; j < Output.Count; j++)
                    {
                        if (Output[j].Ratio > CurrentItem.Ratio)
                        {
                            // (j-1)th most optimal item, because the jth item had a high price-mass ratio.
                            Output.Insert(j, CurrentItem);
                            break;
                        }
                        if (j + 1 == Output.Count)
                        {
                            // Current most optimal item
                            Output.Add(CurrentItem);
                            break;
                        }
                    }
                }                
            }
            return Output;
        }
    }
}
