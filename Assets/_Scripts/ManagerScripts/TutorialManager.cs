using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    //Start
    private string[] line1 = {
        "You're finally awake. I almost lost all hope. Don't worry if you can't see, your visual processor was damaged so I had to be a little creative when putting you back together.", 
        "I jerry rigged a state of the art sonar to your vision. You should be able to see using echolocation. Press the Right Mouse Button to see your surroundings. Press E to get try it out now!"
    };

    //Ping

    private string[] line2 = {
        "Oh my god it worked! I had no doubt in my mind whatsoever. Your sonar senses should be picking up a faint hum of machinery. Follow the sound, and use your echolocation to help you navigate there.", 
        "Oh wait, you do remember how to walk right? Use the WASD keys to move around. And try not to bump into anything, I don't want my impeccable work to get wasted. Again."
    };

    //Trigger 1

    private string[] line3 = {
        "Now if I remember this place correctly, there's probably a few obstacles in your way right about now. Now remember this carefully: we're going to go UP and UNDER. I repeat, UP and UNDER.", 
        "Press Space to Jump, and to nobody's surprise, press the CTRL or C key to crouch. REMEMBER, UP and UNDER."
    };

    //Trigger 2

    private string[] line4 = {
        "Gosh I really hope you remmeber who I am by now because it would be really embarrassing if you didn't. I mean, who just blindly follows a random voice in their head? Although seeing how we're stuck together, I guess you don't have much of a choice.", 
        "Anyways, listen up. I supercharged your sonar so you can see a lot further than normal. Press the Left Mouse Button to shoot out a sonar ping. Try not to bounce it off your face though, this baby can knock your socks off."
    };

    //Trigger 3

    private string[] line5 = {
        "Sigh... If only life were that easy. Although, I guess you wouldn't really know being a robot and all. Life is all about experiences, both good and bad. When we get out of here, promise me you'll find out what it means to live.", 
        "Until then, I guess you can just follow my voice and do exactly as I say. I didn't have time to include a voice module while I was busy putting you back together, but I'll take your unending silence as a resounding 'Hell Yeah'"
    };

    public GameEvent SetDialogue;

    private int index = 0;
    private int length = 5;

    private void Start()
    {
        index = 0;
        FollowTutorial(this, null);
    }

    public void FollowTutorial(Component sender, object data)
    {
        if (index == 0 )
        {
            SetDialogue.Raise(this, line1);
            index++;
        } else if (index == 1 && sender.gameObject.tag.Equals("Player") )
        {
            SetDialogue.Raise(this, line2);
            index++;
        } else if (index == 2 && sender.gameObject.name.Equals("Trigger1") )
        {
            SetDialogue.Raise(this, line3);
            index++;

        } else if (index == 3 && sender.gameObject.name.Equals("Trigger2") )
        {
            SetDialogue.Raise(this, line4);
            index++;

        } else if (index == 4 && sender.gameObject.name.Equals("Trigger3") )
        {
            SetDialogue.Raise(this, line5);
            index++;
        }
    }
}
