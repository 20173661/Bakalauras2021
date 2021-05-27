using UnityEngine;
using TMPro;
using System;

public class OrderInfo : MonoBehaviour {

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
    string orderTime;
    string additionalInfo;
    GameObject firebaseManager;

    [Header("Order Elements")]
    public TMP_Text orderTitle;
    public TMP_Text orderDuration;
    public TMP_Text orderPrice;
    public TMP_Text workerNames;
    public TMP_Text clientNames;
    public TMP_Text orderDate;
    public TMP_Text orderCity;
    public TMP_Text orderAdress;
    public TMP_Text wNumber;
    public TMP_Text cNumber;
    public TMP_Text orderStatus;

    [Header("Panels")]
    public GameObject approvePanel;
    public GameObject CompletePanel;
    public GameObject CancelPanel;

    public bool client;

    public void NewOrderElement(string _workerID, string _id, string _title, string _price, string _pricePer, string _workercity, string _duration, string _durationPer, string _status, string _category,
        string _subcategory, string _about, string _jobTags, string _rating, string _views, string _reviews, string _clientID, string _clientNameSurname, string _clientNumber, string _clientAdress,
        string _clientCity, string _clientOrderDate, string _workerNameSurname, string _workerNumber, string _orderTime, string _additionalInfo) {

        workerID = _workerID;
        id = _id;
        title = _title;
        price = _price;
        pricePer = _pricePer;
        workerCity = _workercity;
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

        clientID = _clientID;
        clientNameSurname = _clientNameSurname;
        clientNumber = _clientNumber;
        clientAdress = _clientAdress;
        clientCity = _clientCity;
        clientOrderDate = _clientOrderDate;
        workerNameSurname = _workerNameSurname;
        workerNumber = _workerNumber;
        orderTime = _orderTime;
        additionalInfo = _additionalInfo;

        orderTitle.text = title;
        orderDuration.text = "Trukmė: " + duration + " " + durationPer;
        orderPrice.text = "Kaina: " + price + " € " + pricePer;
        workerNames.text = "Darbuotojas: " + workerNameSurname;
        clientNames.text = "Klientas: " + clientNameSurname;
        orderDate.text = "Užsakymo data: " + clientOrderDate + "  " + orderTime;
        wNumber.text = "Darbuotojo numeris: " + workerNumber;
        cNumber.text = "Kliento numeris: " + clientNumber;
        orderCity.text = "Užsakymo miestas: " + clientCity;
        orderAdress.text = "Užsakymo adresas: " + clientAdress;
        firebaseManager = GameObject.Find("FirebaseManager");

        refreshOrderStatus();
    }

    public void refreshOrderStatus() {

        if (client) {
            if (status == "0") {
                orderStatus.text = "Statusas: Laukiama patvirtinimo";
                approvePanel.SetActive(false);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(true);
            }
            else if (status == "1") {
                orderStatus.text = "Statusas: Užsakymas patvirtintas";
                approvePanel.SetActive(false);
                string tempDate = clientOrderDate.Replace(":", "/");
                DateTime time = Convert.ToDateTime(tempDate);

                if (time <= DateTime.Now) {
                    CompletePanel.SetActive(false);
                    CancelPanel.SetActive(false);
                }
                else {
                    CancelPanel.SetActive(true);
                    CompletePanel.SetActive(false);
                }
            }
            else if (status == "2") {
                orderStatus.text = "Statusas: Užsakymas atšauktas";
                approvePanel.SetActive(false);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(false);
            }
            else if (status == "3") {
                orderStatus.text = "Statusas: Užsakymas įvykdytas";
                approvePanel.SetActive(false);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(false);
            }
        }
        else { // worker
            if (status == "0") {
                orderStatus.text = "Statusas: Laukiama patvirtinimo";
                approvePanel.SetActive(true);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(false);
            }
            else if (status == "1") {
                orderStatus.text = "Statusas: Užsakymas patvirtintas";
                approvePanel.SetActive(false);
                string tempDate = clientOrderDate.Replace(":", "/");
                DateTime time = Convert.ToDateTime(tempDate);

                if (time <= DateTime.Now) {
                    CompletePanel.SetActive(true);
                    CancelPanel.SetActive(false);
                }
                else {
                    CancelPanel.SetActive(true);
                    CompletePanel.SetActive(false);
                }
            }
            else if (status == "2") {
                orderStatus.text = "Statusas: Užsakymas atšauktas";
                approvePanel.SetActive(false);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(false);
            }
            else if (status == "3") {
                orderStatus.text = "Statusas: Užsakymas įvykdytas";
                approvePanel.SetActive(false);
                CompletePanel.SetActive(false);
                CancelPanel.SetActive(false);
            }
        }
    }

    public void cancelOrder() {
        changeStatus(2);
    }
    public void approveOrder() {
        changeStatus(1);
    }
    public void completeOrder() {
        changeStatus(3);
    }
    public void changeStatus(int newStatus) {
        status = newStatus.ToString();
        firebaseManager.GetComponent<DatabaseManager>().UpdateOrderStatus(id, newStatus);
        refreshOrderStatus();
    }
}
