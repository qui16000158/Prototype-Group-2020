using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This holds basic information about
// NPCs, such as their name and the
// conversation that loads when talking
// to them
public class NPCInfo : MonoBehaviour
{
    public new string name = "NPC"; // The NPCs name (Can be set in inspector)
    public Conversation starterConversation; // The conversation that starts when interacting
}
