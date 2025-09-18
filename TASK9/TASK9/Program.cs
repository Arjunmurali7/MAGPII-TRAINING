using System;
using System.Threading;

class Program
{
    static BankAccount account = new BankAccount(500);

    static void Main()

    {
        Console.WriteLine($"\nInitial Balance: {account.Balance}\n");

        Thread t1 = new Thread(DoTransactions);
        Thread t2 = new Thread(DoTransactions);
        Thread t3 = new Thread(DoTransactions);

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine($"\nFinal Balance: {account.Balance}");
    }

    static void DoTransactions()
    {
        Random rand = new Random();

        for (int i = 0; i < 5; i++)
        {
            int amount = rand.Next(1, 150);
            if (rand.Next(2) == 0)
            {
                account.Deposit(amount);
            }
            else
            {
                account.Withdraw(amount);
            }

            Thread.Sleep(500);
        }
    }
    class BankAccount
    {
        private int balance;
        private readonly object locker = new object();

        public BankAccount(int initialBalance)       
        {
            balance = initialBalance;
        }

        public int Balance
        {
            get
            {
                lock (locker)
                {
                    return balance;
                }
            }
        }

        public void Deposit(int amount)
        {
            lock (locker)
            {
                balance += amount;
                Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] Deposited {amount}, Balance: {balance}");
            }
        }

        public void Withdraw(int amount)
        {
            lock (locker)
            {
                if (amount <= balance)
                {
                    balance -= amount;
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] Withdrew {amount}, Balance: {balance}");
                }
                else
                {
                    Console.WriteLine($"[Thread {Thread.CurrentThread.ManagedThreadId}] Withdrawal of {amount} FAILED (Balance: {balance})");
                }
            }
        }

    }


}