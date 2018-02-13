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

            Console.WriteLine("See you next time!");
            Thread.Sleep(3000);
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
                Console.WriteLine("4 - Show All Parents");
                Console.WriteLine("5 - Show All Children");
                Console.WriteLine("6 - Show All Siblings");
                Console.WriteLine("7 - Show All Nodes");
                Console.WriteLine("0 - Exit");

                int choice;

                bool isCorrectInput = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrectInput)
                {
                    switch (choice)
                    {
                        case 1:
                            InsertLineOfSpace();
                            SelectNodeByName();
                            break;
                        case 2:
                            InsertLineOfSpace();
                            SelectGenesisNode();
                            break;
                        case 3:
                            InsertLineOfSpace();
                            SelectNodeByIndex();
                            break;
                        case 4:
                            InsertLineOfSpace();
                            ShowAllParents();
                            break;
                        case 5:
                            InsertLineOfSpace();
                            ShowAllChildren();
                            break;
                        case 6:
                            InsertLineOfSpace();
                            ShowAllSiblings();
                            break;
                        case 7:
                            InsertLineOfSpace();
                            ShowAllNodes();
                            break;
                        case 0:
                            InsertLineOfSpace();
                            UserWantsToQuit = true;
                            break;
                        default:
                            Console.WriteLine("Option not available, please try again");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input, please enter a number");
                }

            }
        }

        private static void ShowAllNodes()
        {
            foreach (var Node in CreatedGraph.Nodes)
            {
                Console.WriteLine($"[{CreatedGraph.Nodes.IndexOf(Node)}] - \"{Node.ID}\" ");
            }
            Console.WriteLine("\n");
        }

        private static void ShowAllSiblings()
        {
            List<Node> Siblings = CreatedGraph.GetSiblings();
            if (Siblings.Count > 0)
            {
                string NodeNamesInAString = Node.GetListOfNodesAsString(Siblings);
                Console.WriteLine($"List Of All Siblings: {NodeNamesInAString}");
            }

            else
            {
                Console.WriteLine("There are no siblings in this graph.");
            }

            Console.WriteLine("\n");
        }


      

        private static void ShowAllChildren()
        {
            List<Node> Children = CreatedGraph.GetChildren();
            if (Children.Count > 0)
            {
                string NodeNamesInAString = Node.GetListOfNodesAsString(Children);
                Console.WriteLine($"List of all children: {NodeNamesInAString}");
            }

            else
            {
                Console.WriteLine("There are no children in this graph.");
            }
            InsertLineOfSpace();
        }

        private static void InsertLineOfSpace()
        {
            Console.WriteLine("\n");
        }

        private static void ShowAllParents()
        {
            List<Node> Parents = CreatedGraph.GetParents();
            if (Parents.Count > 0)
            {
                string NodeNamesInAString = Node.GetListOfNodesAsString(Parents);
                Console.WriteLine($"List of all parents: {NodeNamesInAString}");
            }

            else
            {
                Console.WriteLine("There are no parents in this graph.");
            }
        }

        public static void InteractWithNode(Node NodeToInteractWith)
        {
            InsertLineOfSpace();

            bool UserWantsToGoBackToMainMenu = false;

            while (!UserWantsToGoBackToMainMenu)
            {
                Console.WriteLine($"What do you want to do with the selected node: \"{NodeToInteractWith.ID}\"?");
                Console.WriteLine("1 - Add Sibling");
                Console.WriteLine("2 - Add Child");

                if (!NodeToInteractWith.IsGenesisNode)
                {
                    Console.WriteLine("3 - Add Parent");
                }

                Console.WriteLine("4 - Show Children");

                if (!NodeToInteractWith.IsGenesisNode)
                {
                    Console.WriteLine("5 - Show Parents");
                }

                Console.WriteLine("6 - Show Siblings");
                Console.WriteLine("0 - Go Back to main menu");

                int choice;

                bool isCorrectInput = int.TryParse(Console.ReadLine(), out choice);

                if (isCorrectInput)
                {
                    switch (choice)
                    {
                        case 1:
                            InsertLineOfSpace();
                            Console.WriteLine("What is the name of the sibling that you want to add?");
                            string NameOfNewNode = Console.ReadLine();
                            NodeToInteractWith.AddSibling(NameOfNewNode);
                            Console.WriteLine("Sibling Added");
                            break;

                        case 2:
                            InsertLineOfSpace();
                            Console.WriteLine("What is the name of the Child that you want to add?");
                            string NameOfChildToAdd = Console.ReadLine();
                            NodeToInteractWith.AddChild(NameOfChildToAdd);
                            Console.WriteLine("Child Added!");
                            break;

                        case 3:
                            InsertLineOfSpace();
                            if (!NodeToInteractWith.IsGenesisNode)
                            {
                                // Do Regular stuff
                                Console.WriteLine("What is the name of the Parent that you want to add?");
                                string NameOfParent = Console.ReadLine();
                                NodeToInteractWith.AddParent(NameOfParent);
                                Console.WriteLine("Parent Added!");
                                break;
                            }

                            else
                            {
                                goto default;
                            }

                        case 4:
                            InsertLineOfSpace();
                            ShowChildrenOfNode(NodeToInteractWith);
                            break;
                        case 5:
                            InsertLineOfSpace();
                            ShowParentsOfNode(NodeToInteractWith);
                            break;
                        case 6:
                            InsertLineOfSpace();
                            ShowSiblingsOfNode(NodeToInteractWith);
                            break;
                        
                        case 0:
                            InsertLineOfSpace();
                            UserWantsToGoBackToMainMenu = true;
                            break;

                        

                        default:
                            InsertLineOfSpace();
                            Console.WriteLine("Option not available in selection, please try again");
                            break;
                    }
                }

                else
                {
                    InsertLineOfSpace();
                    Console.WriteLine("Incorrect input, please enter a number");
                }

            }
        }

        private static void ShowSiblingsOfNode(Node nodeToInteractWith)
        {
            if (nodeToInteractWith.Siblings.Count > 0)
            {
                string ListOfSiblingsAsString = Node.GetListOfNodesAsString(nodeToInteractWith.Siblings);
                Console.WriteLine($"List of siblings of \"{nodeToInteractWith.ID}\": {ListOfSiblingsAsString}");
            }
            else
            {
                Console.WriteLine($"\"{nodeToInteractWith.ID}\" does not have any siblings.");
            }
        }

        private static void ShowParentsOfNode(Node nodeToInteractWith)
        {
            if (nodeToInteractWith.Parents.Count > 0)
            {
                string ListOfParentsAsString = Node.GetListOfNodesAsString(nodeToInteractWith.Parents);
                Console.WriteLine($"List of parents of \"{nodeToInteractWith.ID}\": {ListOfParentsAsString}");
            }
            else
            {
                Console.WriteLine($"\"{nodeToInteractWith.ID}\" does not have any parents.");
            }

        }

        private static void ShowChildrenOfNode(Node nodeToInteractWith)
        {
            if (nodeToInteractWith.Children.Count > 0)
            {
                string ListOfChildrenAsString = Node.GetListOfNodesAsString(nodeToInteractWith.Children);
                Console.WriteLine($"List of children of \"{nodeToInteractWith.ID}\": {ListOfChildrenAsString}");
            }
            else
            {
                Console.WriteLine($"\"{nodeToInteractWith.ID}\" does not have any children.");
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
            else
            {
                Console.WriteLine("A Node With That Name Does Not Exist!");
            }
        }

        private static void StartGenesis()
        {
            Console.WriteLine("Enter the name of the Genesis node of your graph: ");
            string GenesisNodeName = Console.ReadLine();
            CreatedGraph = new Graph(GenesisNodeName);
            InsertLineOfSpace();
            Console.WriteLine("\"" + CreatedGraph.Nodes.First().ID.ToString() + "\"" + " is the Genesis Node!");
        }
    }
}
