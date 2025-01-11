using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryItems;
    [SerializeField] private GameObject[] boxItems;
    [SerializeField] private int strikesLeft = 3;
    [SerializeField] private float currentScore = 0;
    [SerializeField] private GameObject currentPerson;
}
