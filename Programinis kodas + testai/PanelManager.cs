using UnityEngine;

public class PanelManager : MonoBehaviour {

    public static PanelManager instance;

    [Header("Main UI")]
    public GameObject startPageUI;

    [Header("Pvz UI")]
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject userDataUI;
    public GameObject scoreboardUI;

    [Header("Client UI")]
    public GameObject clientUI;
    public GameObject sendNumberUI;
    public GameObject enterNumberUI; 
    public GameObject clientLoginUI;
    public GameObject clientSearchUI;
    public GameObject clientRegisterUI;

    public GameObject clientSettingsUI;
    public GameObject searchWindowUI;
    public GameObject clientServiceListUI;
    public GameObject clientServiceUI;
    public GameObject clientOrdersUI;
    public GameObject clientLowerPanelUI;
    public GameObject clientUpperPanelUI;

    [Header("Worker UI")]
    public GameObject workerUI;
    public GameObject workerHomeUI; 
    public GameObject workerLoginUI;
    public GameObject workerRegisterUI;
    public GameObject workerPreLoginUI;

    public GameObject workerJobsUI;
    public GameObject workerNewJobUI;    
    public GameObject workerOrdersUI;
    public GameObject workerAgendaUI;
    public GameObject workerSettingsUI;
    public GameObject workerLowerPanelUI;
    public GameObject workerUpperPanelUI;

    public GameObject unverfiedWorkerUI;

    private void Awake() {

        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void UnverfiedWorkerScreen() {

        CloseAllScreens();
        unverfiedWorkerUI.SetActive(true);
    }

    public void CloseAllScreens() {

        CloseMainPanels();
        CloseLoginScreens();
        CloseClientScreens();
        CloseWorkerScreens();
    }
    public void CloseMainPanels() {

        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
        startPageUI.SetActive(false);
        workerPreLoginUI.SetActive(false);
        workerLoginUI.SetActive(false);
        workerRegisterUI.SetActive(false);
        clientRegisterUI.SetActive(false);
        clientLoginUI.SetActive(false);
        clientUI.SetActive(false);
        workerUI.SetActive(false);
        unverfiedWorkerUI.SetActive(false);
    }
    public void CloseClientScreens() {

        clientSearchUI.SetActive(false);
    }
    public void CloseWorkerScreens() {

        workerHomeUI.SetActive(false);
    }
    public void CloseLoginScreens() {

        sendNumberUI.SetActive(false);
        enterNumberUI.SetActive(false);
    }

    public void ClientRegisterScreen() {

        CloseAllScreens();
        clientRegisterUI.SetActive(true);
    }
         
    public void ClientLoginScreen() {

        CloseAllScreens();
        clientLoginUI.SetActive(true);
    }
    public void SendNumberScreen() {

        CloseLoginScreens();
        sendNumberUI.SetActive(true);
    }
    public void EnterNumberScreen() {

        CloseLoginScreens();
        enterNumberUI.SetActive(true);
    }
    public void StartPageScreen() {

        CloseAllScreens();
        startPageUI.SetActive(true);
    }
    public void WorkerPreLoginScreen() {

        CloseAllScreens();
        workerPreLoginUI.SetActive(true);
    }

    public void LoginScreen() {

        CloseAllScreens();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() {

        CloseAllScreens();
        registerUI.SetActive(true);
    }

    public void UserDataScreen() {

        CloseAllScreens();
        userDataUI.SetActive(true);
    }
    public void ScoreboardScreen() {

        CloseAllScreens();
        scoreboardUI.SetActive(true);
    }
    public void ClientScreen() {

        CloseAllScreens();
        clientUI.SetActive(true);
    }
    public void ClientMainScreen() {

        CloseClientScreens();
        clientSearchUI.SetActive(true);
    }
    public void WorkerLoginScreen() {

        CloseAllScreens();
        workerLoginUI.SetActive(true);
    }
    public void WorkerRegisterScreen() {

        CloseAllScreens();
        workerRegisterUI.SetActive(true);
    }
    public void WorkerScreen() {

        CloseAllScreens();
        workerUI.SetActive(true);
    }
    public void WorkerHomeScreen() {

        CloseWorkerScreens();
        workerHomeUI.SetActive(true);
    }       
}
