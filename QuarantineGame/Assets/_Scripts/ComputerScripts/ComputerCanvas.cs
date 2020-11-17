using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerCanvas : MonoBehaviour
{
    Canvas computerScreen;
    public Text emailText;
    public int currentEmail;

    public List<string> emailsInInbox = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        computerScreen = GetComponent<Canvas>();
        EventManager.AddEmail += StartAddEmail;
    }

    public void StartAddEmail(string Email)
    {
        emailsInInbox.Add(Email);
        emailText.text = Email;
    }

    public void ChangeEmail()
    {
        Debug.Log("trying to change email");
        for (int i = 0; i < emailsInInbox.Count; i++)
        {
            currentEmail = i;
            emailsInInbox[i] = emailText.text; // is this bad?
        }
    }

    private void OnDestroy()
    {
        EventManager.AddEmail -= StartAddEmail;
    }
}
