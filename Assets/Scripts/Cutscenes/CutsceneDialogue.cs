using UnityEngine;
using UnityEngine.Playables;

public class CutsceneDialogue : MonoBehaviour
{
    public PlayableDirector timeline;
    public Conversation[] convo;
    private int currIndex;
    public bool finished;

    private void Start()
    {
        currIndex = 0;
    }

    public void StartCutscene()
    {
        timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);
        DialogueManager.StartConversation(convo[currIndex]);
        currIndex++;
        finished = true;
    }

    private void Update()
    {
        if (finished && !GameManager.instance.playerFrozen)
        {
            timeline.playableGraph.GetRootPlayable(0).SetSpeed(1);
            finished = false;
        }

    }
}
