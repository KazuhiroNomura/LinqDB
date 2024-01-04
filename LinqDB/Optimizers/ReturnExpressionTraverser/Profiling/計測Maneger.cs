using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;

using LinqDB.Helpers;
using Timer=System.Timers.Timer;
namespace LinqDB.Optimizers.ReturnExpressionTraverser.Profiling;
//public sealed class 計測Maneger:IDisposable{
//    internal static class Reflection{
//        public static readonly MethodInfo サンプリング開始= typeof(計測Maneger).GetMethod(nameof(計測Maneger.サンプリング開始),BindingFlags.NonPublic|BindingFlags.Instance)!;
//        public static readonly MethodInfo サンプリング終了= typeof(計測Maneger).GetMethod(nameof(計測Maneger.サンプリング終了),BindingFlags.NonPublic|BindingFlags.Instance)!;
//    }
//    private readonly ManualResetEvent ManualResetEvent=new(false);
//    private bool disposed;
//    public int continue数=0,カウント数=0;
//    public 計測Maneger(){
//        this.disposed = false;
//        ThreadPool.QueueUserWorkItem(o=>{
//            var ManualResetEvent=this.ManualResetEvent;
//            ManualResetEvent.WaitOne();
//            try{
//                while(true){
//                    var 実行中=this.実行中;
//                    if(実行中 is null){
//                        if(ManualResetEvent.WaitOne(0)){
//                            this.continue数++;
//                            continue;
//                        }
//                        this.カウント数++;
//                        break;
//                    }
//                    実行中.呼出回数++;
//                }
//            } catch(Exception ex){
//                Trace.WriteLine(ex.Message);
//            }
//        });
//    }
//    internal 計測? 実行中;

//    internal void サンプリング開始(){
//        this.ManualResetEvent.Set();
//    }
//    internal void サンプリング終了(){
//        this.実行中=null;
//        this.ManualResetEvent.Set();
//    }
//    public void Dispose(){
//        GC.SuppressFinalize(this);
//        this.Dispose(true);
//    }
//    ~計測Maneger(){
//        this.Dispose(false);
//    }
//    private void Dispose(bool disposing){
//        if (this.disposed){
//            return;
//        }
//        this.disposed = true;
//        if (disposing){
//            // マネージリソースの解放処理
//            this.ManualResetEvent.Dispose();
//        }
//        // アンマネージリソースの解放処理
//    }
//}
public sealed class 計測Maneger:IDisposable{
    internal static class Reflection{
        public static readonly MethodInfo サンプリング開始= typeof(計測Maneger).GetMethod(nameof(計測Maneger.サンプリング開始),BindingFlags.NonPublic|BindingFlags.Instance)!;
        public static readonly MethodInfo サンプリング終了= typeof(計測Maneger).GetMethod(nameof(計測Maneger.サンプリング終了),BindingFlags.NonPublic|BindingFlags.Instance)!;
    }
    private readonly ManualResetEvent ManualResetEvent=new(false);
    private bool disposed;
    public static int スレッド数=0;
    public int continue数=0,カウント数=0;
    private Timer Timer=new();
    public 計測Maneger(){
        this.disposed = false;
        var Timer=this.Timer;
        Timer.Interval=1;
        Timer.Elapsed+=(sender, e)=>{
            var 実行中=this.実行中;
            if(実行中 is null){
                this.カウント数++;
            }else
                実行中.サンプリング数++;
        };
        //ThreadPool.QueueUserWorkItem(o=>{
        //    var ManualResetEvent=this.ManualResetEvent;
        //    ManualResetEvent.WaitOne();
        //    Interlocked.Increment(ref スレッド数);
        //    try{
        //        while(true){
        //            var 実行中=this.実行中;
        //            if(実行中 is null){
        //                if(ManualResetEvent.WaitOne(10)){
        //                    this.continue数++;
        //                    continue;
        //                }
        //                this.カウント数++;
        //                break;
        //            }
        //            実行中.サンプリング数++;
        //        }
        //    } catch(Exception ex){
        //        Trace.WriteLine(ex.Message);
        //    }
        //});
    }
    internal 計測? 実行中;

    internal void サンプリング開始(){
        this.Timer.Start();
        this.ManualResetEvent.Set();
    }
    internal void サンプリング終了(){
        this.Timer.Stop();
        this.実行中=null;
        this.ManualResetEvent.Set();
    }
    public void Dispose(){
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }
    ~計測Maneger(){
        this.Dispose(false);
    }
    private void Dispose(bool disposing){
        if (this.disposed){
            return;
        }
        this.disposed = true;
        if (disposing){
            // マネージリソースの解放処理
            this.ManualResetEvent.Dispose();
        }
        // アンマネージリソースの解放処理
    }

    private readonly StringBuilder sb=new();
    private readonly List<string>ListString=new();
    internal readonly Stopwatch Stopwatch=new();
    public string Analize(計測?計測){
        if(計測 is null) return string.Empty;
        //if(this.Count==0) return string.Empty;
        var ListString=this.ListString;
        ListString.Clear();
        var sb=this.sb;
        計測.Analize(ListString,sb,this.Stopwatch.ElapsedMilliseconds);
        sb.Clear();
        foreach(var a in ListString) sb.AppendLine(a);
        return sb.ToString();
    }
}
//20311228 1166
