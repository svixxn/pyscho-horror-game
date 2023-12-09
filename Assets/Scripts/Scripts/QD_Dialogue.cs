using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumDialogue
{
    /// <summary>
    /// QD_Dialogue is a list of conversations that contain speakers, messages, and player choices.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Dialogue/Dialogue")]
    public class QD_Dialogue : ScriptableObject
    {
        public List<QD_Conversation> Conversations = new List<QD_Conversation>();
        public List<QD_Speaker> Speakers = new List<QD_Speaker>();
        public List<QD_Message> Messages = new List<QD_Message>();
        public List<QD_Choice> Choices = new List<QD_Choice>();

        /// <summary>
        /// Returns a conversation with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QD_Conversation GetConversation(int id)
        {
            foreach (var conversation in Conversations)
                if (conversation.ID == id)
                    return conversation;
            return null;
        }
        /// <summary>
        /// Returns a conversation with the given name.
        /// </summary>
        /// <param name="name">The name of the conversation.</param>
        /// <returns></returns>
        public QD_Conversation GetConversation(string name)
        {
            foreach (var conversation in Conversations)
                if (conversation.Name == name)
                    return conversation;
            return null;
        }
        /// <summary>
        /// Returns a speaker with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QD_Speaker GetSpeaker(int id)
        {
            foreach (var speaker in Speakers)
                if (speaker.ID == id)
                    return speaker;
            return null;
        }
        /// <summary>
        /// Returns a speaker with the given name.
        /// </summary>
        /// <param name="name">The name of the speaker.</param>
        /// <returns></returns>
        public QD_Speaker GetSpeaker(string name)
        {
            foreach (var speaker in Speakers)
                if (speaker.Name == name)
                    return speaker;
            return null;
        }
        /// <summary>
        /// Returns a message with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QD_Message GetMessage(int id)
        {
            foreach (var message in Messages)
                if (message.ID == id)
                    return message;
            return null;
        }
        /// <summary>
        /// Returns a choice with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QD_Choice GetChoice(int id)
        {
            foreach (var choice in Choices)
                if (choice.ID == id)
                    return choice;
            return null;
        }

        /// <summary>
        /// Returns the index of a conversation in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetConversationIndex(int id)
        {
            int conversationCount = Conversations.Count;
            for (int i = 0; i < conversationCount; ++i)
                if (Conversations[i].ID == id)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a conversation in the list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetConversationIndex(string name)
        {
            int conversationCount = Conversations.Count;
            for (int i = 0; i < conversationCount; ++i)
                if (Conversations[i].Name == name)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a speaker in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetSpeakerIndex(int id)
        {
            int speakerCount = Speakers.Count;
            for (int i = 0; i < speakerCount; ++i)
                if (Speakers[i].ID == id)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a speaker in the list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetSpeakerIndex(string name)
        {
            int speakerCount = Speakers.Count;
            for (int i = 0; i < speakerCount; ++i)
                if (Speakers[i].Name == name)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a message in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetMessageIndex(int id)
        {
            int messageCount = Messages.Count;
            for (int i = 0; i < messageCount; ++i)
                if (Messages[i].ID == id)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a choice in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetChoiceIndex(int id)
        {
            int choiceCount = Choices.Count;
            for (int i = 0; i < choiceCount; ++i)
                if (Choices[i].ID == id)
                    return i;
            return -1;
        }

        /// <summary>
        /// Sets a conversation with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="conversation"></param>
        /// <returns></returns>
        public void SetConversation(int id, QD_Conversation conversation)
        {
            Conversations[GetConversationIndex(id)] = conversation;
        }
        /// <summary>
        /// Sets a speaker with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="speaker"></param>
        /// <returns></returns>
        public void SetSpeaker(int id, QD_Speaker speaker)
        {
            Speakers[GetSpeakerIndex(id)] = speaker;
        }
        /// <summary>
        /// Sets a message with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void SetMessage(int id, QD_Message message)
        {
            Messages[GetMessageIndex(id)] = message;
        }
        /// <summary>
        /// Sets a choice with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="choice"></param>
        /// <returns></returns>
        public void SetChoice(int id, QD_Choice choice)
        {
            Choices[GetChoiceIndex(id)] = choice;
        }
    }
}