using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PickUpManager : MonoBehaviour
{
    public GameObject messagePrefab;

    public float maxLifeTime;
    public float smoothTime, minDstToFirstPos;
    public float sizeSmoothTime, minSizeToDestroy;

    public List<NotificationData> activeNotifications = new();
    public Transform spawnPos;
    public Transform[] possiblePositions;


    void Update()
    {
        if (activeNotifications.Count > 0) UpdateNotifications();
    }

    void UpdateNotifications()
    {
        for (int i = 0; i < activeNotifications.Count; i++)
        {
            var current = activeNotifications[i];

            current.lifeTime += Time.deltaTime;

            if (current.transform.localScale.x <= minSizeToDestroy)
            {
                Destroy(current.transform.gameObject);
                activeNotifications.Remove(current);

                continue;
            }

            if (current.lifeTime >= maxLifeTime)
            {
                current.transform.localScale = Vector3.SmoothDamp(current.transform.localScale, Vector3.zero, ref current.scaleVelocity, sizeSmoothTime);
            }

            if (current.arrivedAtStart)
            {
                int posIndex = i;
                if (posIndex >= possiblePositions.Length) posIndex = possiblePositions.Length - 1;
                Vector3 targetPos = possiblePositions[posIndex].position;
                current.transform.position = Vector3.SmoothDamp(current.transform.position, targetPos, ref current.velocity, smoothTime);

                continue;
            }
            else
            {
                current.transform.position = Vector3.SmoothDamp(current.transform.position, possiblePositions[0].position, ref current.velocity, smoothTime);
            }

            if (!current.arrivedAtStart && Vector3.Distance(current.transform.position, possiblePositions[0].position) < minDstToFirstPos)
            {
                current.arrivedAtStart = true;
            }

        }
    }

    public void AddNotification(string name, int amount, Sprite sprite)
    {
        var obj = Instantiate(messagePrefab, spawnPos.position, Quaternion.identity, spawnPos).transform;
        var notification = new NotificationData(obj, amount, name);
        activeNotifications.Insert(0, notification);

        obj.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"   +{amount} {name}";
        obj.GetChild(1).GetComponent<Image>().sprite = sprite;
    }    
    
    public void AddNotification2(string name, int amount, Sprite sprite)
    {
        var obj = Instantiate(messagePrefab, spawnPos.position, Quaternion.identity, spawnPos).transform;
        var notification = new NotificationData(obj, amount, name);
        activeNotifications.Insert(0, notification);

        obj.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"   -{amount} {name}";
        obj.GetChild(1).GetComponent<Image>().sprite = sprite;

        obj.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        obj.GetChild(1).GetComponent<Image>().color = Color.red;
    }

    [System.Serializable]
    public class NotificationData
    {
        public float lifeTime;
        public Transform transform;
        public int amount;
        public string itemName;
        public bool arrivedAtStart;
        public Vector3 velocity;
        public Vector3 scaleVelocity;

        public NotificationData(Transform obj, int amount, string itemName)
        {
            this.lifeTime = 0;
            this.transform = obj;
            this.amount = amount;
            this.itemName = itemName;
            arrivedAtStart = false;
            velocity = Vector3.zero;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PickUpManager))]
[CanEditMultipleObjects]
public class NotificationEditor : Editor
{
    PickUpManager manager;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        if (!Application.isPlaying) return;
        manager = target as PickUpManager;
        if (GUILayout.Button("Test"))
        {
            manager.AddNotification("Wood", 3, null);
        }
    }
}
#endif