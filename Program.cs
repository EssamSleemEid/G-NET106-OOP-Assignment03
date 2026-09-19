using static G_NET106_OOP_Assignment03.Program;

namespace G_NET106_OOP_Assignment03
{
    internal class Program
    {
        public struct DeliveryAddress
        {
            public string City;
            public string Street;
            public int BuldingNumber;
            public DeliveryAddress(string city, string street, int buldingNumber)
            {
                City = city;
                Street = street;
                BuldingNumber = buldingNumber;
            }
            public string GetFullAddress()
            {
                return $"bulding number : {BuldingNumber}, street : {Street}, city : {City}";
            }
        }

        public class Shipment
        {
            private string description;
            private double weight;
            private decimal deliveryFee;
            private string trackingCode;
            public DeliveryAddress Destination { get; set; }
            public string TrackingCode
            {
                get { return trackingCode; }
            }
            public string Description
            {
                get { return description; }

                set
                {
                    if (value != null)
                    {
                        description = value;
                    }
                }
            }
            public double Weight
            {
                get { return weight; }

                set
                {
                    if (value > 0)
                    {
                        weight = value;
                    }
                }
            }
            public decimal DeliveryFee
            {
                get { return deliveryFee; }

                set
                {

                    if (value > 0)
                    {
                        deliveryFee = value;
                    }
                }
            }
            public virtual decimal EstimatedCost
            {
                get { return DeliveryFee + (decimal)(Weight * 5); }
            }
            public Shipment(string trackingCode)
            {
                this.trackingCode = trackingCode == null ? "unknown" : trackingCode;

                description = "unknown";
                weight = 1;
                deliveryFee = 50;
                Destination = new DeliveryAddress("Cairo", "Unknown Street", 0);
            }
            public Shipment(string trackingCode, string description, double weight, decimal deliveryFee, DeliveryAddress destination)
            {
                this.trackingCode = trackingCode == null ? "Unknown" : trackingCode;
                this.description = description == null ? "Unknown" : description;
                this.weight = weight > 0 ? weight : 1;
                this.deliveryFee = deliveryFee > 0 ? deliveryFee : 50;
                Destination = destination;
            }
            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee;
                }
            }
            public void UpdateWeight(double newWeight)
            {
                if (newWeight > 0)
                {
                    Weight = newWeight;
                }
            }
            public void UpdateWeight(double newWeight, double packingWeight)
            {
                if (newWeight > 0 && packingWeight >= 0)
                {
                    Weight = newWeight + packingWeight;
                }
            }
            public virtual void PrintShipment()
            {
                Console.WriteLine("trackingCode : " + TrackingCode);
                Console.WriteLine("description : " + Description);
                Console.WriteLine("weight : " + Weight + " kg");
                Console.WriteLine("deliveryFee : " + DeliveryFee);
                Console.WriteLine("Destination : " + Destination.GetFullAddress());
                Console.WriteLine("estimatedCost : " + EstimatedCost + " egy");
            }
        }
        public class StandardShipment : Shipment
        {
            public StandardShipment(string description, double weight, decimal deliveryFee, string trackingCode, DeliveryAddress Destination) : base(trackingCode, description, weight, deliveryFee, Destination)
            {
            }
            public override void PrintShipment()
            {
                Console.WriteLine("Standard Shipment");
                base.PrintShipment();
            }
        }

        public class ExpressShipment : Shipment
        {
            private decimal ExtraFee;

            public decimal extrafee
            {
                get { return ExtraFee; }

                set
                {
                    if (value >= 0)
                    {
                        ExtraFee = value;
                    }
                }
            }

            public override decimal EstimatedCost
            {
                get { return DeliveryFee + (decimal)(Weight * 5) + ExtraFee; }
            }

            public ExpressShipment(string description, double weight, decimal deliveryFee, string trackingCode, DeliveryAddress Destination, decimal ExtraFee) : base(trackingCode, description, weight, deliveryFee, Destination)
            {
                extrafee = ExtraFee;
            }
            public override void PrintShipment()
            {
                Console.WriteLine("Express Shipment");
                base.PrintShipment();
                Console.WriteLine("Extra Fee : " + ExtraFee + " EGP");
            }
        }

        public class InternationalShipment : Shipment
        {
            private string DestinationCountry;
            private decimal CustomerFee;

            public string destinationCountry
            {
                get { return DestinationCountry; }

                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        DestinationCountry = value;
                    }
                }
            }

            public decimal customerFee
            {
                get { return CustomerFee; }

                set
                {
                    if (value >= 0)
                    {
                        CustomerFee = value;
                    }
                }
            }

            public override decimal EstimatedCost
            {
                get { return DeliveryFee + (decimal)(Weight * 5) + customerFee; }
            }

            public InternationalShipment(string description, double weight, decimal deliveryFee, string trackingCode, DeliveryAddress Destination, string DestinationCountry, decimal CustomerFee) : base(trackingCode, description, weight, deliveryFee, Destination)
            {
                destinationCountry = DestinationCountry;
                customerFee = CustomerFee;
            }

            public virtual void GenerateCustomsReport()
            {
                Console.WriteLine("Customs Report Generated.");
            }

            public override void PrintShipment()
            {
                Console.WriteLine("International Shipment");
                base.PrintShipment();
                Console.WriteLine("Destination Country : " + DestinationCountry);
                Console.WriteLine("Customs Fee : " + CustomerFee + " EGP");
            }
        }

        public class PriorityInternationalShipment : InternationalShipment
        {
            public PriorityInternationalShipment(string description,double weight,decimal deliveryFee,string trackingCode,DeliveryAddress Destination,string DestinationCountry,decimal CustomerFee): base(description,weight,deliveryFee,trackingCode,Destination,DestinationCountry,CustomerFee)
            {

            }
            public override void GenerateCustomsReport()
            {
                Console.WriteLine("Customs Report Generated.");
            }

        }

            public class DeliveryCenter
        {
            public string CenterName { get; set; }
            public Driver Driver { get; set; }
            private Shipment[] shipments;

            public DeliveryCenter(string centerName)
            {
                CenterName = centerName;
                shipments = new Shipment[20];
            }

            public Shipment this[int index]
            {
                get
                {
                    if (index >= 0 && index < shipments.Length)
                        return shipments[index];

                    return default;
                }

                set
                {
                    if (index >= 0 && index < shipments.Length)
                        shipments[index] = value;
                }
            }

            public Shipment this[string trackingCode]
            {
                get
                {
                    for (int i = 0; i < shipments.Length; i++)
                    {
                        if (shipments[i].TrackingCode == trackingCode)
                            return shipments[i];
                    }

                    return default;
                }
            }

            public bool AddShipment(Shipment shipment)
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i].TrackingCode == null)
                    {
                        shipments[i] = shipment;
                        return true;
                    }
                }

                return false;
            }

            public bool RemoveShipment(string trackingCode)
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null && shipments[i].TrackingCode == trackingCode)
                    {
                        shipments[i] = null;
                        return true;
                    }
                }
                return false;
            }

            public void PrintAllShipments()
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null)
                    {
                        shipments[i].PrintShipment();
                        Console.WriteLine();
                    }
                }
            }

        }

        public static class DeliveryHelper
        {
            public static void PrintShipmentDetails(Shipment shipment)
            {
                shipment.PrintShipment();
            }
        }

        public sealed class CompletedShipment : Shipment
        {
            public CompletedShipment(string description,double weight,decimal deliveryFee,string trackingCode,DeliveryAddress Destination): base(trackingCode,description,weight,deliveryFee,Destination)
            {

            }
        }

        public class Driver
        {
            public string Name { get; set; }

            public Driver(string name)
            {

                Name = name;
            }
        }


        static void Main(string[] args)
        {
            #region Part01

            #region Question01
            //a)What is the difference between Method Overloading and Method Overriding?

            /*
              overloading is having more than one method with the same name but diffrent parameter in the same class 
              
              overriding is child class provide new implementation for a method inherited from the parent
            */

            //b)  What is the difference between Static Binding and Dynamic Binding?

            /*
              the static is the method to be executed is determined at compile time

              the dynamic is the method to be executed is detrmined at run time 
             */
            #endregion

            #region Question02
            //a)  What is the purpose of the sealed keyword when applied to a class?

            //prevent other classes from inheriting from that class

            //b)  What is the difference between a sealed class and a sealed method?

            //sealed class can't be inherited and the sealed method can't be overrided

            //c)  Can a sealed method be overridden? Why?

            //no bc sealed prevent further ovveriding of that method 
            #endregion
            #endregion

            #region Part02

            Driver driver01 = new Driver("essam");

            DeliveryCenter center01 = new DeliveryCenter("sinai delevery center");

            center01.Driver = driver01;

            Console.WriteLine("Driver : " + center01.Driver.Name);

            DeliveryAddress address01 = new DeliveryAddress("north sinai", "el masaeed", 1);


            DeliveryAddress address02 = new DeliveryAddress("north sinai", "el dahia", 2);


            DeliveryAddress address03 = new DeliveryAddress("north sinai", "el reisa", 3);

            StandardShipment standardShipment01 = new StandardShipment("samsung A34 phone ",1.2,55,"A034",address01);

            center01.AddShipment(standardShipment01);

            ExpressShipment expressShipment01 = new ExpressShipment("laptop dell presession5570 ",5,120,"L507", address02,20);

            center01.AddShipment(expressShipment01);

            InternationalShipment internationalShipment01 = new InternationalShipment("huawei smart watch Gt2",0.4,200,"HG02",address03,"Egypt",50);

            center01.AddShipment(internationalShipment01);

            center01.PrintAllShipments();

            DeliveryHelper.PrintShipmentDetails(standardShipment01);
            DeliveryHelper.PrintShipmentDetails(expressShipment01);
            DeliveryHelper.PrintShipmentDetails(internationalShipment01);

            Console.WriteLine("original weight : " + standardShipment01.Weight + " Kg");

            standardShipment01.UpdateWeight(0.2);

            Console.WriteLine("updated weight : " + standardShipment01.Weight + " Kg");


            Console.WriteLine("original weight without packing : " + expressShipment01.Weight + " kg");

            expressShipment01.UpdateWeight(2, 0.8);

            Console.WriteLine("updated weight with packing weight : " + expressShipment01.Weight + " kg");


            Shipment[] shipments ={standardShipment01,expressShipment01,internationalShipment01};

            foreach (Shipment shipment in shipments)
            {
                shipment.PrintShipment();
            }

            CompletedShipment completedShipment01 = new CompletedShipment("Headset Esport g607",0.5,80,"G601",address01);

            completedShipment01.PrintShipment();

            PriorityInternationalShipment priorityShipment01 = new PriorityInternationalShipment( "mouse mo305",0.8,200,"M305",address03,"Egypt",70);

            priorityShipment01.PrintShipment();

            #endregion
        }
    }
}
