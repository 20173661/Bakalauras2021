using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour {
    
    public GameObject _calendarPanel;
    public GameObject ClientSservice_UI;
    public Text _yearNumText;
    public Text _monthNumText;
    public GameObject _item;
    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;
    private DateTime _dateTime;
    public static CalendarController _calendarInstance;
    TMP_InputField _target;

    void Start() {

        _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++) {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;

            float ItemWidth = item.transform.GetComponent<RectTransform>().sizeDelta.x;
            float ItemHeight = item.transform.GetComponent<RectTransform>().sizeDelta.y;

            item.transform.localPosition = new Vector3((i % 7) * (ItemWidth + 1) + startPos.x, startPos.y - (i / 7) * (ItemHeight + 1), startPos.z);

            _dateItems.Add(item);
        }
        _dateTime = DateTime.Now;
        CreateCalendar();
        _calendarPanel.SetActive(false);
    }

    void CreateCalendar() {

        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++) {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false);

            if (i >= index) {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month) {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();
                    date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString();
    }

    int GetDays(DayOfWeek day) {

        switch (day) {
            case DayOfWeek.Monday: return 0;
            case DayOfWeek.Tuesday: return 1;
            case DayOfWeek.Wednesday: return 2;
            case DayOfWeek.Thursday: return 3;
            case DayOfWeek.Friday: return 4;
            case DayOfWeek.Saturday: return 5;
            case DayOfWeek.Sunday: return 6;
        }
        return 0;
    }
    public void YearPrev() {

        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext() {

        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev() {

        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext() {

        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(TMP_InputField target) {

        _calendarPanel.SetActive(true);
        _target = target;
    }
    public void OnDateItemClick(string day) {
        _target.text = _yearNumText.text + ":" + _monthNumText.text + ":" + day;
        _calendarPanel.SetActive(false);
        ClientSservice_UI.GetComponent<ServiceElement>().selectedDate(_target.text);
    }
}
