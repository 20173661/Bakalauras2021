using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace TrilleonAutomation {

	[AutomationClass]
	public class TestCases : MonoBehaviour {

		[SetUpClass]
		public IEnumerator SetUpClass() {
			yield return null;
		}

		[SetUp]
		public IEnumerator SetUp() {
			yield return null;
		}	

		[Automation("Test Cases")]
		public IEnumerator WorkerLogin() {//TA3

			GameObject parentObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "WorkerButton", false), "Click object with name WorkerButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "LoginButton", false), "Click object with name LoginButton"));
			GameObject middleLevelObject = null;
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "blobas@gmail.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "123456"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Login_Btn", false), "Click object with name Login_Btn"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AddNewJobButton", false), "Click object with name AddNewJobButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToJobsButton", false), "Click object with name BackToJobsButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerLoginDosNotExist() {//TA4

			GameObject parentObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "WorkerButton", false), "Click object with name WorkerButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "LoginButton", false), "Click object with name LoginButton"));
			GameObject middleLevelObject = null;
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "iexist@test.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "idontexist"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Login_Btn", false), "Click object with name Login_Btn"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AddNewJobButton", false), "Click object with name AddNewJobButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToJobsButton", false), "Click object with name BackToJobsButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerRegister() {//TA7

			GameObject parentObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "WorkerButton", false), "Click object with name WorkerButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "RegisterButton", false), "Click object with name RegisterButton"));
			GameObject middleLevelObject = null;
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Username_Input").GetComponent<TMP_InputField>(), "Test"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "test@test.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "test123"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Confirm_Input").GetComponent<TMP_InputField>(), "test123"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Name_Input").GetComponent<TMP_InputField>(), "Vardenis"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Surname_Input").GetComponent<TMP_InputField>(), "Pavardenis"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Identification_Input").GetComponent<TMP_InputField>(), "12345678910"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Number_Input").GetComponent<TMP_InputField>(), "+123456789"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerRegister_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Register_Btn", false), "Click object with name Register_Btn"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AddNewJobButton", false), "Click object with name AddNewJobButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToJobsButton", false), "Click object with name BackToJobsButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerRegisterWithoutInfo() {//TA8

			GameObject parentObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "WorkerButton", false), "Click object with name WorkerButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "RegisterButton", false), "Click object with name RegisterButton"));
			GameObject middleLevelObject = null;
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Username_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Confirm_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Name_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Surname_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Identification_Input").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Number_Input").GetComponent<TMP_InputField>(), ""));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerRegister_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Register_Btn", false), "Click object with name Register_Btn"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkersOrdersList() {//TA13

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "OrdersButton", false), "Click object with name OrdersButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrders_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "WorkerOrderListElement", false), "Click object with name WorkerOrderListElement"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToOrderListButton", false), "Click object with name BackToOrderListButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerCancelOrder() {//TA17

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "OrdersButton", false), "Click object with name OrdersButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrders_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "WorkerOrderListElement", false), "Click object with name WorkerOrderListElement"));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrderInfo_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Button", false), "Click object with name Button"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToOrderListButton", false), "Click object with name BackToOrderListButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerConfirmOrder() {//TA18

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "OrdersButton", false), "Click object with name OrdersButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrders_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "WorkerOrderListElement", false), "Click object with name WorkerOrderListElement"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrderInfo_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Button (1)", false), "Click object with name Button (1)"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToOrderListButton", false), "Click object with name BackToOrderListButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerConfirmOrderDone() {//TA19

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "OrdersButton", false), "Click object with name OrdersButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrders_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "WorkerOrderListElement", false), "Click object with name WorkerOrderListElement"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerOrderInfo_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Button (1)", false), "Click object with name Button (1)"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToOrderListButton", false), "Click object with name BackToOrderListButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerProfileChange() {//TA22

			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "WorkerButton", false), "Click object with name WorkerButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "LoginButton", false), "Click object with name LoginButton"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "test@test.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "test123"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Login_Btn", false), "Click object with name Login_Btn"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "SettingsButton", false), "Click object with name SettingsButton"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Username_Input").GetComponent<TMP_InputField>(), "Pakeistas"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "darbuotojas@test.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "pakeistas123"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Confirm_Input").GetComponent<TMP_InputField>(), "pakeistas123"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Number_Input").GetComponent<TMP_InputField>(), "+10000000"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "ChangeProfileButton", false), "Click object with name ChangeProfileButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "SignOutButton", false), "Click object with name SignOutButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerSettings_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Email_Input").GetComponent<TMP_InputField>(), "darbuotojas@test.com"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(parentObject, By.Name, "Password_Input").GetComponent<TMP_InputField>(), "pakeistas123"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Login_Btn", false), "Click object with name Login_Btn"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));

		}
		[Automation("Test Cases")]
		public IEnumerator WorkerCreateOrder() {//TA23

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AddNewJobButton", false), "Click object with name AddNewJobButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerNewJob_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Title").GetComponent<TMP_InputField>(), "Testing"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Price").GetComponent<TMP_InputField>(), "2"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 0: per valandą", false), "Click object with name Item 0: per valandą"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "City").GetComponent<TMP_InputField>(), "Klaipėda"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Duration").GetComponent<TMP_InputField>(), "5"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: h", false), "Click object with name Item 1: h"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: Santechnikos darbai", false), "Click object with name Item 1: Santechnikos darbai"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: Boileris", false), "Click object with name Item 1: Boileris"));
			yield return StartCoroutine(Q.driver.Scroll(Q.driver.FindIn(parentObject, By.Name, "Scroll View"), 1, true, 100));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "AboutJob").GetComponent<TMP_InputField>(), "Tai yra testavimo atvejis"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Tags").GetComponent<TMP_InputField>(), "Testavimas Testavimo Atvejai"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerNewJob_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "SaveButton", false), "Click object with name SaveButton"));
			yield return StartCoroutine(Q.driver.Scroll(Q.driver.FindIn(parentObject, By.Name, "Scroll View"), 1, true, 0));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Button", false), "Click object with name Button"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerCreateEmptyOrder() {//TA24

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AddNewJobButton", false), "Click object with name AddNewJobButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerNewJob_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Title").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Price").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "City").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Duration").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Blocker", false), "Click object with name Blocker"));
			yield return StartCoroutine(Q.driver.Scroll(Q.driver.FindIn(parentObject, By.Name, "Scroll View"), 1, true, 100));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Blocker", false), "Click object with name Blocker"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Tags").GetComponent<TMP_InputField>(), ""));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerNewJob_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "SaveButton", false), "Click object with name SaveButton"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToJobsButton", false), "Click object with name BackToJobsButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerEditOrder() {//TA26

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "JobsButton", false), "Click object with name JobsButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerJobs_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "EditButton", false), "Click object with name EditButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerEditJob_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Title").GetComponent<TMP_InputField>(), "Testing2"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Price").GetComponent<TMP_InputField>(), "22"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: per dieną", false), "Click object with name Item 1: per dieną"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "City").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Duration").GetComponent<TMP_InputField>(), "10"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: h", false), "Click object with name Item 1: h"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Viewport", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 1: Santechnikos darbai", false), "Click object with name Item 1: Santechnikos darbai"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Blocker", false), "Click object with name Blocker"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "AboutJob").GetComponent<TMP_InputField>(), "Tai yra testavimo atvejis2"));
			yield return StartCoroutine(Q.driver.Scroll(Q.driver.FindIn(parentObject, By.Name, "Scroll View"), 1, true, 100));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "Tags").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Scroll(Q.driver.FindIn(parentObject, By.Name, "Scroll View"), 1, true, 0));
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerEditJob_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "SaveButton", false), "Click object with name SaveButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerJobs_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "JobsElement", false), "Click object with name JobsElement"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "Button", false), "Click object with name Button"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerAgendaTime() {//TA29

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AgendaButton", false), "Click object with name AgendaButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerAgenda_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "EditWorkDaysButton", false), "Click object with name EditWorkDaysButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "FromField (TMP)").GetComponent<TMP_InputField>(), ""));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Panel", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 3: 8", false), "Click object with name Item 3: 8"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Button", false), "Click object with name Button"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "UntilField (TMP) (1)").GetComponent<TMP_InputField>(), ""));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Panel", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 13: 18", false), "Click object with name Item 13: 18"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "Panel", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Item 0: 00", false), "Click object with name Item 0: 00"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Button", false), "Click object with name Button"));
		}
		[Automation("Test Cases")]
		public IEnumerator WorkerAgendaEmpty() {//TA30

			WorkerLogin();
			GameObject parentObject = null;
			GameObject middleLevelObject = null;
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			parentObject = Q.driver.Find(By.Name, "Canvas", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "AgendaButton", false), "Click object with name AgendaButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerAgenda_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "EditWorkDaysButton", false), "Click object with name EditWorkDaysButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Toggle", false), "Click object with name Toggle"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "FromField (TMP)").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "TimePickerPanel", false), "Click object with name TimePickerPanel"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "UntilField (TMP) (1)").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "TimePickerPanel", false), "Click object with name TimePickerPanel"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "SaveButton", false), "Click object with name SaveButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "WorkerAgenda_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "EditWorkDaysButton", false), "Click object with name EditWorkDaysButton"));
			middleLevelObject = Q.driver.FindIn(parentObject, By.Name, "EditWorkDays_UI", false);
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(middleLevelObject, By.Name, "Toggle", false), "Click object with name Toggle"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "FromField (TMP)").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "TimePickerPanel", false), "Click object with name TimePickerPanel"));
			yield return StartCoroutine(Q.driver.SendKeys(Q.driver.FindIn(middleLevelObject, By.Name, "UntilField (TMP) (1)").GetComponent<TMP_InputField>(), ""));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "TimePickerPanel", false), "Click object with name TimePickerPanel"));
			yield return StartCoroutine(Q.driver.Click(Q.driver.FindIn(parentObject, By.Name, "BackToWorkDaysButton", false), "Click object with name BackToWorkDaysButton"));
		}
		[Automation("Test Cases")]
		public IEnumerator Raiting() {//TA35

			double sk1 = 1.1;
			double sk2 = 5;
			double sk3 = 3.5;
			double ats = (sk1 + sk2 + sk3) / (3);
			if (ats == 3.2) {
				yield return Q.assert.Pass("pass");
			}
			else {
				yield return Q.assert.Fail("fail");
			}
		}

		[TearDown]
		public IEnumerator TearDown() {

			yield return null;
		}

		[TearDownClass]
		public IEnumerator TearDownClass() {

			yield return null;
		}
	}
}
