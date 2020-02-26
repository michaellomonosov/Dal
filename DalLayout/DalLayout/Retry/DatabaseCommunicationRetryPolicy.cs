﻿using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DalLayout.Retry
{
    public class DatabaseCommunicationRetryPolicy : IRetryPolicy
    {
        private const int RetryCount = 3;
        private const int WaitBetweenRetriesInMilliseconds = 1000;
        private readonly int[] _sqlExceptions = new[] { 53, -2 };
        private readonly AsyncRetryPolicy _retryPolicyAsync;
        private readonly Policy _retryPolicy;
        //add PolicyWrap
        public DatabaseCommunicationRetryPolicy()
        {
            _retryPolicyAsync = Policy
                .Handle<SqlException>(exception => _sqlExceptions.Contains(exception.Number))
                .WaitAndRetryAsync(
                    retryCount: RetryCount,
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(WaitBetweenRetriesInMilliseconds)
                );

            _retryPolicy = Policy
                .Handle<SqlException>(exception => _sqlExceptions.Contains(exception.Number))
                .WaitAndRetry(
                    retryCount: RetryCount,
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(WaitBetweenRetriesInMilliseconds)
                );
        }

        public void Execute(Action operation)
        {
            _retryPolicy.Execute(operation.Invoke);
        }

        public TResult Execute<TResult>(Func<TResult> operation)
        {
            return _retryPolicy.Execute(operation.Invoke);
        }

        public async Task Execute(Func<Task> operation, CancellationToken cancellationToken)
        {
            await _retryPolicyAsync.ExecuteAsync(operation.Invoke);
        }

        public async Task<TResult> Execute<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
        {
            return await _retryPolicyAsync.ExecuteAsync(operation.Invoke);
        }
    }
}
