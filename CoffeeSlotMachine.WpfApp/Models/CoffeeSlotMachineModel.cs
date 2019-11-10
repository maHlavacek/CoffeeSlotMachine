using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoffeeSlotMachine.WpfApp.Models
{
    public class CoffeeSlotMachineModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Logic.CoffeeSlotMachine CoffeeSlotMachine { get; set; }

        public CoffeeSlotMachineModel()
        {
            CoffeeSlotMachine = new Logic.CoffeeSlotMachine();

            Update();
        }

        private void Update()
        {
            SetDepotCounters();
            SetInsertCounters();
            SetEjectionCounters();
            OnPropertyChanged(nameof(ProductInfos));
        }
        private void SetDepotCounters()
        {
            CoffeeSlotMachine.GetCounterForDepot(5, out int counter);
            FiveCentInDepot = counter;

            CoffeeSlotMachine.GetCounterForDepot(10, out counter);
            TenCentInDepot = counter;

            CoffeeSlotMachine.GetCounterForDepot(20, out counter);
            TwentyCentInDepot = counter;

            CoffeeSlotMachine.GetCounterForDepot(50, out counter);
            FiftyCentInDepot = counter;

            CoffeeSlotMachine.GetCounterForDepot(100, out counter);
            HundredCentInDepot = counter;

            CoffeeSlotMachine.GetCounterForDepot(200, out counter);
            TwoHundredCentInDepot = counter;
        }
        private void SetInsertCounters()
        {
            CoffeeSlotMachine.GetCounterForInsert(5, out int counter);
            FiveCentInInsert = counter;

            CoffeeSlotMachine.GetCounterForInsert(10, out counter);
            TenCentInInsert = counter;

            CoffeeSlotMachine.GetCounterForInsert(20, out counter);
            TwentyCentInInsert = counter;

            CoffeeSlotMachine.GetCounterForInsert(50, out counter);
            FiftyCentInInsert = counter;

            CoffeeSlotMachine.GetCounterForInsert(100, out counter);
            HundredCentInInsert = counter;

            CoffeeSlotMachine.GetCounterForInsert(200, out counter);
            TwoHundredCentInInsert = counter;
        }
        private void SetEjectionCounters()
        {
            CoffeeSlotMachine.GetCounterForEjection(5, out int counter);
            FiveCentInEjection = counter;

            CoffeeSlotMachine.GetCounterForEjection(10, out counter);
            TenCentInEjection = counter;

            CoffeeSlotMachine.GetCounterForEjection(20, out counter);
            TwentyCentInEjection = counter;

            CoffeeSlotMachine.GetCounterForEjection(50, out counter);
            FiftyCentInEjection = counter;

            CoffeeSlotMachine.GetCounterForEjection(100, out counter);
            HundredCentInEjection = counter;

            CoffeeSlotMachine.GetCounterForEjection(200, out counter);
            TwoHundredCentInEjection = counter;
        }

        private int fiveCentInDepot;
        public int FiveCentInDepot
        {
            get { return fiveCentInDepot; }
            set
            {
                fiveCentInDepot = value;
                OnPropertyChanged(nameof(FiveCentInDepot));
            }
        }
        private int tenCentInDepot;
        public int TenCentInDepot
        {
            get { return tenCentInDepot; }
            set
            {
                tenCentInDepot = value;
                OnPropertyChanged(nameof(TenCentInDepot));
            }
        }
        private int twentyCentInDepot;
        public int TwentyCentInDepot
        {
            get { return twentyCentInDepot; }
            set
            {
                twentyCentInDepot = value;
                OnPropertyChanged(nameof(TwentyCentInDepot));
            }
        }
        private int fiftyCentInDepot;
        public int FiftyCentInDepot
        {
            get { return fiftyCentInDepot; }
            set
            {
                fiftyCentInDepot = value;
                OnPropertyChanged(nameof(FiftyCentInDepot));
            }
        }
        private int hundredCentInDepot;
        public int HundredCentInDepot
        {
            get { return hundredCentInDepot; }
            set
            {
                hundredCentInDepot = value;
                OnPropertyChanged(nameof(HundredCentInDepot));
            }
        }
        private int twoHundredCentInDepot;
        public int TwoHundredCentInDepot
        {
            get { return twoHundredCentInDepot; }
            set
            {
                twoHundredCentInDepot = value;
                OnPropertyChanged(nameof(TwoHundredCentInDepot));
            }
        }

        private int fiveCentInInsert;
        public int FiveCentInInsert
        {
            get { return fiveCentInInsert; }
            set
            {
                fiveCentInInsert = value;
                OnPropertyChanged(nameof(FiveCentInInsert));
            }
        }
        private int tenCentInInsert;
        public int TenCentInInsert
        {
            get { return tenCentInInsert; }
            set
            {
                tenCentInInsert = value;
                OnPropertyChanged(nameof(TenCentInInsert));
            }
        }
        private int twentyCentInInsert;
        public int TwentyCentInInsert
        {
            get { return twentyCentInInsert; }
            set
            {
                twentyCentInInsert = value;
                OnPropertyChanged(nameof(TwentyCentInInsert));
            }
        }
        private int fiftyCentInInsert;
        public int FiftyCentInInsert
        {
            get { return fiftyCentInInsert; }
            set
            {
                fiftyCentInInsert = value;
                OnPropertyChanged(nameof(FiftyCentInInsert));
            }
        }
        private int hundredCentInInsert;
        public int HundredCentInInsert
        {
            get { return hundredCentInInsert; }
            set
            {
                hundredCentInInsert = value;
                OnPropertyChanged(nameof(HundredCentInInsert));
            }
        }
        private int twoHundredCentInInsert;
        public int TwoHundredCentInInsert
        {
            get { return twoHundredCentInInsert; }
            set
            {
                twoHundredCentInInsert = value;
                OnPropertyChanged(nameof(TwoHundredCentInInsert));
            }
        }

        private int fiveCentInEjection;
        public int FiveCentInEjection
        {
            get { return fiveCentInEjection; }
            set
            {
                fiveCentInEjection = value;
                OnPropertyChanged(nameof(FiveCentInEjection));
            }
        }
        private int tenCentInEjection;
        public int TenCentInEjection
        {
            get { return tenCentInEjection; }
            set
            {
                tenCentInEjection = value;
                OnPropertyChanged(nameof(TenCentInEjection));
            }
        }
        private int twentyCentInEjection;
        public int TwentyCentInEjection
        {
            get { return twentyCentInEjection; }
            set
            {
                twentyCentInEjection = value;
                OnPropertyChanged(nameof(TwentyCentInEjection));
            }
        }
        private int fiftyCentInEjection;
        public int FiftyCentInEjection
        {
            get { return fiftyCentInEjection; }
            set
            {
                fiftyCentInEjection = value;
                OnPropertyChanged(nameof(FiftyCentInEjection));
            }
        }
        private int hundredCentInEjection;
        public int HundredCentInEjection
        {
            get { return hundredCentInEjection; }
            set
            {
                hundredCentInEjection = value;
                OnPropertyChanged(nameof(HundredCentInEjection));
            }
        }
        private int twoHundredCentInEjection;
        public int TwoHundredCentInEjection
        {
            get { return twoHundredCentInEjection; }
            set
            {
                twoHundredCentInEjection = value;
                OnPropertyChanged(nameof(TwoHundredCentInEjection));
            }
        }

        public string[] Coins
        {
            get
            {
                return new string[] { "5", "10", "20", "50", "100", "200" };
            }
        }
        public string[] ProductNames => CoffeeSlotMachine.ProductNames;
        public string[] ProductInfos
        {
            get
            {
                string[] result = CoffeeSlotMachine.ProductNames;

                for (int i = 0; i < result.Length; i++)
                {
                    CoffeeSlotMachine.GetCounterForProduct(result[i], out int counter);

                    while (result[i].Length < 30)
                        result[i] += " ";

                    result[i] = $"{result[i]}{counter}";
                }
                return result;
            }
        }

        public bool InsertCoin(int coin)
        {
            bool result = CoffeeSlotMachine.InsertCoin(coin);
            Update();
            return result;
        }
        public bool SelectProduct(string product)
        {
            bool result = CoffeeSlotMachine.SelectProduct(product, out _);
            Update();
            return result;
        }
        public void CancelOrder()
        {
            CoffeeSlotMachine.CancelOrder();
            Update();
        }

        public void EmptyEjection()
        {
            CoffeeSlotMachine.EmptyEjection(out _);
            Update();
        }
    }
}
