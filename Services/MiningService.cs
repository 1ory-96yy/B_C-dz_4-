using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

namespace ConsoleApp1.Services
{
    public class MiningService
    {
        private readonly HashingService _hashingService;

        public MiningService()
        {
            _hashingService = new HashingService();
        }

        public void MineBlock(Block block, int difficulty)
        {
            string transactionsData = string.Concat(block.transactions.Select(t => t.ToRowString()));

            using var sha256 = SHA256.Create();
            var hashBuffer = new byte[32];

            int fullZeroBytes = difficulty / 2;
            bool hasHalfNibble = (difficulty % 2) != 0;

            while (true)
            {
                if (block.nonce % 100000 == 0)
                {
                    Console.Write(".");
                }

                var input = $"{block.index}{block.timestamp}{transactionsData}{block.previousHash}{block.nonce}";
                var inputBytes = Encoding.UTF8.GetBytes(input);

                sha256.TryComputeHash(inputBytes, hashBuffer, out _);

                bool isValid = ValidateDifficulty(hashBuffer, fullZeroBytes, hasHalfNibble);

                if (isValid)
                {
                    block.hash = Convert.ToHexString(hashBuffer).ToLower();
                    break;
                }

                block.nonce++;
            }
        }
        private bool ValidateDifficulty(byte[] hashBytes, int fullZeroBytes, bool hasHalfNibble)
        {
            for (int i = 0; i < fullZeroBytes; i++)
            {
                if (hashBytes[i] != 0)
                {
                    return false;
                }
            }

            if (hasHalfNibble)
            {
                byte nextByte = hashBytes[fullZeroBytes];
                if ((nextByte >> 4) != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
