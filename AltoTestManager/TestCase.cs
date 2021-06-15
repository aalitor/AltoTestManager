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
        public string Description { get; set; }
        public ObservableCollection<string> ImagePaths { get; set; }

        private TestCaseStatus caseStatus;

        public TestCaseStatus CaseStatus
        {
            get { return caseStatus; }
            set
            {
                if(value != caseStatus)
                {
                    caseStatus = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("CaseStatus"));
                }
            }
        }
        

        public TestCase(string description, TestCaseStatus status = TestCaseStatus.Untested)
        {
            ImagePaths = new ObservableCollection<string>();
            this.Description = description;
            this.CaseStatus = status;
        }


        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
