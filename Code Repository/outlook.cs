using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace Code_Repository
{
    public partial class outlook : Form
    {
        public outlook()
        {
            InitializeComponent();

            
        }

        bool accessmail( )
        {
            try
                


            {
                string lblSubject,lblAttachmentName,txtBody,lblSenderName,lblSenderEmail,lblCreationdate,searchcrit;
                DataTable inb = new DataTable() ;
                int MNUMB =0;
                Microsoft.Office.Interop.Outlook.Application myApp = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.NameSpace mapiNameSpace = myApp.GetNamespace("MAPI");
                Microsoft.Office.Interop.Outlook.MAPIFolder myInbox = mapiNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);







                if (myInbox.Items.Count > 0)
                {
                    // Grab the Subject
                    lblSubject = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).Subject;
                    //Grab the Attachment Name
                    if (((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).Attachments.Count > 0)
                    {
                        lblAttachmentName = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).Attachments[1].FileName;
                    }
                    else
                    {
                        lblAttachmentName  = "No Attachment";
                    }
                    // Grab the Body
                    txtBody = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).Body;
                    // Sender Name
                    lblSenderName = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).SenderName;
                    // Sender Email
                    lblSenderEmail = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).SenderEmailAddress;
                    // Creation date
                    lblCreationdate = ((Microsoft.Office.Interop.Outlook.MailItem)myInbox.Items[myInbox.Items.Count]).CreationTime.ToString();
                }
                else
                {
                    MessageBox.Show("There are no emails in your Inbox.");
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;

            }
        
        }


        // this is the method I have been using
public static DataTable ConvertTo<T>(IList<T> list)
{
    DataTable table = CreateTable<T>();
    Type entityType = typeof(T);
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

    foreach (T item in list)
    {
        DataRow row = table.NewRow();

        foreach (PropertyDescriptor prop in properties)
        {
            row[prop.Name] = prop.GetValue(item);
        }

        table.Rows.Add(row);
    }

    return table;
}    

public static DataTable CreateTable<T>()
{
    Type entityType = typeof(T);
    DataTable table = new DataTable(entityType.Name);
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

    foreach (PropertyDescriptor prop in properties)
    {
        // HERE IS WHERE THE ERROR IS THROWN FOR NULLABLE TYPES
        table.Columns.Add(prop.Name, prop.PropertyType);
    }

    return table;
}
}





        private void button1_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Outlook.Application myApp = new Microsoft.Office.Interop.Outlook.ApplicationClass();

              accessmail();

        }

        

    }
}
