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
public class UserPasswordDictionary:IEnumerable<KeyValuePair<string,string>>{
    private readonly ConcurrentDictionary<string,string> ConcurrentDictionary=new();
    /// <summary>
    /// ユーザ名をキーとして、パスワードはハッシュ化して保存する。
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Password"></param>
    public void Add(string User,string Password) {
        var Provider = SHA256.Create();
        Provider.ComputeHash(Encoding.Unicode.GetBytes(Password));
        this.ConcurrentDictionary.TryAdd(
            User,
            BitConverter.ToString(Provider.Hash).Replace("-","")
        );
        //this.ConcurrentDictionary.AddOrUpdate(
        //    User,
        //    SHA256.Hash,
        //    (User1,Hash)=> Hash
        //);
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
    /// <param name="PasswordHash"></param>
    /// <returns></returns>
    public bool Authentication(string User,string PasswordHash) {
        if(!this.ConcurrentDictionary.TryGetValue(User,out var Hash)) 
            return false;
        return PasswordHash==Hash;
    }
    /// <summary>
    /// ユーザ名とパスワードハッシュを列挙する。
    /// </summary>
    /// <returns></returns>
    public IEnumerator<KeyValuePair<string,string>> GetEnumerator()=>this.ConcurrentDictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()=>this.GetEnumerator();

    /*
    var SHA256 = new SHA256CryptoServiceProvider();
SHA256.ComputeHash(Encoding.Unicode.GetBytes(Password));
//                var SHA256=this.SHA256;
SendStream=new CryptoStream(
    EncodeStream,
    Common.Aes.CreateEncryptor(SHA256.Hash, IV16),
CryptoStreamMode.Write
);
    */
}