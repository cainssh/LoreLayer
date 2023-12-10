using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreLayer.Dialogues
{
    public class Dialogue
    {
        public List<DialogueNode> Nodes { get; private set; }

        public Dialogue()
        {
            Nodes = new List<DialogueNode>();
        }

        public void AddNode(DialogueNode node)
        {
            Nodes.Add(node);
        }

        public DialogueNode GetNodeById(int id)
        {
            return Nodes.Find(node => node.Id == id);
        }

        // Additional methods for dialogue functionality
    }

    public class DialogueNode
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<int> NextNodeIds { get; set; }

        public DialogueNode(int id, string text)
        {
            Id = id;
            Text = text;
            NextNodeIds = new List<int>();
        }

        // Additional properties and methods
    }
}
