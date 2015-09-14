﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;

namespace WageCalculation.Models
{
    public class WageCalculatorModel
    {
        public String time { get; set; }
        public String wage { get; set; }
        public String tax { get; set; }

        public String incomeAfterTax(String wage, String time, String tax)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            double income = 0.000;
            double taxPercent = 0.00;

            if (wage.Contains(','))
            {
                wage = wage.Replace(',', '.');
            }
            if (tax.Contains(','))
            {
                tax = tax.Replace(',', '.');
            }

            taxPercent = double.Parse(tax) / 100;
            income = (double.Parse(wage) * parseIntoDouble(time));
            income-= income * taxPercent;
            
            return "" + income;
        }

        
        public String incomeBeforeTax(String wage, String time)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            double income = 0.000;

            if (wage.Contains(','))
            {
                wage = wage.Replace(',', '.');
            }

            income = double.Parse(wage) * parseIntoDouble(time);
            
            return "" + income;
        }

        public double parseIntoDouble(String time)
        {
            double result = 0.00;

            double minutes = 0;
            int hours = 0;

            minutes = double.Parse(time.Split(':')[1])/60;
            hours = int.Parse(time.Split(':')[0]);

            result = hours + minutes;
            return result;
        }

        public string totalHours(string time)
        {
            
            var Days = time.Split(';');// 9:00 + 12:00 / 8:30 + 16:00 

            int hours = 0;
            int minutes = 0;

            foreach(string day in Days){
                hours+= int.Parse(dayHours(day).Split(':')[0]); 
                minutes += int.Parse(dayHours(day).Split(':')[1]);
            }

            hours = hours + (int)(minutes / 60);
            minutes = (int)(minutes % 60);
            if (minutes < 10)
            {
                time = hours + ":0" + minutes;
            }
            else
            time = hours + ":" + minutes;

            return time;
        }

        public String dayHours(String day)
        {
            var Day = day.Split('+');
            var startHour = Day[0].Split(':')[0];//9
            var startMinute = Day[0].Split(':')[1];//02

            var endHour = Day[1].Split(':')[0];//12
            var endMinute = Day[1].Split(':')[1];//00

            var totalHours = int.Parse(endHour)- int.Parse(startHour);
            var totalMinutes = int.Parse(endMinute) - int.Parse(startMinute);

            if(totalMinutes < 0)
            {
                totalHours--;
                totalMinutes = totalMinutes + 60;
            }
            return "" + totalHours + ':' + totalMinutes;
        }
    }

    

}

