using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobEditor : MonoBehaviour {

    public GameObject firebaseManager;
    public GameObject JobsList;

    [Header("Job Elements")]
    public TMP_InputField jobTitle;
    public TMP_InputField jobPrice;
    public TMP_Dropdown jobPricePer;
    public TMP_InputField jobCity;
    public TMP_InputField jobDuration;
    public TMP_Dropdown jobDurationPer;
    public Slider jobStatus;
    public TMP_Dropdown jobCategory;
    public TMP_Dropdown jobSubcategory;
    public TMP_InputField aboutJob;
    public TMP_InputField jobTags;
    public string serviceId;
    public void LoadInfo(string id, string title, string price, string pricePer, string city, string duration, string durationPer, string status, string category, string subcategory, string about, string tags) {

        firebaseManager.GetComponent<DatabaseManager>().GetCategoriesFromDBButton();
        serviceId = id;
        jobTitle.text = title;
        jobPrice.text = price;
        jobCity.text = city;
        jobDuration.text = duration;
        aboutJob.text = about;
        jobTags.text = tags;
        jobStatus.value = int.Parse(status);
        jobCategory.value = jobCategory.options.FindIndex(option => option.text == category);
        firebaseManager.GetComponent<DatabaseManager>().GetEditSubCategoriesFromDBButton(category);
        jobDurationPer.value = jobDurationPer.options.FindIndex(option => option.text == durationPer);
        jobPricePer.value = jobPricePer.options.FindIndex(option => option.text == pricePer);
        jobSubcategory.value = jobSubcategory.options.FindIndex(option => option.text == subcategory);
        this.gameObject.SetActive(true);
    }
    public void OnDeleteButton() {

        firebaseManager.GetComponent<DatabaseManager>().DeleteServiceButton(serviceId);
        this.gameObject.SetActive(false);
        JobsList.SetActive(true);
    }
    public void OnSaveButton() {

        string id = serviceId;
        string title = jobTitle.text;
        string price = jobPrice.text;
        string pricePer = jobPricePer.options[jobPricePer.value].text;
        string city = jobCity.text;
        string duration = jobDuration.text;
        string durationPer = jobDurationPer.options[jobDurationPer.value].text;
        float status = jobStatus.value;
        string category = jobCategory.options[jobCategory.value].text;
        string subcategory = jobSubcategory.options[jobSubcategory.value].text;
        string about = aboutJob.text;
        string tags = jobTags.text;

        firebaseManager.GetComponent<DatabaseManager>().EditServiceButton(id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, tags);

        this.gameObject.SetActive(false);
        JobsList.SetActive(true);
    }
}
