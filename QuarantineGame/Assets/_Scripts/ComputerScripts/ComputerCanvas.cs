using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerCanvas : MonoBehaviour
{
    enum RenderModeStates { camera, world };
    RenderModeStates m_RenderModeStates;

    Canvas computerScreen;
    public Text emailText;
    public int currentEmail = 0;

    public bool hasWorked = false;

    public List<string> emailsInInbox = new List<string>();

    public GameObject workPrompt;
    public Text questLogUI;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Vector3 originalScale;
    private RectTransform self;
    private float originalWidth;
    private float originalHeight;

    // Start is called before the first frame update
    void Start()
    {
        computerScreen = GetComponent<Canvas>();
        EventManager.AddEmail += StartAddEmail;
        EventManager.ActivateWorkPrompt += StartActivateWorkPrompt;
        EventManager.RemoveWorkPrompt += StartRemoveWorkPrompt;

        self = transform.GetComponent<RectTransform>();
        originalPos = transform.position;
        originalScale = transform.localScale;
        originalRot = transform.localRotation;
        originalWidth = self.rect.width;
        originalHeight = self.rect.height;
    }

    public void StartAddEmail(string Email)
    {
        emailsInInbox.Add(Email);
        emailText.text = Email;
        currentEmail = emailsInInbox.Count - 1;
    }

    public void StartActivateWorkPrompt()
    {
        workPrompt.SetActive(true);
    }

    public void StartRemoveWorkPrompt()
    {
        workPrompt.SetActive(false);
    }

    public void Work()
    {
        if (questLogUI.text.Contains("Work") && !questLogUI.text.Contains("Work - Completed"))
        {
            questLogUI.text = questLogUI.text.Replace("Work", "Work - Completed");
        }
        workPrompt.SetActive(false);
        startSkipTimeAnim();
        
    }
    public void startSkipTimeAnim()
    {
        StartCoroutine(SkipTimeAnim());
    }

    public IEnumerator SkipTimeAnim()
    {
        EventManager.Blackout();
        yield return new WaitForSecondsRealtime(1.5f);
        EventManager.OnFastForward(180);
        EventManager.BlackoutReverse();
        
    }

    public void ChangeEmail()
    {
        if(emailsInInbox.Count > 0)
        {
            currentEmail += 1;
            //Debug.Log("trying to change email");

            if (currentEmail >= emailsInInbox.Count)
            {
                currentEmail = 0;
            }

            emailText.text = emailsInInbox[currentEmail];
        }
    }

    private void OnDestroy()
    {
        EventManager.AddEmail -= StartAddEmail;
        EventManager.ActivateWorkPrompt -= StartActivateWorkPrompt;
        EventManager.RemoveWorkPrompt -= StartRemoveWorkPrompt;
    }

    public void SitAtComputer()
    {
        EventManager.OnSeize(true);
        if(computerScreen.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            computerScreen.renderMode = RenderMode.ScreenSpaceOverlay;
            computerScreen.sortingOrder = -1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LeaveComputer()
    {
        EventManager.OnSeize(false);
        if (computerScreen.renderMode != RenderMode.WorldSpace)
        {
            computerScreen.renderMode = RenderMode.WorldSpace;
            computerScreen.sortingOrder = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.position = originalPos;
            transform.localScale = originalScale;
            transform.localRotation = originalRot;
            self.sizeDelta = new Vector2(originalWidth, originalHeight);
        }
    }

}
