namespace LinqDB.Databases.Dom;
public interface IName{
    string Name { get; set; }
    
    public string EscapedName{
        get
        {
            var s = this.Name;
            //switch(s) {
            //    case "event":
            //    case "operator": s='@'+s; break;
            //}
            return s.
                Replace(' ','_').
                Replace('-','_');
        }
    }
}
