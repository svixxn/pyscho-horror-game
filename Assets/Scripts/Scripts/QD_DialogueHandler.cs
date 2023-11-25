using UnityEngine;

namespace QuantumTek.QuantumDialogue
{
    /// <summary>
    /// Gives basic information about a message, including its id, type (either message or choice), and next message id.
    /// </summary>
    [System.Serializable]
    public struct QD_MessageInfo
    {
        public int ID;
        public int NextID;
        public QD_NodeType Type;

        public QD_MessageInfo(int id, int nextID, QD_NodeType type)
        {
            ID = id;
            NextID = nextID;
            Type = type;
        }
    }

    /// <summary>
    /// QD_DialogueHandler is responsible for controlling a dialogue and the conversations within.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Dialogue/Dialogue Handler")]
    [DisallowMultipleComponent]
    public class QD_DialogueHandler : MonoBehaviour
    {
        public QD_Dialogue dialogue;
        [HideInInspector] public int currentConversationIndex = -1;
        [HideInInspector] public QD_Conversation currentConversation;
        [HideInInspector] public QD_MessageInfo currentMessageInfo;

        /// <summary>
        /// Returns the current message, if it is a message node.
        /// </summary>
        /// <returns></returns>
        public QD_Message GetMessage()
        {
            if (currentMessageInfo.Type != QD_NodeType.Message)
                return null;
            return dialogue.GetMessage(currentMessageInfo.ID);
        }

        /// <summary>
        /// Returns the current choice, if it is a choice node.
        /// </summary>
        /// <returns></returns>
        public QD_Choice GetChoice()
        {
            if (currentMessageInfo.Type != QD_NodeType.Choice)
                return null;
            return dialogue.GetChoice(currentMessageInfo.ID);
        }

        /// <summary>
        /// Sets the current conversation based on the name of it.
        /// </summary>
        /// <param name="name">The name of the conversation.</param>
        public void SetConversation(string name)
        {
            currentConversationIndex = dialogue.GetConversationIndex(name);
            if (currentConversationIndex < 0 || currentConversationIndex >= dialogue.Conversations.Count)
                return;
            currentConversation = dialogue.Conversations[currentConversationIndex];
            currentMessageInfo = new QD_MessageInfo(currentConversation.FirstMessage, GetNextID(currentConversation.FirstMessage), QD_NodeType.Message);
        }

        /// <summary>
        /// Returns the id of the next message.
        /// </summary>
        /// <param name="id">The ID of the current message.</param>
        /// <param name="choice">The choice, if the current message is a choice. By default -1, meaning the current node is a message node.</param>
        /// <returns></returns>
        public int GetNextID(int id, int choice = -1)
        {
            int nextID = -1;
            QD_NodeType type = GetMessageType(id);
            if (type == QD_NodeType.Message)
            {
                QD_Message m = dialogue.GetMessage(id);
                if (m.NextMessage >= 0)
                    nextID = m.NextMessage;
            }
            else if (type == QD_NodeType.Conversation)
            {
                QD_Choice c = dialogue.GetChoice(id);
                if (c.NextMessages[choice] >= 0)
                    nextID = c.NextMessages[choice];
            }
            return nextID;
        }

        /// <summary>
        /// Returns the type of a message with the given ID. Base if there is no next message.
        /// </summary>
        /// <param name="id">The id of the message or choice.</param>
        /// <returns></returns>
        public QD_NodeType GetMessageType(int id)
        {
            if (dialogue.GetMessageIndex(id) >= 0)
                return QD_NodeType.Message;
            else if (dialogue.GetChoiceIndex(id) >= 0)
                return QD_NodeType.Choice;
            return QD_NodeType.Base;
        }

        /// <summary>
        /// Goes to the next message and returns its ID and type. Base if there is no next message, but otherwise Message or Choice.
        /// </summary>
        /// <param name="choice">The choice, if the current message is a choice. By default -1, meaning the current node is a message node.</param>
        /// <returns></returns>
        public QD_MessageInfo NextMessage(int choice = -1)
        {
            if (currentMessageInfo.NextID < 0 && choice == -1)
            {
                currentMessageInfo = new QD_MessageInfo(-1, -1, QD_NodeType.Base);
                return currentMessageInfo;
            }

            QD_NodeType type = GetMessageType(currentMessageInfo.NextID);
            int id = -1;
            int nextID = -1;
            
            if (currentMessageInfo.Type == QD_NodeType.Message)
            {
                id = currentMessageInfo.NextID;
                
                if (type == QD_NodeType.Message && id >= 0)
                {
                    QD_Message m = dialogue.GetMessage(id);
                    nextID = m.NextMessage;
                }
            }
            else if (currentMessageInfo.Type == QD_NodeType.Choice && choice >= 0)
            {
                QD_Choice c = dialogue.GetChoice(currentMessageInfo.ID);
                currentMessageInfo.NextID = c.NextMessages[choice];
                id = currentMessageInfo.NextID;
                type = QD_NodeType.Message;
                QD_Message m = dialogue.GetMessage(id);
                nextID = m.NextMessage;
            }

            currentMessageInfo = new QD_MessageInfo(id, nextID, type);
            return currentMessageInfo;
        }
    }
}