using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphsFromLinks
{
    class Program
    {
        static Graph CreatedGraph;
        static void Main(string[] args)
        {
            StartGenesis();
            GoIntoOptionsLoop();

            Console.ReadLine();
        }

        private static void GoIntoOptionsLoop()
        {
            bool UserWantsToQuit = false;
            while (!UserWantsToQuit)
            {
                Console.WriteLine("Please Select an option:");
                Console.WriteLine("1 - Select A Specific Node by name");
                Console.WriteLine("2 - Select Genesis Node");
                Console.WriteLine("3 - Select Node By Index");
                Console.WriteLine("0 - Exit");

                int choice;

                bool isCorrectInput = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrectInput)
                {
                    switch (choice)
                    {
                        case 1:
                            SelectNodeByName();
                            break;

                        case 2:
                            SelectGenesisNode();
                            break;
                        case 3:
                            SelectNodeByIndex();
                            break;
                        case 0:
                            Console.WriteLine("See you next time!");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Option not available, please try again");
                            break;
                    }
                }

            }
        }

        public static void InteractWithNode(Node NodeToInteractWith)
        {
            bool userWantsToGoBackToMainMenu = false;

            while (!userWantsToGoBackToMainMenu)
            {
                Console.WriteLine($"What do you want to do with the selected node: \"{NodeToInteractWith.ID}\"?");
                Console.WriteLine("1 - Add Link");
                Console.WriteLine("2 - Add Child");
                if (!NodeToInteractWith.IsGenesisNode)
                {
                    Console.WriteLine("3 - Add Parent");
                }
                Console.WriteLine("0 - Exit");

                int choice;

                bool isCorrectInput = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrectInput)
                {
                    switch (choice)
                    {
                        case 1:
                            SelectNodeByName();
                            break;

                        case 2:
                            SelectGenesisNode();
                            break;
                        
                        case 0:
                            Console.WriteLine("See you next time!");
                            Thread.Sleep(3000);
                            Environment.Exit(0);
                            break;

                        case 3:
                            if (!NodeToInteractWith.IsGenesisNode)
                            {
                                // Do Regular stuff
                                SelectNodeByIndex();
                                break;
                            }

                            else
                            {
                                goto default;
                            }
                            
                        default:
                            Console.WriteLine("Option not available, please try again");
                            break;
                    }
                }

            }
        }


        private static void SelectNodeByIndex()
        {
            Console.WriteLine("Enter of the index of the node that you want to interact with");
            int index;
            bool isCorrectInput = int.TryParse(Console.ReadLine(), out index);

            if (isCorrectInput)
            {
                try
                {
                    Node NodeToUse = CreatedGraph.Nodes[index];
                    InteractWithNode(NodeToUse);
                }
                catch (Exception)
                {
                    Console.WriteLine("There are no nodes with this index yet. Please try a lower index next time.");
                }

            }


        }

        private static void SelectGenesisNode()
        {
            InteractWithNode(CreatedGraph.GenesisNode);
        }

        private static void SelectNodeByName()
        {
            Console.WriteLine("Enter the name of the node you want to interact with: ");
            string NodeName = Console.ReadLine();
            if (CreatedGraph.Contains(NodeName))
            {
                Node NodeToUse = CreatedGraph.GetNode(NodeName);
                InteractWithNode(NodeToUse);
            }
        }

        private static void StartGenesis()
        {
            Console.WriteLine("Enter the name of the Genesis node of your graph: ");
            string GenesisNodeName = Console.ReadLine();
            CreatedGraph = new Graph(GenesisNodeName);
            Console.WriteLine("\"" + CreatedGraph.Nodes.First().ID.ToString() + "\"" + " is the Genesis Node!");
        }
    }
}
