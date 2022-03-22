using System;

namespace CapsuleHotel
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface();
        }

        private static void UserInterface()
        {
            string[] capsuleHotel = new string[StartUp()];

            bool running = true;
            while (running)
            {
                switch (ViewMenuSelectOption())
                {
                    case "1":
                        capsuleHotel = CheckIn(capsuleHotel);
                        break;
                    case "2":
                        capsuleHotel = CheckOut(capsuleHotel);
                        break;
                    case "3":
                        ViewGuests(capsuleHotel);
                        break;
                    case "4":
                        running = Exit();
                        break;
                    default:
                        Display("InvalidInput");
                        Display("PressAnyKey");
                        break;
                }
            }
            Display("PressAnyKey");
            return;
        }

        private static int StartUp()
        {
            Display("StartUp");
            int capacity = ValidateIntegerInput(Console.ReadLine());
            Console.WriteLine($"\nThere are {capacity} unoccupied capsules ready to be booked.\n");
            Display("PressAnyKey");
            return capacity;
        }

        private static string ViewMenuSelectOption()
        {
            Display("ViewMenu");
            return Console.ReadLine();
        }

        private static string[] CheckIn(string[] capsuleHotel)
        {
            Display("CheckIn");
            string guestName = Console.ReadLine();
            
            Console.Write($"Enter Capsule #[1 - {capsuleHotel.Length}]: ");
            int capsuleNumber = ValidateIntegerInput(Console.ReadLine());
            capsuleNumber = capsuleHotel.Length > capsuleNumber ? capsuleNumber : ValidateCapsuleExists(capsuleHotel, capsuleNumber);

            capsuleNumber = ValidateRoom("", capsuleHotel, capsuleNumber);
            capsuleHotel[capsuleNumber - 1] = guestName;
            Display("CheckInSuccess");

            Display("PressAnyKey");
            return capsuleHotel;
        }

        private static string[] CheckOut(string[] capsuleHotel)
        {
            Display("CheckOut");
            
            Console.Write($"Enter Capsule #[1 - {capsuleHotel.Length}]: ");
            int capsuleNumber = ValidateIntegerInput(Console.ReadLine());
            capsuleNumber = capsuleHotel.Length > capsuleNumber ? capsuleNumber : ValidateCapsuleExists(capsuleHotel, capsuleNumber);

            capsuleNumber = ValidateRoom("occupied", capsuleHotel, capsuleNumber);
            capsuleHotel[capsuleNumber - 1] = "";
            Display("CheckOutSuccess");

            Display("PressAnyKey");
            return capsuleHotel;
        }

        private static void ViewGuests(string[] capsuleHotel)
        {
            Display("ViewGuests");
            Console.Write($"Enter Capsule #[1 - {capsuleHotel.Length}]: ");
            int capsuleNumber = ValidateIntegerInput(Console.ReadLine());

            capsuleNumber = ValidateCapsuleExists(capsuleHotel, capsuleNumber);
            DisplayGuestsDetails(capsuleHotel, capsuleNumber);

            Display("PressAnyKey");
            return;
        }

        private static bool Exit()
        {
            Display("Exit");
            return Console.ReadLine().ToLower() != "y" ? true : false;
        }

        private static void Display(string displayType)
        {
            switch (displayType)
            {
                case "StartUp":
                    Console.WriteLine("Welcome to the Samurai Capsule Hotel!");
                    Console.WriteLine("-------------------------------------");
                    Console.Write("Enter the number of capsules available: ");
                    break;
                case "ViewMenu":
                    Console.Clear();
                    Console.WriteLine("Samurai Capsule Hotel Menu");
                    Console.WriteLine("--------------------------\n");
                    Console.WriteLine("1. Check In");
                    Console.WriteLine("2. Check Out");
                    Console.WriteLine("3. View Guests");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine("---------------\n");
                    Console.WriteLine("Enter The Number of your Choice: ");
                    break;
                case "CheckIn":
                    Console.Clear();
                    Console.WriteLine("Guest Check In");
                    Console.WriteLine("--------------");
                    Console.Write("\nEnter Guest Name: ");
                    break;
                case "CheckInSuccess":
                    Console.WriteLine("Success! Welcome to Samurai Capsule Hotel!!");
                    break;
                case "CheckInFail":
                    Console.WriteLine("Sorry!! Someone is occupying that tiny ass space.");
                    Console.WriteLine("Please choose another tiny ass space to occupy.");
                    break;
                case "CheckOut":
                    Console.Clear();
                    Console.WriteLine("Guest Check Out");
                    Console.WriteLine("---------------");
                    break;
                case "CheckOutSuccess":
                    Console.WriteLine("Thank You for staying at Samurai Capsule Hotel!!");
                    break;
                case "CheckOutFail":
                    Console.WriteLine("Dude(tte), that capsule is empty. You can't check out of an empty capsule. Wake up!");
                    Console.WriteLine("Please choose another capsule.");
                    break;
                case "ViewGuests":
                    Console.Clear();
                    Console.WriteLine("View Guests");
                    Console.WriteLine("-----------");
                    break;
                case "Exit":
                    Console.Clear();
                    Console.WriteLine("Exit");
                    Console.WriteLine("----");
                    Console.WriteLine("Are you sure you want to exit?");
                    Console.WriteLine("All data will be lost.");
                    Console.WriteLine("Exit[y/n]");
                    break;
                case "InvalidInput":
                    Console.WriteLine("Input not recognized. Please enter a number 1-3 or 4 to Exit");
                    break;
                case "PressAnyKey":
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
            return;
        }

        private static void DisplayGuestsDetails(string[] capsuleHotel, int capsuleNumber)
        {
            Console.WriteLine("Capsule: Guest");
            if (capsuleHotel.Length <= 10)
            {
                for (int i = 0; i < capsuleHotel.Length; i++)
                {
                    DisplayGuest(capsuleHotel, i);
                }
            }
            else if (capsuleHotel.Length > 10 && capsuleHotel.Length - capsuleNumber <= 5)
            {
                for (int i = (capsuleHotel.Length - 11); i < capsuleHotel.Length; i++)
                {
                    DisplayGuest(capsuleHotel, i);
                }
            }
            else if (capsuleNumber <= 5)
            {
                for (int i = 0; i <= 10; i++)
                {
                    DisplayGuest(capsuleHotel, i);
                }
            }
            else
            {
                for (int i = capsuleNumber - 6; i < capsuleNumber + 5; i++)
                {
                    DisplayGuest(capsuleHotel, i);
                }
            }
            return;
        }

        private static void DisplayGuest(string[] capsuleHotel, int i)
        {
            string occupant = !String.IsNullOrEmpty(capsuleHotel[i]) ? capsuleHotel[i] : "[unoccupied]";
            Console.WriteLine($"{i + 1}: {occupant}");
            return;
        }

        private static int ValidateRoom(string emptyOrOccupied, string[] capsuleHotel, int capsuleNumber)
        {
            while (emptyOrOccupied != "occupied" ? !String.IsNullOrEmpty(capsuleHotel[capsuleNumber - 1]) : String.IsNullOrEmpty(capsuleHotel[capsuleNumber - 1]))
            {
                Display(emptyOrOccupied != "occupied" ? "CheckInFail" : "CheckOutFail");
                capsuleNumber = ValidateIntegerInput(Console.ReadLine());
                capsuleNumber = ValidateCapsuleExists(capsuleHotel, capsuleNumber);
            }
            return capsuleNumber;
        }

        private static int ValidateCapsuleExists(string[] capsuleHotel, int capsuleNumber)
        {
            while (capsuleNumber > capsuleHotel.Length)
            {
                Console.WriteLine($"That room does not exist.\nEnter a Capsule Number [1 - {capsuleHotel.Length}]: ");
                capsuleNumber = ValidateIntegerInput(Console.ReadLine());
            }
            return capsuleNumber;
        }

        private static int ValidateIntegerInput(string input)
        {
            bool isValid = int.TryParse(input, out int output);
            while (!isValid || output < 1)
            {
                Console.WriteLine("That is not a valid number. Please enter a positive integer.");
                input = Console.ReadLine();
                isValid = int.TryParse(input, out output);
            }
            return output;
        }
    }
}
