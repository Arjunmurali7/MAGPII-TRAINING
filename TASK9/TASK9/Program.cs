using System;
using System.Threading;

class Program
{
    static BankAccount account = new BankAccount(500);// Shared bank account with initial balance

    static void Main()

    {
        Console.WriteLine($"\nInitial Balance: {account.Balance}\n");// Display initial balance

        Thread t1 = new Thread(DoTransactions);// Create multiple threads to perform transactions
        Thread t2 = new Thread(DoTransactions);
        Thread t3 = new Thread(DoTransactions);

        t1.Start();// Start the threads
        t2.Start();
        t3.Start();

        t1.Join();// Wait for all threads to complete
        t2.Join();
        t3.Join();

        Console.WriteLine($"\nFinal Balance: {account.Balance}");
    }

    static void DoTransactions() // Method for performing random deposits and withdrawals
    {
        Random rand = new Random(); // Random number generator

        for (int i = 0; i < 5; i++)// Each thread performs 5 transactions
        {
            int amount = rand.Next(1, 150); // Random amount between 1 and 150
            if (rand.Next(2) == 0)// Randomly decide to deposit or withdraw
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
        private int balance;// Account balance
        private readonly object locker = new object();// Object for locking 

        public BankAccount(int initialBalance)// Constructor to initialize balance       
        {
            balance = initialBalance; // Set initial balance
        }

        public int Balance // Property to get current balance
        {
            get
            {
                lock (locker)
                {
                    return balance; // Return balance in a thread-safe manner
                }
            }
        }

        public void Deposit(int amount)
        {
            lock (locker)// Locking to ensure thread safety
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