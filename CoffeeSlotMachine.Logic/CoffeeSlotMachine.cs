using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeSlotMachine.Logic
{
    public class CoffeeSlotMachine
    {
		public const int Price = 50;
		private int[] Coins { get; set; }
		private int[] DepotCoins { get; set; }
		private int[] InsertCoins { get; set; }
		private int[] EjectionCoins { get; set; }

		private string[] Products { get; set; }
		private int[] ProductCounters { get; set; }

		public int CoinsInDepot
		{
			get
			{
				return SumArrayValues(DepotCoins);
			}
		}
		public int ProductsAvailable
		{
			get
			{
				return Products.Length;
			}
		}
		public string[] ProductNames
		{
			get
			{
				string[] result = new string[Products.Length];

				for (int i = 0; i < Products.Length; i++)
				{
					result[i] = Products[i];
				}
				return result;
			}
		}
		public CoffeeSlotMachine()
		{
			Coins = new int[] { 5, 10, 20, 50, 100, 200 };
			InsertCoins = new int[] { 0, 0, 0, 0, 0, 0 };
			EjectionCoins = new int[] { 0, 0, 0, 0, 0, 0 };
			DepotCoins = new int[] { 3, 3, 3, 3, 3, 3 };

			Products = new string[] { "Cappuccino", "Mocca", "Kakao" };
			ProductCounters = new int[] { 0, 0, 0 };
		}

		public CoffeeSlotMachine(int[] coinsInDepot, string[] productNames)
			: this()
		{
			if (coinsInDepot == null)
				throw new ArgumentNullException(nameof(coinsInDepot));

			if (coinsInDepot.Length != Coins.Length)
				throw new ArgumentException("Invalid count of coins");

			if (productNames == null)
				throw new ArgumentNullException(nameof(productNames));

			for (int i = 0; i < coinsInDepot.Length; i++)
			{
				if (coinsInDepot[i] >= 0)
				{
					DepotCoins[i] = coinsInDepot[i];
				}
			}
			Products = new string[productNames.Length];
			ProductCounters = new int[productNames.Length];

			for (int i = 0; i < productNames.Length; i++)
			{
				Products[i] = productNames[i];
				ProductCounters[i] = 0;
			}
		}

		public bool InsertCoin(int coin)
		{
			bool result = false;
			int coinIdx = GetIndexOf(Coins, coin);

			if (coinIdx > -1)
			{
				result = true;
				InsertCoins[coinIdx]++;
			}
			return result;
		}
		public bool SelectProduct(string product, out int donation)
		{
			bool result = false;
			int restValue = SumCoinValues(Coins, InsertCoins) - Price;
			int productIdx = GetIndexOf(Products, product);

			donation = 0;
			if (productIdx >= 0 && restValue >= 0)
			{
				result = true;
				donation = restValue;
				ProductCounters[productIdx]++;
				DepotCoins = AddArrays(DepotCoins, InsertCoins);
				EmptyArray(InsertCoins);

				int coinIdx = Coins.Length - 1;

				while (coinIdx >= 0 && donation > 0)
				{
					if (DepotCoins[coinIdx] > 0 && Coins[coinIdx] <= donation)
					{
						EjectionCoins[coinIdx]++;
						DepotCoins[coinIdx]--;
						donation -= Coins[coinIdx];
					}
					else
					{
						coinIdx--;
					}
				}
			}
			return result;
		}

		public void CancelOrder()
		{
			EjectionCoins = AddArrays(EjectionCoins, InsertCoins);
			EmptyArray(InsertCoins);
		}

		public int[] EmptyDepot(out int sum)
		{
			int[] result = CopyArray(DepotCoins);
			sum = SumCoinValues(Coins, DepotCoins);

			EmptyArray(DepotCoins);
			return result;
		}
		public int[] EmptyEjection(out int sum)
		{
			int[] result = CopyArray(EjectionCoins);
			sum = SumCoinValues(Coins, EjectionCoins);

			EmptyArray(EjectionCoins);
			return result;
		}

		public bool GetCounterForProduct(string product, out int counter)
		{
			bool result = false;
			int productIdx = GetIndexOf(Products, product);

			counter = 0;
			if (productIdx >= 0)
			{
				result = true;
				counter = ProductCounters[productIdx];
			}
			return result;
		}
		public bool GetCounterForDepot(int coin, out int counter)
		{
			bool result = false;
			int coinIdx = GetIndexOf(Coins, coin);

			counter = 0;
			if (coinIdx >= 0)
			{
				result = true;
				counter = DepotCoins[coinIdx];
			}
			return result;
		}
		public bool GetCounterForInsert(int coin, out int counter)
		{
			bool result = false;
			int coinIdx = GetIndexOf(Coins, coin);

			counter = 0;
			if (coinIdx >= 0)
			{
				result = true;
				counter = InsertCoins[coinIdx];
			}
			return result;
		}
		public bool GetCounterForEjection(int coin, out int counter)
		{
			bool result = false;
			int coinIdx = GetIndexOf(Coins, coin);

			counter = 0;
			if (coinIdx >= 0)
			{
				result = true;
				counter = EjectionCoins[coinIdx];
			}
			return result;
		}

		#region Helpers
		private static void EmptyArray(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
		}
		private static int[] CopyArray(int[] array)
		{
			int[] result = new int[array.Length];

			for (int i = 0; i < array.Length; i++)
			{
				result[i] = array[i];
			}
			return result;
		}
		private static int[] AddArrays(int[] left, int[] right)
		{
			int[] result = new int[left.Length];

			for (int i = 0; i < left.Length && i < right.Length; i++)
			{
				result[i] = left[i] + right[i];
			}
			return result;
		}
		private static int SumArrayValues(int[] array)
		{
			int result = 0;

			for (int i = 0; i < array.Length; i++)
			{
				result += array[i];
			}
			return result;
		}
		private static int SumCoinValues(int[] coins, int[] array)
		{
			int result = 0;

			for (int i = 0; i < coins.Length && i < array.Length; i++)
			{
				result += coins[i] * array[i];
			}
			return result;
		}
		private static int GetIndexOf(int[] array, int elem)
		{
			int result = -1;

			for (int i = 0; i < array.Length && result == -1; i++)
			{
				if (array[i] == elem)
				{
					result = i;
				}
			}
			return result;
		}
		private static int GetIndexOf(string[] array, string elem)
		{
			int result = -1;

			for (int i = 0; i < array.Length && result == -1; i++)
			{
				if (array[i].Equals(elem))
				{
					result = i;
				}
			}
			return result;
		}
		#endregion Helpers
	}
}
