using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LinqDB.Remote.Servers;

/// <summary>
/// パスワードをハッシュ化してユーザの認証を行う連想配列
/// </summary>
public class UserPasswordDictionary:IEnumerable<KeyValuePair<string,ReadOnlyMemory<byte>>>{
    private readonly SHA256 Provider=SHA256.Create();
    //private sealed class BytesEqualityComparer:EqualityComparer<ReadOnlyMemory<byte>>{
    //    public override bool Equals(ReadOnlyMemory<byte> x,ReadOnlyMemory<byte> y)=>x.Span.SequenceEqual(y.Span);
    //    public override int GetHashCode(ReadOnlyMemory<byte> obj){
    //        var HashCode=new HashCode();
    //        HashCode.AddBytes(obj.Span);
    //        return HashCode.ToHashCode();
    //    }
    //}
    //private readonly ConcurrentDictionary<ReadOnlyMemory<byte>,ReadOnlyMemory<byte>> ConcurrentDictionary=new(new BytesEqualityComparer());
    private readonly ConcurrentDictionary<string,ReadOnlyMemory<byte>> ConcurrentDictionary=new();
    /// <summary>
    /// ユーザ名をキーとして、パスワードはハッシュ化して保存する。
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Password"></param>
    public void Add(string User,string Password)=>this.ConcurrentDictionary.TryAdd(User,this.Hash(Password));
    private byte[]Hash(string input){
        var Provider = this.Provider;
        Provider.Initialize();
        return Provider.ComputeHash(Encoding.Unicode.GetBytes(input));
    }
    /// <summary>
    /// ユーザーを削除する。
    /// </summary>
    /// <param name="User"></param>
    public void Remove(string User) => this.ConcurrentDictionary.TryRemove(
        User,
        out var _
    );
    /// <summary>
    /// ユーザーとパスワードハッシュが一致するか。
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    public bool Authentication(string User,ReadOnlyMemory<byte> Password) {
        if(!this.ConcurrentDictionary.TryGetValue(User,out var Hash)) 
            return false;
        return Password.Span.SequenceEqual(Hash.Span);
    }
    /// <summary>
    /// ユーザ名とパスワードハッシュを列挙する。
    /// </summary>
    /// <returns></returns>
    public IEnumerator<KeyValuePair<string,ReadOnlyMemory<byte>>> GetEnumerator()=>this.ConcurrentDictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()=>this.GetEnumerator();
}