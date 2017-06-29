using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.ObjectModel;
using System.Collections;
using Microsoft.Win32;

using System.ComponentModel;
using System.Drawing;


namespace xmlFirst {
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window {
        public MainWindow() {
            InitializeComponent();
           
        }

        private void button_Click(object sender,RoutedEventArgs e)
        {


            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\aeprakti\Desktop\xmlFirst\Sipan.xml");



            Image dfg = new Image();
           

            XmlElement pereon = doc.CreateElement("Person");

            XmlElement id = doc.CreateElement("Id");
            id.InnerText=tbId.Text;

            XmlElement first = doc.CreateElement("First");
            first.InnerText=tbFirst.Text;

            XmlElement last = doc.CreateElement("Last");
            last.InnerText=tbLast.Text;

            XmlElement tel = doc.CreateElement("Tel");
            tel.InnerText=tbTel.Text;

            XmlElement image = doc.CreateElement("Image");
            image.InnerText = tbImage.Text;



            pereon.AppendChild(id);
            pereon.AppendChild(first);
            pereon.AppendChild(last);
            pereon.AppendChild(tel);
            pereon.AppendChild(image);

            doc.DocumentElement.AppendChild(pereon);

            doc.Save(@"C:\Users\aeprakti\Desktop\xmlFirst\Sipan.xml");
            dataGrid.Items.Refresh();
            MessageBox.Show("Created");



        }


        public void btnShow_Click(object sender,RoutedEventArgs e) 
        {
            List<Person> p1 = new List<Person>();
 

            XmlSerializer serial = new XmlSerializer(typeof(List<Person>));
            using(FileStream fs = new FileStream(@"C:\Users\aeprakti\Desktop\xmlFirst\Sipan.xml", FileMode.Open,FileAccess.Read))
            {
                p1=serial.Deserialize(fs) as List<Person>;
                fs.Close();

            }

            dataGrid.ItemsSource=p1;

            

        }

        private void btnRemove_Click(object sender,RoutedEventArgs e)
        {

            Person per = (Person)dataGrid.SelectedItem;
            int idd = per.Id;
            string fir = per.First.ToString();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\aeprakti\Desktop\xmlFirst\xmlFirst\bin\Debug\Sipan.xml");

            foreach(XmlNode item in doc.SelectNodes("ArrayOfPerson/Person")) {
                if(item.SelectSingleNode("Id").InnerText == idd.ToString()) {
                    item.ParentNode.RemoveChild(item);
                    MessageBox.Show(fir + " Deleted");
                }
            }

            doc.Save(@"C:\Users\aeprakti\Desktop\xmlFirst\xmlFirst\bin\Debug\Sipan.xml");
            UPD();
            

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Person per = (Person)dataGrid.SelectedItem;
            int idd = per.Id;
            string fir = per.First.ToString();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\aeprakti\Desktop\xmlFirst\Sipan.xml");

            foreach (XmlNode item in doc.SelectNodes("ArrayOfPerson/Person"))
            {
                if (item.SelectSingleNode("Id").InnerText == idd.ToString())
                {
                    item.SelectSingleNode("First").InnerText = tbUpdate.Text;
                   
                    MessageBox.Show(fir + " Updated!");
                }
            }
            doc.Save(@"C:\Users\aeprakti\Desktop\xmlFirst\Sipan.xml");

        }

        void UPD()
        {
            List<Person> p1 = new List<Person>();
            XmlSerializer serial = new XmlSerializer(typeof(List<Person>));
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\Sipan.xml", FileMode.Open, FileAccess.Read))
            {
                p1 = serial.Deserialize(fs) as List<Person>;
            }
            dataGrid.ItemsSource = p1;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            tbImage.Text = ofd.FileName;            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Person per = (Person)dataGrid.SelectedItem;
            MessageBox.Show(per.Image);
        }
    }
}
