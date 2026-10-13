using UnityEngine;
using TMPro; //required while using TMPro

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text; //needed while using TMPro

    public Animator animator;

    public void Highlight()
    {
        animator.SetTrigger("highlight");
    }


    public void setScore(int value)
    {
        text.text = value.ToString();
    }
}
