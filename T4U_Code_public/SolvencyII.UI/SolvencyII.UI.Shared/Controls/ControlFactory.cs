using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Factory to create controls.
    /// </summary>
    public class ControlFactory
    {

        public static Control CreateControl(string ctrlName, string partialName)
        {
            try
            {
                Control ctrl;
                switch (ctrlName)
                {
                    case "Label":
                        ctrl = new Label();
                        break;
                    case "TextBox":
                        ctrl = new TextBox();
                        break;
                    case "PictureBox":
                        ctrl = new PictureBox();
                        break;
                    case "ListView":
                        ctrl = new ListView();
                        break;
                    case "ComboBox":
                        ctrl = new ComboBox();
                        break;
                    case "Button":
                        ctrl = new Button();
                        break;
                    case "CheckBox":
                        ctrl = new CheckBox();
                        break;
                    case "MonthCalender":
                        ctrl = new MonthCalendar();
                        break;
                    case "DateTimePicker":
                        ctrl = new DateTimePicker();
                        break;
                    default:
                        //Assembly controlAsm = Assembly.LoadWithPartialName(partialName);
                        //Changed to avoid warnings 
                        Assembly controlAsm = System.Reflection.Assembly.Load(partialName);
                        Type controlType = controlAsm.GetType(partialName + "." + ctrlName);
                        ctrl = (Control)Activator.CreateInstance(controlType);
                        break;

                }
                return ctrl;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("create control failed:" + ex.Message);
                return new Control();
            }
        }

        public static void SetControlProperties(Control ctrl, Hashtable propertyList)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ctrl);

            foreach (PropertyDescriptor myProperty in properties)
            {
                if (propertyList.Contains(myProperty.Name))
                {
                    Object obj = propertyList[myProperty.Name];
                    try
                    {
                        myProperty.SetValue(ctrl, obj);
                    }
                    catch (Exception ex)
                    {
                        //do nothing, just continue
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }

                }

            }

        }

        public static Control CloneCtrl(Control ctrl)
        {

            CBFormCtrl cbCtrl = new CBFormCtrl(ctrl);
            Control newCtrl = ControlFactory.CreateControl(cbCtrl.CtrlName, cbCtrl.PartialName);

            ControlFactory.SetControlProperties(newCtrl, cbCtrl.PropertyList);

            return newCtrl;
        }



        #region Clipboard Support
        /// <summary>
        /// Summary description for CBFormCtrl.
        /// </summary>
        [Serializable()]
        public class CBFormCtrl//clipboard form control
        {
            private static DataFormats.Format format;
            private string ctrlName;
            private string partialName;
            private Hashtable propertyList = new Hashtable();

            static CBFormCtrl()
            {
                // Registers a new data format with the windows Clipboard
                format = DataFormats.GetFormat(typeof(CBFormCtrl).FullName);
            }

            public static DataFormats.Format Format
            {
                get
                {
                    return format;
                }
            }
            public string CtrlName
            {
                get
                {
                    return ctrlName;
                }
                set
                {
                    ctrlName = value;
                }
            }

            public string PartialName
            {
                get
                {
                    return partialName;
                }
                set
                {
                    partialName = value;
                }
            }

            public Hashtable PropertyList
            {
                get
                {
                    return propertyList;
                }

            }


            public CBFormCtrl()
            {

            }

            public CBFormCtrl(Control ctrl)
            {
                CtrlName = ctrl.GetType().Name;
                PartialName = ctrl.GetType().Namespace;

                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(ctrl);

                foreach (PropertyDescriptor myProperty in properties)
                {
                    try
                    {
                        if (myProperty.PropertyType.IsSerializable)
                            propertyList.Add(myProperty.Name, myProperty.GetValue(ctrl));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                        //do nothing, just continue
                    }

                }

            }


        }
        #endregion


    }
}
