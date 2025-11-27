using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardFace : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    public Element element;
    public int power;
    public float rotationSpeed;
    public float rotationAmount;
    public Image cardback;

    Vector3 rotation;
    Vector3 movement;

    private float randomRot;

    public void EnableBack()
    {
        cardback.gameObject.SetActive(true);
    }

    public bool TryDisableBack()
    {
        if (cardback.gameObject.activeSelf)
        {
            cardback.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    private void Start()
    {
        randomRot = Random.Range(-rotationAmount, rotationAmount);
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * 25);

        if (!target.GetComponent<Card>().Played)
        {
            Vector3 pos = (transform.position - target.transform.position);

            movement = Vector3.Lerp(movement, pos, 25 * Time.deltaTime);

            /*if (target.GetComponent<Card>().IsDragging)
                movementRotation = movement;
            else*/
            Vector3 movementRotation = movement;

            rotation = Vector3.Lerp(rotation, movementRotation, rotationSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                Mathf.Clamp(movementRotation.x, -rotationAmount, rotationAmount));
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, randomRot);
        }
    }
}