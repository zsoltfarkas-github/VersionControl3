﻿using otodikfeladat.Entities;
using otodikfeladat.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace otodikfeladat
{
    public partial class Form1 : Form
    {

        BindingList<RateData> Rates = new BindingList<RateData>();
        public string result2;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Rates;
            hivas();
            feldolgozas();
        }

        void hivas()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            result2 = result;
        }

        void feldolgozas()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result2);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;

                Rates.Add(rate);
            }
        }
    }
}
