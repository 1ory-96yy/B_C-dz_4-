using ConsoleApp1.Models;

namespace ConsoleApp1.Services
{
    public class BlockChainDisplayService
    {
        public void printChain(List<Models.Block> chain)
        {
            foreach (var block in chain)
            {
                Console.WriteLine($"Index: {block.index}");
                Console.WriteLine($"Timestamp: {block.timestamp}");
                Console.WriteLine($"Hash: {block.hash}");
                Console.WriteLine($"Nonce: {block.nonce}");
                Console.WriteLine($"Mining Difficulty: {block.Difficulty}");
                Console.WriteLine($"Previous Hash: {block.previousHash}");
                Console.WriteLine(new string('-', 20));
                printTransaction(block.transactions);

            }

        }

        public void printChainValidity(bool isValid)
        {
            if (isValid)
                Console.WriteLine("The blockchain is valid.");
            else
                Console.WriteLine("The blockchain is invalid.");
        }


        public void printTransaction(List<transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"From: {transaction.from}, To: {transaction.to}, Amount: {transaction.amount}");
                Console.WriteLine(new string('-', 20));

            }
        }

        public void PrintTransactionHistory(string address, List<Block> chain)
        {
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"Transaction History for: {address}");
            Console.WriteLine(new string('=', 60));

            bool transactionFound = false;

            for (int i = 1; i < chain.Count; i++)
            {
                var block = chain[i];
                var relevantTransactions = block.transactions
                    .Where(t => t.from == address || t.to == address)
                    .ToList();

                if (relevantTransactions.Count > 0)
                {
                    transactionFound = true;
                    Console.WriteLine($"\nBlock #{block.index} | Timestamp: {block.timestamp}");
                    Console.WriteLine(new string('-', 60));

                    foreach (var transaction in relevantTransactions)
                    {
                        if (transaction.from == address)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"SENT to {transaction.to}: {transaction.amount} coins");
                            Console.ResetColor();
                        }
                        else if (transaction.to == address)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"RECEIVED from {transaction.from}: {transaction.amount} coins");
                            Console.ResetColor();
                        }
                    }
                }
            }

            Console.WriteLine();
            if (!transactionFound)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Транзакцій для адреси '{address}' не знайдено.");
                Console.ResetColor();
            }
            Console.WriteLine(new string('=', 60));
        }

        public void FindAndDisplayLargestTransaction(List<Block> chain)
        {
            var largestTransaction = chain
                .Where(b => b.index > 0)
                .SelectMany(b => b.transactions.Select(t => new { Block = b, Transaction = t }))
                .OrderByDescending(x => x.Transaction.amount)
                .FirstOrDefault();

            if (largestTransaction == null)
            {
                Console.WriteLine("No transactions found in the blockchain.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Найбільша транзакція в мережі:");
            Console.ResetColor();
            Console.WriteLine($"   Блок #{largestTransaction.Block.index}");
            Console.WriteLine($"   {largestTransaction.Transaction.from} → {largestTransaction.Transaction.to}");
            Console.WriteLine($"   Сума: {largestTransaction.Transaction.amount} coins");
            Console.WriteLine(new string('=', 60));
        }

    }
}
