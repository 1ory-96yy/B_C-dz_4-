using System;
using System.Linq;
using System.Threading;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var blocckChain = new BlockChaineService();
            var displayService = new BlockChainDisplayService();
            var program = new Program();

            for (int i =0; i <10; i++)
            {
                var transactions = program.GenerateRandomTransactions();
                try
                {
                    blocckChain.AddBlock(transactions);
                }
                catch (InvalidOperationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Warning: {ex.Message}");
                    Console.ResetColor();
                }
                Thread.Sleep(2000);
            }

            displayService.printChain(blocckChain.chain.ToList());
            Console.WriteLine("\n--- Smart Data Tampering Attack Simulation ---"+ new string('-', 100));
            var firstBlock = blocckChain.chain[1];
            firstBlock.transactions[0].amount = 1_000_000;
            var hashingService = new HashingService();
            firstBlock.hash = hashingService.ComputeHash(firstBlock);
            Console.WriteLine("Blockchain validity after tampering: " + blocckChain.IsChainValid());
            displayService.printChain(blocckChain.chain.ToList());
        }

        List<transaction> GenerateRandomTransactions()
        {
            var transaktionServise = new TransaktionServise();
            var tx1 = transaktionServise.CreateTransaction("Alice", "Bob",10.0);
            var tx2 = transaktionServise.CreateTransaction("Bob", "Charlie",20.0);
            var tx3 = transaktionServise.CreateTransaction("Charlie", "Dave",30.0);
            var tx4 = transaktionServise.CreateTransaction("Dave", "Eve",40.0);
            var tx5 = transaktionServise.CreateTransaction("Eve", "Frank",50.0);
            var tx6 = transaktionServise.CreateTransaction("Frank", "Grace",60.0);
            var tx7 = transaktionServise.CreateTransaction("Grace", "Heidi",70.0);
            var random = new Random();
            var numberOfTransactions = random.Next(1,6);
            var transactions = new List<transaction>();
            foreach (var tx in new[] { tx1, tx2, tx3, tx4, tx5, tx6, tx7 }.OrderBy(x => random.Next()).Take(numberOfTransactions))
            {
                transactions.Add(tx);
            }
            return transactions;
        }
    }
}
