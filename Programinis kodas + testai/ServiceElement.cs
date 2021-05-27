using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServiceElement : MonoBehaviour {

    public string workerID;
    string id;
    string title;
    string price;
    string pricePer;
    string city;
    string duration;
    string durationPer;
    string status;
    string category;
    string subcategory;
    string about;
    string jobTags;
    string rating;
    string views;
    string reviews;

    string inactiveDay;
    public string mon;
    public string tue;
    public string wed;
    public string thu;
    public string fri;
    public string sat;
    public string sun;

    public List<string> monH;
    public List<string> tueH;
    public List<string> wedH;
    public List<string> thuH;
    public List<string> friH;
    public List<string> satH;
    public List<string> sunH;

    [Header("Job Elements")]
    public TMP_Text jobTitle;
    public TMP_Text jobStatus;
    public TMP_Text jobPrice;
    public TMP_Text jobRating;
    public TMP_Text jobSeen;
    public TMP_Text jobCity;
    public TMP_Text jobAbout;
    public TMP_Text jobReviews;
    public TMP_Text jobDuration;
    GameObject firebaseManager;

    [Header("Panels")]
    public GameObject orderPanel;
    public GameObject backToService;
    public GameObject backToFavourites;

    public void NewJobElement(string _workerID, string _id, string _title, string _price, string _pricePer, string _city, string _duration, string _durationPer, string _status, string _category,
        string _subcategory, string _about, string _jobTags, string _rating, string _views, string _reviews) {

        workerID = _workerID;
        id = _id;
        title = _title;
        price = _price;
        pricePer = _pricePer;
        city = _city;
        duration = _duration;
        durationPer = _durationPer;
        status = _status;
        category = _category;
        subcategory = _subcategory;
        about = _about;
        jobTags = _jobTags;
        rating = _rating;
        views = _views;
        reviews = _reviews;

        jobTitle.text = title;
        if (status == "0") jobStatus.text = "Statusas: Privatus";
        else if (status == "1") jobStatus.text = "Statusas: Viešas";
        jobPrice.text = "Kaina: " + price + " € " + pricePer;
        jobRating.text = "Įvertinimas: " + rating + "/5";
        jobSeen.text = "Peržiūros: " + views;
        jobCity.text = "Miestas: " + city;
        jobAbout.text = "Apie paslaugą: " + about;
        jobReviews.text = "Atsiliepimai: " + reviews;
        jobDuration.text = "Trukmė: " + duration + " " + durationPer;
        inactiveDay = "Nedirba";
        firebaseManager = GameObject.Find("FirebaseManager");

        GetWorkerShedule();

    }

    public void GetWorkerShedule() {

        firebaseManager.GetComponent<DatabaseManager>().GetWorkerSheduleById(this.gameObject);
    }

    public void selectedDate(string date) {

        monH.Clear();
        tueH.Clear();
        wedH.Clear();
        thuH.Clear();
        friH.Clear();
        satH.Clear();
        sunH.Clear();

        string[] dateArray = date.Split(char.Parse(":"));
        string year = dateArray[0];
        string month = dateArray[1];
        string day = dateArray[2];

        DateTime time = Convert.ToDateTime(year + "/" + month + "/" + day);
        Debug.Log("" + time.DayOfWeek.ToString());

        if (time.DayOfWeek.ToString() == "Monday") {
            if (mon != inactiveDay) {
                string[] tempArray = mon.Split(char.Parse("-"));
                string[] tempArray2 =  tempArray[0].Split(char.Parse(":"));
                Debug.Log("" + tempArray2[0].Replace(" ", ""));
                int startH = int.Parse(tempArray2[0].Replace(" ",""));
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0].Replace(" ", ""));

                for(int i=startH; i<endH; i++) monH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(monH);
        }
        else if (time.DayOfWeek.ToString() == "Tuesday") {
            if (tue != inactiveDay) {
                string[] tempArray = tue.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) tueH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(tueH);
        }
        else if (time.DayOfWeek.ToString() == "Wednesday") {
            if (wed != inactiveDay) {
                string[] tempArray = wed.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) wedH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(wedH);
        }
        else if (time.DayOfWeek.ToString() == "Thursday") {
            if (thu != inactiveDay) {
                string[] tempArray = thu.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) thuH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(thuH);
        }
        else if (time.DayOfWeek.ToString() == "Friday") {
            if (fri != inactiveDay) {
                string[] tempArray = fri.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) friH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(friH);
        }
        else if (time.DayOfWeek.ToString() == "Saturday") {
            if (sat != inactiveDay) {
                string[] tempArray = sat.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) satH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(satH);
        }
        else if (time.DayOfWeek.ToString() == "Sunday") {
            if (sun != inactiveDay) {
                string[] tempArray = sun.Split(char.Parse("-"));
                string[] tempArray2 = tempArray[0].Split(char.Parse(":"));
                int startH = int.Parse(tempArray2[0]);
                string[] tempArray3 = tempArray[1].Split(char.Parse(":"));
                int endH = int.Parse(tempArray3[0]);

                for (int i = startH; i < endH; i++) sunH.Add(i + "");
            }
            orderPanel.GetComponent<OrderUI>().UpdateDrowpdown(sunH);
        }
    }
    public void OnPress() {

        orderPanel.GetComponent<OrderUI>().NewOrderElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
        firebaseManager.GetComponent<DatabaseManager>().GetClientDataForOrder(orderPanel);
        firebaseManager.GetComponent<DatabaseManager>().GetWorkerDataForOrder(orderPanel, workerID);
        this.gameObject.SetActive(false);
        orderPanel.SetActive(true);
    }
}
