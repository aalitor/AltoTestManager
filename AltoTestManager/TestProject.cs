using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltoTestManager
{
    class TestProject
    {
        public ObservableCollection<TestCase> TestCases { get; set; }
        public string Caption { get; set; }

        public bool IsTestEnvironment { get; set; }
        public bool IsPreprodEnvironment { get; set; }
        public TestProjectStatus Status { get; set; }

        public TestProject(string caption, TestProjectStatus status = TestProjectStatus.NotFinished)
        {
            this.Caption = caption;
            this.Status = status;
            TestCases = new ObservableCollection<TestCase>();
            IsTestEnvironment = true;
            IsPreprodEnvironment = false;
        }
    }
}
