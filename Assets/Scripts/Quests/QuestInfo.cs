using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This holds a single quest, this is
// necessary since monobehaviours are
// destroyed once a scene exists without
// its parent object.
public class QuestInfo : MonoBehaviour{
    public Quest quest; // The quest attached to this object
}