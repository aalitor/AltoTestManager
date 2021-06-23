using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;
using MessageBox = System.Windows.MessageBox;
namespace AltoTestManager
{
    class MainWindowVM : INotifyPropertyChanged
    {
        private Notification notification;
        private bool isModeUpdate;

        public bool IsModeUpdate
        {
            get { return isModeUpdate; }

            set
            {
                isModeUpdate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsModeUpdate"));
            }
        }
        private TestCase selectedTestCaseToUpdate;

        public TestCase SelectedTestCaseToUpdate
        {
            get { return selectedTestCaseToUpdate; }
            set
            {
                selectedTestCaseToUpdate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedTestCaseToUpdate"));
            }
        }

        public Notification Notification
        {
            get { return notification; }
            set
            {
                notification = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Notification"));
            }
        }
        public string DataFolder
        {
            get
            {
                return Properties.Settings.Default.DataFolder;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    MessageBox.Show("Klasör bulunamadı!");
                    return;
                }
                Properties.Settings.Default.DataFolder = value;
                Properties.Settings.Default.Save();
                readJson();
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
        }
        public string JsonPath
        {
            get
            {
                return Path.Combine(DataFolder, "altotestprojeleri.json");
            }
        }
        public ObservableCollection<TestProject> TestProjects { get; set; }
        public RelayCommand SelectDataPath { get; set; }
        public RelayCommand SelectDataFolder { get; set; }

        public RelayCommand CommandChangeTestCase { get; set; }
        public RelayCommand CommandAddNewTestCase { get; set; }
        public RelayCommand CommandAddNewTestProject { get; set; }
        public RelayCommand CommandDeleteTestProject { get; set; }
        private TestProject selectedTestProject;
        private string selectedImagePath;
        private ImageSource imageSource;
        public RelayCommand CommandDeleteSelectedImagePath { get; set; }
        public RelayCommand CommandDeleteSelectedTestCase { get; set; }
        public RelayCommand CommandGetImageFromClipboard { get; set; }
        public RelayCommand CommandExportTestProjectWord { get; set; }
        public RelayCommand CommandNewTestCase { get; set; }
        public RelayCommand CommandCopyImageToClipboard { get; set; }
        public RelayCommand CommandShowLargeImageWindow { get; set; }
        public RelayCommand CommandChangeUpdateMode { get; set; }
        public RelayCommand SelectedItemChangedCommand { get; set; }
        public RelayCommand CommandSaveJson { get; set; }
        public RelayCommand CommandChangeUpdateAddMode { get; set; }
        public RelayCommand CommandTestCaseSelectedChanged { get; set; }
        public ImageSource ImgSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ImgSource"));

            }
        }

        public string SelectedImagePath
        {
            get { return selectedImagePath; }
            set
            {
                selectedImagePath = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedImagePath"));
            }
        }

        public TestProject SelectedProject
        {
            get { return selectedTestProject; }
            set
            {
                selectedTestProject = value;
                IsModeUpdate = false;
                SelectedTestCaseToUpdate = new TestCase("");
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedProject"));

            }
        }
        private TestCase selectedTestCase;
        public TestCase SelectedTestCase
        {
            get
            {
                return selectedTestCase;
            }
            set
            {
                if (value == null)
                    return;
                selectedTestCase = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedTestCase"));

            }
        }
        private string newCaseDescription;

        public string NewCaseDescription
        {
            get { return newCaseDescription; }
            set
            {
                newCaseDescription = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NewCaseDescription"));
            }
        }

        public MainWindowVM()
        {
            TestProjects = new ObservableCollection<TestProject>();
            CommandDeleteTestProject = new RelayCommand(new Action<object>(deleteTestProject));
            CommandChangeTestCase = new RelayCommand(new Action<object>(changeTestCaseStatus));
            CommandAddNewTestCase = new RelayCommand(new Action<object>(addNewTestCase));
            CommandAddNewTestProject = new RelayCommand(new Action<object>(addNewTestProject));
            CommandDeleteSelectedImagePath = new RelayCommand(new Action<object>(deleteSelectedImagePath));
            CommandDeleteSelectedTestCase = new RelayCommand(new Action<object>(deleteSelectedTestCase));
            CommandGetImageFromClipboard = new RelayCommand(new Action<object>(getImageFromClipboard));
            CommandExportTestProjectWord = new RelayCommand(new Action<object>(exportTestProjectWord));
            CommandNewTestCase = new RelayCommand(new Action<object>(createNewTestCase));
            CommandCopyImageToClipboard = new RelayCommand(new Action<object>(copyImageToClipboard));
            CommandShowLargeImageWindow = new RelayCommand(new Action<object>(showLargeImageWindow));
            CommandChangeUpdateMode = new RelayCommand(new Action<object>(changeUpdateMode));
            CommandSaveJson = new RelayCommand(new Action<object>(saveJson));
            CommandTestCaseSelectedChanged = new RelayCommand(new Action<object>(testcaseSelectedChanged));
            SelectedItemChangedCommand = new RelayCommand(new Action<object>((x) =>
            {
                var lv = (System.Windows.Controls.ListView)x;
                lv.SelectedIndex = 0;
            }));
            SelectDataFolder = new RelayCommand(new Action<object>(selectDataFolder));
            Notification = new AltoTestManager.Notification() { Text = "", Type = 0 };
            readJson();
            if (TestProjects == null)
                TestProjects = new ObservableCollection<TestProject>();
            if (string.IsNullOrEmpty(DataFolder))
                DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void selectDataFolder(object obj)
        {
            using (var ofd = new FolderBrowserDialog())
            {
                ofd.RootFolder = Environment.SpecialFolder.Desktop;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    DataFolder = ofd.SelectedPath;
                }
            }
        }

        private void testcaseSelectedChanged(object obj)
        {
            var arr = (Tuple<TestCase, System.Windows.Controls.ListView>)obj;
            SelectedTestCaseToUpdate = arr.Item1;
            var lv = arr.Item2;
            lv.SelectedItem = SelectedTestCaseToUpdate;
            IsModeUpdate = true;
        }
        private void changeUpdateMode(object obj)
        {
            IsModeUpdate = false;
            SelectedTestCaseToUpdate = new TestCase("");
        }

        private void showLargeImageWindow(object obj)
        {
            if (obj is string)
            {
                var imgpath = (string)obj;
                if (!File.Exists(imgpath))
                {
                    Notification = new AltoTestManager.Notification()
                    {
                        Text = "Görsel dosyası yerinde bulunamadı!",
                        Type = -1
                    };
                    return;
                }
                var largeImageWindow = new LargeImageDisplayerWindow(imgpath);
                largeImageWindow.ShowDialog();
            }
        }

        private void copyImageToClipboard(object obj)
        {
            if (obj is string)
            {
                var img = (string)obj;
                if (File.Exists(img))
                {
                    System.Windows.Forms.Clipboard.SetImage(Image.FromFile(img));
                }
            }
        }

        private void createNewTestCase(object obj)
        {
            if (obj is System.Windows.Controls.ListView)
            {
                var lv = (System.Windows.Controls.ListView)obj;
                lv.SelectedItem = null;
                lv.SelectedIndex = -1;
            }
        }

        private void exportTestProjectWord(object obj)
        {
            try
            {


                if (obj == null || !(obj is TestProject))
                    return;

                var proj = (TestProject)obj;

                object oMissing = System.Reflection.Missing.Value; object oEndOfDoc = "\\endofdoc";

                Word._Application oWord;
                Word._Document oDoc = new Word.Document();
                oWord = new Word.Application();
                oWord.Visible = false;
                oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                var num = 1;
                foreach (var item in proj.TestCases)
                {
                    Word.Paragraph oPara1;
                    oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara1.Range.Text = string.Format("{0}. {1}", num++, item.Description);
                    oPara1.Range.InsertParagraphAfter();
                    foreach (var pic in item.ImagePaths)
                    {
                        oPara1.Range.InlineShapes.AddPicture(pic);
                    }
                    oPara1.Range.InsertParagraphAfter();
                }
                oWord.Visible = true;

                //oDoc.SaveAs(filename);
                //((Microsoft.Office.Interop.Word._Document)oDoc).Close();
                //oDoc = null;
                //((Microsoft.Office.Interop.Word._Application)oWord).Quit(ref oMissing, ref oMissing, ref oMissing);
                //oWord = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
        }

        private void getImageFromClipboard(object obj)
        {
            if (System.Windows.Clipboard.ContainsImage())
            {
                // ImageUIElement.Source = Clipboard.GetImage(); // does not work
                System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                if (clipboardData != null)
                {
                    if (clipboardData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                    {
                        System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)clipboardData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                        addNewImage(
                            System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()));

                    }
                }
            }
        }

        private void deleteSelectedTestCase(object obj)
        {
            if (obj is TestCase)
            {
                var selected = (TestCase)obj;
                if (selected != null)
                {
                    SelectedProject.TestCases.RemoveAll(x => x.Description == selected.Description);
                }
            }
            saveJson();
        }

        void changeTestCaseStatus(object parameter)
        {
            if (parameter is TestCase)
            {
                var testcase = (TestCase)parameter;
                if (testcase != null)
                {
                    var num = (int)testcase.CaseStatus;
                    testcase.CaseStatus = (TestCaseStatus)((num + 1) % 3);
                }
            }
            saveJson();
        }
        void deleteSelectedImagePath(object ss)
        {
            var selectedItem = (string)ss;
            if (selectedItem != null)
            {
                SelectedTestCase.ImagePaths.Remove(selectedItem);
                File.Delete(selectedItem);
                saveJson();
            }
        }

        void addNewTestCase(object parameter)
        {
            if (parameter is string)
            {
                var caseDesc = (string)parameter;
                if (string.IsNullOrEmpty(caseDesc))
                {
                    Notification.Text = "Test senaryosu için açıklama giriniz";
                    Notification.Type = -1;
                    return;
                }
                if (SelectedProject != null &&
                    SelectedProject.TestCases != null &&
                    SelectedProject.TestCases.Any(x => x.Description == caseDesc))
                {
                    Notification.Text = "Aynı açıklamaya ait test senaryosu zaten mevcut!";
                    Notification.Type = -1;
                    return;
                }
                var testcase = new TestCase(caseDesc, TestCaseStatus.Untested);
                SelectedProject.TestCases.Add(testcase);
                saveJson();
            }
        }
        public void addNewImage(ImageSource img)
        {
            if (SelectedProject != null && SelectedTestCase != null)
            {
                var imgid = Guid.NewGuid().ToString();
                var projdir = checkProjectFolder();
                var imgpath = Path.Combine(projdir, imgid + ".jpg");
                SaveClipboardImageToFile(img, imgpath);
                selectedTestCase.ImagePaths.Add(imgpath);
                saveJson();
                ImgSource = null;
            }
        }
        public void SaveClipboardImageToFile(ImageSource img, string filePath)
        {
            //var image = Clipboard.GetImage();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Frames.Add(BitmapFrame.Create(img as BitmapSource));
                encoder.Save(fileStream);
            }
        }
        string checkProjectFolder()
        {
            var doc = DataFolder;
            var projdir = Path.Combine(doc, SelectedProject.Caption);
            if (!Directory.Exists(projdir))
            {
                Directory.CreateDirectory(projdir);
            }
            return projdir;
        }
        void addNewTestProject(object parameter)
        {
            var projname = (string)parameter;

            if (string.IsNullOrEmpty(projname))
            {
                Notification.Text = "Proje ismi giriniz";
                Notification.Type = -1;
                return;
            }

            if (TestProjects.Any(x => x.Caption.Equals(projname)))
            {
                Notification.Text = "Aynı isimli bir proje zaten var, ekleme yapılamaz";
                Notification.Type = -1;
                return;
            }
            var proj = new TestProject(projname);
            TestProjects.Add(proj);
            MessageBox.Show(string.Format("{0} projesi eklendi", projname));
            projname = "";
            saveJson();

        }

        void deleteTestProject(object parameter)
        {
            if (parameter is TestProject)
            {
                var selectedProj = (TestProject)parameter;
                if (selectedProj != null)
                {
                    var capt = selectedProj.Caption;
                    TestProjects.RemoveAll(x => x.Caption == capt);
                    MessageBox.Show(string.Format("{0} projesi silindi", capt));
                }
            }
            saveJson();

        }
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        void saveJson(object obj = null)
        {
            var jsondata = JsonConvert.SerializeObject(TestProjects, Formatting.Indented);
            File.WriteAllText(JsonPath, jsondata);
        }
        void readJson()
        {
            if (File.Exists(JsonPath))
                TestProjects = JsonConvert.DeserializeObject<ObservableCollection<TestProject>>(
                    File.ReadAllText(JsonPath));
            else
                TestProjects = new ObservableCollection<TestProject>();
        }


    }
}
