using UnityEngine;

namespace QuantumTek.QuantumDialogue.Demo
{
    public class QD_ChoiceButton : MonoBehaviour
    {
        public int number;
        public QD_DialogueDemo demo;

        public void Select() => demo.Choose(number);
    }
}