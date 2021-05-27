using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimePicker : MonoBehaviour {

    public TMP_Dropdown h, m;
    public GameObject selectedObject;
    public int oldH;
    public int oldM;
    
    public void getObject(GameObject obj) {

        this.gameObject.SetActive(true);
        selectedObject = obj;
        string tempH = selectedObject.GetComponent<TMP_InputField>().text;

        if (tempH != "" && tempH != "-") {
            string[] tempHArray = tempH.Split(char.Parse(":"));
            oldH = int.Parse(tempHArray[0]);
            oldM = int.Parse(tempHArray[1]);

            int minutesId = oldM * 12 / 60;
            int hoursId;
            if (oldH > 4) hoursId = oldH - 5;
            else hoursId = oldH - 5 + 24;

            h.SetValueWithoutNotify(hoursId);
            m.SetValueWithoutNotify(minutesId);
        }
        else {
            h.SetValueWithoutNotify(0);
            m.SetValueWithoutNotify(0);
        }
    }
    public void save() {
        
        string hour = h.options[h.value].text;
        string min = m.options[m.value].text;
        selectedObject.GetComponent<TMP_InputField>().text = hour + ":" + min;
        this.gameObject.SetActive(false);
    }
}
