using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace GUI.VM;

public class DllWatcher:DependencyObject {
    //private static readonly DependencyProperty _Files = DependencyProperty.Register(nameof(Name),typeof(String),typeof(NameDependencyObject));
    public ObservableCollection<string> Files { get; } = new();
    public DllWatcher(string パス) {
        ////監視するディレクトリを指定
        //var myAssembly = Assembly.GetEntryAssembly();
        //var パス = Path.GetDirectoryName(myAssembly.Location);
        var watcher =new FileSystemWatcher();
        watcher.Path=パス;
        var FileNames=Directory.GetFiles(パス,"*.dll",SearchOption.TopDirectoryOnly);
        var Files=this.Files;
        foreach(var FileName in FileNames) {
            Files.Add(Path.GetFileName(FileName));
        }
        //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
        watcher.NotifyFilter=
            (NotifyFilters.LastAccess
             |NotifyFilters.LastWrite
             |NotifyFilters.FileName
             |NotifyFilters.DirectoryName);
        //すべてのファイルを監視
        watcher.Filter="";
        //UIのスレッドにマーシャリングする
        //コンソールアプリケーションでの使用では必要ない
        //watcher.SynchronizingObject=Window;

        //イベントハンドラの追加
        watcher.Changed+=this.Changed;
        watcher.Created+=this.Created;
        watcher.Deleted+=this.Deleted;
        watcher.Renamed+=this.Renamed;

        //監視を開始する
        watcher.EnableRaisingEvents=true;
    }
    private readonly Regex Regex = new(@"\.dll$",RegexOptions.IgnoreCase);
    private void Changed(object sender, FileSystemEventArgs e) {
    }
    private void Created(object sender,FileSystemEventArgs e) {
        this.Dispatcher.Invoke(() =>{
            // 文字列が正規表現にマッチするかどうか調べる
            if(this.Regex.IsMatch(e.Name)) {
                this.Files.Add(e.Name);
            }
        });
    }
    private void Deleted(object sender,FileSystemEventArgs e) {
        this.Dispatcher.Invoke(() => {
            if(this.Regex.IsMatch(e.Name)) {
                this.Files.Remove(e.Name);
            }
        });
    }
    private void Renamed(object sender, RenamedEventArgs e) {
        var Index=this.Files.IndexOf(e.OldName);
        if(Index>=0) {
            this.Files[Index]=e.Name;
        }
    }
}