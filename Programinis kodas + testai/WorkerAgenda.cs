using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WorkerAgenda : MonoBehaviour {

    public GameObject EditPanel;
    public GameObject ShowPanel;
    public GameObject UpperPanel;
    public GameObject LowerPanel;

    public TMP_Text mon;
    public TMP_Text tue;
    public TMP_Text wed;
    public TMP_Text thu;
    public TMP_Text fri;
    public TMP_Text sat;
    public TMP_Text sun;

    public TMP_InputField mon1, mon2;
    public TMP_InputField tue1, tue2;
    public TMP_InputField wed1, wed2;
    public TMP_InputField thu1, thu2;
    public TMP_InputField fri1, fri2;
    public TMP_InputField sat1, sat2;
    public TMP_InputField sun1, sun2;

    public Toggle MonCheckbox;
    public Toggle TueCheckbox;
    public Toggle WedCheckbox;
    public Toggle ThuCheckbox;
    public Toggle FriCheckbox;
    public Toggle SatCheckbox;
    public Toggle SunCheckbox;

    string inactiveDay = "Nedirba";
   
    public void checkboxChanged() {

        if(MonCheckbox.isOn) {
            mon1.interactable = true;
            mon2.interactable = true;
        }
        else {
            mon1.interactable = false;
            mon2.interactable = false;
        }
        if (TueCheckbox.isOn) {
            tue1.interactable = true;
            tue2.interactable = true;
        }
        else {
            tue1.interactable = false;
            tue2.interactable = false;
        }
        if (WedCheckbox.isOn) {
            wed1.interactable = true;
            wed2.interactable = true;
        }
        else {
            wed1.interactable = false;
            wed2.interactable = false;
        }
        if (ThuCheckbox.isOn) {
            thu1.interactable = true;
            thu2.interactable = true;
        }
        else {
            thu1.interactable = false;
            thu2.interactable = false;
        }
        if (FriCheckbox.isOn) {
            fri1.interactable = true;
            fri2.interactable = true;
        }
        else {
            fri1.interactable = false;
            fri2.interactable = false;
        }
        if (SatCheckbox.isOn) {
            sat1.interactable = true;
            sat2.interactable = true;
        }
        else {
            sat1.interactable = false;
            sat2.interactable = false;
        }
        if (SunCheckbox.isOn) {
            sun1.interactable = true;
            sun2.interactable = true;
        }
        else {
            sun1.interactable = false;
            sun2.interactable = false;
        }
    }
    
    public void loadShedule() {
        GameObject.Find("FirebaseManager").GetComponent<DatabaseManager>().GetWorkerShedule(this.gameObject);
    }
     
    public void loadSheduleToEdit() {

        if (mon.text != inactiveDay) {
            string[] tempArray = mon.text.Split(char.Parse("-"));
            mon1.text = tempArray[0].Replace(" ", "");
            mon2.text = tempArray[1].Replace(" ", "");
            MonCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            mon1.text = "";
            mon2.text = "";
            MonCheckbox.SetIsOnWithoutNotify(false);
        }

        if (tue.text != inactiveDay) {
            string[] tempArray = tue.text.Split(char.Parse("-"));
            tue1.text = tempArray[0].Replace(" ", "");
            tue2.text = tempArray[1].Replace(" ", "");
            TueCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            tue1.text = "";
            tue2.text = "";
            TueCheckbox.SetIsOnWithoutNotify(false);
        }

        if (wed.text != inactiveDay) {
            string[] tempArray = wed.text.Split(char.Parse("-"));
            wed1.text = tempArray[0].Replace(" ", "");
            wed2.text = tempArray[1].Replace(" ", "");
            WedCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            wed1.text = "";
            wed2.text = "";
            WedCheckbox.SetIsOnWithoutNotify(false);
        }

        if (thu.text != inactiveDay) {
            string[] tempArray = thu.text.Split(char.Parse("-"));
            thu1.text = tempArray[0].Replace(" ", "");
            thu2.text = tempArray[1].Replace(" ", "");
            ThuCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            thu1.text = "";
            thu2.text = "";
            ThuCheckbox.SetIsOnWithoutNotify(false);
        }

        if (fri.text != inactiveDay) {
            string[] tempArray = fri.text.Split(char.Parse("-"));
            fri1.text = tempArray[0].Replace(" ", "");
            fri2.text = tempArray[1].Replace(" ", "");
            FriCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            fri1.text = "";
            fri2.text = "";
            FriCheckbox.SetIsOnWithoutNotify(false);
        }

        if (sat.text != inactiveDay) {
            string[] tempArray = sat.text.Split(char.Parse("-"));
            sat1.text = tempArray[0].Replace(" ", "");
            sat2.text = tempArray[1].Replace(" ", "");
            SatCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            sat1.text = "";
            sat2.text = "";
            SatCheckbox.SetIsOnWithoutNotify(false);
        }

        if (sun.text != inactiveDay) {
            string[] tempArray = sun.text.Split(char.Parse("-"));
            sun1.text = tempArray[0].Replace(" ", "");
            sun2.text = tempArray[1].Replace(" ", "");
            SunCheckbox.SetIsOnWithoutNotify(true);
        }
        else {
            sun1.text = "";
            sun2.text = "";
            SunCheckbox.SetIsOnWithoutNotify(false);
        }

        checkboxChanged();
    }

    public void saveShedule() {

        if (mon1.text != "" && mon2.text != "" && MonCheckbox.isOn) {
            mon.text = mon1.text + " - " + mon2.text;
        }
        else {
            MonCheckbox.SetIsOnWithoutNotify(false);
            mon.text = inactiveDay;
        }

        if (tue1.text != "" && tue2.text != "" && TueCheckbox.isOn) {
            tue.text = tue1.text + " - " + tue2.text;
        }
        else {
            TueCheckbox.SetIsOnWithoutNotify(false);
            tue.text = inactiveDay;
        }

        if (wed1.text != "" && wed2.text != "" && WedCheckbox.isOn) {
            wed.text = wed1.text + " - " + wed2.text;
        }
        else {
            WedCheckbox.SetIsOnWithoutNotify(false);
            wed.text = inactiveDay;
        }

        if (thu1.text != "" && thu2.text != "" && ThuCheckbox.isOn) {
            thu.text = thu1.text + " - " + thu2.text;
        }
        else {
            ThuCheckbox.SetIsOnWithoutNotify(false);
            thu.text = inactiveDay;
        }

        if (fri1.text != "" && fri2.text != "" && FriCheckbox.isOn) {
            fri.text = fri1.text + " - " + fri2.text;
        }
        else {
            FriCheckbox.SetIsOnWithoutNotify(false);
            fri.text = inactiveDay;
        }

        if (sat1.text != "" && sat2.text != "" && SatCheckbox.isOn) {
            sat.text = sat1.text + " - " + sat2.text;
        }
        else {
            SatCheckbox.SetIsOnWithoutNotify(false);
            sat.text = inactiveDay;
        }

        if (sun1.text != "" && sun2.text != "" && SunCheckbox.isOn) {
            sun.text = sun1.text + " - " + sun2.text;
        }
        else {
            SunCheckbox.SetIsOnWithoutNotify(false);
            sun.text = inactiveDay;
        }

        GameObject.Find("FirebaseManager").GetComponent<DatabaseManager>().SetWorkerShedule(mon.text, tue.text, wed.text, thu.text, fri.text, sat.text, sun.text);

        ShowPanel.SetActive(true);
        EditPanel.SetActive(false);
        LowerPanel.SetActive(true);
        UpperPanel.SetActive(true);
    }
}
