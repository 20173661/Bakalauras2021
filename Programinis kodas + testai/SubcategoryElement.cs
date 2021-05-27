using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubcategoryElement : MonoBehaviour {

    public TMP_Text buttonName;
    public void OnPress() {

        GameObject.Find("FirebaseManager").GetComponent<DatabaseManager>().LoadJobByCategory(buttonName.text);
    }
}
