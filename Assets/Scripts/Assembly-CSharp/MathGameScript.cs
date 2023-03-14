using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000C2 RID: 194
public class MathGameScript : MonoBehaviour
{
    // Token: 0x06000982 RID: 2434 RVA: 0x000231E0 File Offset: 0x000215E0
    private void Start()
    {
        this.gc.ActivateLearningGame();
        if (this.gc.notebooks == 1)
        {
            this.QueueAudio(this.bal_intro);
            this.QueueAudio(this.bal_howto);
        }
        this.NewProblem();
        if (this.gc.spoopMode)
        {
            this.baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
        }
    }

    // Token: 0x06000983 RID: 2435 RVA: 0x00023270 File Offset: 0x00021670
    private void Update()
    {
        if (!this.baldiAudio.isPlaying)
        {
            if (this.audioInQueue > 0 & !this.gc.spoopMode)
            {
                this.PlayQueue();
            }
            this.baldiFeed.SetBool("talking", false);
        }
        else
        {
            this.baldiFeed.SetBool("talking", true);
        }
        if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & this.questionInProgress)
        {
            this.questionInProgress = false;
            this.CheckAnswer();
        }
        if (this.problem > 3)
        {
            this.endDelay -= 1f * Time.unscaledDeltaTime;
            if (this.endDelay <= 0f)
            {
                GC.Collect();
                this.ExitGame();
            }
        }
    }

    // Token: 0x06000984 RID: 2436 RVA: 0x00023350 File Offset: 0x00021750
    private void NewProblem()
    {
        this.playerAnswer.text = string.Empty;
        this.problem++;
        this.playerAnswer.ActivateInputField();
        if (this.problem <= 3)
        {
            this.QueueAudio(this.bal_problems[this.problem - 1]);
            if ((this.gc.mode == "story" & (this.problem <= 2 || this.gc.notebooks <= 1)) || (this.gc.mode == "endless" & (this.problem <= 2 || this.gc.notebooks != 2)))
            {
                this.num1 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                this.num2 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
                this.sign = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num1)]);
                if (this.sign == 0)
                {
                    this.solution = this.num1 + this.num2;
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n \n",
                        this.num1,
                        "+",
                        this.num2,
                        "="
                    });
                    this.QueueAudio(this.bal_plus);
                }
                else if (this.sign == 1)
                {
                    this.solution = this.num1 - this.num2;
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n \n",
                        this.num1,
                        "-",
                        this.num2,
                        "="
                    });
                    this.QueueAudio(this.bal_minus);
                }
                this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num2)]);
                this.QueueAudio(this.bal_equals);
            }
            else
            {
                this.impossibleMode = true;
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                this.QueueAudio(this.bal_screech);
                if (this.sign == 0)
                {
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                    this.QueueAudio(this.bal_plus);
                    this.QueueAudio(this.bal_screech);
                    this.QueueAudio(this.bal_times);
                    this.QueueAudio(this.bal_screech);
                }
                else if (this.sign == 1)
                {
                    this.questionText.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                    this.QueueAudio(this.bal_divided);
                    this.QueueAudio(this.bal_screech);
                    this.QueueAudio(this.bal_plus);
                    this.QueueAudio(this.bal_screech);
                }
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (this.sign == 0)
                {
                    this.questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                }
                else if (this.sign == 1)
                {
                    this.questionText2.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                }
                this.num1 = UnityEngine.Random.Range(1f, 9999f);
                this.num2 = UnityEngine.Random.Range(1f, 9999f);
                this.num3 = UnityEngine.Random.Range(1f, 9999f);
                this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
                if (this.sign == 0)
                {
                    this.questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n",
                        this.num1,
                        "+(",
                        this.num2,
                        "X",
                        this.num3,
                        "="
                    });
                }
                else if (this.sign == 1)
                {
                    this.questionText3.text = string.Concat(new object[]
                    {
                        "SOLVE MATH Q",
                        this.problem,
                        ": \n (",
                        this.num1,
                        "/",
                        this.num2,
                        ")+",
                        this.num3,
                        "="
                    });
                }
                this.QueueAudio(this.bal_equals);
            }
            this.questionInProgress = true;
        }
        else
        {
            this.endDelay = 5f;
            if (!this.gc.spoopMode)
            {
                this.questionText.text = "WOW! YOU EXIST!";
            }
            else if (this.gc.mode == "endless" & this.problemsWrong <= 0)
            {
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.endlessHintText[num];
            }
            else if (this.gc.mode == "story" & this.problemsWrong >= 3)
            {
                this.questionText.text = "I HEAR MATH THAT BAD";
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
                if (this.baldiScript.isActiveAndEnabled) this.baldiScript.Hear(this.playerPosition, 7f);
                this.gc.failedNotebooks++;
            }
            else
            {
                int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
                this.questionText.text = this.hintText[num2];
                this.questionText2.text = string.Empty;
                this.questionText3.text = string.Empty;
            }
        }
    }

    // Token: 0x06000985 RID: 2437 RVA: 0x00023BB8 File Offset: 0x00021FB8
    public void OKButton()
    {
        this.CheckAnswer();
    }

    // Token: 0x06000986 RID: 2438 RVA: 0x00023BC0 File Offset: 0x00021FC0
    public void CheckAnswer()
    {
        if (this.playerAnswer.text == "31718")
        {
            base.StartCoroutine(this.CheatText("THIS IS WHERE IT ALL BEGAN"));
            SceneManager.LoadSceneAsync("TestRoom");
        }
        else if (this.playerAnswer.text == "53045009")
        {
            base.StartCoroutine(this.CheatText("USE THESE TO STICK TO THE CEILING!"));
            this.gc.Fliparoo();
        }
        if (this.problem <= 3)
        {
            if (this.playerAnswer.text == this.solution.ToString() & !this.impossibleMode)
            {
                this.results[this.problem - 1].texture = this.correct;
                this.baldiAudio.Stop();
                this.ClearAudioQueue();
                int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
                this.QueueAudio(this.bal_praises[num]);
                this.NewProblem();
            }
            else
            {
                this.problemsWrong++;
                this.results[this.problem - 1].texture = this.incorrect;
                if (!this.gc.spoopMode)
                {
                    this.baldiFeed.SetTrigger("angry");
                    this.gc.ActivateSpoopMode();
                }
                if (this.gc.mode == "story")
                {
                    if (this.problem == 3)
                    {
                        this.baldiScript.GetAngry(1f);
                    }
                    else
                    {
                        this.baldiScript.GetTempAngry(0.25f);
                    }
                }
                else
                {
                    this.baldiScript.GetAngry(1f);
                }
                this.ClearAudioQueue();
                this.baldiAudio.Stop();
                this.NewProblem();
            }
        }
    }

    // Token: 0x06000987 RID: 2439 RVA: 0x00023D9F File Offset: 0x0002219F
    private void QueueAudio(AudioClip sound)
    {
        this.audioQueue[this.audioInQueue] = sound;
        this.audioInQueue++;
    }

    // Token: 0x06000988 RID: 2440 RVA: 0x00023DBD File Offset: 0x000221BD
    private void PlayQueue()
    {
        this.baldiAudio.PlayOneShot(this.audioQueue[0]);
        this.UnqueueAudio();
    }

    // Token: 0x06000989 RID: 2441 RVA: 0x00023DD8 File Offset: 0x000221D8
    private void UnqueueAudio()
    {
        for (int i = 1; i < this.audioInQueue; i++)
        {
            this.audioQueue[i - 1] = this.audioQueue[i];
        }
        this.audioInQueue--;
    }

    // Token: 0x0600098A RID: 2442 RVA: 0x00023E1C File Offset: 0x0002221C
    private void ClearAudioQueue()
    {
        this.audioInQueue = 0;
    }

    // Token: 0x0600098B RID: 2443 RVA: 0x00023E28 File Offset: 0x00022228
    private void ExitGame()
    {
        if (this.problemsWrong <= 0 & this.gc.mode == "endless")
        {
            this.baldiScript.GetAngry(-1f);
        }
        this.gc.DeactivateLearningGame(base.gameObject);
    }

    // Token: 0x0600098C RID: 2444 RVA: 0x00023E80 File Offset: 0x00022280
    public void ButtonPress(int value)
    {
        if (value >= 0 & value <= 9)
        {
            this.playerAnswer.text = this.playerAnswer.text + value;
        }
        else if (value == -1)
        {
            this.playerAnswer.text = this.playerAnswer.text + "-";
        }
        else
        {
            this.playerAnswer.text = string.Empty;
        }
    }

    // Token: 0x0600098D RID: 2445 RVA: 0x00023F04 File Offset: 0x00022304
    private IEnumerator CheatText(string text)
    {
        for (; ; )
        {
            this.questionText.text = text;
            this.questionText2.text = string.Empty;
            this.questionText3.text = string.Empty;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    // Token: 0x04000641 RID: 1601
    public GameControllerScript gc;

    // Token: 0x04000642 RID: 1602
    public BaldiScript baldiScript;

    // Token: 0x04000643 RID: 1603
    public Vector3 playerPosition;

    // Token: 0x04000644 RID: 1604
    public GameObject mathGame;

    // Token: 0x04000645 RID: 1605
    public RawImage[] results = new RawImage[3];

    // Token: 0x04000646 RID: 1606
    public Texture correct;

    // Token: 0x04000647 RID: 1607
    public Texture incorrect;

    // Token: 0x04000648 RID: 1608
    public TMP_InputField playerAnswer;

    // Token: 0x04000649 RID: 1609
    public TMP_Text questionText;

    // Token: 0x0400064A RID: 1610
    public TMP_Text questionText2;

    // Token: 0x0400064B RID: 1611
    public TMP_Text questionText3;

    // Token: 0x0400064C RID: 1612
    public Animator baldiFeed;

    // Token: 0x0400064D RID: 1613
    public Transform baldiFeedTransform;

    // Token: 0x0400064E RID: 1614
    public AudioClip bal_plus;

    // Token: 0x0400064F RID: 1615
    public AudioClip bal_minus;

    // Token: 0x04000650 RID: 1616
    public AudioClip bal_times;

    // Token: 0x04000651 RID: 1617
    public AudioClip bal_divided;

    // Token: 0x04000652 RID: 1618
    public AudioClip bal_equals;

    // Token: 0x04000653 RID: 1619
    public AudioClip bal_howto;

    // Token: 0x04000654 RID: 1620
    public AudioClip bal_intro;

    // Token: 0x04000655 RID: 1621
    public AudioClip bal_screech;

    // Token: 0x04000656 RID: 1622
    public AudioClip[] bal_numbers = new AudioClip[10];

    // Token: 0x04000657 RID: 1623
    public AudioClip[] bal_praises = new AudioClip[5];

    // Token: 0x04000658 RID: 1624
    public AudioClip[] bal_problems = new AudioClip[3];

    // Token: 0x04000659 RID: 1625
    public Button firstButton;

    // Token: 0x0400065A RID: 1626
    private float endDelay;

    // Token: 0x0400065B RID: 1627
    private int problem;

    // Token: 0x0400065C RID: 1628
    private int audioInQueue;

    // Token: 0x0400065D RID: 1629
    private float num1;

    // Token: 0x0400065E RID: 1630
    private float num2;

    // Token: 0x0400065F RID: 1631
    private float num3;

    // Token: 0x04000660 RID: 1632
    private int sign;

    // Token: 0x04000661 RID: 1633
    private float solution;

    // Token: 0x04000662 RID: 1634
    private string[] hintText = new string[]
    {
        "I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG",
        "I HEAR EVERY DOOR YOU OPEN"
    };

    // Token: 0x04000663 RID: 1635
    private string[] endlessHintText = new string[]
    {
        "That's more like it...",
        "Keep up the good work or see me after class..."
    };

    // Token: 0x04000664 RID: 1636
    private bool questionInProgress;

    // Token: 0x04000665 RID: 1637
    private bool impossibleMode;

    // Token: 0x04000666 RID: 1638
    private bool joystickEnabled;

    // Token: 0x04000667 RID: 1639
    private int problemsWrong;

    // Token: 0x04000668 RID: 1640
    private AudioClip[] audioQueue = new AudioClip[20];

    // Token: 0x04000669 RID: 1641
    public AudioSource baldiAudio;
}
