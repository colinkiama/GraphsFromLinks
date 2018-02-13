using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsFromLinks
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new List<Node>();
        public Node GenesisNode { get; set; }
        public Graph(string GenesisNodeID)
        {
            GenesisNode = new Node(GenesisNodeID, this, true);
            Nodes.Add(GenesisNode);
        }

        public bool Contains(string NodeID)
        {
            bool isNodeInGraph = false;
            int count = Nodes.Where(p => p.ID == NodeID).Count();
            if (count > 0)
            {
                isNodeInGraph = true;
            }

            return isNodeInGraph;
        }

        public void AddNode(Node NodeToAdd)
        {
            Nodes.Add(NodeToAdd);
        }

        public Node GetNode(string NodeID)
        {
            Node nodeToReturn = Nodes.Where(p => p.ID == NodeID).First();
            return nodeToReturn;
        }

        internal List<Node> GetSiblings()
        {
            int count = this.Nodes.Count;
            List<Node> SiblingsToReturn = new List<Node>();
            for (int i = 0; i < count; i++)
            {
                if (this.Nodes[i].Siblings.Count > 0)
                {
                    SiblingsToReturn.Add(this.Nodes[i]);
                }
            }
            return SiblingsToReturn;
        }

        internal List<Node> GetChildren()
        {
            int count = this.Nodes.Count;
            List<Node> ChildrenToReturn = new List<Node>();
            for (int i = 0; i < count; i++)
            {
                if (this.Nodes[i].Parents.Count > 0)
                {
                    ChildrenToReturn.Add(this.Nodes[i]);
                }
            }
            return ChildrenToReturn;
        }

        internal List<Node> GetParents()
        {
            int count = this.Nodes.Count;
            List<Node> ChildrenToReturn = new List<Node>();
            for (int i = 0; i < count; i++)
            {
                if (this.Nodes[i].Children.Count > 0)
                {
                    ChildrenToReturn.Add(this.Nodes[i]);
                }
            }
            return ChildrenToReturn;
        }
    }

    public class Node
    {
        public List<Node> Parents { get; } = new List<Node>();
        public List<Node> Children { get; } = new List<Node>();
        public List<Node> Siblings { get; } = new List<Node>();
        public string ID { get; }
        public bool IsGenesisNode { get; } = false;
        public Graph BirthPlace { get; }  
        

        public static string GetListOfNodesAsString(List<Node> Nodes)
        {
            string NodeNamesInAString = "";
            int count = Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < count - 1)
                {
                    NodeNamesInAString += Nodes[i].ID + ", ";
                }

                else
                {
                    NodeNamesInAString += Nodes[i].ID;
                }

            }

            return NodeNamesInAString;
        }

        public Node(string id, Graph birthPlace, bool isGenesisNode = false)
        {
            ID = id;

            BirthPlace = birthPlace;

            if (isGenesisNode)
            {
                IsGenesisNode = isGenesisNode;
            }
        }

        public bool AddLink(string LinkID)
        {
            bool isLinkSuccessful = false;
            if (!BirthPlace.Contains(LinkID))
            {
                Node NodeToLink = new Node(LinkID, this.BirthPlace);
                AddNodeAsSibling(ref NodeToLink);
                isLinkSuccessful = true;
            }
            else
            {
                Node NodeToLink = BirthPlace.GetNode(LinkID);
                isLinkSuccessful = AddLink(NodeToLink);
            }

            return isLinkSuccessful;
        }

        public bool AddLink(Node OtherNode)
        {
            bool isLinkSuccessful = false;

            if (this.BirthPlace == OtherNode.BirthPlace)
            {
                AddNodeAsSibling(ref OtherNode);
                isLinkSuccessful = true;
            }

            return isLinkSuccessful;
        }

        private void AddNodeAsSibling(ref Node nodeToLink)
        {
            if (!nodeToLink.Siblings.Contains(this))
            {
                nodeToLink.Siblings.Add(this);
                this.Siblings.Add(nodeToLink);
                this.BirthPlace.AddNode(nodeToLink);
            }
            
        }


        public bool AddChild(string NodeID)
        {
            bool isLinkSuccessful = false;
            if (!BirthPlace.Contains(NodeID))
            {
                Node NodeToLink = new Node(NodeID, this.BirthPlace);
                AddNodeAsChild(ref NodeToLink);
                isLinkSuccessful = true;
            }
            else
            {
                Node NodeToLink = BirthPlace.GetNode(NodeID);
                isLinkSuccessful = AddChild(NodeToLink);
            }

            return isLinkSuccessful;
        }


        public bool AddChild(Node OtherNode)
        {
            bool isLinkSuccessful = false;

            if (this.BirthPlace == OtherNode.BirthPlace)
            {
                AddNodeAsChild(ref OtherNode);
                isLinkSuccessful = true;
            }

            return isLinkSuccessful;
        }


        private void AddNodeAsChild(ref Node nodeToLink)
        {
            if (!nodeToLink.Parents.Contains(this))
            {
                nodeToLink.Parents.Add(this);
                this.Children.Add(nodeToLink);
                this.BirthPlace.AddNode(nodeToLink);
            }
        }


        public bool AddParent(string NodeID)
        {
            bool isLinkSuccessful = false;
            if (!BirthPlace.Contains(NodeID))
            {
                Node NodeToLink = new Node(NodeID, this.BirthPlace);
                AddNodeAsParent(ref NodeToLink);
                isLinkSuccessful = true;
            }
            else
            {
                Node NodeToLink = BirthPlace.GetNode(NodeID);
                isLinkSuccessful = AddParent(NodeToLink);
            }

            return isLinkSuccessful;
        }

        public bool AddParent(Node OtherNode)
        {
            bool isLinkSuccessful = false;

            if (this.BirthPlace == OtherNode.BirthPlace)
            {
                AddNodeAsParent(ref OtherNode);
                isLinkSuccessful = true;
            }

            return isLinkSuccessful;
        }


        private void AddNodeAsParent(ref Node nodeToLink)
        {
            if (!nodeToLink.Children.Contains(this))
            {
                nodeToLink.Children.Add(this);
                this.Parents.Add(nodeToLink);
                this.BirthPlace.AddNode(nodeToLink);
            }
        }
    }


}
