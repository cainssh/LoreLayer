using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoreLayer
{
    public class Dialogue
    {
        public List<DialogueLine> Lines { get; set; }

        public Dialogue()
        {
            Lines = new List<DialogueLine>();
        }

        public void AddLine(DialogueLine line)
        {
            Lines.Add(line);
        }

        // Additional methods for dialogue functionality (like branching logic, conditional lines, etc.)
    }

    public class DialogueLine
    {
        public string Text { get; set; }
        public List<DialogueOption> Options { get; set; }

        public DialogueLine(string text)
        {
            Text = text;
            Options = new List<DialogueOption>();
        }

        // Method to add dialogue options
        public void AddOption(DialogueOption option)
        {
            Options.Add(option);
        }
    }

    public class DialogueOption
    {
        public string Text { get; set; }
        public Action OnSelect { get; set; }

        public DialogueOption(string text, Action onSelect)
        {
            Text = text;
            OnSelect = onSelect;
        }

        public void Select()
        {
            OnSelect?.Invoke();
        }
    }
}

