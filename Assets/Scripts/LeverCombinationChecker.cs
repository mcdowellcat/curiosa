using UnityEngine;

public class LeverCombinationChecker : MonoBehaviour
{
    bool diamondDown, clubDown, heartUp, spadeDown;
    [SerializeField] GameObject objectToReveal1;
    [SerializeField] GameObject objectToReveal2;
    [SerializeField] AudioSource successAudio;  // assign in Inspector

    bool hasPlayedAudio = false; // prevents replaying constantly

    // Diamond lever hits its “down” position
    public void Diamond_Deactivated()
    {
        diamondDown = true;
        CheckCombination();
    }
    // Diamond lever hits its “up” position
    public void Diamond_Activated()
    {
        diamondDown = false;
        CheckCombination();
    }

    // Club lever down
    public void Club_Deactivated()
    {
        clubDown = true;
        CheckCombination();
    }
    // Club lever up
    public void Club_Activated()
    {
        clubDown = false;
        CheckCombination();
    }

    // Heart lever up
    public void Heart_Activated()
    {
        heartUp = true;
        CheckCombination();
    }
    // Heart lever down
    public void Heart_Deactivated()
    {
        heartUp = false;
        CheckCombination();
    }

    // Spade lever down
    public void Spade_Deactivated()
    {
        spadeDown = true;
        CheckCombination();
    }
    // Spade lever up
    public void Spade_Activated()
    {
        spadeDown = false;
        CheckCombination();
    }

    void CheckCombination()
    {
        // only when D-down, C-down, H-up, S-down
        bool correct = diamondDown && clubDown && heartUp && spadeDown;

        objectToReveal1.SetActive(correct);
        objectToReveal2.SetActive(correct);

        if (correct && !hasPlayedAudio)
        {
            if (successAudio != null)
            {
                successAudio.Play();
            }
            hasPlayedAudio = true;
        }
        else if (!correct)
        {
            hasPlayedAudio = false; // reset so it can play again next time
        }
    }
}
