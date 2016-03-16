using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolvencyII.Domain.ENumerators;
using SolvencyII.UI.Shared.Controls;

namespace Tests
{
    [TestClass]
    public class SolvencyIICurrencyTextBoxTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            SolvencyCurrencyTextBox cntrl = new SolvencyCurrencyTextBox();
            cntrl.ColumnType = SolvencyDataType.Percentage;
            cntrl.Result = 0.23;
            Assert.AreEqual("23.00 %", cntrl.Text);
            cntrl.Result = 0.23456789;
            Assert.AreEqual("23.46 %~", cntrl.Text);

        }

        [TestMethod]
        public void TestMethod2()
        {
            SolvencyCurrencyTextBox cntrl = new SolvencyCurrencyTextBox();
            cntrl.ColumnType = SolvencyDataType.Percentage;
            cntrl.OnGotFocusStub(new EventArgs { });
            cntrl.Text = "23";
            cntrl.OnLostFocusStub(new EventArgs { });
           
            Assert.AreEqual("0.23", cntrl.Result.ToString());
            Assert.AreEqual("23.00 %", cntrl.Text);

            cntrl.OnGotFocusStub(new EventArgs { });
            cntrl.OnLostFocusStub(new EventArgs { });

            Assert.AreEqual("0.23", cntrl.Result.ToString());
            Assert.AreEqual("23.00 %", cntrl.Text);


            cntrl.Text = "23.456789";
            cntrl.OnLostFocusStub(new EventArgs { });
            Assert.AreEqual("23.46 %~", cntrl.Text);

        }


    }
}
