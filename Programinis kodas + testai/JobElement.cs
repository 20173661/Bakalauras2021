using TMPro;
using UnityEngine;

public class JobElement : MonoBehaviour {
    
    string workerID;
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
    GameObject firebaseManager;

    [Header("Panels")]
    GameObject serviceInfoPanel;
    GameObject serviceListPanel;
    GameObject serviceEditPanel;

    [Header("Job Elements")]
    public TMP_Text jobTitle;
    public TMP_Text jobStatus;
    public TMP_Text jobPrice;
    public TMP_Text jobRating;
    public TMP_Text jobSeen;
    public TMP_Text jobReviews;
    public TMP_Text jobDuration;

    public void NewJobElement (string _workerID, string _id, string _title, string _price, string _pricePer, string _city, string _duration, string _durationPer, string _status, string _category,
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
        if(status == "0") jobStatus.text = "Statusas: Privatus";
        else if (status == "1") jobStatus.text = "Statusas: Viešas";
        jobPrice.text = "Kaina: "+ price + " € " + pricePer;
        jobRating.text = "Įvertinimas: " + rating + "/5";
        jobSeen.text = "Peržiūros: " + views;
        jobReviews.text = "Atsiliepimai: " + reviews;
        jobDuration.text = "Trukmė: " + duration + " " + durationPer;
        firebaseManager = GameObject.Find("FirebaseManager");

        serviceInfoPanel = firebaseManager.GetComponent<DatabaseManager>().ClientServiceInfoPanel;
        serviceListPanel = firebaseManager.GetComponent<DatabaseManager>().ClientServiceListPanel;
        serviceEditPanel = firebaseManager.GetComponent<DatabaseManager>().serviceEditPanel;
    }

    public void ClientOnPressFav() { //from favourites

        serviceInfoPanel.GetComponent<ServiceElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
        serviceInfoPanel.SetActive(true);
        serviceInfoPanel.GetComponent<ServiceElement>().backToService.SetActive(false);
        serviceInfoPanel.GetComponent<ServiceElement>().backToFavourites.SetActive(true);
        serviceListPanel.SetActive(false);
    }

    public void ClientOnPress() { //from services
    
        serviceInfoPanel.GetComponent<ServiceElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
        serviceInfoPanel.SetActive(true);
        serviceInfoPanel.GetComponent<ServiceElement>().backToService.SetActive(true);
        serviceInfoPanel.GetComponent<ServiceElement>().backToFavourites.SetActive(false);
        serviceListPanel.SetActive(false);
    }
    public void ClientAddFavourite() {

        firebaseManager.GetComponent<DatabaseManager>().AddFavourite(id);
    }
    public void ClientRemoveFavourite() {

        firebaseManager.GetComponent<DatabaseManager>().RemoveFavorite(id);     
    }
    public void WorkerOnPress() {

        serviceInfoPanel.GetComponent<ServiceElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
        serviceInfoPanel.SetActive(true);
        serviceListPanel.SetActive(false);
    }
    public void EditOnPress() {

        serviceEditPanel.GetComponent<JobEditor>().LoadInfo(id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags);
        serviceEditPanel.SetActive(true);
        serviceListPanel.SetActive(false);
    }
}
