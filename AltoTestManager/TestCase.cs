using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoTestManager
{
    class TestCase : INotifyPropertyChanged
    {
        private string description;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public ObservableCollection<string> ImagePaths { get; set; }

        private TestCaseStatus caseStatus;

        public TestCaseStatus CaseStatus
        {
            get { return caseStatus; }
            set
            {
                if (value != caseStatus)
                {
                    caseStatus = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CaseStatus"));
                }
            }
        }

        private string testData;

        public string TestData
        {
            get { return testData; }
            set
            {
                if (value != testData)
                {
                    testData = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TestData"));
                }
            }
        }

        public TestCase(string description, string testData, TestCaseStatus status = TestCaseStatus.Untested)
        {
            ImagePaths = new ObservableCollection<string>();
            this.Description = description;
            this.CaseStatus = status;
            this.TestData = testData;
        }


        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
