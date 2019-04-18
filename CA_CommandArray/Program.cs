using FinchAPI;
using System;
using System.Collections.Generic;

namespace CommandArray
{
    // *************************************************************
    // App: Command Array
    // Author: Witte, Hannah
    // Date: 3.2.19
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;

            int delayDuration = 0;
            int numberOfCommands = 0;
            int motorSpeed = 0;
            int LEDBrightness = 0;
            //FinchCommand[] commands = null;
            List<FinchCommand> commands = new List<FinchCommand>();

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) Get Finch Robot Commands"); 
                Console.WriteLine("\t3) Display Finch Robot Command");
                Console.WriteLine("\t4) Execute FInch Robot Commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        DisplayGetFinchCommands(commands, numberOfCommands);
                        break;
                    case "3":
                        DisplayFinchCommands(commands);
                        break;
                    case "4":
                        DisplayExecuteFinchCommands(myFinch, commands, motorSpeed, LEDBrightness, delayDuration);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayExecuteFinchCommands(Finch myFinch, List<FinchCommand> commands, int motorSpeed, int lEDBrightness, int delayDuration)
        {
            DisplayHeader("Execute Finch Commands");

            Console.WriteLine("Click any key when ready to execute commands.");
            DisplayContinuePrompt();

            for (int index = 0; index < commands.Count; index++)
            {
                Console.WriteLine($"Command: {commands[index]}");

                switch (commands[index])
                {
                    case FinchCommand.DONE:
                        break;
                    case FinchCommand.MOVEFORWARD:
                        myFinch.setMotors(motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.MOVEBACKWARD:
                        myFinch.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.STOPMOTORS:
                        myFinch.setMotors(0, 0);
                        break;
                    case FinchCommand.DELAY:
                        myFinch.wait(delayDuration);
                        break;
                    case FinchCommand.TURNRIGHT:
                        myFinch.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case FinchCommand.TURNLEFT:
                        myFinch.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case FinchCommand.LEDON:
                        myFinch.setLED(lEDBrightness, lEDBrightness, lEDBrightness);
                        break;
                    case FinchCommand.LEDOFF:
                        myFinch.setLED(0, 0, 0);
                        break;
                    default:
                        break;
                }
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<FinchCommand> commands, int numberOfCommands)
        {
            FinchCommand command;
           
            DisplayHeader("Get Finch Commands");

            for (int index = 0; index < numberOfCommands; index++)
            {
                Console.Write($"Command #{index + 1}:");
                Enum.TryParse(Console.ReadLine().ToUpper(), out commands);
                commands.Add(command);
            }

            Console.WriteLine();
            Console.WriteLine("The Commands:");
            foreach (FinchCommand finchCommand in commands)
            {
                Console.WriteLine("\t" + finchCommand);
            }


            DisplayContinuePrompt();

        }

        static void DisplayFinchCommands(List<FinchCommand> commands)
        {
            DisplayHeader("Finch Commands");

            if (commands != null)
            {
                Console.WriteLine("The Commands:");
                foreach (FinchCommand command in commands)
                {
                    Console.WriteLine(command);
                }
            }

            else 
            {
                Console.WriteLine("Please enter Finch Robot Commands first");
            }


            DisplayContinuePrompt();

        }

        static int DisplayGetDelayDuration()
        {
            int delayDuration;
            string userResponse;

            DisplayHeader("Length of Delay");

            Console.Write("Enter length of delay (milliseconds):");

            userResponse = Console.ReadLine();
            delayDuration = int.Parse(userResponse);


            DisplayContinuePrompt();

            return delayDuration;

        }

        /// <summary>
        /// get the number of commands from the user
        /// </summary>
        /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
        {
            int numberOfCommands;
            string userResponse;

            DisplayHeader("Number of Commands");

            Console.Write("Enter the number of commands:");
            userResponse = Console.ReadLine();

            numberOfCommands = int.Parse(userResponse);

            return numberOfCommands;
        }

        /// <summary>
        /// get motor speed from the user
        /// </summary>
        /// <returns>motor speed</returns>
        static int DisplayMotorSpeed()
        {
            int motorSpeed;
            string userResponse;

            DisplayHeader("Motor Speed");

            Console.Write("Enter the Motor Speed [1 - 225]:");
            userResponse = Console.ReadLine();

            motorSpeed = int.Parse(userResponse);

            return motorSpeed;
        }

        /// <summary>
        /// get LED brightness from the user
        /// </summary>
        /// <returns>LED brightness</returns>
        static int LEDBrightness()
        {
            int LEDBrightness;
            string userResponse;

            DisplayHeader("LED Brightness");

            Console.Write("Enter LED Brightness [1 - 225]:");
            userResponse = Console.ReadLine();

            LEDBrightness = int.Parse(userResponse);

            return LEDBrightness;
        }

        /// <summary>
        /// initialize and confirm the finch connects
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
        {
            DisplayHeader("Initialize the Finch");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            while (!myFinch.connect())
            {
                Console.WriteLine("Please confirm the Finch Robot is connect");
                DisplayContinuePrompt();
            }

            FinchConnectedAlert(myFinch);
            Console.WriteLine("Your Finch Robot is now connected");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// audio notification that the finch is connected
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
        {
            myFinch.setLED(0, 255, 0);

            for (int frequency = 17000; frequency > 100; frequency -= 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(10);
            }

            myFinch.noteOff();
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\tProgram Your Finch");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen and disconnect finch robot
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tThank You!");
            Console.WriteLine();

            myFinch.disConnect();

            DisplayContinuePrompt();
        }

        #region HELPER  METHODS

        /// <summary>
        /// display header
        /// </summary>
        /// <param name="header"></param>
        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        /// <summary>
        /// display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        #endregion
    }
}
