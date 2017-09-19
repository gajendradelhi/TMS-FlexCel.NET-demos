﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using FlexCel.Report;
using FlexCel.Demo.SharedData;


namespace UserDefinedFunctions
{
    public partial class mainForm: System.Windows.Forms.Form
    {

        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            AutoRun();
        }

        public void AutoRun()
        {
            using (FlexCelReport ordersReport = SharedData.CreateReport())
            {
                ordersReport.SetValue("Date", DateTime.Now);
                ordersReport.SetUserFunction("Orders", new OrdersImp(SharedData.GetOrders()));

                string DataPath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".."), "..") + Path.DirectorySeparatorChar;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ordersReport.Run(DataPath + "User Defined Functions.template.xls", saveFileDialog1.FileName);

                    if (MessageBox.Show("Do you want to open the generated file?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Process.Start(saveFileDialog1.FileName);
                    }
                }
            }
        }


        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

    }

    #region Orders Implementation
    class OrdersImp: TFlexCelUserFunction
    {
        DataView OrdersDv;

        public OrdersImp(DataTable OrdersTable)
        {
            OrdersDv = new DataView(OrdersTable, String.Empty, "EmployeeID,ShipVia", DataViewRowState.CurrentRows);
        }
        public override object Evaluate(object[] parameters)
        {
            if (parameters == null || parameters.Length != 2)
                throw new ArgumentException("Bad parameter count in call to Orders() user-defined function");

            int Count = OrdersDv.FindRows(parameters).Length;
            if (Count > 0) return Count; else return null;
        }
    }
    #endregion

}
