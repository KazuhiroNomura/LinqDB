# LinqDB
Optimize Expression Tree,RPC for MessagePack,load schema from SQL Server
Expression Treeを最適化する
Delegate.Compileではラムダを跨ぐParameter,ILに直接書き込めない定数,キャプチャ変数はClosureというobject配列に書き込まれているが配列範囲チェック、アンボクシングのコストがかかるのでそれを回避するためにジェネリックはTuple<>に値を格納するようにした。
Linqで使っている主要なメソッド、例えばSelectはLinq実装では全要素に対してselectorデリゲートを実行して戻り値をIEnumerable<T>で返すようにしているが最適化ではループでデリゲートをインライン展開して実行し戻り値をIEnumerable<T>で返す。
ループの内側の不変変数を外出しした。
リモートプロシージャルコール。Serverを実行しているホストにClientを実行しているホストからリクエストを出しレスポンスを受け取る。
通信はMemoryPackを使う。試験的にMessagePack,Utf8Jsonも使っている。
SQL Serverのサンプルデータベースを読み込んでアセンブリに変換してデータのロード、ランダムデータの作成、ストアドプロシージャの呼び出し、クエリの呼び出しをする
AdventureWorksは2008R2,2012,2014,2016,2019全て対応している

