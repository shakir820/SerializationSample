using SerializationSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SerializationSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var p = new Person();
                p.Name = NameTextBox.Text;
                p.Address = AddressTextBox.Text;
                var localFolder = ApplicationData.Current.LocalFolder;
                FileStream writer = new FileStream(localFolder.Path + @"\gg.xml", FileMode.Create, FileAccess.Write);
                DataContractSerializer ser = new DataContractSerializer(typeof(Person));
                ser.WriteObject(writer, p);
                writer.Close();
            }
            catch(Exception ex)
            {

            }
            
        }

        private void ReadBtn_Click(object sender, RoutedEventArgs e)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            if (!File.Exists(localFolder.Path + @"\gg.xml"))
            {
                return;
            }
            
            FileStream fs = new FileStream(localFolder.Path + @"\gg.xml", FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(Person));

            // Deserialize the data and read it from the instance.
            Person deserializedPerson = (Person)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();

            NameTextBox.Text = deserializedPerson.Name;
            AddressTextBox.Text = deserializedPerson.Address;
        }
    }
}
