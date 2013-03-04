// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IDatabase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PetaPoco
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IDatabase : IDisposable
    {
        bool KeepConnectionAlive { get; set; }

        IDbConnection Connection { get; }

        bool EnableAutoSelect { get; set; }

        bool EnableNamedParams { get; set; }

        bool ForceDateTimesToUtc { get; set; }

        int CommandTimeout { get; set; }

        int OneTimeCommandTimeout { get; set; }

        string LastSQL { get; }

        object[] LastArgs { get; }

        string LastCommand { get; }

        void OpenSharedConnection();

        void CloseSharedConnection();

        Transaction GetTransaction();

        void OnBeginTransaction();

        void OnEndTransaction();

        void BeginTransaction();

        void AbortTransaction();

        void CompleteTransaction();

        IDbCommand CreateCommand(IDbConnection connection, string sql, params object[] args);

        void OnException(Exception x);

        IDbConnection OnConnectionOpened(IDbConnection conn);

        void OnConnectionClosing(IDbConnection conn);

        void OnExecutingCommand(IDbCommand cmd);

        void OnExecutedCommand(IDbCommand cmd);

        int Execute(string sql, params object[] args);

        int Execute(Sql sql);

        T ExecuteScalar<T>(string sql, params object[] args);

        T ExecuteScalar<T>(Sql sql);

        List<T> Fetch<T>(string sql, params object[] args);

        List<T> Fetch<T>(Sql sql);

        void BuildPageQueries<T>(long skip, long take, string sql, ref object[] args, out string sqlCount, out string sqlPage);

        Page<T> Page<T>(long page, long itemsPerPage, string sql, params object[] args);

        Page<T> Page<T>(long page, long itemsPerPage, Sql sql);

        List<T> Fetch<T>(long page, long itemsPerPage, string sql, params object[] args);

        List<T> Fetch<T>(long page, long itemsPerPage, Sql sql);

        List<T> SkipTake<T>(long skip, long take, string sql, params object[] args);

        List<T> SkipTake<T>(long skip, long take, Sql sql);

        IEnumerable<T> Query<T>(string sql, params object[] args);

        List<TRet> Fetch<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, params object[] args);

        List<TRet> Fetch<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, params object[] args);

        List<TRet> Fetch<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, params object[] args);

        IEnumerable<TRet> Query<T1, T2, TRet>(Func<T1, T2, TRet> cb, string sql, params object[] args);

        IEnumerable<TRet> Query<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, string sql, params object[] args);

        IEnumerable<TRet> Query<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, string sql, params object[] args);

        List<TRet> Fetch<T1, T2, TRet>(Func<T1, T2, TRet> cb, Sql sql);

        List<TRet> Fetch<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, Sql sql);

        List<TRet> Fetch<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, Sql sql);

        IEnumerable<TRet> Query<T1, T2, TRet>(Func<T1, T2, TRet> cb, Sql sql);

        IEnumerable<TRet> Query<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> cb, Sql sql);

        IEnumerable<TRet> Query<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> cb, Sql sql);

        List<T1> Fetch<T1, T2>(string sql, params object[] args);

        List<T1> Fetch<T1, T2, T3>(string sql, params object[] args);

        List<T1> Fetch<T1, T2, T3, T4>(string sql, params object[] args);

        IEnumerable<T1> Query<T1, T2>(string sql, params object[] args);

        IEnumerable<T1> Query<T1, T2, T3>(string sql, params object[] args);

        IEnumerable<T1> Query<T1, T2, T3, T4>(string sql, params object[] args);

        List<T1> Fetch<T1, T2>(Sql sql);

        List<T1> Fetch<T1, T2, T3>(Sql sql);

        List<T1> Fetch<T1, T2, T3, T4>(Sql sql);

        IEnumerable<T1> Query<T1, T2>(Sql sql);

        IEnumerable<T1> Query<T1, T2, T3>(Sql sql);

        IEnumerable<T1> Query<T1, T2, T3, T4>(Sql sql);

        IEnumerable<TRet> Query<TRet>(Type[] types, object cb, string sql, params object[] args);

        IEnumerable<T> Query<T>(Sql sql);

        bool Exists<T>(object primaryKey);

        T Single<T>(object primaryKey);

        T SingleOrDefault<T>(object primaryKey);

        T Single<T>(string sql, params object[] args);

        T SingleOrDefault<T>(string sql, params object[] args);

        T First<T>(string sql, params object[] args);

        T FirstOrDefault<T>(string sql, params object[] args);

        T Single<T>(Sql sql);

        T SingleOrDefault<T>(Sql sql);

        T First<T>(Sql sql);

        T FirstOrDefault<T>(Sql sql);

        string EscapeTableName(string str);

        string EscapeSqlIdentifier(string str);

        object Insert(string tableName, string primaryKeyName, object poco);

        object Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco);

        object Insert(object poco);

        int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue);

        int Update(string tableName, string primaryKeyName, object poco, object primaryKeyValue, IEnumerable<string> columns);

        int Update(string tableName, string primaryKeyName, object poco);

        int Update(string tableName, string primaryKeyName, object poco, IEnumerable<string> columns);

        int Update(object poco, IEnumerable<string> columns);

        int Update(object poco);

        int Update(object poco, object primaryKeyValue);

        int Update(object poco, object primaryKeyValue, IEnumerable<string> columns);

        int Update<T>(string sql, params object[] args);

        int Update<T>(Sql sql);

        int Delete(string tableName, string primaryKeyName, object poco);

        int Delete(string tableName, string primaryKeyName, object poco, object primaryKeyValue);

        int Delete(object poco);

        int Delete<T>(object pocoOrPrimaryKey);

        int Delete<T>(string sql, params object[] args);

        int Delete<T>(Sql sql);

        bool IsNew(string primaryKeyName, object poco);

        bool IsNew(object poco);

        void Save(string tableName, string primaryKeyName, object poco);

        void Save(object poco);

        string FormatCommand(IDbCommand cmd);

        string FormatCommand(string sql, object[] args);
    }
}