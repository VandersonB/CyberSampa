using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInput : MonoBehaviour
{
    public GameObject menuPhone;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            menuPhone.SetActive(true);
        }
    }
}
