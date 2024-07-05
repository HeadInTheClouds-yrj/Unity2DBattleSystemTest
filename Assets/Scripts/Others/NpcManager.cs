using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcManager : MonoBehaviour
{

    public static NpcManager Instance;
    private List<NpcAI> npcs = new List<NpcAI>();
    private void OnEnable()
    {
    }
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddNpc(NpcAI npcAI)
    {
        npcs.Add(npcAI);
    }
    public List<NpcAI> GetNpcs()
    {
        return npcs;
    }
}
