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

        public Node GetNode(string NodeID)
        {
            Node nodeToReturn = Nodes.Where(p => p.ID == NodeID).First();
            return nodeToReturn;
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
            }
        }
    }


}
