using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePopUp : MonoBehaviour
{



    public GameObject messageObject;
    public BillBoard messageBillBoard;
    public TextMeshProUGUI text;

    public Vector3 fullScale = Vector3.one;
    public float scalingTime = 0.2f;

    bool messageUp = false;

    public List<Message> queuedMessages = new List<Message>();

    public void PopMessage(string text ="No Text Sent", bool quable = false, float messageDuration = 2)
    {
        if(messageUp)
        {
            if (quable)
            {
                Message msg;
                msg.message = text;
                msg.sentTime = Time.time ;
                msg.duration = messageDuration;

                queuedMessages.Add(msg);
            }
            return;
        }
        messageUp = true;

        messageObject.transform.localScale = Vector3.zero;
        messageObject.SetActive(true);
        this.text.text = text;

        messageObject.transform.LeanScale(fullScale, scalingTime).setOnComplete(()=> 
        {
            StartCoroutine(WaitThenDo(messageDuration, () => 
            {
                messageObject.transform.LeanScale(Vector3.zero, scalingTime).setOnComplete(() =>
                {
                    messageObject.SetActive(false);

                    //Check Queued Messages
                    StartCoroutine(WaitThenDo(1, () =>
                    {
                        messageUp = false;
                        GetQueueMessage();

                    }));

                }); ;
            }));
        });

    }

    private void GetQueueMessage()
    {
        while (queuedMessages.Count > 0 && messageUp == false)
        {
            if (Time.time - queuedMessages[0].sentTime < 5)
            {
                //Send Unexpired message
                PopMessage(queuedMessages[0].message, false, queuedMessages[0].duration);
                queuedMessages.RemoveAt(0);
                break;
            }
            else
            {
                Debug.Log("Message expired");
                queuedMessages.RemoveAt(0);
            }
        }
    }

    public void CancelMessage(bool playQueued = false)
    {
        StopAllCoroutines();
        LeanTween.cancel(messageObject);
        messageObject.transform.localScale = Vector3.zero;
        messageObject.SetActive(false);
        messageUp = false;
        if (playQueued) GetQueueMessage();
    }
    IEnumerator WaitThenDo(float wait, System.Action callbackDo)
    {
        yield return new WaitForSeconds(wait);

        callbackDo?.Invoke();
    }


    public struct Message 
    {
        public string message;
        public float sentTime;
        public float duration;
    }
}
