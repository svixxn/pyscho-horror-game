using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumDialogue
{
    /// <summary>
    /// QD_Message represents one message in a conversation said by a speaker.
    /// </summary>
    [System.Serializable]
    public class QD_Message
    {
        public int ID;
        public int Speaker = -1;
        public string SpeakerName = "";
        public int PreviousMessage = -1;
        public int NextMessage = -1;
        public string MessageText;
        public AudioClip Clip;

        public QD_Message(int id)
        { ID = id; }

        /// <summary>
        /// The function called to modify the node's data whenever a node connects.
        /// </summary>
        /// <param name="dialogue">The dialogue data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connecting node.</param>
        /// <param name="connectionKnobID">The id of the connecting knob.</param>
        /// <param name="knobID">The id of this node's knob.</param>
        /// <param name="knobType">The type of this node's knob.</param>
        public void OnConnect(QD_Dialogue dialogue, QD_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QD_KnobType knobType)
        {
            if (knobType == QD_KnobType.Input)
            {
                if (connectionKnobID == 0 && knobID == 0)
                {
                    if (connectionType == QD_NodeType.Speaker)
                    {
                        QD_Speaker speaker = dialogue.GetSpeaker(connectionID);
                        Speaker = speaker.ID;
                        SpeakerName = speaker.Name;
                    }
                    else if (connectionType == QD_NodeType.Message)
                    {
                        QD_Message message = dialogue.GetMessage(connectionID);
                        Speaker = message.Speaker;
                        SpeakerName = message.SpeakerName;
                    }
                }
                else if (PreviousMessage != connectionID && connectionKnobID == 1 && knobID == 1)
                    PreviousMessage = connectionID;
            }
            else if (knobType == QD_KnobType.Output)
            {
                if (NextMessage != connectionID && knobID == 1)
                    NextMessage = connectionID;
            }
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="dialogue">The dialogue data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        public void OnDisconnect(QD_Dialogue dialogue, QD_NodeType connectionType, int connectionID)
        {
            if (Speaker == connectionID)
            {
                SpeakerName = "";
                Speaker = -1;
            }
            if (PreviousMessage == connectionID)
                PreviousMessage = -1;
            if (NextMessage == connectionID)
                NextMessage = -1;
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="dialogue">The dialogue data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        /// <param name="knobID">The id of the knob.</param>
        /// <param name="knobType">The type of the knob.</param>
        public void OnDisconnect(QD_Dialogue dialogue, QD_NodeType connectionType, int connectionID, int knobID, QD_KnobType knobType)
        {
            if (knobType == QD_KnobType.Input)
            {
                if (knobID == 0 && Speaker == connectionID)
                {
                    SpeakerName = "";
                    Speaker = -1;
                }
                if (PreviousMessage == connectionID && knobID == 1)
                    PreviousMessage = -1;
            }
            else if (knobType == QD_KnobType.Output)
            {
                if (NextMessage == connectionID && knobID == 1)
                    NextMessage = -1;
            }
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="dialogue">The dialogue data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        /// <param name="connectionKnobID">The id of the connected knob.</param>
        /// <param name="knobID">The id of the knob.</param>
        /// <param name="knobType">The type of the knob.</param>
        public void OnDisconnect(QD_Dialogue dialogue, QD_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QD_KnobType knobType)
        {
            OnDisconnect(dialogue, connectionType, connectionID, knobID, knobType);
        }
    }
}