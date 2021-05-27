using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobCreateManager : MonoBehaviour {

    public GameObject firebaseManager;

    [Header("Create New Job")]
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

    public void OnSaveButton() {

        string title = jobTitle.text;
        string price = jobPrice.text;
        string pricePer = jobPricePer.options[jobPricePer.value].text;
        string city = jobCity.text;
        string duration = jobDuration.text;
        string durationPer = jobDurationPer.options[jobDurationPer.value].text;
        float  status = jobStatus.value;
        string category = jobCategory.options[jobCategory.value].text;
        string subcategory = jobSubcategory.options[jobSubcategory.value].text;
        string about = aboutJob.text;
        string tags = jobTags.text;
        
        firebaseManager.GetComponent<DatabaseManager>().RegisterServiceButton(title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, tags);
        this.gameObject.SetActive(false);
    }
}
