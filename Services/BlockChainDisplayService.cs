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

    }
}
