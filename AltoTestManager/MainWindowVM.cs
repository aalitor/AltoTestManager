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
using Office = Microsoft.Office.Interop;
using MessageBox = System.Windows.MessageBox;
namespace AltoTestManager
{
    class MainWindowVM : INotifyPropertyChanged
    {
        public ObservableCollection<TestProject> TestProjects { get; set; }
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
            readJson();
            if (TestProjects == null)
                TestProjects = new ObservableCollection<TestProject>();
        }

        private void exportTestProjectWord(object obj)
        {
            if (obj == null || !(obj is TestProject))
                return;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Word Document (*.docx) | *.docx";
            var filename = "";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                filename = dialog.FileName;
            }
            else
            {
                return;
            }
            var proj = (TestProject)obj;
            Office.Word.Application ap = new Office.Word.Application();
            object missing = System.Reflection.Missing.Value;
            Office.Word.Document document = ap.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            //document.InlineShapes.AddPicture(@"C:\Users\ermcnnj\Desktop\apple.png");
            var num = 1;
            foreach (var item in proj.TestCases)
            {
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                para1.Range.Text = string.Format("{0}. {1}", num++, item.Description);
                para1.Range.InsertParagraphAfter();
                foreach (var pic in item.ImagePaths)
                {
                    para1.Range.InlineShapes.AddPicture(pic);
                }
                document.Paragraphs.Add(para1);
            }
            document.SaveAs2(filename);
            ((Microsoft.Office.Interop.Word._Document)document).Close();
            document = null;
            ((Microsoft.Office.Interop.Word._Application)ap).Quit(ref missing,ref missing, ref missing);
            ap = null;
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
                    MessageBox.Show("Test senaryosu için açıklama giriniz!");
                    return;
                }
                if (SelectedProject != null &&
                    SelectedProject.TestCases != null &&
                    SelectedProject.TestCases.Any(x => x.Description == caseDesc))
                {
                    MessageBox.Show("Aynı açıklamaya ait test senaryosu zaten mevcut!");
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
            var doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                MessageBox.Show("Proje ismi giriniz");
                return;
            }

            if (TestProjects.Any(x => x.Caption.Equals(projname)))
            {
                MessageBox.Show("Aynı isimli bir proje zaten var, ekleme yapılamaz");
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

        void saveJson()
        {
            var docdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "altotestprojeleri.json";
            var fullpath = Path.Combine(docdir, filename);

            var jsondata = JsonConvert.SerializeObject(TestProjects, Formatting.Indented);
            File.WriteAllText(fullpath, jsondata);
        }
        void readJson()
        {
            var docdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "altotestprojeleri.json";
            var fullpath = Path.Combine(docdir, filename);
            if (File.Exists(fullpath))
                TestProjects = JsonConvert.DeserializeObject<ObservableCollection<TestProject>>(
                    File.ReadAllText(fullpath));
            else
                TestProjects = new ObservableCollection<TestProject>();
        }
    }
}
