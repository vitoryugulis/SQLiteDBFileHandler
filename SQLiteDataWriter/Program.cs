using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSource = @"Data Source=C:\\DEV\\SQLite\\transactionStorage.db;Version=3;New=False;Compress=True;";
            Console.WriteLine("Pressione qualquer tecla para inserir 10000 linhas na tabela transactions");
            Console.ReadKey();
            var dao = new TransactionsDAO(dataSource);

            var result = dao.WriteTenThousandRows();

            if (result)
            {
                Console.WriteLine("Sucesso");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Falha");
            Console.ReadKey();
            return;
        }
    }
}
