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

        private string cloneButtonContent;

        public string CloneButtonContent
        {
            get { return cloneButtonContent; }
            set
            {
                cloneButtonContent = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CloneButtonContent"));
            }
        }


        public ObservableCollection<TestProject> EditTestProjects { get; set; }
        public Stretch SelectedStretch
        {
            get { return (Stretch)Properties.Settings.Default.StretchType; }
            set
            {
                Properties.Settings.Default.StretchType = (int)value;
                Properties.Settings.Default.Save();
            }
        }
        private bool isTestEnvironment;
        private bool isPreprodEnvironment;
        private string updaterPath;

        public string UpdaterPath
        {
            get { return Properties.Settings.Default.UpdaterPath; }
            set
            {
                Properties.Settings.Default.UpdaterPath = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool IsPreprodEnvironment
        {
            get { return isPreprodEnvironment; }
            set
            {
                isPreprodEnvironment = value;
                updateTestProjectsByEnv();
                if (isPreprodEnvironment)
                    CloneButtonContent = "Test Ortamına Klonla";
                PropertyChanged(this, new PropertyChangedEventArgs("IsPreprodEnvironment"));
            }
        }

        public bool IsTestEnvironment
        {
            get { return isTestEnvironment; }
            set
            {
                isTestEnvironment = value;
                if (isTestEnvironment)
                    CloneButtonContent = "Preprod Ortamına Klonla";
                updateTestProjectsByEnv();
                PropertyChanged(this, new PropertyChangedEventArgs("IsTestEnvironment"));
            }
        }

        private bool addIsTestEnvironment;
        private bool addIsPreprodEnvironment;

        public bool AddIsPreprodEnvironment
        {
            get { return addIsPreprodEnvironment; }
            set
            {
                addIsPreprodEnvironment = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AddIsPreprodEnvironment"));
                updateEditTestProjectsByEnv();
            }
        }

        public bool AddIsTestEnvironment
        {
            get { return addIsTestEnvironment; }
            set
            {
                addIsTestEnvironment = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AddIsTestEnvironment"));
                updateEditTestProjectsByEnv();
            }
        }

        public List<Stretch> StretchEnumList
        {
            get
            {
                var list = Enum.GetValues(typeof(Stretch)).Cast<Stretch>().ToList();
                return list;
            }
        }

        public bool IsModeUpdate
        {
            get { return isModeUpdate; }

            set
            {
                isModeUpdate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsModeUpdate"));
            }
        }

        public bool WordOpenSaveAsDialog
        {
            get { return Properties.Settings.Default.WordOpenSaveAsDialog; }
            set
            {
                if (value != Properties.Settings.Default.WordOpenSaveAsDialog)
                {
                    Properties.Settings.Default.WordOpenSaveAsDialog = value;
                    Properties.Settings.Default.Save();
                    PropertyChanged(this, new PropertyChangedEventArgs("WordOpenSaveAsDialog"));
                }
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
        public ObservableCollection<TestProject> TestProjectsByEnv { get; set; }
        public RelayCommand SelectDataPath { get; set; }
        public RelayCommand SelectDataFolder { get; set; }
        public RelayCommand CommandAddNewLine { get; set; }
        public RelayCommand CommandChangeTestCase { get; set; }
        public RelayCommand CommandAddNewTestCase { get; set; }
        public RelayCommand CommandAddNewTestProject { get; set; }
        public RelayCommand UpdateProgramCommand { get; set; }
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
        public RelayCommand CommandCloneTestProject { get; set; }
        public RelayCommand CommandCopySelectedTestCaseText { get; set; }
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
                SelectedTestCaseToUpdate = new TestCase("", "");
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
            TestProjectsByEnv = new ObservableCollection<TestProject>();
            EditTestProjects = new ObservableCollection<TestProject>();
            readJson();
            IsTestEnvironment = true;
            AddIsTestEnvironment = true;
            SelectedStretch = Stretch.Uniform;
            CommandCloneTestProject = new RelayCommand(new Action<object>(cloneTestProject));
            CommandCopySelectedTestCaseText = new RelayCommand(new Action<object>(copySelectedTestCaseText));
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
            CommandAddNewLine = new RelayCommand(new Action<object>(commandAddNewLineAction));
            UpdateProgramCommand = new RelayCommand(new Action<object>(updateProgramCommandAction));
            PropertyChanged += MainWindowVM_PropertyChanged;
            SelectedItemChangedCommand = new RelayCommand(new Action<object>((x) =>
            {
                var lv = (System.Windows.Controls.ListView)x;
                lv.SelectedIndex = 0;
            }));
            SelectDataFolder = new RelayCommand(new Action<object>(selectDataFolder));
            Notification = new AltoTestManager.Notification() { Text = "", Type = 0 };
            if (TestProjects == null)
                TestProjects = new ObservableCollection<TestProject>();
            if (string.IsNullOrEmpty(DataFolder))
                DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            updateTestProjectsByEnv();
            updateEditTestProjectsByEnv();
        }

        void MainWindowVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            saveJson();
        }

        private void updateProgramCommandAction(object obj)
        {
            Process.Start(UpdaterPath);
            System.Windows.Application.Current.Shutdown();
        }

        private void commandAddNewLineAction(object obj)
        {
            var txtbox = (System.Windows.Controls.TextBox)obj;
            txtbox.AppendText(Environment.NewLine);
            txtbox.CaretIndex = Int32.MaxValue;
        }
        void cloneTestProject(object obj)
        {
            if (obj != null && obj is TestProject)
            {
                var sel = (TestProject)obj;
                var cloneproj = new TestProject(sel.Caption);
                foreach (var testcase in sel.TestCases)
                {
                    var clonecase = new TestCase(testcase.Description, testcase.TestData, TestCaseStatus.Untested);
                    cloneproj.TestCases.Add(clonecase);
                }
                cloneproj.IsTestEnvironment = IsPreprodEnvironment;
                cloneproj.IsPreprodEnvironment = IsTestEnvironment;
                addNewTestProject(cloneproj);
            }
        }
        void copySelectedTestCaseText(object obj)
        {
            if (obj == null)
            {
                Notification.Text = "Seçili bir senaryo bulunmamakta";
                Notification.Type = -1;
                return;
            }
            if (obj is TestCase)
            {
                var sel = (TestCase)obj;
                System.Windows.Forms.Clipboard.SetText(sel.Description);
                Notification.Text = "Test senaryosu metni kopyalandı";
                Notification.Type = 1;
            }
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
            if (arr.Item1 == null)
            {
                Notification.Text = "Seçili bir senaryo bulunmamakta";
                Notification.Type = -1;
                return;
            }
            SelectedTestCaseToUpdate = arr.Item1;
            var lv = arr.Item2;
            lv.SelectedItem = SelectedTestCaseToUpdate;
            IsModeUpdate = true;
        }
        private void changeUpdateMode(object obj)
        {
            IsModeUpdate = false;
            SelectedTestCaseToUpdate = new TestCase("", "");
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
                var largeImageWindow = new LargeImageDisplayerWindow(imgpath, SelectedStretch);
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
                {
                    MessageBox.Show("Proje seçiniz!");
                    return;
                }

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
                    var oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);

                    var textcolor = item.CaseStatus == TestCaseStatus.Failed ? Word.WdColorIndex.wdRed :
                        item.CaseStatus == TestCaseStatus.Success ? Word.WdColorIndex.wdGreen
                        : Word.WdColorIndex.wdBlack;

                    oPara1.Range.Text = string.Format("Senaryo {0}. {1} ", num++,
                        item.CaseStatus == TestCaseStatus.Failed ? "Başarısız" :
                        item.CaseStatus == TestCaseStatus.Success ? "Başarılı" : "Test Edilmedi");
                    oPara1.Range.Font.ColorIndex = textcolor;
                    oPara1.Range.InsertParagraphAfter();

                    var oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara2.Range.Font.ColorIndex = Word.WdColorIndex.wdBlack;
                    oPara2.Range.Text = string.Format("{0}", item.Description);

                    oPara2.Range.InsertParagraphAfter();

                    oPara2.Range.Text = item.TestData;

                    foreach (var pic in item.ImagePaths)
                    {
                        if (IsValidPath(pic, false) && File.Exists(pic))
                        {
                            oPara2.Range.InsertParagraphAfter();
                            oPara2.Range.InlineShapes.AddPicture(pic);
                        }
                    }
                    oPara2.Range.InsertParagraphAfter();
                    oPara2.Range.InsertParagraphAfter();
                    oPara2.Range.InsertParagraphAfter();
                    oPara2.Range.InsertParagraphAfter();
                }
                oWord.Visible = !WordOpenSaveAsDialog;
                if (!WordOpenSaveAsDialog)
                {

                    oDoc.Activate();
                    oWord.Activate();
                }
                if (WordOpenSaveAsDialog)
                {
                    using (var ofd = new SaveFileDialog())
                    {
                        ofd.Filter = "Word documents (*.docx) | *.docx";
                        ofd.RestoreDirectory = true;
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            oDoc.SaveAs(ofd.FileName);
                            ((Microsoft.Office.Interop.Word._Document)oDoc).Close();
                            oDoc = null;
                            ((Microsoft.Office.Interop.Word._Application)oWord).Quit(ref oMissing, ref oMissing, ref oMissing);
                            oWord = null;
                        }
                    }
                    Notification.Text = "Dosya kaydedildi";
                    Notification.Type = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
            }
        }
        private bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
        private void getImageFromClipboard(object obj)
        {
            if (System.Windows.Clipboard.ContainsImage())
            {
                if (SelectedProject == null)
                {
                    Notification.Text = "Görseli eklemek için proje seçmeniz gerekiyor";
                    Notification.Type = -1;
                    return;
                }
                if (SelectedTestCase == null)
                {
                    Notification.Text = "Görseli eklemek için test senaryosu seçmeniz gerekiyor";
                    Notification.Type = -1;
                    return;
                }
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

            else
            {
                Notification.Text = "Panoda kopyalanmış görsel bulunamadı";
                Notification.Type = -1;
                return;
            }
        }

        private void deleteSelectedTestCase(object obj)
        {
            if (obj == null)
            {
                Notification.Text = "Seçili bir senaryo bulunmamakta";
                Notification.Type = -1;
                return;
            }
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
            try
            {
                var selectedItem = (string)ss;
                if (selectedItem != null)
                {
                    File.Delete(selectedItem);
                    SelectedTestCase.ImagePaths.Remove(selectedItem);
                    saveJson();
                }
            }
            catch (Exception ex)
            {
                Notification.Text = ex.Message;
                Notification.Type = -1;
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
                var testcase = new TestCase(caseDesc, "", TestCaseStatus.Untested);
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
            if (parameter is string)
            {
                var projname = (string)parameter;

                if (string.IsNullOrEmpty(projname))
                {
                    Notification.Text = "Proje ismi giriniz";
                    Notification.Type = -1;
                    return;
                }

                if (EditTestProjects.Any(x => x.Caption.Equals(projname)))
                {
                    MessageBox.Show(string.Format(
                        "Aynı isimli bir proje {0} ortamında zaten var, ekleme yapılamaz",
                        AddIsTestEnvironment ? "Test" : "Preprod"));
                    return;
                }

                foreach (var item in Path.GetInvalidPathChars())
                {
                    projname = projname.Replace(item.ToString(), "");
                }

                foreach (var item in Path.GetInvalidFileNameChars())
                {
                    projname = projname.Replace(item.ToString(), "");
                }
                var proj = new TestProject(projname);
                proj.IsTestEnvironment = AddIsTestEnvironment;
                proj.IsPreprodEnvironment = AddIsPreprodEnvironment;
                TestProjects.Add(proj);
                MessageBox.Show(string.Format("{0} projesi eklendi", projname));
                projname = "";
                saveJson();
            }
            else if (parameter is TestProject)
            {
                var proj = (TestProject)parameter;
                var projname = proj.Caption;

                if (string.IsNullOrEmpty(projname))
                {
                    Notification.Text = "Proje ismi giriniz";
                    Notification.Type = -1;
                    return;
                }

                if (TestProjects.Any(x => x.Caption.Equals(projname) &&
                    x.IsPreprodEnvironment == proj.IsPreprodEnvironment &&
                    x.IsTestEnvironment == proj.IsTestEnvironment))
                {
                    MessageBox.Show(string.Format(
                        "Aynı isimli bir proje {0} ortamında zaten var, ekleme yapılamaz",
                        proj.IsPreprodEnvironment ? "Preprod" : "Test"));
                    return;
                }
                foreach (var item in Path.GetInvalidPathChars())
                {
                    projname = projname.Replace(item.ToString(), "");
                }
                foreach (var item in Path.GetInvalidFileNameChars())
                {
                    projname = projname.Replace(item.ToString(), "");
                }
                proj.Caption = projname;
                TestProjects.Add(proj);

                MessageBox.Show(string.Format("{0} projesi {1} ortamına klonlandı", projname,
                    proj.IsTestEnvironment ? "test" : "preprod"));
                projname = "";
                saveJson();
            }
            updateTestProjectsByEnv();
            updateEditTestProjectsByEnv();
        }

        void deleteTestProject(object parameter)
        {
            if (parameter is TestProject)
            {
                var selectedProj = (TestProject)parameter;
                if (selectedProj != null)
                {
                    var capt = selectedProj.Caption;
                    TestProjects.RemoveAll(x => x.Caption == capt && x.IsPreprodEnvironment == AddIsPreprodEnvironment &&
                        x.IsTestEnvironment == AddIsTestEnvironment);
                    MessageBox.Show(string.Format("{0} projesi silindi", capt));
                }
            }
            updateTestProjectsByEnv();
            updateEditTestProjectsByEnv();
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
            {
                TestProjects = JsonConvert.DeserializeObject<ObservableCollection<TestProject>>(
                    File.ReadAllText(JsonPath));
                foreach (var proj in TestProjects)
                {
                    if (proj.IsTestEnvironment || proj.IsPreprodEnvironment)
                        continue;
                    proj.IsTestEnvironment = true;
                }
                updateTestProjectsByEnv();
            }
            else
                TestProjects = new ObservableCollection<TestProject>();
        }

        void updateTestProjectsByEnv()
        {
            TestProjectsByEnv.Clear();
            foreach (var item in TestProjects.Where(x => x.IsTestEnvironment == isTestEnvironment
                && x.IsPreprodEnvironment == isPreprodEnvironment))
            {
                TestProjectsByEnv.Add(item);
            }
        }
        void updateEditTestProjectsByEnv()
        {
            EditTestProjects.Clear();
            foreach (var item in TestProjects.Where(x => x.IsTestEnvironment == isTestEnvironment
                && x.IsPreprodEnvironment == isPreprodEnvironment))
            {
                EditTestProjects.Add(item);
            }
        }


    }
}
