using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeSlotMachine.Logic;

namespace CoffeeSlotMachine.LogicUnitTest
{
    [TestClass]
    public class CoffeeSlotMachineUnitTest
    {
        /// <summary>
        /// Vergleich zweier Arrays gleichen Typs mit vergleichbaren Elementen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool CompareArrays<T>(T[] first, T[] second) where T : IComparable<T>
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i].CompareTo(second[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod()]
        public void DefaultConstructor_01()
        {
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine();
            Assert.AreEqual(18, coffeeSlotMachine.CoinsInDepot, "Je Wert sollen 3 Münzen im Depot sein");
            Assert.AreEqual(3, coffeeSlotMachine.ProductsAvailable, "Defaultkonstruktor soll drei Produkte anlegen");
        }

        [TestMethod()]
        public void OwnConstructor_01()
        {
            int[] coinsInDepot = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "1-6 Münzen sollen im Depot sein");
            Assert.AreEqual(4, coffeeSlotMachine.ProductsAvailable, "Vier Produkte anlegen");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OwnConstructor_02()
        {
            int[] coinsInDepot = null;
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OwnConstructor_03()
        {
            int[] coinsInDepot = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = null;
            new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OwnConstructor_04()
        {
            int[] coinsInDepot = { 1, 2, 3, 4, 5 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
        }

        [TestMethod()]
        public void OwnConstructor_05()
        {
            int[] coinsInDepot = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
            coinsInDepot[0] = 10;
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "1-6 Münzen sollen im Depot sein");
            Assert.AreEqual(4, coffeeSlotMachine.ProductsAvailable, "Vier Produkte anlegen");
        }

        [TestMethod()]
        public void OwnConstructorAllEmpty_01()
        {
            int[] coinsInDepot = { 0, 0, 0, 0, 0, 0 }; ;
            string[] productNames = new string[0];
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
            Assert.AreEqual(0, coffeeSlotMachine.CoinsInDepot, "Leeres Depot");
            Assert.AreEqual(0, coffeeSlotMachine.ProductsAvailable, "Keine Produkte");
        }

        [TestMethod()]
        public void InsertCoin_01()
        {
            int[] coins = { 5, 10, 20, 50, 100, 200 };
            int[] coinsInDepot = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
            for (int i = 0; i < coins.Length; i++)
            {
                Assert.AreEqual(true, coffeeSlotMachine.InsertCoin(coins[i]), $"{coins[i]} is valid");
            }
        }

        [TestMethod()]
        public void InsertCoin_02()
        {
            int[] coins = { 5, 10, 20, 50, 100, 200, 0, -1, 1, 199, 201 };
            int[] coinsInDepot = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coinsInDepot, productNames);
            for (int i = 0; i < coins.Length; i++)
            {
                if (i < 6)
                {
                    Assert.AreEqual(true, coffeeSlotMachine.InsertCoin(coins[i]), $"{coins[i]} is valid");
                }
                else
                {
                    Assert.AreEqual(false, coffeeSlotMachine.InsertCoin(coins[i]), $"{coins[i]} is invalid");
                }
            }
        }

        [TestMethod()]
        public void NormalOrder_01()
        {
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            int[] returnCoinsExpected = { 0, 0, 0, 1, 0, 0 };  // 1* 50 cent
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(100);
            Assert.IsTrue(ok, "InsertCoin sollte funktionieren");
            ok = coffeeSlotMachine.SelectProduct("Kaffee", out int donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), returnCoinsExpected), "Arrays mit Retourgeld sind nicht gleich");
            Assert.AreEqual(0, donation, "Es konnte das gesamte Restgeld zurückgegeben werden");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "100 cent hinein und 50 cent hinaus");
        }

        [TestMethod()]
        public void ThreeOrders200Cent_01()
        {
            int[] coins = { 0, 0, 0, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);

            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out int donation);
            Assert.AreEqual(0, donation, "Es konnte das gesamte Restgeld zurückgegeben werden");
            Assert.AreEqual(6, coffeeSlotMachine.CoinsInDepot, "6 * 200 cent");
        }

        [TestMethod()]
        public void FourOrders200CentWithDonation_01()
        {
            int[] coins = { 0, 0, 0, 3, 3, 3 };
            int[] returnCoinsExpected = { 0, 0, 0, 0, 0, 0 };  // 1 * 100 und 1* 50 cent
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out int donation);
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), new int[] { 0, 0, 0, 1, 1, 0 }), "Es bleiben keine passenden Münzen mehr übrig");
            Assert.AreEqual(0, donation, "Kein Spende");

            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out donation);
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), new int[] { 0, 0, 0, 1, 1, 0 }), "Es bleiben keine passenden Münzen mehr übrig");
            Assert.AreEqual(0, donation, "Kein Spende");

            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out donation);
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), new int[] { 0, 0, 0, 1, 1, 0 }), "Es bleiben keine passenden Münzen mehr übrig");
            Assert.AreEqual(0, donation, "Kein Spende");

            coffeeSlotMachine.InsertCoin(200);
            bool ok = coffeeSlotMachine.SelectProduct("Kaffee", out donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), new int[] { 0, 0, 0, 0, 0, 0 }), "Es bleiben keine passenden Münzen mehr übrig");
            Assert.AreEqual(150, donation, "Kein Retourgeld mehr");
            Assert.AreEqual(7, coffeeSlotMachine.CoinsInDepot, "7 * 200 cent");
        }

        [TestMethod()]
        public void FourOrders200CentWithLessDonation_01()
        {
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            int[] returnCoinsExpected = { 2, 3, 4, 0, 0, 0 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            bool ok = coffeeSlotMachine.SelectProduct("Kaffee", out int donation);
            Assert.IsTrue(ok, "SelectProduct sollte funktionieren");
            Assert.IsTrue(CompareArrays(coffeeSlotMachine.EmptyEjection(out _), returnCoinsExpected), "Ganzes Kleingeld wurde zurückgegeben");
            Assert.AreEqual(30, donation, "30 cent fehlen beim retourgeld");
            Assert.AreEqual(7, coffeeSlotMachine.CoinsInDepot, "7 * 200 cent");
        }

        [TestMethod()]
        public void FourOrders200CentEmptyMachine_01()
        {
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);

            coffeeSlotMachine.EmptyDepot(out int cents);
            Assert.AreEqual(1400, cents, "7 * 200 Cent");
            Assert.AreEqual(0, coffeeSlotMachine.CoinsInDepot, "Depot wurde geleert");
            coffeeSlotMachine.EmptyDepot(out cents);
            Assert.AreEqual(0, cents, "Depot wurde gerade geleert");
        }

        [TestMethod()]
        public void IllegalCoins_01()
        {
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(7);
            Assert.IsFalse(ok, "7 ist keine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(2);
            Assert.IsFalse(ok, "2 ist keine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsFalse(ok, "Es wurden bereits 50 Cent eingeworfen");
        }


        [TestMethod()]
        public void MoreCoins_01()
        {
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Münzen wurden noch nicht übernommen");
            ok = coffeeSlotMachine.SelectProduct("Tee", out _);
            Assert.IsTrue(ok, "Auswahl Tee sollte funktionieren");
            Assert.AreEqual(24, coffeeSlotMachine.CoinsInDepot, "3 Münzen dazu eingenommen");
        }

        [TestMethod()]
        public void MoreCoinsAndCancel_01()
        {
            int[] returnCoins;
            int[] returnCoinsExpected = { 0, 1, 2, 0, 0, 0 };
            int[] coins = { 1, 2, 3, 4, 5, 6 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);
            bool ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(20);
            Assert.IsTrue(ok, "20 ist eine gültige Münze");
            ok = coffeeSlotMachine.InsertCoin(10);
            Assert.IsTrue(ok, "10 ist eine gültige Münze");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Münzen wurden noch nicht übernommen");
            coffeeSlotMachine.CancelOrder();
            returnCoins = coffeeSlotMachine.EmptyEjection(out _);
            Assert.IsTrue(CompareArrays(returnCoins, returnCoinsExpected), "Stornierte Münzen stimmen nicht");
            Assert.AreEqual(21, coffeeSlotMachine.CoinsInDepot, "Alter Zustand bleibt erhalten");

        }

        [TestMethod()]
        public void ProductsCounter_01()
        {
            int[] coins = { 2, 3, 4, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);

            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(100);

            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(100);
            bool ok = coffeeSlotMachine.SelectProduct("tee", out _);
            Assert.IsFalse(ok, "tee gibt es nicht, nur Tee");
            coffeeSlotMachine.SelectProduct("Tee", out _);
            coffeeSlotMachine.InsertCoin(50);
            coffeeSlotMachine.SelectProduct("Suppe", out _);

            coffeeSlotMachine.GetCounterForProduct("Kaffee", out int counter);
            Assert.AreEqual(2, counter, "Es wurde 2 * Kaffee bestellt");
            coffeeSlotMachine.GetCounterForProduct("irgendwas", out counter);
            Assert.IsFalse(ok, "Produkt irgendwas ist nicht vorhanden");
            Assert.AreEqual(0, counter, "Im Fehlerfall 0 zurückgeben");
            coffeeSlotMachine.GetCounterForProduct("Suppe", out counter);
            Assert.AreEqual(1, counter, "Suppe gab es 1*");
            coffeeSlotMachine.GetCounterForProduct("Milch", out counter);
            Assert.AreEqual(0, counter, "Milch wurde nie bestellt");
        }

        [TestMethod()]
        public void CoinsCounter_01()
        {
            int[] coins = { 3, 3, 3, 3, 3, 3 };
            string[] productNames = { "Kaffee", "Tee", "Suppe", "Milch" };
            Logic.CoffeeSlotMachine coffeeSlotMachine = new Logic.CoffeeSlotMachine(coins, productNames);

            coffeeSlotMachine.InsertCoin(200);
            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(5);
            coffeeSlotMachine.InsertCoin(5);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(50);

            coffeeSlotMachine.SelectProduct("Kaffee", out _);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.InsertCoin(20);
            coffeeSlotMachine.SelectProduct("Tee", out _);
            coffeeSlotMachine.InsertCoin(50);
            coffeeSlotMachine.SelectProduct("Suppe", out _);
            
            coffeeSlotMachine.GetCounterForDepot(5, out int counter);
            Assert.AreEqual(5, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForDepot(10, out counter);
            Assert.AreEqual(1, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForDepot(20, out counter);
            Assert.AreEqual(6, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForDepot(50, out counter);
            Assert.AreEqual(4, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForDepot(100, out counter);
            Assert.AreEqual(2, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
            coffeeSlotMachine.GetCounterForDepot(200, out counter);
            Assert.AreEqual(4, counter, "Am Papier durchspielen, möglichst große Münzen zurückgeben");
        }
    }
}
