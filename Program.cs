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


            #endregion
        }
    }
}
