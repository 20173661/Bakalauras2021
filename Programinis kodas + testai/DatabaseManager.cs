using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class DatabaseManager : MonoBehaviour {

    //Firebase variables
    [Header("Database")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser LoggedInUser;
    public DatabaseReference database;

    //Login variables
    [Header("Login Worker")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text errorText;
    public TMP_Text successText;

    //Register variables
    [Header("Register Worker")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_InputField workerNameRegisterField;
    public TMP_InputField workerSurnameRegisterField;
    public TMP_InputField workerIDRegisterField;
    public TMP_InputField workerPhoneRegisterField;
    public TMP_Text errorRegisterText;

    [Header("Register Client")]
    public TMP_InputField clientEmailRegisterField;
    public TMP_InputField clientNameRegisterField;
    public TMP_InputField clientSurnameRegisterField;

    [Header("Login Client")]
    [SerializeField] InputField phoneNumber;
    [SerializeField] InputField CountryCode;
    private uint TimeoutTime = 90000; // time in ms
    PhoneAuthProvider provider;
    private string VerificationId;[SerializeField] Text debug;
    [SerializeField] InputField otp;

    [Header("Job Categories")]
    public GameObject categoryElement;
    public TMP_Dropdown jobCategory;
    public TMP_Dropdown jobSubcategory;
    public TMP_Dropdown EditjobCategory;
    public TMP_Dropdown EditjobSubcategory;
    public List<string> categories = new List<string>();
    public List<string> subcategories = new List<string>();

    [Header("Worker Job List Element")]
    public GameObject jobElement;
    public Transform jobContent;

    [Header("Client Job List Element")]
    public GameObject jobListElement;
    public Transform jobListContent;

    [Header("Cat Serv Opem UI")]
    public GameObject categoryPanel;
    public GameObject servicePanel;
    public GameObject orderPanel;
    public GameObject favouritesPanel;

    [Header("Service Element")]
    public GameObject ClientServiceInfoPanel;
    public GameObject ClientServiceListPanel;

    public GameObject WorkerServiceInfoPanel;
    public GameObject WorkerServiceListPanel;
    public GameObject serviceEditPanel;

    [Header("Client Order List")]
    public GameObject clientOrderPrefab;
    public Transform clientOrderContent;

    [Header("Client Favourite List")]
    public GameObject clientFavouritePrefab;
    public Transform clientFavouriteContent;

    [Header("Worker Order List")]
    public GameObject workerOrderPrefab;
    public Transform workerOrderContent;

    [Header("Order info panels")]
    public GameObject WorkerOrderInfoPanel;
    public GameObject ClientOrderInfoPanel;

    void Awake() {

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {

            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    void Start() {

        //Initialize Trilleon testing
        TrilleonAutomation.AutomationMaster.Initialize();
    }

    private void InitializeFirebase() {

        Debug.Log("Connecting to Database");
        auth = FirebaseAuth.DefaultInstance;
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void ClearLoginFeilds() {

        emailLoginField.text = "";
        passwordLoginField.text = "";
        successText.text = "";
        errorText.text = "";
    }
    public void ClearRegisterFeilds() {

        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
        errorRegisterText.text = "";
    }

    public void send_sms_code() {

        provider = PhoneAuthProvider.GetInstance(auth);
        provider.VerifyPhoneNumber(CountryCode.text + phoneNumber.text, TimeoutTime, null, verificationCompleted: (credential) =>{}, verificationFailed: (error) =>{},
          codeSent: (id, token) =>{
              VerificationId = id;
              debug.text = "code sent";
              PanelManager.instance.EnterNumberScreen();
          }, codeAutoRetrievalTimeOut: (id) =>{});
    }

    public void PhoneLogin() {

        //verify sms code for client login
        StartCoroutine(otp_verification());
    }
    private IEnumerator otp_verification()
    {
        Credential credential = provider.GetCredential(VerificationId, otp.text);
        var VerifyCode = auth.SignInWithCredentialAsync(credential);
        yield return new WaitUntil(predicate: () => VerifyCode.IsCompleted);

        LoggedInUser = VerifyCode.Result;

        errorText.text = "Bandykite dar kartą";
        successText.text = "Prisijungta";

        var LoggedInClient = database.Child("clients").Child(LoggedInUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => LoggedInClient.IsCompleted);

        if (LoggedInClient.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {LoggedInClient.Exception}");
        }
        else if (LoggedInClient.Result.Value == null) {
            PanelManager.instance.ClientRegisterScreen();
        }
        else {
            PanelManager.instance.ClientScreen();
            PanelManager.instance.ClientMainScreen();
            ClearLoginFeilds();
            ClearRegisterFeilds();
        }
    }

    public void LoginButton() {

        //verify email and pass for worker login
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    private IEnumerator Login(string _email, string _password) {

        var WorkerLogin = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => WorkerLogin.IsCompleted);

        if (WorkerLogin.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {WorkerLogin.Exception}");
            FirebaseException firebaseEx = WorkerLogin.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode) {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            errorText.text = message;
        }
        else {  //successfully logged in
            LoggedInUser = WorkerLogin.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", LoggedInUser.DisplayName, LoggedInUser.Email);
            successText.text = "Prisijungta";
            yield return new WaitForSeconds(2);
            checkIfVerified();
            ClearLoginFeilds();
        }
    }

    public void checkIfVerified() {

        StartCoroutine(checkVerification());
    }
    private IEnumerator checkVerification() {

        var Verification = database.Child("workers").Child(LoggedInUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => Verification.IsCompleted);

        if (Verification.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {Verification.Exception}");
        }
        else {
            DataSnapshot VerificationData = Verification.Result;
            string verify = VerificationData.Child("Verification").Value.ToString();
            int verification = int.Parse(verify);

            if (verification == 1) {
                LoadWorkerJobs();
                PanelManager.instance.WorkerScreen();
                PanelManager.instance.WorkerHomeScreen();
                ClearLoginFeilds();
                ClearRegisterFeilds();
            }
            else {
                PanelManager.instance.UnverfiedWorkerScreen();
                ClearLoginFeilds();
                ClearRegisterFeilds();
            } 
        }
    }

    public void LogOut() {
        auth.SignOut();
        PanelManager.instance.StartPageScreen();
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    public void RegisterButton() {

        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text, workerNameRegisterField.text, workerSurnameRegisterField.text, workerIDRegisterField.text, workerPhoneRegisterField.text));
    }
    private IEnumerator Register(string _email, string _password, string _username, string workerName, string workerSurname, string workerID, string workerPhone) {

        var Register = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => Register.IsCompleted);

        if (Register.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {Register.Exception}");
            FirebaseException firebaseEx = Register.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            string message = "Register Failed!";
            switch (errorCode) {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    message = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use";
                    break;
            }
            errorRegisterText.text = message;
        }
        else {
            LoggedInUser = Register.Result;

            if (LoggedInUser != null) {
                UserProfile profile = new UserProfile { DisplayName = _username };
                var Profile = LoggedInUser.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => Profile.IsCompleted);
                if (Profile.Exception != null) {
                    Debug.LogWarning(message: $"Failed to connect: {Profile.Exception}");
                    errorRegisterText.text = "Username Set Failed!";
                }
                else {
                    var Worker = database.Child("workers").Child(LoggedInUser.UserId).RunTransaction(Data =>{
                        Data.Child("Name").Value = workerName;
                        Data.Child("Surname").Value = workerSurname;
                        Data.Child("Email").Value = _email;
                        Data.Child("Identification").Value = workerID;
                        Data.Child("Verification").Value = "0";
                        Data.Child("Phone").Value = workerPhone;
                        return TransactionResult.Success(Data);
                    });
                    yield return new WaitUntil(predicate: () => Worker.IsCompleted);

                    PanelManager.instance.WorkerScreen();
                    PanelManager.instance.WorkerHomeScreen();
                    ClearRegisterFeilds();
                    ClearLoginFeilds();
                }
            }
        }
        
    }

    public void RegisterClientButton() {

        StartCoroutine(RegisterClient(clientEmailRegisterField.text, clientNameRegisterField.text, clientSurnameRegisterField.text));
    }
    private IEnumerator RegisterClient(string _email, string clientName, string clientSurname) {

        if (LoggedInUser != null) {
            UserProfile profile = new UserProfile { DisplayName = clientName };
            var Profile = LoggedInUser.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(predicate: () => Profile.IsCompleted);

            if (Profile.Exception != null) {
                Debug.LogWarning(message: $"Failed to connect: {Profile.Exception}");
                errorRegisterText.text = "Username Set Failed!";
            }
            else {
                var Client = database.Child("clients").Child(LoggedInUser.UserId).RunTransaction(Data =>{
                    Data.Child("Email").Value = _email;
                    Data.Child("Name").Value = clientName;
                    Data.Child("Surname").Value = clientSurname;
                    Data.Child("Phone").Value = CountryCode.text + phoneNumber.text;
                    return TransactionResult.Success(Data);
                });
                yield return new WaitUntil(predicate: () => Client.IsCompleted);

                PanelManager.instance.ClientScreen();
                PanelManager.instance.ClientMainScreen();
                ClearRegisterFeilds();
                ClearLoginFeilds();
            }
        }
    }

    public void RegisterServiceButton(string title, string price, string pricePer, string city, string duration, string durationPer, float status, string category, string subcategory, string aboutJob, string jobTags) {

        StartCoroutine(RegisterService(title, price, pricePer, city, duration, durationPer, status, category, subcategory, aboutJob, jobTags));
    }

    private IEnumerator RegisterService(string title, string price, string pricePer, string city, string duration, string durationPer, float status, string category, string subcategory, string aboutJob, string jobTags) {
        
        string key = database.Child("jobs").Push().Key;
        var Service = database.Child("jobs").Child(key).RunTransaction(Data =>{
       
            Data.Child("WorkerID").Value = LoggedInUser.UserId;
            Data.Child("Title").Value = title;
            Data.Child("Price").Value = price;
            Data.Child("PricePerService").Value = pricePer;
            Data.Child("City").Value = city;
            Data.Child("Duration").Value = duration;
            Data.Child("DurationPerService").Value = durationPer;
            Data.Child("Status").Value = status;
            Data.Child("Category").Value = category;
            Data.Child("Subcategory").Value = subcategory;
            Data.Child("About").Value = aboutJob;
            Data.Child("JobTags").Value = jobTags;
            Data.Child("Rating").Value = 0;
            Data.Child("Views").Value = 0;
            Data.Child("Reviews").Value = 0;
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Service.IsCompleted);
        ClearRegisterFeilds();
        ClearLoginFeilds();
        LoadWorkerJobs();
    }

    public void CreateCategoriesButton() {

        StartCoroutine(AddCategoriesToDB());
    }
    private IEnumerator AddCategoriesToDB() {

        List<string> EList = new List<string>();
        EList.Add("Apšvietimas");
        EList.Add("Jungikliai ir maitinimo lizdai");
        List<string> SList = new List<string>();
        SList.Add("Kriauklė");
        SList.Add("Boileris");
        List<string> VList = new List<string>();
        VList.Add("Virtuvės valymas");
        VList.Add("Išorinio fasado valymas");

        var Category = database.Child("categories").RemoveValueAsync();
        yield return new WaitUntil(predicate: () => Category.IsCompleted);

        Category = database.Child("categories").RunTransaction(Data =>
        {
            foreach (string child in EList) {
                Data.Child("Elektros darbai").Child(child).Value = "";
            }

            foreach (string child in SList) {
                Data.Child("Santechnikos darbai").Child(child).Value = "";
            }

            foreach (string child in VList) {
                Data.Child("Valymo darbai").Child(child).Value = "";
            }
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Category.IsCompleted);
    }

    public void GetCategoriesFromDBButton() {

        StartCoroutine(GetCategoriesFromDB());
    }
    private IEnumerator GetCategoriesFromDB() {

        var Category = database.Child("categories").GetValueAsync();
        yield return new WaitUntil(predicate: () => Category.IsCompleted);

        if (Category.Exception != null) {
            Debug.LogWarning(message: $"Failed to connecT: {Category.Exception}");
        }
        else {
            categories.Clear();
            DataSnapshot Categories = Category.Result;

            foreach (DataSnapshot cat in Categories.Children.Reverse<DataSnapshot>()) {
                categories.Add(cat.Key);
            }

            //fill category dropdown
            jobCategory.ClearOptions();
            jobCategory.options.Clear();

            foreach (string category in categories) {
                jobCategory.options.Add(new TMP_Dropdown.OptionData() { text = category });
                EditjobCategory.options.Add(new TMP_Dropdown.OptionData() { text = category });
            }
        }
    }

    public void GetSubCategoriesFromDBButton(string category) {

        StartCoroutine(GetSubCategoriesFromDB(category));
    }
    private IEnumerator GetSubCategoriesFromDB(string Cagetory) {

        var Category = database.Child("categories").Child(Cagetory).GetValueAsync();
        yield return new WaitUntil(predicate: () => Category.IsCompleted);

        subcategories.Clear();
        DataSnapshot Subcategories = Category.Result;

        foreach (DataSnapshot subcat in Subcategories.Children.Reverse<DataSnapshot>()) {
            subcategories.Add(subcat.Key);
        }

        //fill subcategory dropdown
        jobSubcategory.ClearOptions();
        jobSubcategory.options.Clear();

        foreach (string subcategory in subcategories) {
            jobSubcategory.options.Add(new TMP_Dropdown.OptionData() { text = subcategory });
        }
    }

    public void FillSubcategoryDropdown() {

        string category = jobCategory.options[jobCategory.value].text;
        GetSubCategoriesFromDBButton(category);
    }
    public void FillEditSubcategoryDropdown() {

        string category = EditjobCategory.options[EditjobCategory.value].text;
        GetEditSubCategoriesFromDBButton(category);
    }

    public void GetEditSubCategoriesFromDBButton(string category) {

        StartCoroutine(GetEditSubCategoriesFromDB(category));
    }
    private IEnumerator GetEditSubCategoriesFromDB(string Cagetory) {

        var Category = database.Child("categories").Child(Cagetory).GetValueAsync();
        yield return new WaitUntil(predicate: () => Category.IsCompleted);

        subcategories.Clear();
        //Data has been retrieved
        DataSnapshot Subcategories = Category.Result;

        foreach (DataSnapshot subcat in Subcategories.Children.Reverse<DataSnapshot>()) {
            subcategories.Add(subcat.Key);
        }

        //fill subcategory dropdown
        EditjobSubcategory.ClearOptions();
        EditjobSubcategory.options.Clear();

        foreach (string subcategory in subcategories) {
            EditjobSubcategory.options.Add(new TMP_Dropdown.OptionData() { text = subcategory });
        }
    }

    public void LoadWorkerJobs() {

        StartCoroutine(LoadAllWorkerJobs());
    }

    private IEnumerator LoadAllWorkerJobs() {

        var Job = database.Child("jobs").GetValueAsync();
        yield return new WaitUntil(predicate: () => Job.IsCompleted);

        if (Job.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {Job.Exception}");
        }
        else {
            DataSnapshot Jobs = Job.Result;

            foreach (Transform child in jobContent.transform) {
                Destroy(child.gameObject);
            }

            foreach (DataSnapshot job in Jobs.Children.Reverse<DataSnapshot>()) {
                if (job.Child("WorkerID").Value.ToString() == LoggedInUser.UserId) {
                    string workerID = job.Child("WorkerID").Value.ToString();
                    string id = job.Key.ToString();
                    string title = job.Child("Title").Value.ToString();
                    string price = job.Child("Price").Value.ToString();
                    string pricePer = job.Child("PricePerService").Value.ToString();
                    string city = job.Child("City").Value.ToString();
                    string duration = job.Child("Duration").Value.ToString();
                    string durationPer = job.Child("DurationPerService").Value.ToString();
                    string status = job.Child("Status").Value.ToString();
                    string category = job.Child("Category").Value.ToString();
                    string subcategory = job.Child("Subcategory").Value.ToString();
                    string about = job.Child("About").Value.ToString();
                    string jobTags = job.Child("JobTags").Value.ToString();
                    string rating = job.Child("Rating").Value.ToString();
                    string views = job.Child("Views").Value.ToString();
                    string reviews = job.Child("Reviews").Value.ToString();

                    GameObject jobPrefabElement = Instantiate(jobElement, jobContent);
                    jobPrefabElement.GetComponent<JobElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
                }
            }
        }
    }

    public void LoadJobByCategory(string subcategory) {

        StartCoroutine(LoadJobList(subcategory));
    }

    private IEnumerator LoadJobList(string Subcategory)
    {
        var Job = database.Child("jobs").GetValueAsync();
        yield return new WaitUntil(predicate: () => Job.IsCompleted);

        DataSnapshot Jobs = Job.Result;

        foreach (Transform child in jobListContent.transform) {
            Destroy(child.gameObject);
        }

        foreach (DataSnapshot job in Jobs.Children.Reverse<DataSnapshot>())
        {
            if (job.Child("Subcategory").Value.ToString() == Subcategory)
            {
                string workerID = job.Child("WorkerID").Value.ToString();
                string id = job.Key.ToString();
                string title = job.Child("Title").Value.ToString();
                string price = job.Child("Price").Value.ToString();
                string pricePer = job.Child("PricePerService").Value.ToString();
                string city = job.Child("City").Value.ToString();
                string duration = job.Child("Duration").Value.ToString();
                string durationPer = job.Child("DurationPerService").Value.ToString();
                string status = job.Child("Status").Value.ToString();
                string category = job.Child("Category").Value.ToString();
                string subcategory = job.Child("Subcategory").Value.ToString();
                string about = job.Child("About").Value.ToString();
                string jobTags = job.Child("JobTags").Value.ToString();
                string rating = job.Child("Rating").Value.ToString();
                string views = job.Child("Views").Value.ToString();
                string reviews = job.Child("Reviews").Value.ToString();

                GameObject jobListPrefabElement = Instantiate(jobListElement, jobListContent);
                jobListPrefabElement.GetComponent<JobElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
            }
        }
        categoryPanel.SetActive(false);
        servicePanel.SetActive(true); 
    }

    public void GetClientDataForOrder(GameObject orderPanel) {

        StartCoroutine(GetClientOrderData(orderPanel));
    }

    private IEnumerator GetClientOrderData(GameObject orderPanel) {

        var Client = database.Child("clients").Child(LoggedInUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => Client.IsCompleted);

        if (Client.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {Client.Exception}");
        }
        else {
            DataSnapshot client = Client.Result;
            string name = client.Child("Name").Value.ToString();
            string surname = client.Child("Surname").Value.ToString();
            string phone = client.Child("Phone").Value.ToString();
            string fullName = name + " " + surname;

            orderPanel.GetComponent<OrderUI>().OrderInfo(LoggedInUser.UserId, fullName, phone);
        }
    }
    public void GetWorkerDataForOrder(GameObject orderPanel, string workerID) {

        StartCoroutine(GetWorkerOrderData(orderPanel, workerID));
    }

    private IEnumerator GetWorkerOrderData(GameObject orderPanel, string workerID) {

        var Worker = database.Child("workers").Child(workerID).GetValueAsync();
        yield return new WaitUntil(predicate: () => Worker.IsCompleted);

        if (Worker.Exception != null) {
            Debug.LogWarning(message: $"Failed to connect: {Worker.Exception}");
        }
        else {
            DataSnapshot worker = Worker.Result;
            string name = worker.Child("Name").Value.ToString();
            string surname = worker.Child("Surname").Value.ToString();
            string phone = worker.Child("Phone").Value.ToString();
            string fullName = name + " " + surname;

            orderPanel.GetComponent<OrderUI>().WorkerInfo(fullName, phone);
        }
    }

    public void CreateClientOrder(string workerID, string id, string title, string price, string pricePer, string workercity, string duration, string durationPer, string status, string category, string subcategory, string about,
        string jobTags, string rating, string views, string reviews, string clientID, string clientNameSurname, string clientNumber, string clientAdress, string clientCity, string clientOrderDate, string workerNameSurname, 
        string workerNumber, string orderTime, string additionalInfo) {

        StartCoroutine(CreateOrder(workerID, id, title, price, pricePer, workercity, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews, clientID, 
            clientNameSurname, clientNumber, clientAdress, clientCity, clientOrderDate, workerNameSurname, workerNumber, orderTime, additionalInfo));
    }

    private IEnumerator CreateOrder(string workerID, string id, string title, string price, string pricePer, string workerCity, string duration, string durationPer, string status, string category, string subcategory, string about,
        string jobTags, string rating, string views, string reviews, string clientID, string clientNameSurname, string clientNumber, string clientAdress, string clientCity, string clientOrderDate, string workerNameSurname,
        string workerNumber, string orderTime, string additionalInfo) {

        string key = database.Child("orders").Push().Key;
        var Order = database.Child("orders").Child(key).RunTransaction(Data =>
        {
            Data.Child("WorkerID").Value = workerID;
            Data.Child("ServiceID").Value = id;
            Data.Child("Title").Value = title;
            Data.Child("Price").Value = price;
            Data.Child("PricePer").Value = pricePer;
            Data.Child("WorkerCity").Value = workerCity;
            Data.Child("Duration").Value = duration;
            Data.Child("DurationPer").Value = durationPer;
            Data.Child("Status").Value = "0";
            Data.Child("Category").Value = category;
            Data.Child("Subcategory").Value = subcategory;
            Data.Child("About").Value = about;
            Data.Child("JobTags").Value = jobTags;
            Data.Child("Rating").Value = rating;
            Data.Child("Views").Value = views;
            Data.Child("Reviews").Value = reviews;
            Data.Child("ClientID").Value = clientID;
            Data.Child("ClientNameSurname").Value = clientNameSurname;
            Data.Child("ClientNumber").Value = clientNumber;
            Data.Child("ClientAdress").Value = clientAdress;
            Data.Child("ClientCity").Value = clientCity;
            Data.Child("ClientOrderDate").Value = clientOrderDate;
            Data.Child("WorkerNameSurname").Value = workerNameSurname;
            Data.Child("WorkerNumber").Value = workerNumber;
            Data.Child("OrderTime").Value = orderTime;
            Data.Child("AdditionalInfo").Value = additionalInfo;
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Order.IsCompleted);

        LoadAllClientOrders();
    }

    public void UpdateOrderStatus(string orderId, int newStatus) {

        StartCoroutine(UpdateStatus(orderId, newStatus));
    }

    private IEnumerator UpdateStatus(string orderId, int newStatus) {

        var Order = database.Child("orders").Child(orderId).RunTransaction(Data =>
        { 
            Data.Child("Status").Value = newStatus.ToString();
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Order.IsCompleted);
    }
    public void LoadWorkerOrders() {

        StartCoroutine(LoadAllWorkerOrders());
    }

    private IEnumerator LoadAllWorkerOrders() {

        var Order = database.Child("orders").OrderByChild("WorkerID").EqualTo(LoggedInUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => Order.IsCompleted);

        DataSnapshot Orders = Order.Result;

        foreach (Transform child in workerOrderContent.transform) {
            Destroy(child.gameObject);
        }

        foreach (DataSnapshot order in Orders.Children.Reverse<DataSnapshot>()) {
            string workerID = order.Child("WorkerID").Value.ToString();
            string id = order.Key.ToString();
            string title = order.Child("Title").Value.ToString();
            string price = order.Child("Price").Value.ToString();
            string pricePer = order.Child("PricePer").Value.ToString();
            string workerCity = order.Child("WorkerCity").Value.ToString();
            string duration = order.Child("Duration").Value.ToString();
            string durationPer = order.Child("DurationPer").Value.ToString();
            string status = order.Child("Status").Value.ToString();
            string category = order.Child("Category").Value.ToString();
            string subcategory = order.Child("Subcategory").Value.ToString();
            string about = order.Child("About").Value.ToString();
            string jobTags = order.Child("JobTags").Value.ToString();
            string rating = order.Child("Rating").Value.ToString();
            string views = order.Child("Views").Value.ToString();
            string reviews = order.Child("Reviews").Value.ToString();
            string clientID = order.Child("ClientID").Value.ToString();
            string clientNameSurname = order.Child("ClientNameSurname").Value.ToString();
            string clientNumber = order.Child("ClientNumber").Value.ToString();
            string clientAdress = order.Child("ClientAdress").Value.ToString();
            string clientCity = order.Child("ClientCity").Value.ToString();
            string clientOrderDate = order.Child("ClientOrderDate").Value.ToString();
            string workerNameSurname = order.Child("WorkerNameSurname").Value.ToString();
            string workerNumber = order.Child("WorkerNumber").Value.ToString();
            string orderTime = order.Child("OrderTime").Value.ToString();
            string additionalInfo = order.Child("AdditionalInfo").Value.ToString();

            GameObject orderPrefabElement = Instantiate(workerOrderPrefab, workerOrderContent);
            orderPrefabElement.GetComponent<OrderElement>().NewOrderElement(workerID, id, title, price, pricePer, workerCity, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews, clientID, clientNameSurname,
                clientNumber, clientAdress, clientCity, clientOrderDate, workerNameSurname, workerNumber, orderTime, additionalInfo);
        }
    }

    public void LoadClientOrders() {

        StartCoroutine(LoadAllClientOrders());
    }

    private IEnumerator LoadAllClientOrders() {

        var Order = database.Child("orders").OrderByChild("ClientID").EqualTo(LoggedInUser.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => Order.IsCompleted);

        if (Order.Exception != null) {
            Debug.LogWarning(message: $"Failed to register task with {Order.Exception}");
        }
        else {
            DataSnapshot Orders = Order.Result;

            foreach (Transform child in clientOrderContent.transform) {
                Destroy(child.gameObject);
            }

            foreach (DataSnapshot order in Orders.Children.Reverse<DataSnapshot>()) {
                string workerID = order.Child("WorkerID").Value.ToString();
                string id = order.Key.ToString();
                string title = order.Child("Title").Value.ToString();
                string price = order.Child("Price").Value.ToString();
                string pricePer = order.Child("PricePer").Value.ToString();
                string workerCity = order.Child("WorkerCity").Value.ToString();
                string duration = order.Child("Duration").Value.ToString();
                string durationPer = order.Child("DurationPer").Value.ToString();
                string status = order.Child("Status").Value.ToString();
                string category = order.Child("Category").Value.ToString();
                string subcategory = order.Child("Subcategory").Value.ToString();
                string about = order.Child("About").Value.ToString();
                string jobTags = order.Child("JobTags").Value.ToString();
                string rating = order.Child("Rating").Value.ToString();
                string views = order.Child("Views").Value.ToString();
                string reviews = order.Child("Reviews").Value.ToString();
                string clientID = order.Child("ClientID").Value.ToString();
                string clientNameSurname = order.Child("ClientNameSurname").Value.ToString();
                string clientNumber = order.Child("ClientNumber").Value.ToString();
                string clientAdress = order.Child("ClientAdress").Value.ToString();
                string clientCity = order.Child("ClientCity").Value.ToString();
                string clientOrderDate = order.Child("ClientOrderDate").Value.ToString();
                string workerNameSurname = order.Child("WorkerNameSurname").Value.ToString();
                string workerNumber = order.Child("WorkerNumber").Value.ToString();
                string orderTime = order.Child("OrderTime").Value.ToString();
                string additionalInfo = order.Child("AdditionalInfo").Value.ToString();

                GameObject orderPrefabElement = Instantiate(clientOrderPrefab, clientOrderContent);
                orderPrefabElement.GetComponent<OrderElement>().NewOrderElement(workerID, id, title, price, pricePer, workerCity, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews, clientID, clientNameSurname,
                    clientNumber, clientAdress, clientCity, clientOrderDate, workerNameSurname, workerNumber, orderTime, additionalInfo);
            }
        }
    }

    public void SetWorkerShedule(string mon, string tue, string wed, string thu, string fri, string sat, string sun) {

        StartCoroutine(SetShedule(mon, tue, wed, thu, fri, sat, sun));
    }

    private IEnumerator SetShedule(string mon, string tue, string wed, string thu, string fri, string sat, string sun) {

        var Worker = database.Child("workers").Child(LoggedInUser.UserId).Child("agenda").RunTransaction(Data =>{
            // 1:30-5:45
            Data.Child("Monday").Value = mon;
            Data.Child("Tuesday").Value = tue;
            Data.Child("Wednesday").Value = wed;
            Data.Child("Thursday").Value = thu;
            Data.Child("Friday").Value = fri;
            Data.Child("Saturday").Value = sat;
            Data.Child("Sunday").Value = sun;
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Worker.IsCompleted);
    }

    public void GetWorkerShedule(GameObject shedule) {

        StartCoroutine(GetShedule(shedule));
    }

    private IEnumerator GetShedule(GameObject shedule) {

        string inactiveDay = "Nedirba";
        var Worker = database.Child("workers").Child(LoggedInUser.UserId).Child("agenda").GetValueAsync();
        yield return new WaitUntil(predicate: () => Worker.IsCompleted);

        if (Worker.Exception != null) {
            shedule.GetComponent<WorkerAgenda>().mon.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().tue.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().wed.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().thu.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().fri.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().sat.text = inactiveDay;
            shedule.GetComponent<WorkerAgenda>().sun.text = inactiveDay;
            Debug.LogWarning(message: $"Failed toconnect: {Worker.Exception}");
        }
        else {
            DataSnapshot childSnapshot = Worker.Result;
            string mon, tue, wed, thu, fri, sat, sun;

            try {
                mon = childSnapshot.Child("Monday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().mon.text = mon;
            }
            catch(Exception e) {
                shedule.GetComponent<WorkerAgenda>().mon.text = inactiveDay;
            }

            try {
                tue = childSnapshot.Child("Tuesday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().tue.text = tue;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().mon.text = inactiveDay;
            }

            try {
                wed = childSnapshot.Child("Wednesday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().wed.text = wed;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().wed.text = inactiveDay;
            }

            try {
                thu = childSnapshot.Child("Thursday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().thu.text = thu;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().thu.text = inactiveDay;
            }

            try {
                fri = childSnapshot.Child("Friday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().fri.text = fri;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().fri.text = inactiveDay;
            }

            try {
                sat = childSnapshot.Child("Saturday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().sat.text = sat;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().sat.text = inactiveDay;
            }

            try {
                sun = childSnapshot.Child("Sunday").Value.ToString();
                shedule.GetComponent<WorkerAgenda>().sun.text = sun;
            }
            catch (Exception e) {
                shedule.GetComponent<WorkerAgenda>().sun.text = inactiveDay;
            }
        }
    }

    public void GetWorkerSheduleById(GameObject service) {

        StartCoroutine(GetSheduleById(service));
    }

    private IEnumerator GetSheduleById(GameObject service) {

        string inactiveDay = "Nedirba";
        string workerId = service.GetComponent<ServiceElement>().workerID;
        var Worker = database.Child("workers").Child(workerId).Child("agenda").GetValueAsync();
        yield return new WaitUntil(predicate: () => Worker.IsCompleted);

        if (Worker.Exception != null) {
            service.GetComponent<ServiceElement>().mon = inactiveDay;
            service.GetComponent<ServiceElement>().tue = inactiveDay;
            service.GetComponent<ServiceElement>().wed = inactiveDay;
            service.GetComponent<ServiceElement>().thu = inactiveDay;
            service.GetComponent<ServiceElement>().fri = inactiveDay;
            service.GetComponent<ServiceElement>().sat = inactiveDay;
            service.GetComponent<ServiceElement>().sun = inactiveDay;
            Debug.LogWarning(message: $"Failed to connect: {Worker.Exception}");
        }
        else {
            DataSnapshot childSnapshot = Worker.Result;
            string mon, tue, wed, thu, fri, sat, sun;

            try {
                mon = childSnapshot.Child("Monday").Value.ToString();
                service.GetComponent<ServiceElement>().mon = mon;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().mon = inactiveDay;
            }

            try {
                tue = childSnapshot.Child("Tuesday").Value.ToString();
                service.GetComponent<ServiceElement>().tue = tue;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().tue = inactiveDay;
            }

            try {
                wed = childSnapshot.Child("Wednesday").Value.ToString();
                service.GetComponent<ServiceElement>().wed = wed;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().wed = inactiveDay;
            }

            try {
                thu = childSnapshot.Child("Thursday").Value.ToString();
                service.GetComponent<ServiceElement>().thu = thu;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().thu = inactiveDay;
            }

            try {
                fri = childSnapshot.Child("Friday").Value.ToString();
                service.GetComponent<ServiceElement>().fri = fri;
            }
            catch (Exception e){
                service.GetComponent<ServiceElement>().fri = inactiveDay;
            }

            try {
                sat = childSnapshot.Child("Saturday").Value.ToString();
                service.GetComponent<ServiceElement>().sat = sat;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().sat = inactiveDay;
            }

            try {
                sun = childSnapshot.Child("Sunday").Value.ToString();
                service.GetComponent<ServiceElement>().sun = sun;
            }
            catch (Exception e) {
                service.GetComponent<ServiceElement>().sun = inactiveDay;
            }
        }
    }

    public void EditServiceButton(string id, string title, string price, string pricePer, string city, string duration, string durationPer, float status, string category, string subcategory, string aboutJob, string jobTags) {

        StartCoroutine(EditService(id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, aboutJob, jobTags));
    }

    private IEnumerator EditService(string id, string title, string price, string pricePer, string city, string duration, string durationPer, float status, string category, string subcategory, string aboutJob, string jobTags) {

        var Job = database.Child("jobs").Child(id).RunTransaction(Data =>
        {
            Data.Child("WorkerID").Value = LoggedInUser.UserId;
            Data.Child("Title").Value = title;
            Data.Child("Price").Value = price;
            Data.Child("PricePerService").Value = pricePer;
            Data.Child("City").Value = city;
            Data.Child("Duration").Value = duration;
            Data.Child("DurationPerService").Value = durationPer;
            Data.Child("Status").Value = status;
            Data.Child("Category").Value = category;
            Data.Child("Subcategory").Value = subcategory;
            Data.Child("About").Value = aboutJob;
            Data.Child("JobTags").Value = jobTags;
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Job.IsCompleted);

        LoadWorkerJobs();
    }

    public void AddFavourite(string id) {

        StartCoroutine(AddToFavourites(id));
    }

    private IEnumerator AddToFavourites(string id) {

        var Client = database.Child("clients").Child(LoggedInUser.UserId).Child("Favourites").RunTransaction(Data =>{
            Data.Child(id).Value = "";
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Client.IsCompleted);
    }

    public void DeleteServiceButton(string id) {

        StartCoroutine(DeleteService(id));
    }

    private IEnumerator DeleteService(string id) {

        var Job = database.Child("jobs").Child(id).RemoveValueAsync();
        yield return new WaitUntil(predicate: () => Job.IsCompleted);
        LoadWorkerJobs();
    }

    public void RemoveFavorite(string id) {

        StartCoroutine(RemoveFromFavorites(id));
    }

    private IEnumerator RemoveFromFavorites(string id) {

        var Client = database.Child("clients").Child(LoggedInUser.UserId).Child("Favourites").Child(id).RemoveValueAsync();
        yield return new WaitUntil(predicate: () => Client.IsCompleted);
        LoadFavourites();
    }

    public void LoadFavourites() {

        StartCoroutine(LoadFavouritesList());
    }

    private IEnumerator LoadFavouritesList() {

        List<string> jobList = new List<string>();
        List<string> newJobList = new List<string>();
        var Client = database.Child("clients").Child(LoggedInUser.UserId).Child("Favourites").GetValueAsync();
        yield return new WaitUntil(predicate: () => Client.IsCompleted);

        jobList.Clear();
        DataSnapshot Clients = Client.Result;

        foreach (DataSnapshot client in Clients.Children.Reverse<DataSnapshot>()) {
            jobList.Add(client.Key);
        }
        
        var Job = database.Child("jobs").GetValueAsync();
        yield return new WaitUntil(predicate: () => Job.IsCompleted);

        DataSnapshot Jobs = Job.Result;

        foreach (Transform child in clientFavouriteContent.transform) {
            Destroy(child.gameObject);
        }

        newJobList.Clear();

        foreach (DataSnapshot job in Jobs.Children.Reverse<DataSnapshot>()) {
            if (jobList.Contains(job.Key)) {

                newJobList.Add(job.Key);
                string workerID = job.Child("WorkerID").Value.ToString();
                string id = job.Key.ToString();
                string title = job.Child("Title").Value.ToString();
                string price = job.Child("Price").Value.ToString();
                string pricePer = job.Child("PricePerService").Value.ToString();
                string city = job.Child("City").Value.ToString();
                string duration = job.Child("Duration").Value.ToString();
                string durationPer = job.Child("DurationPerService").Value.ToString();
                string status = job.Child("Status").Value.ToString();
                string category = job.Child("Category").Value.ToString();
                string subcategory = job.Child("Subcategory").Value.ToString();
                string about = job.Child("About").Value.ToString();
                string jobTags = job.Child("JobTags").Value.ToString();
                string rating = job.Child("Rating").Value.ToString();
                string views = job.Child("Views").Value.ToString();
                string reviews = job.Child("Reviews").Value.ToString();

                GameObject FavListPrefabElement = Instantiate(clientFavouritePrefab, clientFavouriteContent);
                FavListPrefabElement.GetComponent<JobElement>().NewJobElement(workerID, id, title, price, pricePer, city, duration, durationPer, status, category, subcategory, about, jobTags, rating, views, reviews);
            }
        }
        servicePanel.SetActive(false);
        orderPanel.SetActive(false);
        categoryPanel.SetActive(false);
        favouritesPanel.SetActive(true);

        var ClientRemove = database.Child("clients").Child(LoggedInUser.UserId).Child("Favourites").RemoveValueAsync();
        yield return new WaitUntil(predicate: () => ClientRemove.IsCompleted);

        var Favourites = database.Child("clients").Child(LoggedInUser.UserId).Child("Favourites").RunTransaction(Data =>{
            foreach (string job in newJobList) {
                Data.Child(job).Value = "";
            }
            return TransactionResult.Success(Data);
        });
        yield return new WaitUntil(predicate: () => Favourites.IsCompleted);
    }
}
