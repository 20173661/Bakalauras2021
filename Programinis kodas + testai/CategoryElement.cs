using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryElement : MonoBehaviour {
   
    public GameObject category;
    public GameObject subcategory;
    public int status = 0;

    public void OnPress() {

        if (status == 0) {
            subcategory.SetActive(true);
            status = 1;
        }
        else {
            subcategory.SetActive(false);
            status = 0;
        }
    }
}
