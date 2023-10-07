using System;
using System.Collections.ObjectModel;

namespace GUI.VM;

[Serializable]
public class TestRecord {
    //private static readonly DependencyProperty _Type = DependencyProperty.Register(nameof(Property),typeof(String),typeof(TestRecord));
    //public String Property {
    //    get => (String)this.GetValue(_Type);
    //    set => this.SetValue(_Type,value);
    //}
    public string Property { get; set; }
    //public String Field{ get; set; } = "Field";
    public TestRecord(int a)=>this.Property="";
}
public class TestRecords {
    public ObservableCollection<object> Records { get; } = new();
    public TestRecords() {
        //this.Records.Add(new {a=1,b=2});
        this.Records.Add(new TestRecord(3) { Property="Property1" });
        //this.Records.Add(new TestRecord { Property="Property2" });
        //this.Records.Add(new TestRecord { Property="Property3" });
    }
}
/*
public class TestRecords:ObservableCollection<TestRecord> {
    public TestRecords() {
        this.Add(new TestRecord { Property="Property1" });
        this.Add(new TestRecord { Property="Property2" });
    }
}
*/
/*
public class TestRecords {
    public ObservableCollection<TestRecord> Records { get; } = new ObservableCollection<TestRecord>();
    public TestRecords() {
        this.Records.Add(new TestRecord { Property="Property1" });
        this.Records.Add(new TestRecord { Property="Property2" });
    }
}
*/