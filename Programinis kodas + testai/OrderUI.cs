using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderUI : MonoBehaviour {

    string workerID;
    string id;
    string title;
    string price;
    string pricePer;
    string workerCity;
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
    string clientID;
    string clientNameSurname;
    string clientNumber;
    string clientAdress;
    string clientCity;
    string clientOrderDate;
    string workerNameSurname;
    string workerNumber;
    GameObject firebaseManager;

    [Header("Job Elements")]
    public TMP_InputField nameSurname;
    public TMP_InputField number;
    public TMP_InputField workerNames;
    public TMP_InputField adress;
    public TMP_InputField city;
    public TMP_InputField date;
    public TMP_InputField time;
    public TMP_InputField addInfo;
    public TMP_Dropdown hour;
    public TMP_Dropdown min;

    public void NewOrderElement(string _workerID, string _id, string _title, string _price, string _pricePer, string _city, string _duration, string _durationPer, string _status, string _category,
        string _subcategory, string _about, string _jobTags, string _rating, string _views, string _reviews) {

        workerID = _workerID;
        id = _id;
        title = _title;
        price = _price;
        pricePer = _pricePer;
        workerCity = _city;
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
        date.text = DateTime.Now.ToString("yyyy:MM:dd");
        firebaseManager = GameObject.Find("FirebaseManager");
    }

    public void OrderInfo(string _clientID, string _clientNameSurname, string _clientNumber) {

        clientID = _clientID;
        clientNameSurname = _clientNameSurname;
        clientNumber = _clientNumber;
        
        nameSurname.text = clientNameSurname;
        number.text = clientNumber;
    }
    public void WorkerInfo(string _workerNameSurname, string _workerNumber) {

        workerNameSurname = _workerNameSurname;
        workerNumber = _workerNumber;

        workerNames.text = workerNameSurname;
    }

      public void CreateOrder() {

          clientCity = city.text;
          clientAdress = adress.text;
          clientOrderDate = date.text;
          firebaseManager.GetComponent<DatabaseManager>().CreateClientOrder(workerID, id, title, price, pricePer, workerCity, duration, durationPer, status, category, subcategory, about, jobTags,
             rating, views, reviews, clientID, clientNameSurname, clientNumber, clientAdress, clientCity, clientOrderDate, workerNameSurname, workerNumber, time.text, addInfo.text);
      }

    public void UpdateDrowpdown(List<string> hours) {

        hour.ClearOptions();
        min.ClearOptions();
        hour.AddOptions(hours);
        min.AddOptions(new List<string> { "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55" });
    }
}
